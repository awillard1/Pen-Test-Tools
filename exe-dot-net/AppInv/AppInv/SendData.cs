using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

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
