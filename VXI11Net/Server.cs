﻿using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace VXI11Net
{
    public class Server
    {
        // RPC define
        public const int CALL                = 0;
        public const int REPLY               = 1;
        public const int MSG_ACCEPTED        = 0;
        public const int SUCCESS             = 0;
        public const int CREATE_LINK         = 10;
        public const int DEVICE_WRITE        = 11;
        public const int DEVICE_READ         = 12;
        public const int DEVICE_READSTB      = 13;
        public const int DEVICE_TRIGGER      = 14;
        public const int DEVICE_CLEAR        = 15;
        public const int DEVICE_REMOTE       = 16;
        public const int DEVICE_LOCAL        = 17;
        public const int DEVICE_LOCK         = 18;
        public const int DEVICE_UNLOCK       = 19;
        public const int DEVICE_ENABLE_SRQ   = 20;
        public const int DEVICE_DOCMD        = 22;
        public const int DESTROY_LINK        = 23;
        public const int CREATE_INTR_CHAN    = 25;
        public const int DESTROY_INTR_CHAN   = 26;
        public const int DEVICE_ABORT        = 1;
        public const int DEVICE_INTR         = 395185;
        public const int DEVICE_INTR_VERSION = 1;
        public const int DEVICE_INTR_SRQ     = 30;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RPC_MESSAGE_PARAMS
        {
            public int fheader;
            public int xid;
            public int mtype;
            public int rpcvers;
            public int prog;
            public int vers;
            public int proc;
            public int cred_flavor;
            public int cred_len;
            public int verf_flavor;
            public int verf_len;
        };
        public struct CREATE_LINK_PARAMS
        {
            public int clientId;
            public int lockDevice;
            public int lock_timeout;
            public int handle_len;
        };
        public struct CREATE_LINK_REPLY
        {
            public int fheader;
            public int xid;
            public int mtype;
            public int stat;
            public int verf_flavor;
            public int verf_len;
            public int accept_stat;
            public int error;
            public int lid;
            public int abortPort;
            public int maxRecvSize;
        };
        public struct DEVICE_WRITE_PARAMS
        {
            public int lid;
            public int flags;
            public int lock_timeout;
            public int io_timeout;
            public int data_len;
        };
        public struct DEVICE_WRITE_REPLY
        {
            public int fheader;
            public int xid;
            public int mtype;
            public int stat;
            public int verf_flavor;
            public int verf_len;
            public int accept_stat;
            public int error;
            public int data_len;
        };
        public struct DEVICE_READ_PARAMS
        {
            public int lid;
            public int requestSize;
            public int io_timeout;
            public int lock_timeout;
            public int flags;
            public int termChar;
        };
        public struct DEVICE_READ_REPLY
        {
            public int fheader;
            public int xid;
            public int mtype;
            public int stat;
            public int verf_flavor;
            public int verf_len;
            public int accept_stat;
            public int error;
            public int reason;
            public int data_len;
        };
        public struct DEVICE_GENERIC_PARAMS
        {
            public int lid;
            public int flags;
            public int lock_timeout;
            public int io_timeout;
        };
        public struct DEVICE_GENERIC_REPLY
        {
            public int fheader;
            public int xid;
            public int mtype;
            public int stat;
            public int verf_flavor;
            public int verf_len;
            public int accept_stat;
            public int error;
        };
        public struct DEVICE_LOCK_PARAMS
        {
            public int lid;
            public int flags;
            public int lock_timeout;
        };
        public struct CREATE_INTR_CHAN_PARAMS
        {
            public int hostaddr;
            public int hostport;
            public int prognum;
            public int progvers;
            public int progfamily;
        };
        public struct DEVICE_ENABLE_SRQ_PARAMS
        {
            public int lid;
            public int enable;
            public int handle_len;
        };
        public struct DEVICE_READSTB_REPLY
        {
            public int fheader;
            public int xid;
            public int mtype;
            public int stat;
            public int verf_flavor;
            public int verf_len;
            public int accept_stat;
            public int error;
            public byte stb;
        };
        public struct DEVICE_DOCMD_PARAMS
        {
            public int lid;
            public int flags;
            public int io_timeout;
            public int lock_timeout;
            public int cmd;
            public int network_order;
            public int datasize;
            public int data_in_len;
        };
        public struct DEVICE_DOCMD_REPLY
        {
            public int fheader;
            public int xid;
            public int mtype;
            public int stat;
            public int verf_flavor;
            public int verf_len;
            public int accept_stat;
            public int error;
            public int data_out_len;
        };
        // reply create_link
        public static RPC_MESSAGE_PARAMS receive_message(Socket so, out int size)
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(RPC_MESSAGE_PARAMS))];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            RPC_MESSAGE_PARAMS msg = new RPC_MESSAGE_PARAMS();
            msg.fheader     = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            msg.xid         = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            msg.mtype       = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            msg.rpcvers     = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 12));
            msg.prog        = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 16));
            msg.vers        = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 20));
            msg.proc        = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 24));
            msg.cred_flavor = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 28));
            msg.cred_len    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 32));
            msg.verf_flavor = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 36));
            msg.verf_len    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 40));

            size = msg.fheader - Marshal.SizeOf(typeof(RPC_MESSAGE_PARAMS)) + 4;
            if (size <= -1)
            {
                size = size + int.MaxValue + 1;
            }
            return msg;
        }
        // reply create_link
        public static CREATE_LINK_PARAMS receive_create_link(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            CREATE_LINK_PARAMS args = new CREATE_LINK_PARAMS();
            args.clientId      = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            args.lockDevice    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            args.lock_timeout  = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            args.handle_len    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 12));
            return args;
        }
        public static void reply_create_link(Socket so, RPC_MESSAGE_PARAMS msg, int lid, int abortPort, int maxRecvSize)
        {
            CREATE_LINK_REPLY reply = new CREATE_LINK_REPLY();
            int size = Marshal.SizeOf(typeof(CREATE_LINK_REPLY));
            reply.fheader      = IPAddress.HostToNetworkOrder(size - 4 + int.MinValue);
            reply.xid          = IPAddress.HostToNetworkOrder(msg.xid);
            reply.mtype        = IPAddress.HostToNetworkOrder(REPLY);
            reply.stat         = IPAddress.HostToNetworkOrder(MSG_ACCEPTED);
            reply.verf_flavor  = IPAddress.HostToNetworkOrder(0);
            reply.verf_len     = IPAddress.HostToNetworkOrder(0);
            reply.accept_stat  = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.error        = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.lid          = IPAddress.HostToNetworkOrder(lid);
            reply.abortPort    = IPAddress.HostToNetworkOrder(abortPort);
            reply.maxRecvSize  = IPAddress.HostToNetworkOrder(maxRecvSize);

            byte[] packet = new byte[Marshal.SizeOf(typeof(CREATE_LINK_REPLY))];
            GCHandle gchw = GCHandle.Alloc(packet, GCHandleType.Pinned);
            Marshal.StructureToPtr(reply, gchw.AddrOfPinnedObject(), false);
            int byteCount = so.Send(packet);
            gchw.Free();
        }
        // reply device_write
        public static DEVICE_WRITE_PARAMS receive_device_write(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            DEVICE_WRITE_PARAMS args = new DEVICE_WRITE_PARAMS();
            args.lid           = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            args.flags         = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            args.lock_timeout  = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            args.io_timeout    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 12));
            args.data_len      = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 16));
            return args;
        }
        public static void reply_device_write(Socket so, RPC_MESSAGE_PARAMS msg, int data_len)
        {
            DEVICE_WRITE_REPLY reply = new DEVICE_WRITE_REPLY();
            int size = Marshal.SizeOf(typeof(DEVICE_WRITE_REPLY));
            reply.fheader      = IPAddress.HostToNetworkOrder(size - 4 + int.MinValue);
            reply.xid          = IPAddress.HostToNetworkOrder(msg.xid);
            reply.mtype        = IPAddress.HostToNetworkOrder(REPLY);
            reply.stat         = IPAddress.HostToNetworkOrder(MSG_ACCEPTED);
            reply.verf_flavor  = IPAddress.HostToNetworkOrder(0);
            reply.verf_len     = IPAddress.HostToNetworkOrder(0);
            reply.accept_stat  = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.error        = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.data_len     = IPAddress.HostToNetworkOrder(data_len);

            byte[] packet = new byte[Marshal.SizeOf(typeof(DEVICE_WRITE_REPLY))];
            GCHandle gchw = GCHandle.Alloc(packet, GCHandleType.Pinned);
            Marshal.StructureToPtr(reply, gchw.AddrOfPinnedObject(), false);
            int byteCount = so.Send(packet);
            gchw.Free();
        }
        // reply device_read
        public static DEVICE_READ_PARAMS receive_device_read(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            DEVICE_READ_PARAMS args = new DEVICE_READ_PARAMS();
            args.lid           = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            args.requestSize   = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            args.io_timeout    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            args.lock_timeout  = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 12));
            args.flags         = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 16));
            args.termChar      = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 20));
            return args;
        }
        public static void reply_device_read(Socket so, RPC_MESSAGE_PARAMS msg, int reason, string str)
        {
            DEVICE_READ_REPLY reply = new DEVICE_READ_REPLY();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
            int size = Marshal.SizeOf(typeof(DEVICE_READ_REPLY)) + data.Length;
            size = ((size / 4) + 1) * 4;
            reply.fheader      = IPAddress.HostToNetworkOrder(size - 4 + int.MinValue);
            reply.xid          = IPAddress.HostToNetworkOrder(msg.xid);
            reply.mtype        = IPAddress.HostToNetworkOrder(REPLY);
            reply.stat         = IPAddress.HostToNetworkOrder(MSG_ACCEPTED);
            reply.verf_flavor  = IPAddress.HostToNetworkOrder(0);
            reply.verf_len     = IPAddress.HostToNetworkOrder(0);
            reply.accept_stat  = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.error        = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.reason       = IPAddress.HostToNetworkOrder(reason);
            reply.data_len     = IPAddress.HostToNetworkOrder(data.Length);

            byte[] packet = new byte[size];
            GCHandle gchw = GCHandle.Alloc(packet, GCHandleType.Pinned);
            Marshal.StructureToPtr(reply, gchw.AddrOfPinnedObject(), false);
            Buffer.BlockCopy(data, 0, packet, 40, str.Length);
            gchw.Free();
            int byteCount = so.Send(packet);
        }
        // reply device_readstb
        // reply device_trigger
        // reply device_clear
        // reply device_remote
        // reply device_local
        // reply device_abort
        public static DEVICE_GENERIC_PARAMS receive_generic_params(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            DEVICE_GENERIC_PARAMS args = new DEVICE_GENERIC_PARAMS();
            args.lid           = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            args.flags         = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            args.lock_timeout  = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            args.io_timeout    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 12));
            return args;
        }
        public static void reply_device_readstb(Socket so, RPC_MESSAGE_PARAMS msg, byte stb)
        {
            DEVICE_READSTB_REPLY reply = new DEVICE_READSTB_REPLY();
            int size = Marshal.SizeOf(typeof(DEVICE_READSTB_REPLY));
            reply.fheader      = IPAddress.HostToNetworkOrder(size - 4 + int.MinValue);
            reply.xid          = IPAddress.HostToNetworkOrder(msg.xid);
            reply.mtype        = IPAddress.HostToNetworkOrder(REPLY);
            reply.stat         = IPAddress.HostToNetworkOrder(MSG_ACCEPTED);
            reply.verf_flavor  = IPAddress.HostToNetworkOrder(0);
            reply.verf_len     = IPAddress.HostToNetworkOrder(0);
            reply.accept_stat  = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.error        = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.stb          = stb;

            byte[] packet = new byte[Marshal.SizeOf(typeof(DEVICE_READSTB_REPLY))];
            GCHandle gchw = GCHandle.Alloc(packet, GCHandleType.Pinned);
            Marshal.StructureToPtr(reply, gchw.AddrOfPinnedObject(), false);
            gchw.Free();
            int byteCount = so.Send(packet);
        }
        // reply device_lock
        public static DEVICE_LOCK_PARAMS receive_device_lock(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            DEVICE_LOCK_PARAMS args = new DEVICE_LOCK_PARAMS();
            args.lid           = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            args.flags         = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            args.lock_timeout  = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            return args;
        }
        // reply device_unlock
        public static int receive_device_link(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);
            int lid           = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            return lid;
        }
        // reply create_intr_chan
        public static CREATE_INTR_CHAN_PARAMS receive_create_intr_chan(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            CREATE_INTR_CHAN_PARAMS args = new CREATE_INTR_CHAN_PARAMS();
            args.hostaddr      = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            args.hostport      = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            args.prognum       = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            args.progvers      = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 12));
            args.progfamily    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 16));
            return args;
        }
        // reply device_enable_srq
        public static DEVICE_ENABLE_SRQ_PARAMS receive_device_enable_srq(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            DEVICE_ENABLE_SRQ_PARAMS args = new DEVICE_ENABLE_SRQ_PARAMS();
            args.lid           = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            args.enable        = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            args.handle_len    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            return args;
        }
        // reply device_docmd
        public static DEVICE_DOCMD_PARAMS receive_device_docmd(Socket so, RPC_MESSAGE_PARAMS msg, int size)
        {
            byte[] buffer = new byte[size];
            int byteCount = so.Receive(buffer, SocketFlags.None);

            DEVICE_DOCMD_PARAMS args = new DEVICE_DOCMD_PARAMS();
            args.lid           = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 0));
            args.flags         = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 4));
            args.lock_timeout  = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 8));
            args.io_timeout    = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 12));
            args.cmd           = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 16));
            args.network_order = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 20));
            args.datasize      = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 24));
            args.data_in_len   = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer, 28));
            return args;
        }
        public static void reply_device_docmd(Socket so, RPC_MESSAGE_PARAMS msg, int data_out_len)
        {
            DEVICE_DOCMD_REPLY reply = new DEVICE_DOCMD_REPLY();
            int size = Marshal.SizeOf(typeof(DEVICE_DOCMD_REPLY));
            reply.fheader      = IPAddress.HostToNetworkOrder(size - 4 + int.MinValue);
            reply.xid          = IPAddress.HostToNetworkOrder(msg.xid);
            reply.mtype        = IPAddress.HostToNetworkOrder(REPLY);
            reply.stat         = IPAddress.HostToNetworkOrder(MSG_ACCEPTED);
            reply.verf_flavor  = IPAddress.HostToNetworkOrder(0);
            reply.verf_len     = IPAddress.HostToNetworkOrder(0);
            reply.accept_stat  = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.error        = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.data_out_len = IPAddress.HostToNetworkOrder(data_out_len);

            byte[] packet = new byte[Marshal.SizeOf(typeof(DEVICE_DOCMD_REPLY))];
            GCHandle gchw = GCHandle.Alloc(packet, GCHandleType.Pinned);
            Marshal.StructureToPtr(reply, gchw.AddrOfPinnedObject(), false);
            int byteCount = so.Send(packet);
            gchw.Free();
        }
        // reply destroy_link
        public static void reply_device_error(Socket so, RPC_MESSAGE_PARAMS msg, int error)
        {
            DEVICE_GENERIC_REPLY reply = new DEVICE_GENERIC_REPLY();
            int size = Marshal.SizeOf(typeof(DEVICE_GENERIC_REPLY));
            reply.fheader      = IPAddress.HostToNetworkOrder(size - 4 + int.MinValue);
            reply.xid          = IPAddress.HostToNetworkOrder(msg.xid);
            reply.mtype        = IPAddress.HostToNetworkOrder(REPLY);
            reply.stat         = IPAddress.HostToNetworkOrder(MSG_ACCEPTED);
            reply.verf_flavor  = IPAddress.HostToNetworkOrder(0);
            reply.verf_len     = IPAddress.HostToNetworkOrder(0);
            reply.accept_stat  = IPAddress.HostToNetworkOrder(SUCCESS);
            reply.error        = IPAddress.HostToNetworkOrder(error);

            byte[] packet = new byte[Marshal.SizeOf(typeof(DEVICE_GENERIC_REPLY))];
            GCHandle gchw = GCHandle.Alloc(packet, GCHandleType.Pinned);
            Marshal.StructureToPtr(reply, gchw.AddrOfPinnedObject(), false);
            int byteCount = so.Send(packet);
            gchw.Free();
        }
        public static void clear_buffer(Socket so)
        {
            byte[] buffer = new byte[1000];
            int byteCount = 1;
            while (1 <= byteCount)
            {
                byteCount = so.Receive(buffer, SocketFlags.None);
            }
        }
    }
    public class DemoServer
    {

        Task? t;
        string address = "";
        int port;
        bool isEnable = false;
        public void Start(string address, int port)
        {
            this.address = address;
            this.port = port;
            this.isEnable = true;
            this.t = Task.Factory.StartNew( () => { ThreadProc(); });
        }
        public void Stop()
        {
            this.isEnable = false;
        }
        public void ThreadProc()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(this.address);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new(ipAddress, this.port);
            Socket listener = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ipEndPoint);
            listener.Listen(1);
            Socket socket = listener.Accept();
            socket.NoDelay = true;

            while (isEnable)
            {
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
        public static void Main(string[] args)
        {
            DemoServer server = new DemoServer();
            server.Start("127.0.0.1", 10240);
            Thread.Sleep(1000*10);
            server.Stop();
        }
    }
}