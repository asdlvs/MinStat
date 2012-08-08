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
                name = name.Substring(1, name.LastIndexOf('$') - 1);
                columnsNames.Add(c, name);
            }

            List<string> columnValues = columnsNames.Select(x => x.Value).ToList();
            comboBoxActivity.DataSource = new List<string>(columnValues);
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
            try
            {
                Range selectedCells = Globals.ThisAddIn.Application.ActiveWindow.RangeSelection;

                List<Person> people = new List<Person>();
                int selectedCellsColumnsCount = selectedCells.Columns.Count;
                int selectedCellsRowsCount = selectedCells.Rows.Count;
                for (int r = 1; r <= selectedCellsRowsCount; r++)
                {
                    Person p = new Person();
                    p.Title = GetValue<string>(selectedCells[r, GetIndex(comboBoxIdentifier.SelectedItem.ToString())]);
                    p.Post = GetValue<string>(selectedCells[r, GetIndex(comboBoxPost.SelectedItem.ToString())]);
                    p.PostLevelId = GetValue<int>(selectedCells[r, GetIndex(comboBoxPostLevel.SelectedItem.ToString())]);
                    p.EducationLevelId = GetValue<int>(selectedCells[r, GetIndex(comboBoxEducationLevel.SelectedItem.ToString())]);

                    p.BirthYear = GetValue<int>(selectedCells[r, GetIndex(comboBoxBirthYear.SelectedItem.ToString())]);
                    p.HiringYear = GetValue<int>(selectedCells[r, GetIndex(comboBoxHiringYear.SelectedItem.ToString())]);
                    p.StartPostYear = GetValue<int>(selectedCells[r, GetIndex(comboBoxStartPostYear.SelectedItem.ToString())]);
                    p.DismissalYear = GetValue<int>(selectedCells[r, GetIndex(comboBoxDismissalYear.SelectedItem.ToString())]);

                    p.Gender = GetValue<bool>(selectedCells[r, GetIndex(comboBoxGender.SelectedItem.ToString())]);

                    p.WasIncrease = GetValue<bool>(selectedCells[r, GetIndex(comboBoxIncrease.SelectedItem.ToString())]);

                    p.WasValidation = GetValue<bool>(selectedCells[r, GetIndex(comboBoxValidation.SelectedItem.ToString())]);

                    p.YearSalary = GetValue<decimal>(selectedCells[r, GetIndex(comboBoxYearSalary.SelectedItem.ToString())]);

                    p.Activity = GetValue<string>(selectedCells[r, GetIndex(comboBoxActivity.SelectedItem.ToString())]); 

                    people.Add(p);
                }
                SaveFile(people);
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка. Проверьте корректность вводимых данных.");
            }
        }

        private void SaveFile(List<Person> people)
        {
            SaveFileDialog save = new SaveFileDialog();
            DialogResult dialogResult = save.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string path = save.FileName;

                using (FileStream fs = File.OpenWrite(path))
                {
                    using (TextWriter writer = new StreamWriter(fs))
                    {
                        foreach (Person person in people)
                        {
                            string resultString = String.Format("{12};{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}",
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
                                person.DismissalYear,
                                person.Activity
                                );
                            writer.WriteLine(resultString);
                        }
                        writer.Flush();
                    }

                    MessageBox.Show("Файл успешно сохранен!");
                }
            }
            this.Close();
        }

        private T GetValue<T>(dynamic cell)
        {
            if (cell.Value2 == null)
                return default(T);
            else
            {
                if (typeof(T) == typeof(string))
                {
                    return cell.Value2.ToString();
                }
                else if (typeof(T) == typeof(int))
                {
                    return Int32.Parse(cell.Value2.ToString());
                }
                else if (typeof(T) == typeof(decimal))
                {
                    return Decimal.Parse(cell.Value2.ToString());
                }
                else if (typeof(T) == typeof(bool))
                {
                    return cell.Value2.ToString() == "1" || cell.Value2.ToString().ToUpper() == "М" || cell.Value2.ToString().ToUpper() == "True";
                }
                else
                {
                    return default(T);
                }
            }
        }

    }
}
