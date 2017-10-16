using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace DuAn1
{
    /// <summary>
    /// Interaktionslogik für AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {

        private ObservableCollection<Person> danhba = null;
        private MainWindow mainwin = null;
        private bool modif = false;

        public AddContact()
        {
            InitializeComponent();
        }

        public AddContact(ObservableCollection<Person> initlist)
        {

            InitializeComponent();
            danhba = initlist;

        }

        public AddContact(ObservableCollection<Person> initlist, MainWindow win, Boolean modifordel)
        {

            InitializeComponent();
            danhba = initlist;
            mainwin = win;
            modif = modifordel;

        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (modif && (mainwin!=null))
            {
                if (String.IsNullOrEmpty(tbName.Text) || String.IsNullOrEmpty(tbPhone.Text) || (mainwin.lbDanhba.SelectedIndex == -1))
                {

                }
                else
                {
                    try
                    {
                        Person p = mainwin.lbDanhba.SelectedItem as Person;
                        p.Name = tbName.Text;
                        p.Sdt = tbPhone.Text;
                        if (!String.IsNullOrEmpty(tbImagePath.Text))
                        {
                            p.DisplayImagePath = tbImagePath.Text;
                        }
                    }
                    catch (Exception exc)
                    {

                        MessageBox.Show(exc.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        modif = false;
                        mainwin = null;
                    }

                    MessageBox.Show("modify contact successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }
            }
            else
            {
                if (String.IsNullOrEmpty(tbName.Text) || String.IsNullOrEmpty(tbPhone.Text))
                {

                }
                else
                {
                    try
                    {
                        danhba.Add(new Person() { Name = tbName.Text, Sdt = tbPhone.Text, DisplayImagePath = String.IsNullOrEmpty(tbImagePath.Text) ? Person.DefaultImage : tbImagePath.Text });
                    }
                    catch (Exception exc)
                    {

                        MessageBox.Show(exc.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    MessageBox.Show("add new contact successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }

            }

        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void btSelImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();

            openfile.Filter = String.Format("{0} (*.jpg)|*.jpg|{1} (*.png)|*.png|{2} (*.*)|*.*", "jpg format", "png format", "all files");

            if (openfile.ShowDialog() == true)
            {
                tbImagePath.Text = openfile.FileName;
            }
        }
    }
}
