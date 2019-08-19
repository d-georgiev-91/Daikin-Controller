using DaikinController.Serializers;

namespace DaikinSerializers.Test.Models
{
    public class BasicInfo
    {
        public string Ret { get; set; }

        public Power Pow { get; set; }

        public int Mode { get; set; }

        public double? Stemp { get; set; }

        public string Adv { get; set; }

        [SerializerContract(Decode = true)]
        public string Name { get; set; }
    }

    public enum Power
    {
        Off = 0,
        On = 1
    }
}
