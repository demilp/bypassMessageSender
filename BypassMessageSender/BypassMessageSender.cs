using Bypass;
using Bypass.SimpleJSON;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BypassMessageSender
{
    public partial class BypassMessageSender : Form
    {
        BypassClient client;
        public BypassMessageSender()
        {
            InitializeComponent();
            comboBoxType.SelectedIndex = 0;
            client = new BypassClient(ConfigurationManager.AppSettings["ip"], int.Parse(ConfigurationManager.AppSettings["port"]), ConfigurationManager.AppSettings["delimiter"], "messageSender", "tool");
            client.CommandReceivedEvent += Client_CommandReceivedEvent;
        }

        private void Client_CommandReceivedEvent(object sender, CommandEventArgs e)
        {
            Invoke
            (
                (MethodInvoker)delegate { textBoxReceived.Text = e.command; }
            );
            
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string[] ids = textBoxIds.Text.Split(',');
            switch (comboBoxType.SelectedIndex)
            {
                case 0:
                    client.SendData(textBoxData.Text, textBoxTag.Text, ids);
                    break;
                case 1:
                    client.Register(textBoxData.Text, textBoxTag.Text);
                    break;
                case 2:
                    client.Broadcast(textBoxData.Text);
                    break;
                case 3:
                    client.BroadcastAll(textBoxData.Text);
                    break;
                case 4:
                    client.SendCommand("{\"type\":\"needSender\", \"data\":\"" + textBoxData.Text + "\", \"tag\":\"\"}");
                    break;
                default:
                    client.SendData(textBoxData.Text, textBoxTag.Text, ids);
                    break;
            }
        }

        private void BypassMessageSender_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ids = textBoxIds.Text.Split(',');
            switch (comboBoxType.SelectedIndex)
            {
                case 0:
                    client.SendData(textBoxData2.Text, textBoxTag.Text, ids);
                    break;
                case 1:
                    client.Register(textBoxData2.Text, textBoxTag.Text);
                    break;
                case 2:
                    client.Broadcast(textBoxData2.Text);
                    break;
                case 3:
                    client.BroadcastAll(textBoxData2.Text);
                    break;
                case 4:
                    client.SendCommand("{\"type\":\"needSender\", \"data\":\"" + textBoxData2.Text + "\", \"tag\":\"\"}");
                    break;
                default:
                    client.SendData(textBoxData2.Text, textBoxTag.Text, ids);
                    break;
            }
        }
    }
}
