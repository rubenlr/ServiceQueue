using NUnit.Framework;
using Rhino.Mocks;
using ServiceQueue.Core.Business;
using ServiceQueue.Core.Controller;
using ServiceQueue.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ServiceQueue.Core.Test.Business
{
    [TestFixture]
    public class QueueHandlerBusinessTest
    {
        private QueueHandlerBusiness _business;
        private IQueueTypeController _queueTypeControllerMock;

        [SetUp]
        public void ContructMocks()
        {
            _queueTypeControllerMock = MockRepository.GenerateStrictMock<IQueueTypeController>();

            _business = new QueueHandlerBusiness(_queueTypeControllerMock);
        }

        [TearDown]
        public void VerifyMocks()
        {
            _queueTypeControllerMock.VerifyAllExpectations();
        }

        [Test]
        public void ShouldNotCallBeforeCallForQueue()
        {
        }

        [Test]
        public void ShouldGetEmAllWhenCalledFirstTime()
        {
            _queueTypeControllerMock.Expect(x => x.SelectAll()).Return(new Collection<QueueType>()).Repeat.Once();

            var item = _business.NextPendingItem();

            Assert.IsNull(item);
        }

        [Test]
        public void ShouldNotCallSelectAllWhenAlreadyHaveQueueTypes()
        {
            var queueTypes = new Collection<QueueType>
            {
                new QueueType
                {
                    Id = Guid.NewGuid(),
                    MaximoExecucoesSimultaneas = 10
                }
            };
            _queueTypeControllerMock.Expect(x => x.SelectAll()).Return(queueTypes).Repeat.Once();

            var item1 = _business.NextPendingItem();
            var item2 = _business.NextPendingItem();

            Assert.IsNull(item1);
            Assert.IsNull(item2);
        }

        [Test]
        public void ShouldSearchForPendingWhenDoesNotHaveIt()
        {
            _queueTypeControllerMock.Expect(x => x.SelectAll()).Return(QueueTipoUtil.GetRandomCollection()).Repeat.Once();

            var item = _business.NextPendingItem();

            Assert.IsNull(item);

        }

        class QueueTipoUtil
        {
            public static QueueType GetRandom(int maxConcorrenceExecution = 10)
            {
                return new QueueType
                {
                    Id = Guid.NewGuid(),
                    MaximoExecucoesSimultaneas = maxConcorrenceExecution
                };
            }

            public static ICollection<QueueType> GetRandomCollection(int maxItens = 10)
            {
                var max = (new Random().Next() % maxItens) + 1;

                ICollection<QueueType> result = new Collection<QueueType>();

                for (var i = 0; i < max; i++)
                    result.Add(GetRandom());

                return result;
            }
        }
    }
}
