using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sir
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;

        public static bool txtStatus_TextChanged(Server @object)
        {
            throw new NotImplementedException();
        }

        private void txtSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.RestoreDirectory = true;
            saveFile.Title = "Save text Files";
            saveFile.InitialDirectory = "c:\\Documents";
            saveFile.FileName = string.Format("{0}.txt", ".txt");
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "txt files (*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile.FileName))
                    sw.WriteLine(txtStatus.Text);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
                server.Stop();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtStatus.Text += "Server starting...  ";
            System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
            server.Start(ip, Convert.ToInt32(txtPort.Text));
        }

        private void Server_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataReceived;

            timer1.Start();
            Date.Text = DateTime.Now.ToLongDateString();
            Time.Text = DateTime.Now.ToLongTimeString();
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.Text += e.MessageString + "\r\n ";
                e.ReplyLine(string.Format(e.MessageString));
            });
        }

        public void txtStatus_TextChanged(object sender, EventArgs e)
        {

        }

        public void txtStatus_TextChanged()
        {
            throw new NotImplementedException();
        }
    }
}
