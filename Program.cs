using NModbus;
using System.Net;
using System.Net.Sockets;

class Program
{
    static async Task Main(string[] args)
    {
        // 创建TCP监听器
        var slaveTcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 502);
        slaveTcpListener.Start();

        IModbusFactory factory = new ModbusFactory();

        IModbusSlaveNetwork network = factory.CreateSlaveNetwork(slaveTcpListener);

        foreach (byte slaveId in Enumerable.Range(1, 255))
        {
            network.AddSlave(factory.CreateSlave(slaveId));
        }

        Console.WriteLine("Modbus slave listening...");
        await network.ListenAsync();
    }
}
