using MiCarpeta.Document.Repository.Entities;
using System;
using System.Collections.Generic;

namespace MiCarpeta.Document.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        bool Add(TEntity obj);
        List<TEntity> GetByList(List<FilterQuery> valuesAtributeScanOperator);
        bool Update(TEntity obj);
    }
}
