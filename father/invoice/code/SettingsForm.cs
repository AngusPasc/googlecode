using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Invoice
{
    public partial class SettingsForm : Form
    {
        public static int beginYear = 2008; // ������ ���� � Base.workingDirectory ������������� 2008 ����
        private int prevYear, currYear;

        //================================================================================
        // SettingsForm()
        //================================================================================
        public SettingsForm()
        {
            InitializeComponent();
        }

        //================================================================================
        // SettingsForm_Shown
        //    ������������� ���������� ��� �������� �����
        //================================================================================
        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            // � ������� ��� �������� ��������������� �� ��������� ���
            currYear = dateTime.Month > 11 ? dateTime.Year + 1 : dateTime.Year;
            prevYear = currYear - 1;

            // ���������� ����� ��� ������
            prevYearDirLabel.Text = "����� ��� ����-������ " + prevYear.ToString() + " ����";
            currYearDirLabel.Text = "����� ��� ����-������ " + currYear.ToString() + " ����";

            // �������� ������ �� ����
            templatePath.Text      = Base.templateDoc;
            prices.Lines           = Base.prices;
            deleteCheckBox.Checked = Base.confirmDelete;
            unpayCheckBox.Checked  = Base.confirmUnpay;
            prevYearDirPath.Text   = Base.workingDirectory[prevYear - beginYear];
            currYearDirPath.Text   = Base.workingDirectory[currYear - beginYear];
        }

        //================================================================================
        // editCompaniesButton_Click
        //    ���������� ����� �������������� ��������
        //================================================================================
        private void editCompaniesButton_Click(object sender, EventArgs e)
        {
            (new EditCompanyForm()).ShowSettingsDialog(this.Left + this.Width + 10, this.Top);
        }

        //================================================================================
        // changeTemplate_Click
        //    ����� �����-�������
        //================================================================================
        private void changeTemplate_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "�������� Word|*.doc";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                templatePath.Text = openFileDialog.FileName;
        }

        //================================================================================
        // changePrevYearDir_Click
        //    ����� ����� ��� prevYear
        //================================================================================
        private void changePrevYearDir_Click(object sender, EventArgs e)
        {
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
                prevYearDirPath.Text = selectFolderDialog.SelectedPath;
        }

        //================================================================================
        // changeCurrYearDir_Click
        //    ����� ����� ��� currYear
        //================================================================================
        private void changeCurrYearDir_Click(object sender, EventArgs e)
        {
            if (selectFolderDialog.ShowDialog() == DialogResult.OK)
                currYearDirPath.Text = selectFolderDialog.SelectedPath;
        }

        //================================================================================
        // applyButtonClick
        //    �������� ����� ������� apply
        //================================================================================
        private void applyButtonClick(object sender, EventArgs e)
        {
            // ��������� � ���� ��, ��� ��������
            Base.prices        = prices.Lines;
            Base.templateDoc   = templatePath.Text;
            Base.confirmDelete = deleteCheckBox.Checked;
            Base.confirmUnpay  = unpayCheckBox.Checked;

            Base.workingDirectory[prevYear - beginYear] = prevYearDirPath.Text;
            Base.workingDirectory[currYear - beginYear] = currYearDirPath.Text;

            Close();
        }

        //================================================================================
        // cancelButtonClick
        //    �������� ����� ������� cancel
        //================================================================================
        private void cancelButtonClick(object sender, EventArgs e)
        {
            Close(); // ������ ��������� ������ �� ��������
        }
    }
}