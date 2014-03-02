﻿using ServiceQueue.Core.Model.Entity;
using System;
using System.Collections.Generic;

namespace ServiceQueue.Core.Controller
{
    public interface IQueueItemController : ICrud<QueueItem>
    {
        ICollection<QueueItem> SelectPending(Guid idQueueType, int max = 100);
    }
}