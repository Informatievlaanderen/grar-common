namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Api.Extract;
    using Shaperon;

    public static class ExtractBuilder
    {
        public static ExtractFile CreateDbfFile<TDbaseRecord>(
            string fileName,
            DbaseSchema schema,
            IEnumerable<byte[]> records,
            Func<int> getRecordCount) where TDbaseRecord : DbaseRecord, new()
            => CreateDbfFile<byte[], TDbaseRecord>(
                fileName,
                schema,
                records,
                getRecordCount,
                o => o);

        public static ExtractFile CreateDbfFile<T, TDbaseRecord>(
            string fileName,
            DbaseSchema schema,
            IEnumerable<T> records,
            Func<int> getRecordCount,
            Func<T, byte[]> buildRecordFunc) where TDbaseRecord : DbaseRecord, new()
            => new ExtractFile(
                new DbfFileName(fileName),
                (stream, token) =>
                {
                    var dbfFile = DbfFileWriter<TDbaseRecord>.CreateDbfFileWriter<TDbaseRecord>(
                        schema,
                        new DbaseRecordCount(getRecordCount()),
                        stream);

                    foreach (var record in records)
                    {
                        if (token.IsCancellationRequested)
                            return;

                        dbfFile.WriteBytesAs<TDbaseRecord>(buildRecordFunc(record));
                    }

                    dbfFile.WriteEndOfFile();
                });

        public static ExtractFile CreateMetadataDbfFile(
            string fileName,
            IDictionary<string,string> records)
            => new ExtractFile(
                new MetadataDbfFileName(fileName),
                (stream, token) =>
                {
                    foreach (var record in records)
                    {
                        if (record.Key.Length > MetadataDbaseSchema.MetadataMaxLength)
                            throw new DbaseRecordException($"Metadata key has {record.Key.Length} characters, more than allowed {MetadataDbaseSchema.MetadataMaxLength} characters");
                        if (record.Value.Length > MetadataDbaseSchema.ValueMaxLength)
                            throw new DbaseRecordException($"Metadata value has {record.Value.Length} characters, more than allowed {MetadataDbaseSchema.ValueMaxLength} characters");
                    }

                    var dbfFile = DbfFileWriter<MetadataDbaseRecord>.CreateDbfFileWriter<MetadataDbaseRecord>(
                        new MetadataDbaseSchema(),
                        new DbaseRecordCount(records.Count),
                        stream);

                    foreach (var record in records)
                    {
                        if (token.IsCancellationRequested)
                            return;

                        var item = new MetadataDbaseRecord
                        {
                            metadata = { Value = record.Key },
                            value = { Value = record.Value }
                        };

                        dbfFile.WriteBytesAs<MetadataDbaseRecord>(item.ToBytes(DbfFileWriter<MetadataDbaseRecord>.Encoding));
                    }

                    dbfFile.WriteEndOfFile();
                });


        public static ExtractFile CreateShapeFile<TShape>(
            string fileName,
            ShapeType shapeType,
            IEnumerable<byte[]> shapes,
            Func<BinaryReader, ShapeContent> readShape,
            IEnumerable<int> shapeLengths,
            BoundingBox3D boundingBox) where TShape : ShapeContent
            => new ExtractFile(
                new ShpFileName(fileName),
                (stream, token) =>
                {
                    var totalShapeRecordsLength = shapeLengths.Aggregate(
                        new WordLength(0),
                        (current, shapeLength) => current.Plus(ShapeRecord.HeaderLength.Plus(new WordLength(shapeLength))));

                    var shpFile = new ShpFileWriter(
                        new ShapeFileHeader(
                            ShapeFileHeader.Length.Plus(totalShapeRecordsLength),
                            shapeType,
                            boundingBox),
                        stream);

                    var number = RecordNumber.Initial;
                    foreach (var shape in shapes)
                    {
                        if (token.IsCancellationRequested)
                            break;

                        using var shapeStream = new MemoryStream(shape);
                        using var reader = new BinaryReader(shapeStream);

                        var content = readShape(reader);
                        if (content is TShape || content is NullShapeContent)
                        {
                            var shapeRecord = content.RecordAs(number);
                            shpFile.Write(shapeRecord);

                            number = number.Next();
                        }
                    }
                });

        public static ExtractFile CreateShapeIndexFile(
            string fileName,
            ShapeType shapeType,
            IEnumerable<int> shapesLengths,
            Func<int> getRecordCount,
            BoundingBox3D boundingBox)
            => new ExtractFile(
                new ShxFileName(fileName),
                (stream, token) =>
                {
                    var shxFileWriter = new ShxFileWriter(
                        new ShapeFileHeader(
                            ShapeFileHeader.Length.Plus(new WordLength(getRecordCount() * 4)),
                            shapeType,
                            boundingBox),
                        stream);

                    var offset = ShapeRecord.InitialOffset;
                    foreach (var shapeLength in shapesLengths)
                    {
                        if (token.IsCancellationRequested)
                            break;

                        var indexRecord = new ShapeIndexRecord(offset, new WordLength(shapeLength));
                        shxFileWriter.Write(indexRecord);

                        offset = offset.Plus(indexRecord.ContentLength.Plus(ShapeRecord.HeaderLength));
                    }
                });

        public static ExtractFile CreateProjectedCoordinateSystemFile(string fileName, ProjectedCoordinateSystem projectedCoordinateSystem)
            => new ExtractFile(
                new PrjFileName(fileName),
                (stream, token) =>
                {
                    if (token.IsCancellationRequested)
                        return;

                    new PrjFileWriter(stream)
                        .Write(projectedCoordinateSystem);
                });
    }
}
