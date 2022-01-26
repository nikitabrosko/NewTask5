using System;

namespace Application.Common.Exceptions
{
    public class ForeignKeyDeletionException : Exception
    {
        public ForeignKeyDeletionException() 
            : base()
        {
            
        }

        public ForeignKeyDeletionException(string message)
            : base(message)
        {

        }

        public ForeignKeyDeletionException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public ForeignKeyDeletionException(string entity, object foreignKey)
            : base($"Entity \"{entity}\" cannot be deleted, because entity ({foreignKey}) depends on it!")
        {

        }
    }
}