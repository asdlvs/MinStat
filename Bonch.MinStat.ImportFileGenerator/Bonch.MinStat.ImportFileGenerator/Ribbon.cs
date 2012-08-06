using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Ribbon;

namespace Bonch.MinStat.ImportFileGenerator
{
    public partial class Ribbon
    {
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void ImportButton_Click(object sender, RibbonControlEventArgs e)
        {
            BindingForm form = new BindingForm();
            form.ShowDialog();
        }
    }
}
