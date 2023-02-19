using System;
using VXI11Net;

namespace Ivi.Visa.Interop{

    public enum AccessMode
    {
        NO_LOCK
    }
    public class IMessage
    {
        public int Timeout { get; set; }
    }
    public class ResourceManager
    {
        public IMessage Open(string str, AccessMode mode, int a, string b) {
            return new IMessage();
        }
    }
    public class FormattedIO488
    {
        public IMessage IO = new IMessage();
        public void WriteString(string str){}
        public string ReadString(){
            return "";
        }

        public static void Main(string[] args)
        {
            ResourceManager rm = new ResourceManager();  // リソース
            FormattedIO488 inst = new FormattedIO488(); // 機器と通信をしてくれるオブジェクト

            // チェックボックスでLAN接続か、USB接続か選び、リソース文を作成する
            string deviceResource = "TCPIP::XXX.XXX.XXX.XXX::INSTR";

            inst.IO = (IMessage)rm.Open(deviceResource, AccessMode.NO_LOCK, 0, ""); // 機器と接続
            inst.IO.Timeout = 10000;

            // 測定器のIDを取得する
            inst.WriteString("*IDN?");
            string returnStr = inst.ReadString(); // 応答を文字列で取得する
        }
    }
}