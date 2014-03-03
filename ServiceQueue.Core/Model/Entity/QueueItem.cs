using System;

namespace ServiceQueue.Core.Model.Entity
{
    public class QueueItem
    {
        public Guid Id { get; set; }
        public Guid IdQueueType { get; set; }
        public string Message { get; set; }
        public string Lote { get; set; }
        public DateTime Recorded { get; set; }
        public DateTime? Executed { get; set; }
    }

    public class QueueItemHistory
    {
        public Guid Id { get; set; }
        public Guid IdQueueItem { get; set; }
        public DateTime Executed { get; set; }
        public bool Result { get; set; }
        public string ReturnMessage { set; get; }
    }

    public class QueueType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ConcurrenceLimit { get; set; }
        public string Url { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(QueueType obj)
        {
            return Equals(Id, obj.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj is QueueType) return Equals(obj as QueueType);
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
