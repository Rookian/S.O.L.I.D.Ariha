﻿namespace Core.Domain.Bases.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Delete(T entity);
        T[] GetAll();
        T GetById(object id);
        void SaveOrUpdate(T enity);
        void Merge(T entity);
    }
}
