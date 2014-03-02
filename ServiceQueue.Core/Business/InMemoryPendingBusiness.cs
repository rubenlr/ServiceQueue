using System.Collections.Generic;
using ServiceQueue.Core.Model.Entity;

namespace ServiceQueue.Core.Business
{
    class InMemoryPendingBusiness
    {
        private ICollection<QueueType> _types;
        private ICollection<QueueItem> _pendingItens;
    }
}