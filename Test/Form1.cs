﻿using DataIO;
using System;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        static DataIOSerial dataIO = new DataIOSerial();
        static IpAddress ip = new IpAddress();
        static MacAddress mac = new MacAddress();
        static DataIn dataIn = new DataIn();
        static DataOut dataOut = new DataOut();

        public Form1()
        {
            InitializeComponent();
            dataIO.DataChanged += DataIO_DataChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataIO.AddLink(ip);
            dataIO.AddLink(mac);
            dataIO.AddLink(dataIn);
            dataIO.AddLink(dataOut);

            dataIO.Port = serial;
            dataIO.Start();
        }

        private void DataIO_DataChanged(object sender, DataIOEventArgs e)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                tbIp.Text = ip.ToString();
                tbMac.Text = mac.ToString();
                tbTimer.Text = dataIn.Timer.ToString();
                tbLedStatus.Text = dataIn.LedStatus.ToString();
                tbBool.Text = dataIn.Bool.ToString();
                tbInt.Text = dataIn.Int.ToString();
                tbBlinkCount.Text = dataIn.BlinkCount.ToString();
                tbFloat.Text = dataIn.Float.ToString();
                tbDouble.Text = dataIn.Double.ToString();
            });
        }

        private void cbEnableBlink_CheckedChanged(object sender, EventArgs e)
        {
            dataOut.EnableBlink = ((CheckBox)sender).Checked;
            dataIO.DataOutTask();
        }

        private void cbManualCmd_CheckedChanged(object sender, EventArgs e)
        {
            dataOut.ManualCmd = ((CheckBox)sender).Checked;
            dataIO.DataOutTask();
        }

        private void tbBlinkTimer_TextChanged(object sender, EventArgs e)
        {
            int value = 1000;
            int.TryParse(((TextBox)sender).Text, out value);
            dataOut.BlinkTimer = value;
            dataIO.DataOutTask();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataIO.Stop();
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog()
            {
                Filter = "DataIO Logger File | *.bin",
                Multiselect = true
            };
            if(diag.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
