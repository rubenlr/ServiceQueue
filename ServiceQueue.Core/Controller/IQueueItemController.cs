using ServiceQueue.Core.Model.Entity;
using System;
using System.Collections.Generic;

namespace ServiceQueue.Core.Controller
{
    public interface IQueueItemController : IInsertable<QueueItem>, IUpdatable<QueueItem>
    {
        ICollection<QueueItem> SelectPending(Guid idQueueType, int max = 100);
    }
}