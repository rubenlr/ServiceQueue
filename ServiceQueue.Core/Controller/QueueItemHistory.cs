using System;
using System.Collections.Generic;
using ServiceQueue.Core.Model.Entity;

namespace ServiceQueue.Core.Controller
{
    class QueueItemHistoryController : IQueueItemHistory
    {
        public void Insert(QueueItemHistory obj)
        {
            throw new NotImplementedException();
        }

        public void Update(QueueItemHistory obj)
        {
            throw new NotImplementedException();
        }
        
        public ICollection<QueueItem> SelectPending(Guid idQueueType, int max = 100)
        {
            throw new NotImplementedException();
        }
    }
}