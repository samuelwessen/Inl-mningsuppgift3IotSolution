using System;
using SharedLibraries.Services;
using SharedLibraries.Models;
using Microsoft.Azure.Devices.Client;

namespace Inlämningsuppgift3
{
    class Program
    {
        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=ec-win20-samuelw-iothub.azure-devices.net;DeviceId=inlamningsuppgift3iot;SharedAccessKey=JBA1SufYkk8jadrHTO1aN56OlROLlbWS/3zr42maXa4=", TransportType.Mqtt);


        static void Main(string[] args)
        {
            DeviceService.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceService.ReceiveMessageAsync(deviceClient).GetAwaiter();
            Console.ReadKey();
        }
    }
}
