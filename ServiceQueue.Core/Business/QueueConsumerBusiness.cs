using ServiceQueue.Core.Model.Entity;
using System.Threading;
using System.Collections.Generic;
using Ninject;
using ServiceQueue.Core.Controller;
using log4net;

namespace ServiceQueue.Core.Business
{
    class QueueConsumerBusiness
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Dictionary<QueueType, Thread> _queueTypes;
        IQueueTypeController _queueTypeController;
        QueueTypeConsumerBusinessFactory _consumerFactory;

        public QueueConsumerBusiness(IQueueTypeController queueTypeController, QueueTypeConsumerBusinessFactory consumerFactory)
        {
            _queueTypeController = queueTypeController;
            _consumerFactory = consumerFactory;
            _queueTypes = new Dictionary<QueueType, Thread>();
        }

        private void CheckForNewTypes()
        {
            var types = _queueTypeController.SelectAll();

            foreach (var type in types)
            {
                bool exists;

                lock (_queueTypes)
                    exists = _queueTypes.ContainsKey(type);

                if (!exists)
                    AddQueue(type);
            }
        }

        private void AddQueue(QueueType queueType)
        {
            lock (_queueTypes)
                if (_queueTypes.ContainsKey(queueType))
                {
                    log.WarnFormat("AddQueue [abortado] para {0}", queueType);
                    return;
                }

            var thread = new Thread(() => ConsumeBusinessRunner(queueType));

            lock (_queueTypes)
                _queueTypes.Add(queueType, thread);

            thread.Start();
        }

        private void ConsumeBusinessRunner(QueueType queueType)
        {
            _consumerFactory.GetInstance(queueType).Consume();
        }
    }

    class QueueTypeConsumerBusinessFactory
    {
        IKernel _kernel;

        public QueueTypeConsumerBusinessFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public QueueTypeConsumerBusiness GetInstance(QueueType queueType)
        {
            return new QueueTypeConsumerBusiness(_kernel.Get<IQueueTypeController>(),
                _kernel.Get<IQueueItemController>(),
                queueType);
        }
    }
}
