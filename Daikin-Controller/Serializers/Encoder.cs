using System;
using System.Text;

namespace DaikinController.Serializers
{
    public static class Encoder
    {
        public static string Encode(string @string)
        {
            var buffer = new StringBuilder();

            foreach (var character in @string)
            {
                buffer.Append($"%{(int) character:X}");
            }

            return buffer.ToString();
        }

        public static string Decode(string @string)
        {
            return Uri.UnescapeDataString(@string);
        }
    }
}
