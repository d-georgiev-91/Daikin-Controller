using DaikinController.Serializers;
using DaikinSerializers.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DaikinSerializers.Test
{
    [TestClass]
    public class DaikinSerializerDeserializeTests
    {
        [TestMethod]
        public void Deserialize()
        {
            var serializer = new DaikinSerializer<BasicInfo>();
            var model = serializer.Deserialize("ret=OK,pow=0,mode=7,adv=,stemp=24.0,dfd2=0,dfd3=3,dfd4=0,dfd5=0,dfd6=0,dfd7=0,dfdh=0");

            Assert.AreEqual(model.Ret, "OK");
            Assert.IsNull(model.Adv);
            Assert.AreEqual(model.Pow, Power.Off);
            Assert.AreEqual(model.Mode, 7);
            Assert.AreEqual(model.Stemp, 24.0);
        }
    }
}
