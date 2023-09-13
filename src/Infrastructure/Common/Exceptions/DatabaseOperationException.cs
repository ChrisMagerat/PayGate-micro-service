using System.Runtime.Serialization;

namespace PayGate.Infrastructure.Common.Exceptions;

[Serializable]
public class DatabaseOperationException: Exception
{ 
        public DatabaseOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected DatabaseOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
                if (info == null)
                {
                        throw new ArgumentNullException(nameof(info));
                }

                base.GetObjectData(info, context);
        }
}