using ServiceQueue.Core.Model.Entity;
using System;
using System.Collections.Generic;

namespace ServiceQueue.Core.Controller
{
    public interface IInsertable<T>
    {
        void Insert(T obj);
    }

    public interface IUpdatable<T>
    {
        void Update(T obj);
    }

    public interface ISelectableById<T>
    {
        T Select(Guid id);
    }

    public interface IQueueItemHistoryRepository : IInsertable<QueueItemHistory>
    {
    }

    public interface IQueueItemRepository : IInsertable<QueueItem>, IUpdatable<QueueItem>
    {
        ICollection<QueueItem> SelectPending(Guid idQueueType, int max = 100);
    }

    public interface IQueueTypeRepository : IInsertable<QueueType>, IUpdatable<QueueType>, ISelectableById<QueueType>
    {
    }
}