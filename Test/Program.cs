using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using DaikinController;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var devices = new DeviceDiscoverer().Discover();

            Console.WriteLine("Searching...");

            var keyInfo = new ConsoleKeyInfo();

            while (keyInfo.Key != ConsoleKey.Escape)
            {
                keyInfo = Console.ReadKey();
            }
        }
    }
}
