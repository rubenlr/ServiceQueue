namespace ServiceQueue.Core.Business
{
    class QueueConsumerBusiness
    {
        public QueueConsumerBusiness()
        {
        }

        //private void ManterEmExecucao(QueueTipoHandler tipo)
        //{
        //    while (tipo.Pendente.Count > 0 && tipo.Tipo.ConcurrenceLimit > tipo.EmExecucao.Count)
        //    {
        //        QueueItem item;

        //        lock (tipo)
        //            item = tipo.Pendente.OrderBy(x => x.Recorded).FirstOrDefault();

        //        if (item != null)
        //        {
        //            lock (tipo)
        //            {
        //                tipo.Pendente.Remove(item);
        //                tipo.EmExecucao.Add(item);
        //            }

        //            new Task(() =>
        //            {
        //                Executar(item);

        //                lock (tipo)
        //                    tipo.EmExecucao.Remove(item);
        //            }).Start();
        //        }
        //    }
        //}

        //private void Executar(QueueItem item)
        //{

        //}
    }
}
