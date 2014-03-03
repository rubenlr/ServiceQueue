using ServiceQueue.Core.Model.Entity;

namespace ServiceQueue.Core.Controller
{
    public interface IQueueTypeController : IInsertable<QueueType>, IUpdatable<QueueType>, ISelectableById<QueueType>
    {
    }
}