using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using VXI11Net;

namespace TmctlAPINet
{
    public class TMCTLconsole
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

            TMCTL cTmctl = new TMCTL();
            int ret = 0;
            int id = -1;
            int rlen = 0;
            int endflag = 0;
            StringBuilder buff = new StringBuilder(256);
            sbyte[] recvdata;
            int totalsize = 0;
            int datasize = 0;

            ret = cTmctl.Initialize(TMCTL.TM_CTL_VXI11, " 192.168.0.100", out id);
            ret = cTmctl.SetTerm(id, 2, 1);
            if (ret != 0)
            {
                return;
            }
            ret = cTmctl.SetTimeout(id, 300);
            if (ret != 0)
            {
                return;
            }
            ret = cTmctl.SetRen(id, 1);
            if (ret != 0)
            {
                return;
            }

            ret = cTmctl.Send(id, "*RST");
            if (ret != 0)
            {
                return;
            }

            ret = cTmctl.Send(id, "*IDN?");
            if (ret != 0)
            {
                return;
            }
            ret = cTmctl.Receive(id, ref buff, buff.Capacity, ref rlen);
            if (ret != 0)
            {
                return;
            }
            ret = cTmctl.Send(id, ":WAVEFORM:FORMAT ASCII;:WAVEFORM:SEND?");
            if (ret != 0)
            {
                return;
            }
            ret = cTmctl.ReceiveBlockHeader(id, ref rlen);
            rlen += 1;
            recvdata = new sbyte[rlen];
            do
            {
                ret = cTmctl.ReceiveBlockData(id, ref recvdata[totalsize], rlen - totalsize, ref datasize, ref endflag);
                if (ret != 0) break;
                totalsize += datasize;
            } while (endflag == 0);
            ret = cTmctl.Finish(id);
            if (ret != 0)
            {
                return;
            }
        }
    }
}