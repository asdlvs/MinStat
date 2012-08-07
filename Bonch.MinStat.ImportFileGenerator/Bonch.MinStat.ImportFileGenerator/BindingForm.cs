using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            comboBoxYearSalary.DataSource = new List<string>(columnValues);

            comboBoxValidation.DataSource = new List<string>(columnValues);
            comboBoxIncrease.DataSource = new List<string>(columnValues);
            comboBoxGender.DataSource = new List<string>(columnValues);

            comboBoxBirthYear.DataSource = new List<string>(columnValues);
            comboBoxDismissalYear.DataSource = new List<string>(columnValues);
            comboBoxStartPostYear.DataSource = new List<string>(columnValues);
            comboBoxHiringYear.DataSource = new List<string>(columnValues);
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

                p.BirthYear = Int32.Parse(selectedCells[r, GetIndex(comboBoxBirthYear.SelectedItem.ToString())].Value2.ToString());
                p.HiringYear = Int32.Parse(selectedCells[r, GetIndex(comboBoxHiringYear.SelectedItem.ToString())].Value2.ToString());
                p.StartPostYear = Int32.Parse(selectedCells[r, GetIndex(comboBoxStartPostYear.SelectedItem.ToString())].Value2.ToString());
                //p.DismissalYear = Int32.Parse(selectedCells[r, GetIndex(comboBoxDismissalYear.SelectedItem.ToString())].Value2.ToString());

                p.Gender = selectedCells[r, GetIndex(comboBoxGender.SelectedItem.ToString())].Value2.ToString() == "1"
                    || selectedCells[r, GetIndex(comboBoxGender.SelectedItem.ToString())].Value2.ToString().ToUpper() == "М"
                    || selectedCells[r, GetIndex(comboBoxGender.SelectedItem.ToString())].Value2.ToString().ToUpper() == "TRUE";

                p.WasIncrease = selectedCells[r, GetIndex(comboBoxGender.SelectedItem.ToString())].Value2.ToString() == "1"
                    || selectedCells[r, GetIndex(comboBoxGender.SelectedItem.ToString())].Value2.ToString().ToUpper() == "TRUE";

                p.WasValidation = selectedCells[r, GetIndex(comboBoxGender.SelectedItem.ToString())].Value2.ToString() == "1"
                    || selectedCells[r, GetIndex(comboBoxGender.SelectedItem.ToString())].Value2.ToString().ToUpper() == "TRUE";

                p.YearSalary = Decimal.Parse(selectedCells[r, GetIndex(comboBoxYearSalary.SelectedItem.ToString())].Value2.ToString());

                people.Add(p);
                peopleStringBuilder.AppendFormat("{0}, {1}, {2}, {3}", p.Title, p.Post, p.PostLevelId, p.EducationLevelId);
            }
            SaveFile(people);
        }

        private void SaveFile(List<Person> people)
        {
            SaveFileDialog save = new SaveFileDialog();
            DialogResult dialogResult = save.ShowDialog();
            string path = dialogResult.ToString();

            using (FileStream fs = File.OpenWrite(path))
            {
                using (TextWriter writer = new StreamWriter(fs))
                {
                    foreach (Person person in people)
                    {
                        string resultString = String.Format("30;{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}",
                            person.Title,
                            person.Post,
                            person.PostLevelId,
                            person.EducationLevelId,
                            person.YearSalary,
                            person.Gender,
                            person.WasIncrease,
                            person.WasValidation,
                            person.BirthYear,
                            person.HiringYear,
                            person.StartPostYear,
                            person.DismissalYear
                            );
                        writer.WriteLine(resultString);
                    }
                    writer.Flush();
                }
            }
        }

    }
}
