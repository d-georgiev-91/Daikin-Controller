using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

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

        public string Serialize(T model)
        {
            var buffer = new StringBuilder();
            int currentIndex = 0;

            foreach (var modelProperty in ModelProperties)
            {

                var serializerContract = modelProperty.Value.GetCustomAttribute<SerializerContractAttribute>();
                string key;

                if (serializerContract != null && !string.IsNullOrWhiteSpace(serializerContract.PropertyMap))
                {
                    key = serializerContract.PropertyMap;
                }
                else
                {

                    key = modelProperty.Key;
                }

                string value = ConvertToString(model, modelProperty.Value);

                if (serializerContract != null && serializerContract.Encodable)
                {
                    value = Encoder.Encode(value);
                }

                buffer.Append($"{key}={value}");

                if (currentIndex < modelProperties.Count - 1)
                {
                    buffer.Append("&");
                }

                currentIndex++;
            }

            return buffer.ToString();
        }

        private static string ConvertToString(T model, PropertyInfo property)
        {
            var rawValue = property.GetValue(model);

            // Verify how null parameters are send
            if (rawValue == null)
            {
                return "null";
            }

            if (property.PropertyType.IsEnum)
            {
                return Convert.ToString((int)rawValue, CultureInfo.InvariantCulture);
            }

            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {

                var value = rawValue;
                return (bool)value ? "1" : "0";
            }

            return Convert.ToString(rawValue, CultureInfo.InvariantCulture);
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

            if (serializerContract != null && serializerContract.Encodable && dataValue != null)
            {
                dataValue = Encoder.Decode(dataValue);
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
                    var propertyType = Nullable.GetUnderlyingType(modelProperty.Value.PropertyType) ??
                                       modelProperty.Value.PropertyType;

                    propertyValue = Convert.ChangeType(dataValue, propertyType, CultureInfo.InvariantCulture);
                }
                catch (SystemException e) when (e is InvalidCastException || e is FormatException)
                {
                    Debug.WriteLine(e.Message);
                    propertyValue = null;
                }
            }

            modelProperty.Value.SetValue(model, propertyValue);
        }
    }
}
