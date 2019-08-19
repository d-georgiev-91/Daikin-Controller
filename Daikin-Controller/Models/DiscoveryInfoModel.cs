using DaikinController.Serializers;

namespace DaikinController.Models
{
    public class DiscoveryInfoModel
    {
        public string IP { get; set; }

        [SerializerContract(Encodable = true)]
        public string Name { get; set; }

        public string Type { get; set; }
    }
}