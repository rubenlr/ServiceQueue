using System;
using System.Collections.Generic;

namespace ServiceQueue.Core.Controller
{
    public interface ICrud<T>
    {
        void Insert(T obj);
        void Update(T obj);
        ICollection<T> SelectAll();
        T Select(Guid id);
    }
}