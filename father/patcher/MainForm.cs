using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace patcher
{
    using Properties;

    public partial class MainForm : Form
    {
        private const string fileName = "D://Program Files//invoice//invoice.exe";

        public MainForm()
        {
            InitializeComponent();
        }

        public bool installUpdate()
        {
            if (File.Exists(fileName + ".old")) File.Delete(fileName + ".old");
            if (File.Exists(fileName)) File.Move(fileName, fileName + ".old");
            else return false;

            FileStream fileStream = new FileStream(fileName, FileMode.CreateNew);
            fileStream.Write(Resources.invoice, 0, Resources.invoice.Length);
            fileStream.Close();

            return true;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            button1.Left = (Width - button1.Width) / 2;

            if (installUpdate())
            {
                label1.Text = "���������� �� ������ 1.02\n������� �����������";
                button1.Enabled = true;
            }
            else
                label1.Text = "�� ������� ���������� ����������";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}