using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using VXI11Net;

namespace Ivi.Visa.Interop
{
    public class VISA32console
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select VISA Function");
            Console.WriteLine("   1:viOpenDefaultRM");
            Console.WriteLine("   2:viOpen");
            Console.WriteLine("   3:viClose");
            Console.WriteLine("   4:viWrite");
            Console.WriteLine("   5:viRead");
            Console.WriteLine("   6:viAssertTrigger");
            Console.WriteLine("   7:viReadSTB");
            Console.WriteLine("   8:viClear");
            Console.WriteLine("   9:viGpibControlREN");
            Console.WriteLine("   Q:Exit program");
            Console.WriteLine("EXIT:Exit program");

            Console.ReadLine();

            Console.WriteLine("==viOpenDefaultRM==");
            Console.WriteLine(" Call : viOpenDefaultRM : sesn=0x555E4308h ret=VI_SUCCESS(0x0) (634 ms)");

            Console.ReadLine();

            Console.WriteLine(" Session Handle(Hex 0:555E7770h,q) : 0");
            Console.WriteLine(" name (String, N:null) : USB0::0x0B21::0x0030::393154383238353530::INSTR");
            Console.WriteLine(" mode (0:VI_NULL, 1:VI_EXCLUSIVE_LOCK, 4:VI_LOAD_CONFIG, q) : 0");
            Console.WriteLine(" timeout (0-65536, q) : 0");
            Console.WriteLine("==viOpen==");
            Console.WriteLine(" Call : viOpen : vi=0x555E8948 ret=VI_SUCCESS(0x0) (0 ms)");

            Console.ReadLine();

            Console.WriteLine(" Device Handle(Hex,0:555E4768h,1:555E8948h,q) : 1");
            Console.WriteLine("==viClose==");
            Console.WriteLine(" Call : viClose : ret=VI_SUCCESS(0x0) (0 ms)");

            Console.ReadLine();

            Console.WriteLine(" Device Handle(Hex,0:555E4768h,q) : 0");
            Console.WriteLine(" msg (String, N:null) : *IDN?");
            Console.WriteLine("==viWrite==");
            Console.WriteLine(" Call : viWrite : retCnt=5 ret=VI_SUCCESS(0x0) (0 ms)");

            Console.ReadLine();

            Console.WriteLine(" Device Handle(Hex,0:55545780h,q) : 0");
            Console.WriteLine(" Read Size(0-2147483647,q) : 1000");
            Console.WriteLine("==viRead==");
            Console.WriteLine(" Call : viRead : retCnt=32 ret=VI_SUCCESS(0x0) (0 ms)");
            Console.WriteLine(" Response");
            Console.WriteLine("        Message=YOKOGAWA,710130,91T828550,F5.10");
            Console.WriteLine("           +0 +1 +2 +3 +4 +5 +6 +7 +8 +9 +A +B +C +D +E +F");
            Console.WriteLine("55122A20 : 59 4F 4B 4F 47 41 57 41 2C 37 31 30 31 33 30 2C  YOKOGAWA,710130,");
            Console.WriteLine("55122A30 : 39 31 54 38 32 38 35 35 30 2C 46 35 2E 31 30 0A  91T828550,F5.10?");
            Console.WriteLine("55122A40 :");

            Console.ReadLine();

            Console.WriteLine(" Device Handle(Hex,0:555E4768h,q) : 0");
            Console.WriteLine(" protocol (0:VI_TRIG_PROT_DEFAULT, q) : 0");
            Console.WriteLine("==viAssertTrigger==");
            Console.WriteLine(" Call : viAssertTrigger : ret=VI_SUCCESS(0x0) (0 ms)");

            Console.ReadLine();

            Console.WriteLine(" Device Handle(Hex,0:555E4768h,q) : 0");
            Console.WriteLine("==viReadSTB==");
            Console.WriteLine(" Call : viReadSTB : ret=VI_SUCCESS(0x0) (11 ms)");
            Console.WriteLine(" Response");
            Console.WriteLine("        status=68(0x44)");

            Console.ReadLine();

            Console.WriteLine(" Device Handle(Hex,0:555E4768h,q) : 0");
            Console.WriteLine("==viClear==");
            Console.WriteLine(" Call : viClear : ret=VI_SUCCESS(0x0) (1 ms)");

            Console.ReadLine();

            Console.WriteLine(" Device Handle(Hex,0:555E4768h,q) : 0");
            Console.WriteLine(" mode (0:VI_GPIB_REN_DEASSERT,");
            Console.WriteLine("       1:VI_GPIB_REN_ASSERT,");
            Console.WriteLine("       2:VI_GPIB_REN_DEASSERT_GTL,");
            Console.WriteLine("       3:VI_GPIB_REN_ASSERT_ADDRESS,");
            Console.WriteLine("       4:VI_GPIB_REN_ASSERT_LLO,");
            Console.WriteLine("       5:VI_GPIB_REN_ASSERT_ADDRESS_LLO,");
            Console.WriteLine("       6:VI_GPIB_REN_ADDRESS_GTL,, q) : 1");
            Console.WriteLine("==viGpibControlREN==");
            Console.WriteLine(" Call : viGpibControlREN : ret=VI_SUCCESS(0x0) (0 ms)");

            ResourceManager rm = new ResourceManager();
            FormattedIO488 inst = new FormattedIO488();

            string deviceResource = "TCPIP::XXX.XXX.XXX.XXX::INSTR";

            inst.IO = (IMessage)rm.Open(deviceResource, AccessMode.NO_LOCK, 0, "");
            inst.IO.Timeout = 10000;

            inst.WriteString("*IDN?");
            string returnStr = inst.ReadString();
        }
    }
}