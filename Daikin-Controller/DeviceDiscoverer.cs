using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using DaikinController.Serializers;

namespace DaikinController
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using Models;

    public class DeviceDiscoverer
    {
        private const string LISTEN_IP = "0.0.0.0";
        private const int LISTEN_PORT = 30000;
        private const string MULTICAST_GROUP_IP = "224.0.0.1";

        private const int BROADCAST_PORT = 30050;
        private const int RECEIVE_ATTEMPTS = 10;

        private const string UDP_DATA = "DAIKIN_UDP/common/basic_info";

        private UdpClient udpClient;
        private byte[] sendData;
        private int recieveAttempts;

        public DeviceDiscoverer()
        {
            Serializer = new DaikinSerializer<DiscoveryInfoModel>();
            udpClient = new UdpClient();
            sendData = Encoding.UTF8.GetBytes(UDP_DATA);
            recieveAttempts = RECEIVE_ATTEMPTS;
        }

        private DaikinSerializer<DiscoveryInfoModel> Serializer { get; }

        public async Task<IEnumerable<DiscoveryInfoModel>> Discover()
        {
            var devices = new List<DiscoveryInfoModel>();

            udpClient.Client.Bind(new IPEndPoint(IPAddress.Parse(LISTEN_IP), LISTEN_PORT));
            udpClient.EnableBroadcast = true;
            udpClient.JoinMulticastGroup(IPAddress.Parse(MULTICAST_GROUP_IP));

            var broadCastIPs = GetBroadCastIPs();

            foreach (var broadCastIP in broadCastIPs)
            {
                await udpClient.SendAsync(sendData, sendData.Length, new IPEndPoint(broadCastIP, BROADCAST_PORT));
            }

            var receiveResult = await udpClient.ReceiveAsync();

            var message = Encoding.ASCII.GetString(receiveResult.Buffer);
            var deviceInfo = Serializer.Deserialize(message);
            deviceInfo.IP = receiveResult.RemoteEndPoint.Address.ToString();

            devices.Add(deviceInfo);

            return devices;
        }

        private IEnumerable<IPAddress> GetBroadCastIPs()
        {
            var upWifiNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                            i.OperationalStatus == OperationalStatus.Up);

            var unicastInternetAddresses = upWifiNetworkInterfaces
                .SelectMany(i => i.GetIPProperties().UnicastAddresses.Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork));

            var ipAddresses = unicastInternetAddresses
                .Select(u => u.GetBroadcastAddress());

            return ipAddresses;
        }
    }
}