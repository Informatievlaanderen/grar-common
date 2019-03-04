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
        private static DbaseCodePage CodePage => DbaseCodePage.Western_European_ANSI;
        private static Encoding Encoding => CodePage.ToEncoding();

        public DbfFileWriter(DbaseFileHeader header, Stream writeStream)
            : base(Encoding, writeStream) => header.Write(Writer);

        public void Write(TDbaseRecord record) => record.Write(Writer);

        public void WriteBytesAs<T>(byte[] recordBytes)
            where T : TDbaseRecord, new()
        {
            var record = new T();
            record.FromBytes(recordBytes, Encoding);
            Write(record);
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
