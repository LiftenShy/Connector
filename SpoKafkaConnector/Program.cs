using System;
using System.Configuration;
using System.Threading;
using MessageBroker;
using Newtonsoft.Json;
using SpoKafkaConnector.Model;

namespace SpoKafkaConnector
{
    class Program
    {
        private static readonly string Topic = ConfigurationManager.AppSettings["topic"];
        private static readonly string Host = ConfigurationManager.AppSettings["host"];

        static void Main(string[] args)
        {
            Console.WriteLine("Message");

            using (var msgBus = new MessageBus(Host))
            {
                msgBus.SubscribeOnTopic<string>(Topic, SyncDataHandler, CancellationToken.None);
            }
        }

        public static void SyncDataHandler(string data)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(data);
                Console.WriteLine($"User {user}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Message: {e.Message}, Inner exception{e.InnerException}");
            }

            //Send to sharePoint online
        }
    }
}
