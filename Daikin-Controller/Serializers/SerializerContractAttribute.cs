using System;

namespace DaikinController.Serializers
{
    public class SerializerContractAttribute : Attribute
    {
        public bool Decode { get; set; }
    }
}
