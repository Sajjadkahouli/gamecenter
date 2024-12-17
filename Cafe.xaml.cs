using System;
using System.Collections.Generic;
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

namespace Game_Center_Bergamo
{
    /// <summary>
    /// Interaction logic for Cafe.xaml
    /// </summary>
    public partial class Cafe : Window
    {
        public int Hour { get; set; }
        public int Min { get; set; }
        public int SystemID { get; set; }
        public int State { get; set; }
        public Cafe()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cbosystems.Items.Clear();
                var directors = PersonDataAccess.AllSystems.ToList();
                directors.Insert(0, new Models.Systems() { ID = 0, Name = "-- انتخاب کنید --" });
                cbosystems.ItemsSource = directors;
                cbosystems.SelectedIndex = 0;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbosystems.SelectedValue == null)
                    MessageBox.Show("دستگاه را مشخص کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                else if ((int)cbosystems.SelectedValue < 1)
                {
                    MessageBox.Show("دستگاه را مشخص کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }else if(txtH.Text == "" || txtM.Text == "")
                    MessageBox.Show("تایم درستی را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    SystemID = (int)cbosystems.SelectedValue;
                    Hour = Convert.ToInt32(txtH.Text);
                    Min = Convert.ToInt32(txtM.Text);
                    State = 1;
                    this.Close();
                }
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbosystems.SelectedValue == null)
                    MessageBox.Show("دستگاه را مشخص کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                else if ((int)cbosystems.SelectedValue < 1)
                {
                    MessageBox.Show("دستگاه را مشخص کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    SystemID = (int)cbosystems.SelectedValue;
                    Hour = Convert.ToInt32(txtH.Text);
                    Min = Convert.ToInt32(txtM.Text);
                    State = 2;
                    this.Close();
                }
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
    }
}
