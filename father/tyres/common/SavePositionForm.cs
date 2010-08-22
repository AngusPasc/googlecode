using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;

namespace Stopiccot
{
    //================================================================================
    // FormPostion
    //    ���������, ����������� ��������� �����
    //================================================================================
    public struct FormPosition
    {
        public bool Maximized; // ��������� �� ���� �� ���� �����
        public Size Size;      // ������� �����
        public Point Location; // ������� �����

        public FormPosition(int Left, int Top, int Width, int Height, bool Max)
        {
            this.Location = new Point(Left, Top);
            this.Size = new Size(Width, Height);
            this.Maximized = Max;
        }
    }

    //================================================================================
    // SavePositionForm
    //    ����� ������������ ���� �������
    //================================================================================
    public class SavePositionForm : Form
    {
        private FormPosition position;
        private bool saveToRegistry = false;
        private static string RegPath = "Software\\SP\\" + System.Reflection.Assembly.GetExecutingAssembly().FullName.Split(',')[0];

        public Boolean SavePositionToRegistry
        {
            get { return saveToRegistry; }
            set { saveToRegistry = value; }
        }

        public FormPosition Position
        {
            get { return position; }
            set 
            {
                Location = value.Location;
                WindowState = value.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
                // ���� ������� ���� ������ ��������, �� ����� ������ �������� �� �������
                // �.�. �� ������ � ������ ������� ���� ����� ��������
                if (this.FormBorderStyle != FormBorderStyle.FixedDialog)
                    Size = value.Size;

                position = value;
            }
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            position.Location = Location;
            position.Size = Size;
        }

        protected override void OnMove(EventArgs e)
        {
 	        base.OnMove(e);
            position.Maximized = WindowState == FormWindowState.Maximized;
        }

        //================================================================================
        // OnLoad
        //    ���� ������� ���������� ������� ����� � �������, �� ��������� �� ����
        //================================================================================
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (saveToRegistry)
            {
                try
                {
                    string value = (string)Registry.LocalMachine.OpenSubKey(RegPath).GetValue("SavePositionForm " + this.Name);
                    string[] s = value.Split(' ');
                    this.Position = new FormPosition(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), Convert.ToInt32(s[2]), Convert.ToInt32(s[3]), Convert.ToBoolean(s[4]));
                }
                catch
                { 
                    // ���� �� ������� �� � ���� � ���. ������� ����� � ��������� �����
                }
            }
        }

        //================================================================================
        // OnClosing
        //    ��������� ������� ����� � ������ ���� ����
        //================================================================================
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            
            if (saveToRegistry)
            {
                Registry.LocalMachine.CreateSubKey(RegPath).SetValue("SavePositionForm " + this.Name,
                    position.Location.X.ToString() + ' ' +
                    position.Location.Y.ToString() + ' ' +
                    position.Size.Width.ToString() + ' ' +
                    position.Size.Height.ToString() + ' ' +
                    position.Maximized.ToString());
            }
        }
    }    
}
