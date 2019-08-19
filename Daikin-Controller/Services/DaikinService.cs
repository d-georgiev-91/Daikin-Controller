using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DaikinController.Models;
using DaikinController.Serializers;

namespace DaikinController.Services
{
    public class DaikinService
    {
        private HttpClient client;
        private const string ROUTE_URL = "http://{0}/{1}";


        public DaikinService()
        {
            this.client = new HttpClient();
        }

        public async Task<IEnumerable<DiscoveryInfoModel>> GetUnits()
        {
            var discoverer = new DeviceDiscoverer();
            var units = await discoverer.Discover();

            return units;
        }
    }
}