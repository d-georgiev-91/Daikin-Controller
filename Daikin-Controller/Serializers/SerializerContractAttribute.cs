﻿using System;

namespace DaikinController.Serializers
{
    public class SerializerContractAttribute : Attribute
    {
        public string PropertyMap { get; set; }

        public bool Encodable { get; set; }
    }
}
