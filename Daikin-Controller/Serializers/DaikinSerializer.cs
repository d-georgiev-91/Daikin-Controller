using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace DaikinController.Serializers
{
    public class DaikinSerializer<T> where T : class, new()
    {
        private static readonly char[] PropertySeparator = { ',' };
        private static readonly char[] PairSeparator = { '=' };

        private Dictionary<string, PropertyInfo> modelProperties;

        private Dictionary<string, PropertyInfo> ModelProperties
        {
            get
            {
                modelProperties = modelProperties ?? typeof(T).GetProperties().ToDictionary(p => p.Name.ToLower());

                return modelProperties;
            }
        }

        public T Deserialize(string data)
        {
            var pairs = data.Split(PropertySeparator, StringSplitOptions.RemoveEmptyEntries);
            var model = new T();

            foreach (var pair in pairs)
            {
                var pairTokens = pair.Split(PairSeparator, StringSplitOptions.RemoveEmptyEntries);
                var key = pairTokens[0];
                var value = pairTokens.Length >= 2 ? pairTokens[1] : null;

                if (ModelProperties.ContainsKey(key))
                {
                    var property = ModelProperties[key];
                    var serializerContractAttribute = property.GetCustomAttribute<SerializerContractAttribute>();

                    if (value != null && serializerContractAttribute != null && serializerContractAttribute.Decode)
                    {
                        value = Uri.UnescapeDataString(value);
                    }

                    object proeprtyValue = property.PropertyType.IsEnum ? int.Parse(value) : Convert.ChangeType(value, property.PropertyType, CultureInfo.InvariantCulture);

                    property.SetValue(model, proeprtyValue);
                }
            }

            return model;
        }
    }
}
