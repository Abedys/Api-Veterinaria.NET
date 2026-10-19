using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria.Core.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class //una clase que recibe una clase
    {
        #region IRepository<T> Members

        Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties); //Expresion es para recibir parametros

        //Task<List<TEntity>> GetAllSelectAsync(params Expression<Func<TEntity, object>>[] includeProperties);

        /// Función que retorna una entidad, a partir de una consulta.
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);

        //Task<List<TEntity>> FindAllSelectAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);

        /// Retorna la primera entidad encontrada bajo una condición especificada o null sino encontrara registros
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity?> FirstOrDefaultAsNoTrackingAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        /// Registra una entidad
        void Insert(TEntity entity);

        /// Registra varias entidades
        void Insert(IEnumerable<TEntity> entities);

        /// Actualiza una entidad
        void Update(TEntity entity);

        /// Actualiza varias entidades
        void Update(IEnumerable<TEntity> entities);

        /// Elimina una entidad
        void Delete(TEntity entity);

        /// Elimina por Id
        void Delete(object id);

        Task DeleteAsync(object id);

        /// Elimina un conjuto de entidades
        void Delete(IEnumerable<TEntity> entities);

        #endregion IRepository<T> Members
    }
}
