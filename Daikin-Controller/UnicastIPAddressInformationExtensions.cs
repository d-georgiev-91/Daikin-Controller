using System;
using System.Net;
using System.Net.NetworkInformation;

namespace DaikinController
{
    public static class UnicastIPAddressInformationExtensions
    {
        public static IPAddress GetBroadcastAddress(this UnicastIPAddressInformation @this)
        {
            var ipAddressBytes = @this.Address.GetAddressBytes();
            var subnetMaskBytes = @this.IPv4Mask.GetAddressBytes();

            if (ipAddressBytes.Length != subnetMaskBytes.Length)
            {
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");
            }

            var broadcastAddress = new byte[ipAddressBytes.Length];

            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                var ipAddressByte = ipAddressBytes[i];
                var subnetMaskByte = subnetMaskBytes[i];

                broadcastAddress[i] = (byte)(ipAddressByte | (subnetMaskByte ^ 255));
            }

            return new IPAddress(broadcastAddress);
        }
    }
}