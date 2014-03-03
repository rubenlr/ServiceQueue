using System;
using System.Linq;
using System.Threading.Tasks;
using ServiceQueue.Core.Controller;
using ServiceQueue.Core.Model.Entity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using log4net;

namespace ServiceQueue.Core.Business
{
    class QueueTypeConsumerBusiness
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IQueueTypeController _queueTypeController;
        private readonly IQueueItemController _queueItemController;
        private QueueType _type;
        private readonly ICollection<QueueItem> _pending;
        private readonly ICollection<QueueItem> _executing;
        private readonly AutoResetEvent _event;
        private const int MaxQueueSize = 100;

        public QueueTypeConsumerBusiness(IQueueTypeController queueTypeController,
                                         IQueueItemController queueItemController,
                                         QueueType type)
        {
            _queueTypeController = queueTypeController;
            _queueItemController = queueItemController;
            _type = type;
            _event = new AutoResetEvent(true);
            _pending = new Collection<QueueItem>();
            _executing = new Collection<QueueItem>();

            log.InfoFormat("QueueTypeConsumerBusiness [construtor] para {0}", type);
        }

        public void Consume()
        {
            while (_event.WaitOne(10000))
            {
                if (CountPendingItens() <= _type.ConcurrenceLimit)
                {
                    var newPendingItens = _queueItemController.SelectPending(_type.Id);
                    log.DebugFormat("Fila [{0} {1}] menor que o limite [{2}]. Adicionando {3} itens", _type, CountPendingItens(), _type.ConcurrenceLimit, newPendingItens.Count);
                    PutOnPending(newPendingItens);
                }

                while (CountExecuringItens() < _type.ConcurrenceLimit && CountPendingItens() > 0)
                {
                    var item = GetNextPending();

                    if (item != null)
                    {
                        RemoveFromPending(item);
                        PutOnExecuting(item);

                        new Task(() => Consume(item)).Start();
                    }
                }

                if (CountPendingItens() <= MaxQueueSize)
                {
                    var newPendingItens = _queueItemController.SelectPending(_type.Id);
                    log.DebugFormat("Fila [{0} {1}] não está cheia [{2}]. Adicionando {3} itens", _type, CountPendingItens(), MaxQueueSize, newPendingItens.Count);
                    PutOnPending(newPendingItens);
                }

                UpdateType();
            }
        }

        void Consume(QueueItem item)
        {
            try
            {
                log.InfoFormat("Consumindo item {0}", item.Id);
                item.Executed = DateTime.Now;
                _queueItemController.Update(item);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Erro durante a execução do item {0}", item.Id), ex);
                PutOnPending(item);
            }
            finally
            {
                RemoveFromExecuting(item);
                _event.Set();
            }
        }

        void UpdateType()
        {
            lock (this)
                _type = _queueTypeController.Select(_type.Id);
        }

        int CountExecuringItens()
        {
            lock (_executing)
                return _executing.Count;
        }

        int CountPendingItens()
        {
            lock (_pending)
                return _pending.Count;
        }

        QueueItem GetNextPending()
        {
            lock (_pending)
                return _pending.OrderBy(x => x.Recorded).FirstOrDefault(x => x.Executed == null);
        }

        void PutOnPending(QueueItem item)
        {
            lock (_pending)
                _pending.Add(item);
        }

        void PutOnPending(IEnumerable<QueueItem> itens)
        {
            foreach (var item in itens)
                PutOnPending(item);
        }

        void RemoveFromPending(QueueItem item)
        {
            lock (_pending)
                _pending.Remove(item);
        }

        void PutOnExecuting(QueueItem item)
        {
            lock (_executing)
                _executing.Add(item);
        }

        void RemoveFromExecuting(QueueItem item)
        {
            lock (_executing)
                _executing.Remove(item);
        }
    }
}