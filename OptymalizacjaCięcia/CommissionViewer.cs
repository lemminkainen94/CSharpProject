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
    public partial class CommissionViewer : Form
    {
        public List<string> elementDescriptions;

        public CommissionViewer()
        {
            InitializeComponent();
        }

        public CommissionViewer(List<string> descriptions)
        {
            InitializeComponent();
            elementDescriptions = descriptions;
            commissionBox.DataSource = elementDescriptions;
        }

        private void commission_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CommissionViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
