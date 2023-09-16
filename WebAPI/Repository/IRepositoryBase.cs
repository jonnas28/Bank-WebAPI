using System.Linq.Expressions;

namespace WebAPI.Repository
{
    /// <summary>
    /// Represents a base repository interface for CRUD operations on entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// Retrieves all entities of type T.
        /// </summary>
        /// <returns>An IQueryable of T containing all entities.</returns>
        IQueryable<T> FindAll();

        /// <summary>
        /// Retrieves entities of type T based on a specified condition.
        /// </summary>
        /// <param name="expression">The expression representing the condition.</param>
        /// <returns>An IQueryable of T containing entities that match the condition.</returns>
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Creates a new entity of type T.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        void Create(T entity);

        /// <summary>
        /// Updates an existing entity of type T.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity of type T.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T enitity);
    }
}
