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
    public partial class StoreViewer : Form
    {
        public List<string> elementDescriptions;

        public StoreViewer()
        {
            InitializeComponent();
        }

        public StoreViewer(List<string> descriptions)
        {
            InitializeComponent();
            elementDescriptions = descriptions;
            storeBox.DataSource = elementDescriptions;
        }

        private void storeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StoreViewer_Load(object sender, EventArgs e)
        {
            
        }
    }
}
