using System.Collections.Concurrent;

namespace Presistence.Repositraces
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly ConcurrentDictionary<string,object>  _keyValuePairs;

        public UnitofWork(ApplicationDbcontext dbcontext) {
             _dbcontext = dbcontext;
             _keyValuePairs = new (); //Dictionary
        } 


        //constructor

        public async Task<int> SaveChangesAsync() => await _dbcontext.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey>
            GetRepository<TEntity, TKey>() where TEntity : BaseEntitiyID<TKey>
              => (IGenericRepository<TEntity, TKey>) 
               _keyValuePairs.GetOrAdd(typeof(TEntity).Name,
                _ => new GenericRepository<TEntity, TKey>(_dbcontext));
        #region Comment

        //return new GenericRepository<TEntity, TKey>(_dbcontext);
        //REQ==> Instance From Generic Repository 
        //Dictionary
        // Key    :   Value
        // Product : new GenericRepository <Product, int>😍😍😍😍😍😍 

        #endregion

        #region GetOrAdd instead of If else
        //var typename= typeof(TEntity).Name;//Key
        //if(_keyValuePairs.ContainsKey(typename))
        //{
        //    return (IGenericRepository<TEntity, TKey>)_keyValuePairs[typename];
        //}
        //else
        //{
        //   _keyValuePairs.GetOrAdd(typeof(TEntity).Name,_=>new GenericRepository<TEntity,TKey>(_dbcontext));
        //    return (IGenericRepository<TEntity, TKey>)_keyValuePairs[typename];
        //} 
        #endregion 







    }
}
