using System;
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

            Assert.AreEqual("OK", model.Ret);
            Assert.IsNull(model.Adv);
            Assert.AreEqual(Power.Off, model.Pow);
            Assert.AreEqual(7, model.Mode);
            Assert.AreEqual(24.0, model.Stemp);
        }

        [TestMethod]
        public void DeserializeWithDecode()
        {
            var serializer = new DaikinSerializer<BasicInfo>();
            var model = serializer.Deserialize("ret=OK,type=aircon,reg=eu,dst=1,mode=3,ver=3_3_6,pow=0,err=0,location=0,name=%44%61%69%6b%69%6e%41%50%33%30%35%36%32,icon=9,method=polling,port=30050,id=dgeorgiev,pw=7vl41z4u,lpw_flag=0,adp_kind=2,pv=0,cpv=0,cpv_minor=00,led=1,en_setzone=1,mac=90B6869CF38A,adp_mode=run,en_hol=0,grp_name=,en_grp=0");

            
            Assert.AreEqual(Uri.UnescapeDataString("%44%61%69%6b%69%6e%41%50%33%30%35%36%32"),model.Name);
        }

        [TestMethod]
        public void DeserializeNullablePropertyWithNoValue()
        {
            var serializer = new DaikinSerializer<BasicInfo>();
            var model = serializer.Deserialize("ret=OK,type=aircon,reg=eu,dst=1,mode=3,ver=3_3_6,pow=0,err=0,location=0,name=%44%61%69%6b%69%6e%41%50%33%30%35%36%32,icon=9,method=polling,port=30050,id=dgeorgiev,pw=7vl41z4u,lpw_flag=0,adp_kind=2,pv=0,cpv=0,cpv_minor=00,led=1,en_setzone=1,mac=90B6869CF38A,adp_mode=run,en_hol=0,grp_name=,en_grp=0");


            Assert.IsNull(model.Stemp);
        }

        [TestMethod]
        public void DeserializeWithMapProperty()
        {
            var serializer = new DaikinSerializer<SensorInfoModel>();
            var model = serializer.Deserialize("ret=OK,htemp=21.5,hhum=-,otemp=24.0,err=0,cmpfreq=0");


            Assert.AreEqual(21.5,model.IndoorTemperature);
            Assert.IsNull(model.IndoorHumidity);
            Assert.AreEqual(24.0, model.OutdoorTemperature);
            Assert.AreEqual(0, model.Error);
            Assert.AreEqual(model.Cmpfreq, 0);
        }
    }
}
