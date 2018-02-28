using System;
using AustinHarris.JsonRpc;
using Newtonsoft.Json.Linq;

namespace xelviewerService
{
    class Program
    {
        static object[] services = new object[] {
           new ExampleCalculatorService(),
           new RepeatService()
        };

        static void Main(string[] args)
        {
            Console.WriteLine("starting");
            var rpcResultHandler = new AsyncCallback(_ => Console.WriteLine(((JsonRpcStateAsync)_).Result));

            for (string line = Console.ReadLine(); !string.IsNullOrEmpty(line); line = Console.ReadLine())
            {
                var async = new JsonRpcStateAsync(rpcResultHandler, null);
                async.JsonRpc = line;
                JsonRpcProcessor.Process(async);
            }
        }
    }
    
    public class ExampleCalculatorService : JsonRpcService
    {
        [JsonRpcMethod]
        private double add(double l, double r)
        {
            return l+r;
        }

        [JsonRpcMethod]
        private int addInt(int l, int r)
        {
            return l + r;
        }
    }

    public class RepeatService : JsonRpcService
    {
        [JsonRpcMethod]
        private string repeat(string l)
        {
            return l;
        }
    }
}
