using Lab4.classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FilePath = "data/ChimicalElement.XML";
        private ElementsList elementalList = new ElementsList();

        public MainWindow()
        {
            InitializeComponent();
            LoadElementsFromXml();
            PopulateDataGrid(elementalList);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text.Length > 0 && LatinNameTextBox.Text.Length > 0 && SybmolTextBox.Text.Length > 0 && AtomicMassTextBox.Text.Length > 0)
            {
                Element element = new Element(NameTextBox.Text, LatinNameTextBox.Text, SybmolTextBox.Text, float.Parse(AtomicMassTextBox.Text));
                AddElementToXml(element);
                elementalList.AddElement(element);
                elementalList = new ElementsList();
                LoadElementsFromXml();
                PopulateDataGrid(elementalList);
            }
            else
            {
                MessageBox.Show("Нужно заполнить все ячейки");
                return;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ElementDataGrid.SelectedItem is Element selectedElement)
            {
                RemoveElementFromXml(selectedElement);
                elementalList.RemoveElement(selectedElement);
                elementalList = new ElementsList();
                LoadElementsFromXml();
                PopulateDataGrid(elementalList);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadElementsFromXml();
            PopulateDataGrid(elementalList);
        }

        private void LoadElementsFromXml()
        {
            elementalList = new ElementsList();
            XDocument xmlDoc = XDocument.Load(FilePath);
            foreach (XElement elementNode in xmlDoc.Root.Elements("element"))
            {
                Element element = new Element(
                    elementNode.Element("name").Value,
                    elementNode.Element("latin_name").Value,
                    elementNode.Element("symbol").Value,
                    float.Parse(elementNode.Element("atomic_mass").Value, CultureInfo.InvariantCulture)
                );
                elementalList.AddElement(element);

            }
        }

        private void AddElementToXml(Element element)
        {
            XDocument xmlDoc = XDocument.Load(FilePath);
            XElement rootElement = xmlDoc.Root;
            XElement newElement = new XElement("element",
                new XElement("name", element.Name),
                new XElement("latin_name", element.LatinName),
                new XElement("symbol", element.Symbol),
                new XElement("atomic_mass", element.AtomicMass.ToString())
            );
            rootElement.Add(newElement);
            xmlDoc.Save(FilePath);
        }

        private void RemoveElementFromXml(Element element)
        {
            XDocument xmlDoc = XDocument.Load(FilePath);
            XElement rootElement = xmlDoc.Root;
            XElement elementToDelete = rootElement.Elements("element")
                .FirstOrDefault(el =>
                    el.Element("name").Value == element.Name &&
                    el.Element("latin_name").Value == element.LatinName &&
                    el.Element("symbol").Value == element.Symbol &&
                    el.Element("atomic_mass").Value == element.AtomicMass.ToString()
                );
            elementToDelete?.Remove();
            xmlDoc.Save(FilePath);
        }

        private void PopulateDataGrid(ElementsList elementalList)
        {
            ElementDataGrid.ItemsSource = elementalList.GetElements();
        }
    }
}
