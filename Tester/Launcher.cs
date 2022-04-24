using Google.Protobuf;
using Grpc.Net.Client;
using Messenger;
using Messenger.Protocols;

namespace Tester;

public class Launcher
{
    public static void Main(string[] arguments)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5000");
        var client = new Router.RouterClient(channel);

        var sensorRequest = new SensorRequest
        {
            Type = "Gyr",
            ID = 0
        };
        
        var package = new Messenger.Message
        {
            ID = 0,
            Protocol = "SNS_REQ",
            Content = sensorRequest.ToByteString()
        };

        client.Write(new WriteRequest
        {
            Data = package.ToByteString()
        });
    }
}