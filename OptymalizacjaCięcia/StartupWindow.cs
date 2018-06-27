using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptymalizacjaCięcia
{
    public partial class StartupWindow : Form
    {
        public static string storeFile;
        public static string commissionFile;
        public static string cutAssignmentFile;
        public static bool bonding = false;
        SortedDictionary<string, List<StoredElement>> store;
        SortedDictionary<string, List<CommissionElement>> commission;

        public StartupWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void storeButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                storeFile = openFileDialog1.FileName;
            }
            else
            {
                return;
            }

            store = new SortedDictionary<string, List<StoredElement>>();
            ExcelReader.ReadAllCellValues(storeFile, true, ref store, ref commission);
        }

        private void commissionButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                commissionFile = openFileDialog2.FileName;
            }
            else
            {
                return;
            }

            commission = new SortedDictionary<string, List<CommissionElement>>();
            ExcelReader.ReadAllCellValues(commissionFile, false, ref store, ref commission);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (storeFile == null || commissionFile == null)
            {
                MessageBox.Show("Aby uruchomić program, należy dodać obydwa pliki.");
                return;
            }
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                cutAssignmentFile = saveFileDialog1.FileName;
            }
            else
            {
                return;
            }

            Program.Compute(storeFile, commissionFile);
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                bonding = true;
            }
            else
            {
                bonding = false;
            }
        }

        private void StartupWindow_Load(object sender, EventArgs e)
        {

        }

        private void showStoreButton_Click(object sender, EventArgs e)
        {
            if (store == null)
            {
                MessageBox.Show("Najpierw należy podać plik z magazynem.");
                return;
            }

            StoreViewer storeViewer = new StoreViewer(GetElementDescriptions(store));
            storeViewer.Show();
        }

        private void showCommissionButton_Click(object sender, EventArgs e)
        {
            if (commission == null)
            {
                MessageBox.Show("Najpierw należy podać plik ze zleceniem.");
                return;
            }

            CommissionViewer commissionViewer = new CommissionViewer(GetElementDescriptions(commission));
            commissionViewer.Show();
        }

        private List<string> GetElementDescriptions(SortedDictionary<string, List<CommissionElement>> elements)
        {
            List<string> ret = new List<string>();
            CommissionElement prev = new CommissionElement();

            foreach (KeyValuePair<string, List<CommissionElement>> p in elements)
            {
                int lastLen = 0;
                int count = 0;
                foreach (CommissionElement e in p.Value)
                {
                    if (lastLen != e.GetInitialLength())
                    {
                        if (count != 0)
                        {
                            ret.Add(prev.ToString() + " Sztuk: " + count);
                            count = 0;
                        }
                    }

                    prev = e;
                    lastLen = e.GetInitialLength();
                    ++count;
                }

                if (elements.Any())
                {
                    ret.Add(prev.ToString() + "  Sztuk: " + count);
                    ret.Add("");
                }
            }
            
            return ret;
        }

        private List<string> GetElementDescriptions(SortedDictionary<string, List<StoredElement>> elements)
        {
            List<string> ret = new List<string>();
            StoredElement prev = new StoredElement();

            foreach (KeyValuePair<string, List<StoredElement>> p in elements)
            {
                int lastLen = 0;
                int count = 0;
                foreach (StoredElement e in p.Value)
                {
                    if (lastLen != e.GetInitialLength())
                    {
                        if (count != 0)
                        {
                            ret.Add(prev.ToString() + " Sztuk: " + count);
                            count = 0;
                        }
                    }

                    prev = e;
                    lastLen = e.GetInitialLength();
                    ++count;
                }

                if (elements.Any())
                {
                    ret.Add(prev.ToString() + "  Sztuk: " + count);
                    ret.Add("");
                }
            }

            return ret;
        }
    }
}

