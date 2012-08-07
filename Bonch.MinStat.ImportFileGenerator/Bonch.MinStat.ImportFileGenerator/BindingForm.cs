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
            try
            {
                Range selectedCells = Globals.ThisAddIn.Application.ActiveWindow.RangeSelection;

                List<Person> people = new List<Person>();
                StringBuilder peopleStringBuilder = new StringBuilder();
                int selectedCellsColumnsCount = selectedCells.Columns.Count;
                int selectedCellsRowsCount = selectedCells.Rows.Count;
                for (int r = 1; r < selectedCellsRowsCount; r++)
                {
                    Person p = new Person();
                    p.Title = selectedCells[r, 1].Value2;
                    p.Post = selectedCells[r, 2].Value2;
                    p.PostLevelId = Int32.Parse(selectedCells[r, 3].Value2.ToString());
                    p.EducationLevelId = Int32.Parse(selectedCells[r, 4].Value2.ToString());
                    people.Add(p);
                    peopleStringBuilder.AppendFormat("{0}, {1}, {3}, {4}", p.Title, p.Post, p.PostLevelId, p.EducationLevelId);
                }
                MessageBox.Show(peopleStringBuilder.ToString());
                System.Windows.Forms.Label l = new System.Windows.Forms.Label();
                l.Text = peopleStringBuilder.ToString();
                this.Controls.Add(l);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
