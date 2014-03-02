using System;
using System.Collections.Generic;
using ServiceQueue.Core.Model.Entity;

namespace ServiceQueue.Core.Controller
{
    class QueueItemHistory : IQueueItemHistory
    {
        public void Insert(QueueExecutionHistory obj)
        {
            throw new NotImplementedException();
        }

        public void Update(QueueExecutionHistory obj)
        {
            throw new NotImplementedException();
        }

        public ICollection<QueueExecutionHistory> SelectAll()
        {
            throw new NotImplementedException();
        }

        public QueueExecutionHistory Select(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}