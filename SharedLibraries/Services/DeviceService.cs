﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using SharedLibraries.Models;

namespace SharedLibraries.Services
{
    public static class DeviceService
    {
        private static readonly Random rnd = new Random();

        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var data = new TemperatureModel
                {
                    Temperature = rnd.Next(15, 45),
                    Humidity = rnd.Next(20, 60)
                };

                var json = JsonConvert.SerializeObject(data);

                var payload = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(json));
                await deviceClient.SendEventAsync(payload);

                Console.WriteLine($"Message sent: {json}");
                await Task.Delay(60 * 1000);
            }
        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;

                Console.WriteLine($"Message Received: { Encoding.UTF8.GetString(payload.GetBytes())}");

                await deviceClient.CompleteAsync(payload);
            }
        }

        public static async Task SendMessageToDeviceAsync(ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new Microsoft.Azure.Devices.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);
        }
    }
}
