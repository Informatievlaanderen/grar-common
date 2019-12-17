namespace Be.Vlaanderen.Basisregisters.GrAr.Extracts
{
    using System;
    using System.IO;
    using System.Text;
    using Api.Extract;
    using Shaperon;


    public class DbfFileWriter<TDbaseRecord> : ExtractFileWriter
        where TDbaseRecord : DbaseRecord
    {
        public static DbaseCodePage CodePage => DbaseCodePage.Western_European_ANSI;
        public static Encoding Encoding => CodePage.ToEncoding();

        public DbfFileWriter(DbaseFileHeader header, Stream writeStream)
            : base(Encoding, writeStream) => header.Write(Writer);

        public void Write(TDbaseRecord record) => record.Write(Writer);

        public void WriteBytesAs<T>(byte[] recordBytes)
            where T : TDbaseRecord, new()
        {
            var record = new T();
            ReadFromBytes(record, recordBytes, Encoding);
            Write(record);
        }

        private static void ReadFromBytes(DbaseRecord record, byte[] bytes, Encoding encoding)
        {
            using (var input = new MemoryStream(bytes))
            using (var reader = new BinaryReader(input, encoding))
                record.Read(reader);
        }

        public void WriteEndOfFile() => Writer.Write(DbaseRecord.EndOfFile);

        internal static DbfFileWriter<T> CreateDbfFileWriter<T>(
            DbaseSchema schema,
            DbaseRecordCount recordCount,
            Stream writeStream) where T : DbaseRecord
            => new DbfFileWriter<T>(
                new DbaseFileHeader(
                    DateTime.Now,
                    CodePage,
                    recordCount,
                    schema
                ),
                writeStream);
    }
}
