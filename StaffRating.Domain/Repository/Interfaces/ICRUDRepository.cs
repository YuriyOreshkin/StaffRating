﻿using System.Collections.Generic;
using System.Linq;

namespace StaffRating.Domain.Repository.Interfaces
{
    //CRUD
    public interface ICRUDRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();
        void Create(TEntity item);
        void Delete(TEntity item);
        void Update(TEntity item);
    }
}
