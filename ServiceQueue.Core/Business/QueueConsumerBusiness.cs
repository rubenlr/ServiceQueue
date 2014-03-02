using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceQueue.Core.Model.Entity;

namespace ServiceQueue.Core.Business
{
    class QueueConsumerBusiness
    {
        public QueueConsumerBusiness()
        {
        }

        BlockingCollection<QueueTipoHandler> Queue { get; set; }

        class QueueTipoHandler : QueueTipo
        {
            public QueueTipoHandler()
            {
                Pendente = new List<QueueItem>(100);
                EmExecucao = new List<QueueItem>(100);
            }

            public ICollection<QueueItem> Pendente { get; private set; }
            public ICollection<QueueItem> EmExecucao { get; private set; }
        }

        private void ManterEmExecucao(QueueTipoHandler tipo)
        {
            while (tipo.Pendente.Count > 0 && tipo.MaximoExecucoesSimultaneas > tipo.EmExecucao.Count)
            {
                QueueItem item;

                lock (tipo)
                    item = tipo.Pendente.OrderBy(x => x.Gravado).FirstOrDefault();

                if (item != null)
                {
                    lock (tipo)
                    {
                        tipo.Pendente.Remove(item);
                        tipo.EmExecucao.Add(item);
                    }

                    new Task(() =>
                    {
                        Executar(item);

                        lock (tipo)
                            tipo.EmExecucao.Remove(item);
                    }).Start();
                }
            }
        }

        private void Executar(QueueItem item)
        {

        }
    }
}
