namespace DaikinController.Models
{
    public class DeviceInfo
    {
        //"10.194.75.143":"ret=OK,type=aircon,reg=eu,dst=1,ver=3_3_6,pow=0,err=144,location=0,name=%44%61%69%6b%69%6e%41%50%33%30%35%36%32,icon=9,method=polling,port=30050,id=dgeorgiev,pw=7vl41z4u,lpw_flag=0,adp_kind=2,pv=0,cpv=0,cpv_minor=00,led=1,en_setzone=1,mac=90B6869CF38A,adp_mode=run,en_hol=0,grp_name=,en_grp=0"

        public string IP { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }
    }
}
