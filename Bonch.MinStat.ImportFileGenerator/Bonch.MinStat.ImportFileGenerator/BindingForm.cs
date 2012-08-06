using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Bonch.MinStat.ImportFileGenerator
{
    public partial class BindingForm : Form
    {
        public BindingForm()
        {
            InitializeComponent();
        }

        private void BindingForm_Load(object sender, EventArgs e)
        {
            Range selectedCells = Globals.ThisAddIn.Application.ActiveWindow.RangeSelection;
            for(int i = 0; i <  selectedCells.Columns.Count; i++)
            {
                var column = selectedCells.Columns[i];
                MessageBox.Show(column);
            }
            
        }
    }
}
