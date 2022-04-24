using System.Device.Spi;
using Google.Protobuf;
using Grpc.Core;

using Messenger.Protocols;
using Status = Messenger.Protocols.Status;

namespace Messenger.Services;

class RouterImplementer : Router.RouterBase
{
    private SpiDevice _device;
    
    public RouterImplementer()
    {
        var settings = new SpiConnectionSettings(0, 0)
        {
            ClockFrequency = 5000000,
            Mode = SpiMode.Mode0
        };
        // _device = SpiDevice.Create(settings);
        
    }

    public override Task<WriteResponse> Write(WriteRequest request, ServerCallContext context)
    {
        return Task.Factory.StartNew(() =>
        {
            var package = new MemoryStream();
            package.WriteByte(0xBE);
            package.Write(BitConverter.GetBytes(request.Data.Length));
            request.Data.WriteTo(package);
            package.WriteByte(0);
            package.WriteByte(0xED);
            
            // _device.Write(package.ToArray());
            
            return new WriteResponse
            {
                Result = Status.Success
            };
        });
    }

    public override Task<ReadResponse> Read(ReadRequest request, ServerCallContext context)
    {
        return Task.Factory.StartNew(() =>
        {
            var data = new byte[request.Length];

            // _device.Read(data);

            return new ReadResponse
            {
                Data = ByteString.CopyFrom(data)
            };
        });
    }
}