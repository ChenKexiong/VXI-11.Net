using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using VXI11Net;

namespace TmctlAPINet
{
    public class Serverconsole
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select Tmctl Function");
            Console.WriteLine("   1:TmcInitialize");
            Console.WriteLine("   2:TmcFinish");
            Console.WriteLine("   3:TmcSend");
            Console.WriteLine("   4:TmcReceive");
            Console.WriteLine("   5:TmcSetRen");
            Console.WriteLine("   6:TmcDeviceClear");
            Console.WriteLine("   7:TmcDeviceTrigger");
            Console.WriteLine("   Q:Exit program");
            Console.WriteLine("EXIT:Exit program");

            Console.ReadLine();

            Console.WriteLine(" wire (1:GPIB, 2:RS232, 3:USB, 4:Ethernet, 5:USBTMC(DL9000), 6:EthernetUDP,");
            Console.WriteLine("       7:USBTMC2, 8:VXI-11, 9:USB2, 10:VISA USB, 11:Socket, q) : ");
            Console.WriteLine(" address (String(GPIB address), N:null) : ");
            Console.WriteLine(" id pointer (0:DefaultIdPtr, other:NULL) : ");
            Console.WriteLine("==TmcInitialize==");
            Console.WriteLine(" Call : TmcInitialize : ret=SUCCESS(0),  id=0 (77 ms)");

            Console.ReadLine();

            Console.WriteLine(" id (0-127, q) : ");
            Console.WriteLine("==TmcDeviceClear==");
            Console.WriteLine(" Call : TmcDeviceClear : ret=SUCCESS(0) (31 ms)");

            Console.ReadLine();

            Console.WriteLine(" id (0-127, q) : 0");
            Console.WriteLine(" msg (String, N:null) : *CLS;*SRE 255;*IDN?");
            Console.WriteLine("==TmcSend==");
            Console.WriteLine(" Call : TmcSend : ret=SUCCESS(0) (2 ms)");

            Console.ReadLine();

            Console.WriteLine(" id (0-127, q) : 0");
            Console.WriteLine("==TmcFinish==");
            Console.WriteLine(" Call : TmcFinish : ret=SUCCESS(0) (0 ms)");

            string address = "127.0.0.1";
            IPHostEntry ipHostInfo = Dns.GetHostEntry(address);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            int port = 10240;
            IPEndPoint ipEndPoint = new(ipAddress, port);
            Socket listener = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ipEndPoint);
            listener.Listen(1);
            Socket socket = listener.Accept();
            socket.NoDelay = true;

	        int size;
	        Server.RPC_MESSAGE_PARAMS msg = Server.receive_message(socket, out size);
	        if (msg.proc == Server.CREATE_LINK)
	        {
	            Server.receive_create_link(socket, msg, size);
	            Server.reply_create_link(socket, msg, 123, 456, 789);
	        }
	        else if (msg.proc == Server.DEVICE_WRITE)
	        {
	            Server.DEVICE_WRITE_PARAMS wrt = Server.receive_device_write(socket, msg, size);
	            Server.reply_device_write(socket, msg, wrt.data_len);
	        }
	        else if (msg.proc == Server.DEVICE_READ)
	        {
	            Server.receive_device_read(socket, msg, size);
	            Server.reply_device_read(socket, msg, 1, "YOKOGAWA,DLM3000,1.00,dummy");
	        }
	        else if (msg.proc == Server.DEVICE_READSTB)
	        {
	            Server.DEVICE_GENERIC_PARAMS gen = Server.receive_generic_params(socket, msg, size);
	            Server.reply_device_readstb(socket, msg, 8);
	        }
	        else if (msg.proc == Server.DEVICE_TRIGGER)
	        {
	            Server.DEVICE_GENERIC_PARAMS gen = Server.receive_generic_params(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DEVICE_CLEAR)
	        {
	            Server.DEVICE_GENERIC_PARAMS gen = Server.receive_generic_params(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DEVICE_REMOTE)
	        {
	            Server.DEVICE_GENERIC_PARAMS gen = Server.receive_generic_params(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DEVICE_LOCAL)
	        {
	            Server.DEVICE_GENERIC_PARAMS gen = Server.receive_generic_params(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DEVICE_LOCK)
	        {
	            Server.receive_device_lock(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DEVICE_UNLOCK)
	        {
	            int link = Server.receive_device_link(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DEVICE_ENABLE_SRQ)
	        {
	            Server.receive_device_enable_srq(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DEVICE_DOCMD)
	        {
	            Server.DEVICE_DOCMD_PARAMS dcm = Server.receive_device_docmd(socket, msg, size);
	            Server.reply_device_docmd(socket, msg, dcm.data_in_len);
	        }
	        else if (msg.proc == Server.DESTROY_LINK)
	        {
	            int link = Server.receive_device_link(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.CREATE_INTR_CHAN)
	        {
	            Server.receive_create_intr_chan(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DESTROY_INTR_CHAN)
	        {
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else if (msg.proc == Server.DEVICE_ABORT)
	        {
	            int link = Server.receive_device_link(socket, msg, size);
	            Server.reply_device_error(socket, msg, Server.SUCCESS);
	        }
	        else
	        {
	            Server.clear_buffer(socket);
	        }
        }
    }
}