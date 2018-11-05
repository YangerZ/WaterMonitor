using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;

namespace AdminLTE
{
    public class Util
    {
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
    class SynchronousSocketClient
    {
         
        public static string  StartClient(string cmd)
        {
            byte[] bytes = new byte[1024];
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ip, 7070);

                Socket sender = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    sender.Connect(remoteEP);
                  
                   string endpoint=sender.RemoteEndPoint.ToString();
                    byte[] msg = Encoding.ASCII.GetBytes(cmd+ "");
                    int bytesSent = sender.Send(msg);
                   //发送完成后接收到返回数据
                    int bytesRec = sender.Receive(bytes);
                   string result=Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //直接关闭本次连接
                   sender.Shutdown(SocketShutdown.Both);
                   sender.Close();
                    return result;
                }
                catch (ArgumentNullException ane)
                {
                    return "-1" ;//Console.WriteLine("ArgumentNullException:{0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    return "-1"; //Console.WriteLine("SocketException:{0}", se.ToString());
                }
                catch (Exception e)
                {
                    return "-1";//Console.WriteLine("Unexpected exception:{0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                return "-1"; //Console.WriteLine(e.ToString());
            }
        }

    }
}