using DaikinController.Serializers;
using DaikinSerializers.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaikinSerializers.Test
{
    [TestClass]
    public class DaikinSerializerSerializeTests
    {
        [TestMethod]
        public void Serialize()
        {
            var serializer = new DaikinSerializer<BasicInfo>();
            var serializedData = serializer.Serialize(new BasicInfo
            {
                Mode = 5,
                Name = "Test",
                Pow = Power.On,
                Stemp = 23.5,
                Enabled = true
            });

            Assert.AreEqual("ret=null&pow=1&mode=5&stemp=23.5&adv=null&name=%54%65%73%74&enabled=1&boolnullabe=null", serializedData);
        }
    }
}