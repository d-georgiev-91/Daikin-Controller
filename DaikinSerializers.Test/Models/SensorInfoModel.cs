using DaikinController.Serializers;

namespace DaikinSerializers.Test.Models
{
    public class SensorInfoModel
    {
        [SerializerContract(PropertyMap = "htemp")]
        public double IndoorTemperature { get; set; }

        [SerializerContract(PropertyMap = "hhum")]
        public double? IndoorHumidity { get; set; }

        [SerializerContract(PropertyMap = "otemp")]
        public double OutdoorTemperature { get; set; }

        [SerializerContract(PropertyMap = "err")]
        public int Error { get; set; }

        public int Cmpfreq { get; set; }
    }
}
