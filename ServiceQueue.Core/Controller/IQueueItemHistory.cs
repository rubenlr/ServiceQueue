using ServiceQueue.Core.Model.Entity;

namespace ServiceQueue.Core.Controller
{
    public interface IQueueItemHistory : ICrud<QueueExecutionHistory>
    {
    }
}