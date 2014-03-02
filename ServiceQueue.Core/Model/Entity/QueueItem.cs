using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ServiceQueue.Core.Model.Entity
{
    class QueueItem
    {
        public QueueItem()
        {
            HistoricoExecucoes = new BlockingCollection<QueueHistoricoExecucao>();
        }

        public Guid Id { get; set; }
        public Guid IdQueueTipo { get; set; }
        public string Message { get; set; }
        public string Lote { get; set; }
        public DateTime Gravado { get; set; }
        public DateTime Executado { get; set; }

        public BlockingCollection<QueueHistoricoExecucao> HistoricoExecucoes { get; private set; }
    }

    class QueueHistoricoExecucao
    {
        public Guid Id { get; set; }
        public Guid IdQueueItem { get; set; }
        public DateTime Execucao { get; set; }
        public bool Resultado { get; set; }
        public string Retorno { set; get; }
    }

    class QueueTipo
    {

        public Guid Id { get; set; }
        public int MaximoExecucoesSimultaneas { get; set; }
        public string Url { get; set; }
    }
}
