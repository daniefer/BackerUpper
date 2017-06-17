using System;
using System.Runtime.Serialization;

namespace GlacierBackupService
{
    [Serializable]
    internal class InvalidChecksumException : Exception
    {
        public InvalidChecksumException() : base("The file upload failed due to a checksum mismatch. Aborting.")
        {
        }

        public InvalidChecksumException(Exception innerException) : base("The file upload failed due to a checksum mismatch. Aborting.", innerException)
        {
        }

        protected InvalidChecksumException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}