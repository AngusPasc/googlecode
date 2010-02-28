using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Invoice
{
    public partial class MainForm : SavePositionForm
    {
        private ColumnSorter columnSorter;
        private EditBillForm editBillForm;
        private SelectDateForm selectDateForm;

        //================================================================================
        // MainForm
        //================================================================================
        public MainForm()
        {
            InitializeComponent();

            // ������ � �������� ������� �����
            this.Text = "����-������� " + Utils.ToStringWithoutZeroes(new Version(Application.ProductVersion));

            // ���������� �������
            listView.ListViewItemSorter = columnSorter = new ColumnSorter();

            // ��������� ���� � ���������� ����-������� � listView
            Base.Load();
            buildBillList();

            // ���������� ����������� ������� �����
            this.Position = Base.FormPosition;

            // ���������� ����������� ������ ��� ������ �������
            foreach (ColumnHeader column in listView.Columns)
                column.Width = Base.columnWidth[column.Index];

            toggleToolButton.Checked = Base.showPayed;

            // ������ ��� ��������������� �����
            editBillForm = new EditBillForm();
            selectDateForm = new SelectDateForm();

            // ��� ��������� ����� WebClient.DownloadStringAsync, ������� ����
            // � Async, �� ��������� �������� (4-5 �.) ������� ������ ���
            // "�������������� Async".
            backgroundWorker.RunWorkerAsync();
        }

        //================================================================================
        // backgroundWorker_DoWork
        //================================================================================
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // ��������� ����������
            Update.Updater updater = new Update.Updater("http://stopiccot.googlecode.com/files/invoice-version.xml");
            updater.checkForUpdates();
        }

        //================================================================================
        // MainForm_FormClosed
        //    �������� ����� - �� ��������� � �����������
        //================================================================================
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Base.FormPosition = this.Position;
            Base.Save();
            Word.Close();
        }

        //================================================================================
        // buildBillList
        //    ������ ������ ���� ����-������.
        //================================================================================
        private void buildBillList()
        {
            listView.Items.Clear();

            // ��������� �������� ������� �� ��������� ������, �.�. ���� ��������
            // ��������� ������ ������� ��-����������� ��������� listView.Items.Add
            // ����� ���������� �������� �.�. ��� �������������� ������ ��� ����� ����������
            List<BillListViewItem> items = new List<BillListViewItem>();

            for (int i = 0; i < Base.billList.Count - 1; i++)
            {
                Bill bill = Base.billList[i];

                // ���������� ����-������� ���������� ������ ���� ����� �������������� ������
                if (Base.showPayed || !bill.Payed)
                    items.Add(new BillListViewItem(bill, i));
            }

            // �� ���� ��� ��������� ��� ����-�������
            listView.Items.AddRange(items.ToArray());

            listViewSelectionChanged(null, null);
        }

        //================================================================================
        // createNewBill
        //    �������� ����� ����-�������
        //================================================================================
        private void createNewBill(object sender, EventArgs e)
        {
            Bill newBill = new Bill();
            newBill.Add();

            if (editBillForm.EditBill(Base.billList.Count - 1, false))
            {
                // ��������� � ������ � ��������
                listView.Items.Add(new BillListViewItem(Base.billList[Base.billList.Count - 1], Base.billList.Count - 1));
                listView.SelectedIndex = listView.Items.Count - 1;
            }
            else
            {
                Base.billList.RemoveAt(Base.billList.Count - 1);
            }
        }

        //================================================================================
        // listView_DoubleClick
        //    ������� �� ������� ������
        //================================================================================
        private void listView_DoubleClick(object sender, EventArgs e)
        {
            // ���� �����-�� ����-������� �������� - ����������� �
            int index = listView.TranslatedSelectedIndex;
            if (index != -1)
            {
                if (editBillForm.EditBill(index, true))
                {
                    int selectedIndex = listView.SelectedIndex;

                    // �������� ���� ����� �����, � �� ����� ����
                    listView.Items[listView.SelectedIndex] = new BillListViewItem(Base.billList[index], index);

                    listView.SelectedIndex = selectedIndex;
                }
            }
        }

        //================================================================================
        // listView_ColumnWidthChanged
        //    �������� ����������� ������� ����� ��� ��������� �������� ������� listView
        //================================================================================
        private void listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            int minWidth = 12;

            foreach (ColumnHeader column in listView.Columns)
                minWidth += (Base.columnWidth[column.Index] = column.Width);

            this.MinimumSize = new Size(minWidth, 209);
        }

        //================================================================================
        // DeleteInvoices
        //    �������� ����-������
        //================================================================================
        delegate bool FilterFunction<T>(T item);

        private void DeleteInvoices(string confirmText, FilterFunction<BillListViewItem> filterFunction)
        {
            if (!Base.confirmDelete ||
                MessageBox.Show(confirmText, "�������������", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                List<int> indicesToDelete = new List<int>();

                foreach (BillListViewItem item in listView.Items)
                    if (filterFunction(item))
                        indicesToDelete.Add(item.Index);

                indicesToDelete.Sort();

                int i = 0;
                foreach (int index in indicesToDelete)
                {
                    BillListViewItem item = (BillListViewItem)listView.Items[index - i];
                    
                    Base.billList.RemoveAt(item.translatedIndex - i);
                    listView.Items.RemoveAt(index - i);

                    i++;
                }

                listViewSelectionChanged(null, null);
            }
        }

        //================================================================================
        // listViewSelectionChanged
        //    ���� ���������� ������ � �������, ����� ������ �� ��������.
        //================================================================================
        private void listViewSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            wordToolButton.Enabled = deleteToolButton.Enabled = printToolButton.Enabled = (listView.SelectedItems.Count != 0);
        }

        //================================================================================
        // listView_ColumnClick
        //    ���� �� ������� listView. ���������� ������.
        //================================================================================
        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == columnSorter.SortColumn)
            {
                // ���� ������ ��� ������������ �� ���� �������, �� ������ ������� ����������
                columnSorter.Order = columnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // ���� ������� ������ �������, �� ��������� �� �����������
                columnSorter.SortColumn = e.Column; // ������� ������� ����� �������
                columnSorter.Order = SortOrder.Ascending;
            }

            listView.Sort();
        }

        //================================================================================
        // listView_ItemCheck
        //    ���� �� ������� ������ � listView
        //================================================================================
        private void listView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // ���� �������� ����� ������� � � ���������� ���������� ��������, �� ������
            // ��� ��������������
            if (e.CurrentValue == CheckState.Checked)
                if (Base.confirmUnpay && MessageBox.Show("��� ����-������� ����� �� ��������?", "�������������", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.NewValue = CheckState.Checked;

            Base.billList[listView.Translate(e.Index)].Payed = e.NewValue == CheckState.Checked;
        }

        //================================================================================
        // wordToolButton_Click
        //    ������� doc-���� ��� ��������� ����-������� � Microsoft Word
        //================================================================================
        private void wordToolButton_Click(object sender, EventArgs e)
        {
            // ������ doc-����
            string filename = CreateWordDocument(Base.billList[listView.TranslatedSelectedIndex]);

            // � ���� �� ������ �������, �� ��������� ���
            if (filename != null)
            {
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(filename);
                info.UseShellExecute = true;
                info.Verb = "open";
                System.Diagnostics.Process.Start(info);
            }
        }

        //================================================================================
        // printToolButton_Click
        //    ������ doc-����� ��� ��������� ����-��������
        //================================================================================
        private void printToolButton_Click(object sender, EventArgs e)
        {
            // ������ doc-����
            string filename = CreateWordDocument(Base.billList[listView.TranslatedSelectedIndex]);

            // � ���� �� ������ �������, �� �������� ���
            if (filename != null)
            {
                Word.OpenDocument(filename, true);
                Word.PrintDocument();
                Word.CloseDocument();
            }
        }

        //================================================================================
        // deleteToolButton_Click
        //    ������� ���������� ����-�������
        //================================================================================
        private void deleteToolButton_Click(object sender, EventArgs e)
        {
            DeleteInvoices("������� ��� ����-�������?", delegate(BillListViewItem item)
            {
                return listView.SelectedItems.Contains(item);
            });
        }

        //================================================================================
        // toggleToolButton_Click
        //================================================================================
        private void toggleToolButton_Click(object sender, EventArgs e)
        {
            Base.showPayed = toggleToolButton.Checked;

            // ���������� ���������� �������
            int index = listView.TranslatedSelectedIndex;

            // � ��������� ������������� ������
            buildBillList();

            // �������� ������������ ���������, �� ��� �� ������ �������� �.�. ������ ����-�������
            // ����� ���� ��������, � �� ��� ��� �������� ��� ���������� ����-�������
            if (index != -1)
            {
                listView.TranslatedSelectedIndex = index;
                if (listView.SelectedIndex != -1)
                    listView.Items[listView.SelectedIndex].EnsureVisible();
            }
        }

        //================================================================================
        // deletePayedToolButton_Click
        //    �������� �������� ���������� ����-������
        //================================================================================
        private void deletePayedToolButton_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            if (selectDateForm.PickDate(ref date))
            {
                List<int> indicesToRemove = new List<int>();

                for (int i = 0; i < Base.billList.Count; i++)
                {
                    Bill bill = Base.billList[i];
                    if (bill.Payed && bill.Date < date)
                        indicesToRemove.Add(i);
                }

                indicesToRemove.Sort();
                for (int i = 0; i < indicesToRemove.Count; i++)
                    Base.billList.RemoveAt(indicesToRemove[i] - i);

                buildBillList();
            }
        }

        //================================================================================
        // settingsToolButton_Click
        //    ���������� ������ ��������
        //================================================================================
        private void settingsToolButton_Click(object sender, EventArgs e)
        {
            (new SettingsForm()).ShowDialog();
        }

        //================================================================================
        // CreateWordDocument
        //    ������ doc-���� ��� ������ ����-�������
        //================================================================================
        private string CreateWordDocument(Bill bill)
        {
            // ��������� ����� �� ����-������
            if (!File.Exists(Base.templateDoc))
            {
                MessageBox.Show("���� ������ �� ���������.\n\r������� � ���� ���� � ���������� ���������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

            int year = bill.Date.Year;
            string filename = null;

            try
            {
                string workingDirectory = Path.GetDirectoryName(Base.workingDirectory[year - SettingsForm.beginYear]);

                // ��������� ������ �� ����� � ������� ����� ��������� ����-������� ����� ����
                if (!Directory.Exists(workingDirectory))
                    throw new Exception();

                // ������ ���� � ������������ doc-�����
                filename = workingDirectory + "\\��-�" + bill.Company.ShortName + bill.Number.ToString() + '-' + bill.Date.Month.ToString("00") + ".doc";
            }
            catch
            {
                MessageBox.Show("�� ������ ����� ��� ����-������ " + year.ToString() + " ����.\n\r�������� ����� � ���������� ���������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

            Word.OpenDocument(Base.templateDoc, true);

            Word.Replace("%number%", bill.Number.ToString() + '/' + bill.Date.Month.ToString());
            Word.Replace("%date%", bill.Date.ToString("d.MM.yyyy"));
            Word.Replace("%fullName%", bill.Company.FullName);
            Word.Replace("%contractNumber%", bill.Company.ContractNumber);
            Word.Replace("%contractDate%", bill.Company.ContractDate.ToString("d.MM.yyyy"));
            Word.Replace("%car%", bill.Car);
            Word.Replace("%price%", bill.Price.ToString());
            Word.Replace("%priceString%", Utils.ConvertToString(bill.Price));
            Word.Replace("%director%", bill.Company.Director);
            Word.Replace("%directorName%", Utils.ExtractDirectorName(bill.Company.Director));

            int n = 0; string s = "";

            if ((bill.WorkDone & 1) == 1)
                s += (++n).ToString() + ".��� �������.\r";

            if ((bill.WorkDone & 2) == 2)
            {
                s += (++n).ToString() + ".���������� � ������� �����.\r";
                s += (++n).ToString() + ".����������� ��������-����������������� �����.\r";
            }

            if ((bill.WorkDone & 4) == 4)
                s += (++n).ToString() + ".����� �� ����� �������.\r";

            if ((bill.WorkDone & 8) == 8)
                s += (++n).ToString() + ".������ ����� � ������ ������ ��.\r";

            Word.Replace("%workDone%", s);

            s = "";

            if ((bill.WorkDone & 1) == 1)
                s += "���������� �������, ����������� ���� �������, ";

            if ((bill.WorkDone & 2) == 2)
                s += "coc�������� ���������� � ������� �����, ";

            if (s.Length > 0)
                s = Utils.Capitalize(s).Substring(0, s.Length - 2);

            Word.Replace("%workDoneString%", s);

            Word.SaveAs(filename);

            Word.CloseDocument();

            return filename;
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            //...
        }

        
    }
}