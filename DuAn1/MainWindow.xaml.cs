﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Globalization;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;

namespace DuAn1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> danhba;
        //   private AddContact addcontact;
        CultureInfo cul;
        ResourceManager res_man;
        private PLanglist plang;

        public PLanglist Plang
        {
            get => plang;
        }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
            //use embedded resource xml file to binding language
            plang = new PLanglist();
            
            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("DuAn1.Lang.vietnamese.xml");
            if (str == null)
            {
                MessageBox.Show("khong load duoc stream trong assembly", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                plang.Load(str);
            }

            // switch language by Culture Info
            cul = CultureInfo.CreateSpecificCulture("vi");
            res_man = new ResourceManager("DuAn1.Resource.Lang",Assembly.GetExecutingAssembly());
            update_lang();

            danhba = new ObservableCollection<Person>();

            danhba.Add(new Person() { Name = "Duong", Sdt = "+491733911278" });
            danhba.Add(new Person() { Name = "Anh", Sdt = "+491712547551" });
            danhba.Add(new Person() { Name = "Hung", Sdt = "+491712547551" });
            danhba.Add(new Person() { Name = "Nguyen", Sdt = "+491712547551" });
            danhba.Add(new Person() { Name = "Viet", Sdt = "+491712547551" });
            danhba.Add(new Person() { Name = "Tung", Sdt = "+491712547551" });
            lbDanhba.ItemsSource = danhba;
            this.mnvn.IsChecked = true;
            this.mnen.IsChecked = false;

            //test read Xaml file from code behind
            Window window = null;

            using (FileStream fs = new FileStream(@"../../Window1.xaml", FileMode.Open, FileAccess.Read))
            {
                window = (Window) XamlReader.Load(fs);
            }

            Button testButton = (Button) window.FindName("btTest");
              

            testButton.Content = "tested";

            window.ShowDialog();
        }

        private void update_lang()
        {
            mnsellang.Header = res_man.GetString("_mnsellang", cul);

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

            savefile.Filter = String.Format("{0} (*.xml)|*.xml|{1} (*.*)|*.*", "DuAn1 Config File", "All Files");

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
            OpenFileDialog openfile = new OpenFileDialog();

            try
            {
                if(openfile.ShowDialog() == true)
                {

                    string filename = openfile.FileName;

                    if (!String.IsNullOrEmpty(filename))
                    {

                        ListPersonXml peoplexml = ListPersonXml.LoadFile(filename);

                        if(peoplexml != null)
                        {
                            danhba = new ObservableCollection<Person>(peoplexml.people);
                            lbDanhba.ItemsSource = danhba;
                            MessageBox.Show(danhba.Count.ToString(), "");
                        }
                    }

                }
            }catch(Exception exc)
            {
                MessageBox.Show(exc.ToString(), "");
            }

        }

        private void Mnvn_OnChecked(object sender, RoutedEventArgs e)
        {
            cul = CultureInfo.CreateSpecificCulture("vi");
            update_lang();


            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("DuAn1.Lang.vietnamese.xml");
            if (str == null)
            {
                MessageBox.Show("khong load duoc stream trong assembly", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                plang.Load(str);
            }

            this.mnen.IsChecked = false;
        }

        private void Mnen_OnChecked(object sender, RoutedEventArgs e)
        {
            cul = CultureInfo.CreateSpecificCulture("en");
            update_lang();


            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("DuAn1.Lang.english.xml");
            if (str == null)
            {
                MessageBox.Show("khong load duoc stream trong assembly", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                plang.Load(str);
            }

            this.mnvn.IsChecked = false;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }

    public class ListPersonXml
    {


        [XmlArray("Contact"),XmlArrayItem("Person",typeof(Person))]
        public List<Person> people = new List<Person>();

        public static ListPersonXml LoadFile(string file)
        {
            TextReader reader = new StreamReader(file);

            XmlSerializer deserializer = new XmlSerializer(typeof(ListPersonXml));

            ListPersonXml value = null;

            try
            {
                value = (ListPersonXml)deserializer.Deserialize(reader);

            }catch(Exception exc)
            {
                MessageBox.Show(exc.ToString(), "");
            }
            finally
            {
                reader.Close();
            }

            return value;
        }

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
        public const string DefaultImage = "Image/no_image_thumb.gif";
        private string sdt;
        private string name;
        private string imagepath;

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
            /// imagepath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //imagepath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../..");
            //imagepath += "/Image/no_image_thumb.gif";
            imagepath = DefaultImage;
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

        [XmlAttribute("DisplayImagePath")]
        public string DisplayImagePath
        {
            get { return imagepath; }

            set
            {

                imagepath = value;

                NotifyPropertyChanged("DisplayImagePath");
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
