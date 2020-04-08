using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssReader.Services.Models.Interfaces
{
    /// <summary>
    /// Represent repository interface.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IRepository<T> : IDisposable
        where T : BaseModel
    {
        /// <summary>
        /// Get list entities.
        /// </summary>
        /// <returns>List TEntity.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get list entities.
        /// </summary>
        /// <returns>List TEntity.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Add entitiy.
        /// </summary>
        /// <param name="entity"></param>
        void AddItem(BaseModel entity);
    }
}
