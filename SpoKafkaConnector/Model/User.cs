using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpoKafkaConnector.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string SecondName { get; set; }

        public decimal Balance { get; set; }

        [JsonProperty("Cards")]
        private List<Card> Cards { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, SecondName: {SecondName}, Balance: {Balance}, Cards: {Cards}";
        }
    }
}
