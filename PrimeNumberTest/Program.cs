using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DepsWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Test
{
    static class Program
    {
        const string UserName = @"adminn";
        const string Password = @"pas5w0rd";
        static readonly HttpClient Client = new HttpClient();

        static async Task Main()
        {
            using (var fs = new FileStream("TestUri.json", FileMode.Open))
            {
                Client.BaseAddress = new Uri((await JsonSerializer.DeserializeAsync<UriModel>(fs)).Uri);
            }
            await TestRegister("200 result:1");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                AuthenticationSchemes.Basic.ToString(),
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{UserName}:{Password}"))
            );
            await Test("/Rates/UAH/USD", "200 result: 0.03");
            await Test("/Rates/UAH/USD?amount=100", "200 result: 3.6");
            await TestFail("/Rates/UAH/USD?amount=100", "401");
            await TestFailRegister("400");
            Console.ReadKey();
        }
        
        public static async Task TestRegister(string expected)
        {
            var content = new StringContent(JsonSerializer.Serialize(new RegisterModel(UserName, Password)), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/register", content);
            Console.WriteLine($"Send: {Client.BaseAddress + "/register"}");
            Console.WriteLine($"Expected: \t{expected}");
            var str = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Get: \t\t{(int)response.StatusCode} result: {str}");
        }
        public static async Task Test(string uri, string expected)
        {
            var response = await Client.GetAsync(uri);
            Console.WriteLine($"Send: {Client.BaseAddress + uri}");
            Console.WriteLine($"Expected: \t{expected}");
            var ex = await JsonSerializer.DeserializeAsync<decimal>(await response.Content.ReadAsStreamAsync());
            Console.WriteLine($"Get: \t\t{(int)response.StatusCode} result: {ex }");
        }
        public static async Task TestFail(string uri, string expected)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                AuthenticationSchemes.Basic.ToString(),
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{UserName}:11111111")));
            var response = await Client.GetAsync(uri);
            Console.WriteLine($"Send: {Client.BaseAddress + uri}");
            Console.WriteLine($"Expected: \t{expected}");
            var ex = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Get: \t\t{(int)response.StatusCode}");
        }

        public static async Task TestFailRegister(string expected)
        {
            var content = new StringContent(JsonSerializer.Serialize(new RegisterModel("111", Password)), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("/register", content);
            Console.WriteLine($"Send: {Client.BaseAddress + "/register"}");
            Console.WriteLine($"Expected: \t{expected}");
            var str = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Get: \t\t{(int)response.StatusCode}");
        }
    }
}
