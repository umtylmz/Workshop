using Server;
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

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        TcpClient client;
        NetworkStream stream;

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client = new TcpClient(txtConnect.Text,1453);
            stream = client.GetStream();
            Thread dinleyici = new Thread(BaglantiDinle);
            dinleyici.Start();
        }

        BinaryFormatter bf = new BinaryFormatter();

        void BaglantiDinle()
        {
            while (true)
            {
                Mesaj mesaj = (Mesaj)bf.Deserialize(stream);
                lstConservation.Items.Add(mesaj);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Mesaj msg = new Mesaj();
            msg.Gonderen = txtUser.Text;
            msg.Gonderim = DateTime.Now;
            msg.Mesaji = txtSend.Text;
            lstConservation.Items.Add(msg);

            bf.Serialize(stream,msg);
            stream.Flush();
            txtSend.Clear();
            txtSend.Focus();
        }
    }
}
