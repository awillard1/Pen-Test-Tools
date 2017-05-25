using System.Net.Sockets;

namespace AppInv
{
    public static class SendData
    {
        public static void SendResults(SocketInformation s, object o)
        {
            byte[] data = null;
            using (Socket socket = new Socket(s))
            {
                try
                {
                    socket.Send(data);
                    socket.Close();
                }
                catch
                {
                    //not sure what we want to do here.
                }
            }
        }
    }
}
