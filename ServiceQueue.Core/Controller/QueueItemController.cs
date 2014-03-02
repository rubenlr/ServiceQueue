using System;
using System.Collections.Generic;
using ServiceQueue.Core.Model.Entity;

namespace ServiceQueue.Core.Controller
{
    class QueueItemController : IQueueItemController
    {
        public void Insert(QueueItem obj)
        {
            throw new NotImplementedException();
        }

        public void Update(QueueItem obj)
        {
            throw new NotImplementedException();
        }

        public ICollection<QueueItem> SelectAll()
        {
            throw new NotImplementedException();
        }

        public QueueItem Select(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}