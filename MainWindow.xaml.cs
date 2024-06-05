using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PersonLibrary;
using LaboratoryWork_12;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using BinaryFormatter;
using System.Windows.Controls;

namespace labik16
{
    public partial class MainWindow : Window
    {
        private Tree<Person> _personTree;

        public MainWindow()
        {
            /*InitializeComponent();
            _personTree = new Tree<Person>();
            RefreshGrid();*/
            _personTree = new Tree<Person>();
            InitializeComponent();
            personDataGrid.ItemsSource = _personTree;
            personDataGrid.AutoGeneratingColumn += PersonDataGrid_AutoGeneratingColumn;
            RefreshGrid();
        }
        private void PersonDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Institution" || e.PropertyName == "Major" ||  e.PropertyName == "WorkHours")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtPersonAge.Text, out int age))
            {
                var person = new Person { Name = txtPersonName.Text, Age = age };
                _personTree.Add(person);
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Invalid age input");
            }
        }
        private void AddScholar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtPersonAge.Text, out int age) && int.TryParse(txtPersonGrade.Text, out int grade))
            {
                var person = new SchoolStudent { Name = txtPersonName.Text, Age = age, School = txtPersonGrade.Text, Grade = grade };
                _personTree.Add(person);
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtPersonAge.Text, out int age))
            {
                var person = new Student { Name = txtPersonName.Text, Age = age, University = txtPersonUniversity.Text };
                _personTree.Add(person);
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }

        private void AddPartTimeStudent_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtPersonAge.Text, out int age))
            {
                var person = new PartTimeStudent { Name = txtPersonName.Text, Age = age, University = txtPersonUniversity.Text, Employer = txtPersonWorkPlace.Text };
                _personTree.Add(person);
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }

        private void RemovePerson_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtPersonId.Text, out int id))
            {
                var person = _personTree.FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    _personTree.Remove(person);
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Person not found");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID input");
            }
        }

        private void UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtPersonId.Text, out int id) && int.TryParse(txtPersonAge.Text, out int age))
            {
                var person = _personTree.FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    person.Name = txtPersonName.Text;
                    person.Age = age;
                    RefreshGrid();
                }
                else
                {
                    MessageBox.Show("Person not found");
                }
            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }

        private void RefreshGrid()
        {
            personDataGrid.ItemsSource = null;
            personDataGrid.ItemsSource = _personTree.ToList();
        }

        private void LoadCollection_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin|JSON files (*.json)|*.json|XML files (*.xml)|*.xml",
                Title = "Open Collection File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileExtension = Path.GetExtension(openFileDialog.FileName);

                try
                {
                    using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                    {
                        switch (fileExtension)
                        {
                            /*case ".bin":
                                LoadFromBinary(fs);
                                break;*/
                            case ".json":
                                LoadFromJson(fs);
                                break;
                            case ".xml":
                                LoadFromXml(fs);
                                break; // работает ебано
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load collection: {ex.Message}");
                }

                RefreshGrid();
            }
        }
        private void SaveCollection_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin|JSON files (*.json)|*.json|XML files (*.xml)|*.xml",
                Title = "Save Collection File"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileExtension = Path.GetExtension(saveFileDialog.FileName);

                try
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        switch (fileExtension)
                        {
                           /* case ".bin":
                                SaveToBinary(fs);
                                break;*/
                            case ".json":
                                SaveToJson(fs);
                                break;
                            case ".xml":
                                SaveToXml(fs); // работает ебано
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save collection: {ex.Message}");
                }
            }
        }

      /* private void LoadFromBinary(FileStream fs)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            _personTree = (Tree<Person>)formatter.Deserialize(fs);
        }

        private void SaveToBinary(FileStream fs)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, _personTree);
        }*/
      

        private void LoadFromJson(FileStream fs)
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                string json = reader.ReadToEnd();
                _personTree = JsonConvert.DeserializeObject<Tree<Person>>(json);
            }
        }

        private void SaveToJson(FileStream fs)
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                string json = JsonConvert.SerializeObject(_personTree, Newtonsoft.Json.Formatting.Indented);
                writer.Write(json);
            }
        }

        private void LoadFromXml(FileStream fs)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tree<Person>));
            _personTree = (Tree<Person>)serializer.Deserialize(fs);
        }

        private void SaveToXml(FileStream fs)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tree<Person>));
            serializer.Serialize(fs, _personTree);
        }
    }
}