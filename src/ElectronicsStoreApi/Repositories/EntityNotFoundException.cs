using System;

namespace ElectronicsStoreApi.Repositories
{
    /// <summary>
    /// Thrown when an entity cannot be found with a given id from the data layer
    /// </summary>
    /// <typeparam name="T">Type of the entity</typeparam>
    internal class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(long id)
            : base($"Entity of type {typeof(T).Name} with id {id} was not found.")
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}