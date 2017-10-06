using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Xml.Serialization;

namespace DuAn1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> danhba;
        //   private AddContact addcontact;


        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            danhba = new ObservableCollection<Person>();

            danhba.Add(new Person() { Name = "Duong", Sdt = "+491733911278" });
            danhba.Add(new Person() { Name = "Anh", Sdt = "+491712547551" });
            danhba.Add(new Person() { Name = "Hung", Sdt = "+491712547551" });
            danhba.Add(new Person() { Name = "Nguyen", Sdt = "+491712547551" });
            danhba.Add(new Person() { Name = "Viet", Sdt = "+491712547551" });
            danhba.Add(new Person() { Name = "Tung", Sdt = "+491712547551" });
            lbDanhba.ItemsSource = danhba;
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbDanhba.SelectedItem != null)
            {
                danhba.Remove(lbDanhba.SelectedItem as Person);
            }
        }

        private void btModify_Click(object sender, RoutedEventArgs e)
        {
            if (lbDanhba.SelectedIndex >= 0)
            {
                AddContact addcontact = new AddContact(danhba, this, true);
                Person p = lbDanhba.SelectedItem as Person;

                addcontact.tbName.Text = p.Name;
                addcontact.tbPhone.Text = p.Sdt;
                addcontact.ShowDialog();
            }

        }

        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            AddContact addcontact = new AddContact(danhba);

            addcontact.ShowDialog();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();

            savefile.Filter = String.Format("{0} (*.pi)|*.pi|{1} (*.*)|*.*", "DuAn1 Config File", "All Files");

            try
            {
                if (savefile.ShowDialog() == true)
                {
                    ListPersonXml savelist = new ListPersonXml();
                    string filename = savefile.FileName;
                    savelist.CreateFile(filename, new List<Person>(danhba));

                }

            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString(),"");
            }
        }

        private void btLoad_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class ListPersonXml
    {


        [XmlArray("Contact"),XmlArrayItem("Person",typeof(Person))]
        public List<Person> people = new List<Person>();

        //public static ListPersonXml LoadFile(Object file)
        //{

        //}

        public void CreateFile(string file, List<Person> inpeople)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListPersonXml));

            TextWriter writer = new StreamWriter(file);

            people = inpeople;

            serializer.Serialize(writer, this);

            writer.Close();
        }


    }


    public class Person : INotifyPropertyChanged
    {

        private string sdt;
        private string name;

        [XmlAttribute("Phone")]
        public string Sdt
        {
            get { return sdt; }

            set {
                sdt = value;
                NotifyPropertyChanged("Sdt");
            }

        }

        public Person()
        {

        }

        [XmlAttribute("Name")]
        public string Name
        {
            get { return name; }

            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propname)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }
    }
}
