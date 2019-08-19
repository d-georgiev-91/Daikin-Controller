using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var keyValuePairs = pairs.Select(p => p.Split(PairSeparator, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(p => p[0], p => p.Length > 1 ? p[1] : null);

            var model = new T();

            foreach (var modelProperty in ModelProperties)
            {
                SetPropertyValue(model, modelProperty, keyValuePairs);
            }

            return model;
        }

        private void SetPropertyValue(T model, KeyValuePair<string, PropertyInfo> modelProperty, Dictionary<string, string> keyValuePairs)
        {
            string dataValue = null;
            var serializerContract = modelProperty.Value.GetCustomAttribute<SerializerContractAttribute>();

            if (serializerContract?.PropertyMap != null && keyValuePairs.ContainsKey(serializerContract.PropertyMap))
            {
                dataValue = keyValuePairs[serializerContract.PropertyMap];
            }
            else if (keyValuePairs.ContainsKey(modelProperty.Key))
            {
                dataValue = keyValuePairs[modelProperty.Key];
            }

            if (serializerContract != null && serializerContract.Decode && dataValue != null)
            {
                dataValue = Uri.UnescapeDataString(dataValue);
            }


            object propertyValue;

            if (modelProperty.Value.PropertyType.IsEnum)
            {
                propertyValue = int.Parse(dataValue);
            }
            else
            {
                // Some values null values could be sent empty by the wifi device http server or with having a - character
                // thus Cast exception is required
                try
                {
                    propertyValue = Convert.ChangeType(dataValue, modelProperty.Value.PropertyType, CultureInfo.InvariantCulture);
                }
                catch (InvalidCastException e)
                {
                    Debug.WriteLine(e.Message);
                    propertyValue = null;
                }
            }

            modelProperty.Value.SetValue(model, propertyValue);
        }
    }
}
