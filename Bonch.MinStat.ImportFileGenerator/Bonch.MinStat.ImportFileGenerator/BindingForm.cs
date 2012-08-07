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

        Dictionary<int, string> columnsNames = new Dictionary<int, string>();

        private void BindingForm_Load(object sender, EventArgs e)
        {
            Range selectedCells = Globals.ThisAddIn.Application.ActiveWindow.RangeSelection;
            int selectedCellsColumnsCount = selectedCells.Columns.Count;
            for (int c = 1; c <= selectedCellsColumnsCount; c++)
            {
                string name = selectedCells[1, c].Address;
                columnsNames.Add(c, name);
            }

            List<string> columnValues = columnsNames.Select(x => x.Value).ToList();
            comboBoxIdentifier.DataSource = new List<string>(columnValues);
            comboBoxPost.DataSource = new List<string>(columnValues);
            comboBoxPostLevel.DataSource = new List<string>(columnValues);
            comboBoxEducationLevel.DataSource = new List<string>(columnValues);
        }

        private int GetIndex(string value)
        {
            return columnsNames.Single(x => x.Value == value).Key;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Range selectedCells = Globals.ThisAddIn.Application.ActiveWindow.RangeSelection;

            List<Person> people = new List<Person>();
            StringBuilder peopleStringBuilder = new StringBuilder();
            int selectedCellsColumnsCount = selectedCells.Columns.Count;
            int selectedCellsRowsCount = selectedCells.Rows.Count;
            for (int r = 1; r <= selectedCellsRowsCount; r++)
            {
                Person p = new Person();
                p.Title = selectedCells[r, GetIndex(comboBoxIdentifier.SelectedItem.ToString())].Value2;
                p.Post = selectedCells[r, GetIndex(comboBoxPost.SelectedItem.ToString())].Value2;
                p.PostLevelId = Int32.Parse(selectedCells[r, GetIndex(comboBoxPostLevel.SelectedItem.ToString())].Value2.ToString());
                p.EducationLevelId = Int32.Parse(selectedCells[r, GetIndex(comboBoxEducationLevel.SelectedItem.ToString())].Value2.ToString());
                people.Add(p);
                peopleStringBuilder.AppendFormat("{0}, {1}, {2}, {3}", p.Title, p.Post, p.PostLevelId, p.EducationLevelId);
            }
            MessageBox.Show(peopleStringBuilder.ToString());
        }

    }
}
