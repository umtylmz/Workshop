using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        Socket socket;
        NetworkStream stream;
        TcpListener listener;

        private void Form1_Load(object sender, EventArgs e)
        {
            listener = new TcpListener(1453);
            listener.Start();

            socket = listener.AcceptSocket();
            stream = new NetworkStream(socket);

            Thread dinle = new Thread(SoketDinle);
            dinle.Start();
        }

        BinaryFormatter bf = new BinaryFormatter();

        void SoketDinle()
        {
            while (socket.Connected)
            {
                Mesaj alinan = (Mesaj)bf.Deserialize(stream);
            }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            Mesaj msg = new Mesaj();
            msg.Gonderen = "Server";
            msg.Gonderim = DateTime.Now;
            msg.Mesaji = txtMessage.Text;
            lstbxConversation.Items.Add(msg);
            bf.Serialize(stream,msg);
            stream.Flush();

            txtMessage.Clear();
            txtMessage.Focus();
        }
    }
}
