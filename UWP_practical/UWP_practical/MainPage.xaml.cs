using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_practical
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public MainPage()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage
                .ApplicationData
                .Current
                .LocalFolder
                .Path, "database.sqlite");
            conn = new SQLite.Net.SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Customer>();
        }

        public class Customer
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
        }

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            var query = conn.Table<Customer>();
            string id = "";
            string name = "";
            string phone = "";
            foreach (var message in query)
            {
                id = id + " " + message.Id;
                name = name + " " + message.Name;
                phone = phone + " " + message.PhoneNumber;
            }
            textBlock2.Text = "Id" + id + "\nName" + name + "\nPhoneNumber" + phone;
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            var customer = conn.Insert(new Customer()
            {
                Name = textBox.Text,
                PhoneNumber = textBox1.Text

            });
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = conn.Table<Customer>();
                string name = "";
                string phone = "";
                foreach (var message in query)
                {

                    name = message.Name;
                    phone = message.PhoneNumber;

                    if (String.Compare(textBox2.Text, name, true) == 0 && String.Compare(textBox3.Text, phone, true) == 0)
                    {
                        textBlock2.Text = "\nName: " + name + "\nPhoneNumber: " + phone;

                        break;
                    }
                    else
                    {
                        textBlock2.Text = "Không Tìm Được Kết Qủa";
                    }
                }
            }
            catch
            {

            }
        }
    }
}
