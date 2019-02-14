using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace MessageBroker
{
    public sealed class MessageBus : IDisposable
    {
        private Consumer<Null, string> _consumer;

        private readonly IDictionary<string, object> _consumerConfig;

        public MessageBus() : this("localhost")
        {
        }

        public MessageBus(string host)
        {
            _consumerConfig = new Dictionary<string, object>
            {
                { "group.id", "spo-group" },
                { "bootstrap.servers", host }
            };

        }

        public void SubscribeOnTopic<T>(string topic, Action<T> action,
            CancellationToken cancellationToken)
            where T : class
        {
            var msgBus = new MessageBus();
            using (msgBus._consumer = new Consumer<Null, string>(_consumerConfig, null, new StringDeserializer(Encoding.UTF8)))
            {
                msgBus._consumer.Subscribe(topic);

                while (!cancellationToken.IsCancellationRequested)
                {
                    if (msgBus._consumer.Consume(out Message<Null, string> msg, TimeSpan.FromMilliseconds(1)))
                    {
                        action(msg.Value as T);
                    }
                }
            }
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }
    }
}
