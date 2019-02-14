
namespace SpoKafkaConnector.Model
{
    public class Card
    {
        public long Id { get; set; }

        public long Number { get; set; }

        public override string ToString()
        {
            return $"Card Id: {Id}, Number: {Number}";
        }
    }
}
