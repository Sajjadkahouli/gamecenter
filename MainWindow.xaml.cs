using Game_Center_Bergamo.Models;
using Game_Center_Bergamo;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Media;

namespace Game_Center_Bergamo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        PersonDataAccess Person = new PersonDataAccess();
        public decimal CurrentPrice;
        public int Index;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimerps4N1 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimerps4N2 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimerps4N3 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimerps4N4 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimerps5 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimerVIP4 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer dispatcherTimerVIP5 = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Loaded += ToolWindow_Loaded;
                DispatcherTimer LiveTime = new DispatcherTimer();
                LiveTime.Interval = TimeSpan.FromSeconds(1);
                LiveTime.Tick += timer_Tick;
                LiveTime.Start();
                var timer = new Stopwatch();
                dtgBorrow.ItemsSource = PersonDataAccess.Borrows;
                ListCafe.ItemsSource = PersonDataAccess.Mylist;
                DtgFactor.ItemsSource = PersonDataAccess.ListOfinvoices;
                DtgFactorKol.ItemsSource = PersonDataAccess.ListOfinvoicesFinall;
                dtgShowMembers.ItemsSource = PersonDataAccess.AllPersons;
                txtCustumerName.TextChanged += SuggestionBoxOnTextCafeChanged;
                txtbiliard.TextChanged += SuggestionBoxOnTextBiliardChanged;
                txtps4N1.TextChanged += SuggestionBoxOnTextPs4N1Changed;
                txtps4N2.TextChanged += SuggestionBoxOnTextPs4N2Changed;
                txtps4N3.TextChanged += SuggestionBoxOnTextPs4N3Changed;
                txtps4N4.TextChanged += SuggestionBoxOnTextPs4N4Changed;
                txtps5.TextChanged += SuggestionBoxOnTextPs5Changed;
                txtVIP4.TextChanged += SuggestionBoxOnTextVIP4Changed;
                txtVIP5.TextChanged += SuggestionBoxOnTextVIP5Changed;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
        private void mysql()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = DateTime.Now.ToString("yyyy-MM-dd h:mm:ss ");
            
        }
        void TimeNowLive(object sender, EventArgs e)
        {
            lblEndLivebiliard.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        void TimeNowLiveps4N1(object sender, EventArgs e)
        {
            lblEndLiveps4N1.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        void TimeNowLiveps4N2(object sender, EventArgs e)
        {
            lblEndLiveps4N2.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        void TimeNowLiveps4N3(object sender, EventArgs e)
        {
            lblEndLiveps4N3.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        void TimeNowLiveps4N4(object sender, EventArgs e)
        {
            lblEndLiveps4N4.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        void TimeNowLiveVIP5(object sender, EventArgs e)
        {
            lblEndLiveVIP5.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        void TimeNowLiveVIP4(object sender, EventArgs e)
        {
            lblEndLiveVIP4.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        void TimeNowLiveps5(object sender, EventArgs e)
        {
            lblEndLiveps5.Content = DateTime.Now.ToString("HH:mm:ss");
        }
        private void btncafe_Click(object sender, RoutedEventArgs e)
        {
            Cafe cafe = new Cafe();
            cafe.ShowDialog();
        }

        private void btnbiliard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Systems s = new Systems();
                string content = (sender as Button).Content.ToString();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == content)
                    {
                        s = item;
                        break;
                    }
                }
                if (btnbiliard.Background == Brushes.Black)
                {
                    lblEndLivebiliard.Visibility = Visibility.Visible;
                    lblEndbiliard.Visibility = Visibility.Collapsed;
                    lblStartbiliard.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLive;
                    LiveTime.Start();
                    btnbiliard.Background = Brushes.DarkRed;
                }
                else
                {
                    if (txtbiliard.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtbiliard.Text = ReplaceWhitespace(txtbiliard.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtbiliard.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtbiliard.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtbiliard.Text, 0, 3 , 0 , 0);
                        }
                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , 1).ToString() + "تومن";
                        var title = "بستن تایم!";
                        var result = MessageBox.Show(
                            message,                  // the message to show
                            title,                    // the title for the dialog box
                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                            MessageBoxImage.Question); // show a question mark icon

                        // the following can be handled as if/else statements as well
                        switch (result)
                        {
                            case MessageBoxResult.Yes:   // Yes button pressed

                                lblEndLivebiliard.Visibility = Visibility.Collapsed;
                                lblEndbiliard.Visibility = Visibility.Visible;

                                lblEndbiliard.Content = DateTime.Now.ToString("HH:mm:ss");
                                s.timer.Stop();
                                decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , 1);
                                btnbiliard.Background = Brushes.Black;
                                if (price > 0)
                                    MessageBox.Show(Person.AddFactor(p.ID, txtbiliard.Text, lblStartbiliard.Content.ToString(), lblEndLivebiliard.Content.ToString(), price, s.ID));
                                else
                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                txtbiliard.Text = "بیلیارد";
                                s.timer.Reset();
                                break;
                            case MessageBoxResult.No:    // No button pressed
                                break;
                            default:                 // Neither Yes nor No pressed (just in case)

                                break;
                        }
                    }
                }
            }
            catch(Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }


        private void btnps5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Systems s = new Systems();
                string content = (sender as Button).Content.ToString();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == content)
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps5.Background == Brushes.Black)
                {
                    lblEndLiveps5.Visibility = Visibility.Visible;
                    lblEndps5.Visibility = Visibility.Collapsed;
                    lblStartps5.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps5;
                    LiveTime.Start();
                    btnps5.Background = Brushes.DarkRed;
                    
                    Controller3Ps5.IsEnabled = true;
                    Controller4Ps5.IsEnabled = true;
                    
                    Controller3Ps5.IsChecked = false;
                    Controller4Ps5.IsChecked = false;
                }
                else
                {
                    if (txtps5.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps5.Text = ReplaceWhitespace(txtps5.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps5.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps5.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps5.Text, 0, 3 , 0 , 0);
                        }
                        int NumberofController = 2;
                        
                        if (Controller3Ps5.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps5.IsChecked == true)
                        {
                            NumberofController = 4;
                        }
                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p,NumberofController).ToString() + "تومن";
                        var title = "بستن تایم!";
                        var result = MessageBox.Show(
                            message,                  // the message to show
                            title,                    // the title for the dialog box
                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                            MessageBoxImage.Question); // show a question mark icon

                        // the following can be handled as if/else statements as well
                        switch (result)
                        {
                            case MessageBoxResult.Yes:   // Yes button pressed

                                lblEndLiveps5.Visibility = Visibility.Collapsed;
                                lblEndps5.Visibility = Visibility.Visible;

                                lblEndps5.Content = DateTime.Now.ToString("HH:mm:ss");
                                s.timer.Stop();
                              decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController);
                                btnps5.Background = Brushes.Black;
                                if (price > 0)
                                    MessageBox.Show(Person.AddFactor(p.ID, txtps5.Text, lblStartps5.Content.ToString(), lblEndLiveps5.Content.ToString(), price, s.ID));
                                else
                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                txtps5.Text = "سیستم5";
                                s.timer.Reset();
                                
                                Controller3Ps5.IsEnabled = true;
                                Controller4Ps5.IsEnabled = true;
                                
                                Controller3Ps5.IsChecked = false;
                                Controller4Ps5.IsChecked = false;
                                break;
                            case MessageBoxResult.No:    // No button pressed
                                break;
                            default:                 // Neither Yes nor No pressed (just in case)

                                break;
                        }
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void btnVIP4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Systems s = new Systems();
                string content = (sender as Button).Content.ToString();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == content)
                    {
                        s = item;
                        break;
                    }
                }
                if (btnVIP4.Background == Brushes.Black)
                {
                    lblEndLiveVIP4.Visibility = Visibility.Visible;
                    lblEndVIP4.Visibility = Visibility.Collapsed;
                    lblStartVIP4.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveVIP4;
                    LiveTime.Start();
                    btnVIP4.Background = Brushes.DarkRed;
                    
                    Controller3VIP4.IsEnabled = true;
                    Controller4VIP4.IsEnabled = true;
                    
                    Controller3VIP4.IsChecked = false;
                    Controller4VIP4.IsChecked = false;
                }
                else
                {
                    if (txtVIP4.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtVIP4.Text = ReplaceWhitespace(txtVIP4.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtVIP4.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtVIP4.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtVIP4.Text, 0, 3 , 0 , 0);
                        }
                        int NumberofController = 2;
                        
                        if (Controller3VIP4.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4VIP4.IsChecked == true)
                        {
                            NumberofController = 4;
                        }
                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p,NumberofController).ToString() + "تومن";
                        var title = "بستن تایم!";
                        var result = MessageBox.Show(
                            message,                  // the message to show
                            title,                    // the title for the dialog box
                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                            MessageBoxImage.Question); // show a question mark icon

                        // the following can be handled as if/else statements as well
                        switch (result)
                        {
                            case MessageBoxResult.Yes:   // Yes button pressed

                                lblEndLiveVIP4.Visibility = Visibility.Collapsed;
                                lblEndVIP4.Visibility = Visibility.Visible;

                                lblEndVIP4.Content = DateTime.Now.ToString("HH:mm:ss");
                                s.timer.Stop();
                                decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController);
                                btnVIP4.Background = Brushes.Black;
                                if (price > 0)
                                    MessageBox.Show(Person.AddFactor(p.ID, txtVIP4.Text, lblStartVIP4.Content.ToString(), lblEndLiveVIP4.Content.ToString(), price, s.ID));
                                else
                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                txtVIP4.Text = "وی ای پی4";
                                s.timer.Reset();
                                
                                Controller3VIP4.IsEnabled = true;
                                Controller4VIP4.IsEnabled = true;
                                
                                Controller3VIP4.IsChecked = false;
                                Controller4VIP4.IsChecked = false;
                                break;
                            case MessageBoxResult.No:    // No button pressed
                                break;
                            default:                 // Neither Yes nor No pressed (just in case)

                                break;
                        }
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void btnVIP5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Systems s = new Systems();
                string content = (sender as Button).Content.ToString();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == content)
                    {
                        s = item;
                        break;
                    }
                }
                if (btnVIP5.Background == Brushes.Black)
                {
                    lblEndLiveVIP5.Visibility = Visibility.Visible;
                    lblEndVIP5.Visibility = Visibility.Collapsed;
                    lblStartVIP5.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveVIP5;
                    LiveTime.Start();
                    btnVIP5.Background = Brushes.DarkRed;
                    
                    Controller3VIP5.IsEnabled = true;
                    Controller4VIP5.IsEnabled = true;
                    
                    Controller3VIP5.IsChecked = false;
                    Controller4VIP5.IsChecked = false;
                }
                else
                {
                    if (txtVIP5.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtVIP5.Text = ReplaceWhitespace(txtVIP5.Text, "");
              
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtVIP5.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtVIP5.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtVIP5.Text, 0, 3 , 0 , 0);
                        }
                        int NumberofController = 2;
                        
                        if (Controller3VIP5.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4VIP5.IsChecked == true)
                        {
                            NumberofController = 4;
                        }
                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController).ToString() + "تومن";
                        var title = "بستن تایم!";
                        var result = MessageBox.Show(
                            message,                  // the message to show
                            title,                    // the title for the dialog box
                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                            MessageBoxImage.Question); // show a question mark icon

                        // the following can be handled as if/else statements as well
                        switch (result)
                        {
                            case MessageBoxResult.Yes:   // Yes button pressed

                                lblEndLiveVIP5.Visibility = Visibility.Collapsed;
                                lblEndVIP5.Visibility = Visibility.Visible;

                                lblEndVIP5.Content = DateTime.Now.ToString("HH:mm:ss");
                                s.timer.Stop();
                                decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController);
                                btnVIP5.Background = Brushes.Black;
                                if (price > 0)
                                    MessageBox.Show(Person.AddFactor(p.ID, txtVIP5.Text, lblStartVIP5.Content.ToString(), lblEndLiveVIP5.Content.ToString(), price, s.ID));
                                else
                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                txtVIP5.Text = "وی ای پی5";
                                s.timer.Reset();
                                
                                Controller3VIP5.IsEnabled = true;
                                Controller4VIP5.IsEnabled = true;
                               
                                Controller3VIP5.IsChecked = false;
                                Controller4VIP5.IsChecked = false;
                                break;
                            case MessageBoxResult.No:    // No button pressed
                                break;
                            default:                 // Neither Yes nor No pressed (just in case)

                                break;
                        }
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void btnps4N1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Systems s = new Systems();
                string content = (sender as Button).Content.ToString();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == content)
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps4N1.Background == Brushes.Black)
                {
                    lblEndLiveps4N1.Visibility = Visibility.Visible;
                    lblEndps4N1.Visibility = Visibility.Collapsed;
                    lblStartps4N1.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps4N1;
                    LiveTime.Start();
                    btnps4N1.Background = Brushes.DarkRed;
                    
                    Controller3Ps4N1.IsEnabled = true;
                    Controller4Ps4N1.IsEnabled = true;
                    
                    Controller3Ps4N1.IsChecked = false;
                    Controller4Ps4N1.IsChecked = false;
                }
                else
                {
                    if (txtps4N1.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        dispatcherTimerps4N1.Stop();
                        txtps4N1.Text = ReplaceWhitespace(txtps4N1.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps4N1.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps4N1.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps4N1.Text, 0, 3, 0, 0);
                        }
                            int NumberofController = 2;
                            
                            if (Controller3Ps4N1.IsChecked == true)
                            {
                                NumberofController = 3;
                            }
                            if (Controller4Ps4N1.IsChecked == true)
                            {
                                NumberofController = 4;
                            }


                            var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController).ToString() + "تومن";
                            var title = "بستن تایم!";
                            var result = MessageBox.Show(
                                message,                  // the message to show
                                title,                    // the title for the dialog box
                                MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                MessageBoxImage.Question); // show a question mark icon

                            // the following can be handled as if/else statements as well
                            switch (result)
                            {
                                case MessageBoxResult.Yes:   // Yes button pressed
                                    lblEndLiveps4N1.Visibility = Visibility.Collapsed;
                                    lblEndps4N1.Visibility = Visibility.Visible;
                                    lblEndps4N1.Content = DateTime.Now.ToString("HH:mm:ss");
                                    s.timer.Stop();
                                   decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController);
                                    btnps4N1.Background = Brushes.Black;
                                    if(price > 0)
                                        MessageBox.Show(Person.AddFactor(p.ID, txtps4N1.Text, lblStartps4N1.Content.ToString(), lblEndLiveps4N1.Content.ToString(), price, s.ID));
                                    else
                                        MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                    txtps4N1.Text = "سیستم1";
                                    s.timer.Reset();
                                    
                                    Controller3Ps4N1.IsEnabled = true;
                                    Controller4Ps4N1.IsEnabled = true;
                                    
                                    Controller3Ps4N1.IsChecked = false;
                                    Controller4Ps4N1.IsChecked = false;
                                    break;
                                case MessageBoxResult.No:    // No button pressed
                                    break;
                                default:                 // Neither Yes nor No pressed (just in case)
                                    break;
                            }

                        }
                    }
                
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void btnps4N2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Systems s = new Systems();
                string content = (sender as Button).Content.ToString();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == content)
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps4N2.Background == Brushes.Black)
                {
                    lblEndLiveps4N2.Visibility = Visibility.Visible;
                    lblEndps4N2.Visibility = Visibility.Collapsed;
                    lblStartps4N2.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps4N2;
                    LiveTime.Start();
                    btnps4N2.Background = Brushes.DarkRed;
                    
                    Controller3Ps4N2.IsEnabled = true;
                    Controller4Ps4N2.IsEnabled = true;
                    
                    Controller3Ps4N2.IsChecked = false;
                    Controller4Ps4N2.IsChecked = false;
                }
                else
                {
                    if (txtps4N2.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps4N2.Text = ReplaceWhitespace(txtps4N2.Text , "");

                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps4N2.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps4N2.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps4N2.Text, 0, 3 , 0 , 0);
                        }
                        int NumberofController = 2;
                        
                        if (Controller3Ps4N2.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps4N2.IsChecked == true)
                        {
                            NumberofController = 4;
                        }
                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController).ToString() + "تومن";
                        var title = "بستن تایم!";
                        var result = MessageBox.Show(
                            message,                  // the message to show
                            title,                    // the title for the dialog box
                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                            MessageBoxImage.Question); // show a question mark icon

                        // the following can be handled as if/else statements as well
                        switch (result)
                        {
                            case MessageBoxResult.Yes:   // Yes button pressed
                                lblEndLiveps4N2.Visibility = Visibility.Collapsed;
                                lblEndps4N2.Visibility = Visibility.Visible;
                                lblEndps4N2.Content = DateTime.Now.ToString("HH:mm:ss");
                                s.timer.Stop();
                               decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController);
                                btnps4N2.Background = Brushes.Black;
                                if (price > 0)
                                    MessageBox.Show(Person.AddFactor(p.ID, txtps4N2.Text, lblStartps4N2.Content.ToString(), lblEndLiveps4N2.Content.ToString(), price, s.ID));
                                else
                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                txtps4N2.Text = "سیستم2";
                                s.timer.Reset();
                               
                                Controller3Ps4N2.IsEnabled = true;
                                Controller4Ps4N2.IsEnabled = true;
                                
                                Controller3Ps4N2.IsChecked = false;
                                Controller4Ps4N2.IsChecked = false;
                                break;
                            case MessageBoxResult.No:    // No button pressed
                                break;
                            default:                 // Neither Yes nor No pressed (just in case)

                                break;
                        }
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void btnps4N4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Systems s = new Systems();
                string content = (sender as Button).Content.ToString();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == content)
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps4N4.Background == Brushes.Black)
                {
                    lblEndLiveps4N4.Visibility = Visibility.Visible;
                    lblEndps4N4.Visibility = Visibility.Collapsed;
                    lblStartps4N4.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps4N4;
                    LiveTime.Start();
                    btnps4N4.Background = Brushes.DarkRed;
                    
                    Controller3Ps4N4.IsEnabled = true;
                    Controller4Ps4N4.IsEnabled = true;
                    
                    Controller3Ps4N4.IsChecked = false;
                    Controller4Ps4N4.IsChecked = false;
                }
                else
                {
                    if (txtps4N4.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps4N4.Text = ReplaceWhitespace(txtps4N4.Text , "");

                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps4N4.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps4N4.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps4N4.Text, 0, 3 , 0 , 0);
                        }
                        int NumberofController = 2;
                        
                        if (Controller3Ps4N4.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps4N4.IsChecked == true)
                        {
                            NumberofController = 4;
                        }
                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController).ToString() + "تومن";
                        var title = "بستن تایم!";
                        var result = MessageBox.Show(
                            message,                  // the message to show
                            title,                    // the title for the dialog box
                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                            MessageBoxImage.Question); // show a question mark icon

                        // the following can be handled as if/else statements as well
                        switch (result)
                        {
                            case MessageBoxResult.Yes:   // Yes button pressed

                                lblEndLiveps4N4.Visibility = Visibility.Collapsed;
                                lblEndps4N4.Visibility = Visibility.Visible;

                                lblEndps4N4.Content = DateTime.Now.ToString("HH:mm:ss");
                                s.timer.Stop();
                                decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController);
                                btnps4N4.Background = Brushes.Black;
                                if (price > 0)
                                    MessageBox.Show(Person.AddFactor(p.ID, txtps4N4.Text, lblStartps4N4.Content.ToString(), lblEndLiveps4N4.Content.ToString(), price, s.ID));
                                else
                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                txtps4N4.Text = "سیستم4";
                                s.timer.Reset();
                                
                                Controller3Ps4N4.IsEnabled = true;
                                Controller4Ps4N4.IsEnabled = true;
                               
                                Controller3Ps4N4.IsChecked = false;
                                Controller4Ps4N4.IsChecked = false;
                                break;
                            case MessageBoxResult.No:    // No button pressed
                                break;
                            default:                 // Neither Yes nor No pressed (just in case)

                                break;
                        }
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private void btnps4N3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Systems s = new Systems();
                string content = (sender as Button).Content.ToString();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == content)
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps4N3.Background == Brushes.Black)
                {
                    lblEndLiveps4N3.Visibility = Visibility.Visible;
                    lblEndps4N3.Visibility = Visibility.Collapsed;
                    lblStartps4N3.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps4N3;
                    LiveTime.Start();
                    btnps4N3.Background = Brushes.DarkRed;
                    
                    Controller3Ps4N3.IsEnabled = true;
                    Controller4Ps4N3.IsEnabled = true;
                    
                    Controller3Ps4N3.IsChecked = false;
                    Controller4Ps4N3.IsChecked = false;
                }
                else
                {
                    if (txtps4N3.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps4N3.Text = ReplaceWhitespace(txtps4N3.Text, "");

                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps4N3.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps4N3.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps4N3.Text, 0, 3 , 0 , 0);
                        }
                        int NumberofController = 2;
                        
                        if (Controller3Ps4N3.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps4N3.IsChecked == true)
                        {
                            NumberofController = 4;
                        }
                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController).ToString() + "تومن";
                        var title = "بستن تایم!";
                        var result = MessageBox.Show(
                            message,                  // the message to show
                            title,                    // the title for the dialog box
                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                            MessageBoxImage.Question); // show a question mark icon

                        // the following can be handled as if/else statements as well
                        switch (result)
                        {
                            case MessageBoxResult.Yes:   // Yes button pressed

                                lblEndLiveps4N3.Visibility = Visibility.Collapsed;
                                lblEndps4N3.Visibility = Visibility.Visible;

                                lblEndps4N3.Content = DateTime.Now.ToString("HH:mm:ss");
                                s.timer.Stop();
                               decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p , NumberofController);
                                btnps4N3.Background = Brushes.Black;
                                if (price > 0)
                                    MessageBox.Show(Person.AddFactor(p.ID, txtps4N3.Text, lblStartps4N3.Content.ToString(), lblEndLiveps4N3.Content.ToString(), price, s.ID));
                                else
                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                txtps4N3.Text = "سیستم3";
                                s.timer.Reset();
                                
                                Controller3Ps4N3.IsEnabled = true;
                                Controller4Ps4N3.IsEnabled = true;
                                
                                Controller3Ps4N3.IsChecked = false;
                                Controller4Ps4N3.IsChecked = false;
                                break;
                            case MessageBoxResult.No:    // No button pressed
                                break;
                            default:                 // Neither Yes nor No pressed (just in case)

                                break;
                        }
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private decimal CalculatePrice(string time , Systems s ,Persons p , int Controller)
        {
            String[] spearator = { ":" };
            // using the method 
            String[] strlist = time.Split(spearator,
               StringSplitOptions.RemoveEmptyEntries);
            double Hour = Convert.ToDouble(strlist[0]);
            double Min = Convert.ToDouble(strlist[1]);
            decimal pricebyhourcurrent = s.PriceByHour;
            switch (Controller)
            {
                case 2:
                    if (p.Grade == 1)
                    {
                        return Math.Round(Convert.ToDecimal(((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))) / 2,
                                                   MidpointRounding.ToEven);
                    }
                    else if (p.Grade == 2)
                    {
                        return Math.Round(Convert.ToDecimal(((((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))) / 3) * 2),
                                                   MidpointRounding.ToEven);
                    }
                    else
                    {
                        return Math.Round(Convert.ToDecimal(((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))),
                                                   MidpointRounding.ToEven);
                    }
                case 3:
                    pricebyhourcurrent += 15000;
                    if (p.Grade == 1)
                    {
                        return Math.Round(Convert.ToDecimal(((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))) / 2,
                                                   MidpointRounding.ToEven);
                    }
                    else if (p.Grade == 2)
                    {
                        return Math.Round(Convert.ToDecimal(((((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))) / 3) * 2),
                                                   MidpointRounding.ToEven);
                    }
                    else
                    {
                        return Math.Round(Convert.ToDecimal(((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))),
                                                   MidpointRounding.ToEven);
                    }
                    break;
                case 4:
                    pricebyhourcurrent += 30000;
                    if (p.Grade == 1)
                    {
                        return Math.Round(Convert.ToDecimal(((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))) / 2,
                                                   MidpointRounding.ToEven);
                    }
                    else if (p.Grade == 2)
                    {
                        return Math.Round(Convert.ToDecimal(((((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))) / 3) * 2),
                                                   MidpointRounding.ToEven);
                    }
                    else
                    {
                        return Math.Round(Convert.ToDecimal(((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))),
                                                   MidpointRounding.ToEven);
                    }
                    break;
                default:
                    if (p.Grade == 1)
                    {
                        return Math.Round(Convert.ToDecimal(((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))) / 2,
                                                   MidpointRounding.ToEven);
                    }
                    else if (p.Grade == 2)
                    {
                        return Math.Round(Convert.ToDecimal(((((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))) / 3) * 2),
                                                   MidpointRounding.ToEven);
                    }
                    else
                    {
                        return Math.Round(Convert.ToDecimal(((Min / 60) * Convert.ToDouble(pricebyhourcurrent)) + (Hour * Convert.ToDouble(pricebyhourcurrent))),
                                                   MidpointRounding.ToEven);
                    }
                
            }
            
        }
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        private static readonly Regex cot = new Regex(@":");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
        public static string Replacecot(string input, string replacement)
        {
            return cot.Replace(input, replacement);
        }

        private void DtgFactorKol_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Key == Key.Enter)
                {

                    e.Handled = true;
                    DtgFactor.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmitCafe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string NameCofee = "";
                char[] spearator = { ' ' };
                decimal price;
                Index = (ListCafe.SelectedItem.ToString()).Split(spearator,
               StringSplitOptions.RemoveEmptyEntries).Length - 1;
                CurrentPrice = Convert.ToDecimal((ListCafe.SelectedItem.ToString()).Split(spearator,
               StringSplitOptions.RemoveEmptyEntries)[Index]);
                for (int i = 0; i < Index; i++)
                {
                    NameCofee += (ListCafe.SelectedItem.ToString()).Split(spearator,
               StringSplitOptions.RemoveEmptyEntries)[i];
                }
                if (txtCustumerName.Text == "")
                {
                    MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else if (CurrentPrice == 0) {
                    MessageBox.Show("کالایی انتخاب کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                else
                {
                    txtCustumerName.Text = ReplaceWhitespace(txtCustumerName.Text, "");
                    Persons p = new Persons();
                    bool Valid = true;
                    int ids = 0;
                    foreach (var item in PersonDataAccess.AllPersons)
                    {
                        if (item.Name == txtCustumerName.Text)
                        {
                            switch (item.Grade)
                            {
                                case 1:
                                    price = (CurrentPrice * Convert.ToInt32(txtNumberCafe.Text)) / 2;
                                    break;
                                case 2:
                                    price = ((CurrentPrice * Convert.ToInt32(txtNumberCafe.Text)) / 4) * 3;
                                    break;
                                case 3:
                                    price = CurrentPrice * Convert.ToInt32(txtNumberCafe.Text);
                                    break;
                                default:
                                    price = CurrentPrice * Convert.ToInt32(txtNumberCafe.Text);
                                    break;
                            }
                            foreach (var item1 in PersonDataAccess.AllSystems)
                            {
                                if (item1.Name == "کافه")
                                    ids = item1.ID;
                            }
                            MessageBox.Show(Person.AddFactor(item.ID, txtCustumerName.Text, NameCofee, "", price, ids));

                            Valid = false;
                            break;
                        }
                    }
                    if (Valid)
                    {
                        p.ID = PersonDataAccess.IDP;
                        p.Grade = 3;
                        p.Name = txtCustumerName.Text;
                        p.PhoneNumber = 0;
                        Person.AddMember(PersonDataAccess.IDP, txtCustumerName.Text, 0, 3 , 0 , 0);
                        switch (p.Grade)
                        {
                            case 1:
                                price = (CurrentPrice * Convert.ToInt32(txtNumberCafe.Text)) / 2;
                                break;
                            case 2:
                                price = ((CurrentPrice * Convert.ToInt32(txtNumberCafe.Text)) / 4) * 3;
                                break;
                            case 3:
                                price = CurrentPrice * Convert.ToInt32(txtNumberCafe.Text);
                                break;
                            default:
                                price = CurrentPrice * Convert.ToInt32(txtNumberCafe.Text);
                                break;
                        }
                        foreach (var item in PersonDataAccess.AllSystems)
                        {
                            if (item.Name == "کافه")
                                ids = item.ID;
                        }
                        MessageBox.Show(Person.AddFactor(p.ID, txtCustumerName.Text, NameCofee, "", price, ids));

                    }
                    
                    txtCustumerName.Text = "";

                }
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void ListCafe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

      
        private void btnDeleteCafe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var connstr = "Server=localhost;Uid=root;Pwd=S13791381;database=centerbergamo";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    string NameCofee = "";
                    char[] spearator = { ' ' };
                    Index = (ListCafe.SelectedItem.ToString()).Split(spearator,
                   StringSplitOptions.RemoveEmptyEntries).Length - 1;
                    CurrentPrice = Convert.ToDecimal((ListCafe.SelectedItem.ToString()).Split(spearator,
                   StringSplitOptions.RemoveEmptyEntries)[Index]);
                    for (int i = 0; i < Index; i++)
                    {
                        NameCofee += (ListCafe.SelectedItem.ToString()).Split(spearator,
                   StringSplitOptions.RemoveEmptyEntries)[i];
                    }
                    string cmdText = "delete from Cafe where Name = @Name;";
                    MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@Name", NameCofee);
                    cmd.ExecuteNonQuery();
                }
                PersonDataAccess.Mylist.Remove(ListCafe.SelectedItem.ToString());
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void ButtonAddCafe_Click(object sender, RoutedEventArgs e)
        {
            ListCafe.Visibility = Visibility.Hidden;
            dockcafe.Visibility = Visibility.Hidden;
            dockAddCafe.Visibility = Visibility.Visible;
        }

        private void btnSubmitAddCafe_Click(object sender, RoutedEventArgs e)
        {
            if(txtProductName.Text == "")
            {
                MessageBox.Show("اسم کالا را انتخاب کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (txtPriceCafe.Text == "")
            {
                MessageBox.Show("قیمت کالا را انتخاب کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string Result = txtProductName.Text + " " + txtPriceCafe.Text;
                var connstr = "Server=localhost;Uid=root;Pwd=S13791381;database=centerbergamo";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();

                    string cmdText = "insert into cafe(Name , Price) values(@Name , @Value);";
                    MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@Name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@Value", Convert.ToDecimal(txtPriceCafe.Text));
                    cmd.ExecuteNonQuery();
                            
    }
                PersonDataAccess.Mylist.Add(Result);
                txtProductName.Text = "";
                txtPriceCafe.Text = "";
                dockAddCafe.Visibility = Visibility.Hidden;
                ListCafe.Visibility = Visibility.Visible;
                dockcafe.Visibility = Visibility.Visible;
            }
        }


    
        private string _currentInput = "";
        private string _currentSuggestion = "";
        private string _currentText = "";

        private int _selectionStart;
        private int _selectionLength;
        private void SuggestionBoxOnTextCafeChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtCustumerName.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtCustumerName.Text = _currentText;
                        txtCustumerName.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        private void SuggestionBoxOnTextBiliardChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtbiliard.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtbiliard.Text = _currentText;
                        txtbiliard.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        private void SuggestionBoxOnTextPs4N1Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtps4N1.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtps4N1.Text = _currentText;
                        txtps4N1.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        private void SuggestionBoxOnTextPs4N2Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtps4N2.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtps4N2.Text = _currentText;
                        txtps4N2.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        private void SuggestionBoxOnTextPs4N3Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtps4N3.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtps4N3.Text = _currentText;
                        txtps4N3.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        private void SuggestionBoxOnTextPs4N4Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtps4N4.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtps4N4.Text = _currentText;
                        txtps4N4.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        private void SuggestionBoxOnTextPs5Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtps5.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtps5.Text = _currentText;
                        txtps5.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        private void SuggestionBoxOnTextVIP4Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtVIP4.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtVIP4.Text = _currentText;
                        txtVIP4.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        private void SuggestionBoxOnTextVIP5Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                var input = txtVIP5.Text;
                if (input.Length > _currentInput.Length && input != _currentSuggestion)
                {
                    _currentSuggestion = PersonDataAccess.NameOfPerson.FirstOrDefault(x => x.StartsWith(input));
                    if (_currentSuggestion != null)
                    {
                        _currentText = _currentSuggestion;
                        _selectionStart = input.Length;
                        _selectionLength = _currentSuggestion.Length - input.Length;

                        txtVIP5.Text = _currentText;
                        txtVIP5.Select(_selectionStart, _selectionLength);
                    }
                }
                _currentInput = input;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        

        private void Controller3Ps4N1_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Controller4Ps4N1.IsChecked = false;
                
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        private void Controller4Ps4N1_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller3Ps4N1.IsChecked = false;
                
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }
        

        private void Controller3Ps4N2_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Controller4Ps4N2.IsChecked = false;
                
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        private void Controller4Ps4N2_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller3Ps4N2.IsChecked = false;
                
                
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        

        private void Controller3Ps4N3_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Controller4Ps4N3.IsChecked = false;
                
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        private void Controller4Ps4N3_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
            
                Controller3Ps4N3.IsChecked = false;
               
          
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        

        private void Controller3Ps4N4_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
   
                Controller4Ps4N4.IsChecked = false;
 
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        private void Controller4Ps4N4_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller3Ps4N4.IsChecked = false;
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        

        private void Controller3Ps5_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Controller4Ps5.IsChecked = false;
                
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        private void Controller4Ps5_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
           
                Controller3Ps5.IsChecked = false;
               
       
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        

        private void Controller3VIP4_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Controller4VIP4.IsChecked = false;
               
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        private void Controller4VIP4_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
     
                Controller3VIP4.IsChecked = false;
            
               
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        

        private void Controller3VIP5_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Controller4VIP5.IsChecked = false;
                
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        private void Controller4VIP5_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                Controller3VIP5.IsChecked = false;
                
             
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }

        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {
            PersonDataAccess.AllPersons.Clear();
            dtgShowMembers.Items.Refresh();
            foreach (var item in PersonDataAccess.AllPersonsCopy)
            {
                bool val = true;
                foreach (var item2 in PersonDataAccess.AllPersons)
                {
                    if (item == item2)

                    {
                        val = false;
                        break;
                    }
                }
                if (val)
                    PersonDataAccess.AllPersons.Add(item);
            }
            ListCafe.Visibility = Visibility.Visible;
            GridBody.Visibility = Visibility.Visible;
            GridFooter.Visibility = Visibility.Visible; 
            DtgFactor.Visibility = Visibility.Visible;
            btnTasvie.Visibility = Visibility.Visible;
            DtgFactorKol.Visibility = Visibility.Visible;
            GridShowFactors.Visibility = Visibility.Hidden;
            GridShowMembers.Visibility = Visibility.Hidden; 
            GridBorrow.Visibility = Visibility.Hidden;
            GridAddMember.Visibility = Visibility.Hidden; 
            dockcafe.Visibility = Visibility.Visible;
            dockAddCafe.Visibility = Visibility.Hidden;
        }

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {
            PersonDataAccess.AllPersons.Clear();
            dtgShowMembers.Items.Refresh();
            foreach (var item in PersonDataAccess.AllPersonsCopy)
            {
                bool val = true;
                foreach (var item2 in PersonDataAccess.AllPersons)
                {
                    if (item == item2)

                    {
                        val = false;
                        break;
                    }
                }
                if (val)
                    PersonDataAccess.AllPersons.Add(item);
            }
            btnTasvie.Visibility = Visibility.Hidden;
            GridBody.Visibility = Visibility.Hidden;
            GridShowFactors.Visibility = Visibility.Hidden;
            DtgFactor.Visibility = Visibility.Hidden;
            DtgFactorKol.Visibility = Visibility.Hidden;
            GridFooter.Visibility = Visibility.Visible;
            GridShowMembers.Visibility = Visibility.Hidden;
            GridBorrow.Visibility = Visibility.Hidden;
            GridAddMember.Visibility = Visibility.Visible;
        }

        private void btnSubmitAddMember_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtNameAddMember.Text == "")
                {
                    MessageBox.Show(" نام مشتری را وارد کنید", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtNameAddMember.BorderBrush = Brushes.Red;
                }
                else
                {
                    
                    if(txtGardeAddMember.Text == "" || Convert.ToInt32(txtGardeAddMember.Text) < 1 || Convert.ToInt32(txtGardeAddMember.Text) > 3)
                    {
                        txtGardeAddMember.Text = "3";
                    }
                    if(txtPhoneAddMember.Text == "")
                    {
                        txtPhoneAddMember.Text = "0";
                    }
                    txtNameAddMember.Text = ReplaceWhitespace(txtNameAddMember.Text, "");
                    int val = 1;
                    foreach (var item in PersonDataAccess.AllPersons)
                    {
                        if(item.Name == txtNameAddMember.Text)
                        {
                            val = 2;
                            MessageBox.Show("مشتری از قبل وجود دارد", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                    }
                    if (val == 1)
                    {
                        Person.AddMember(PersonDataAccess.IDP, txtNameAddMember.Text, Convert.ToUInt64(txtPhoneAddMember.Text), Convert.ToInt32(txtGardeAddMember.Text) , 0, 0);
                        MessageBox.Show("کاربر اضافه شد", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    txtGardeAddMember.Text = "";
                    txtNameAddMember.Text = "";
                    txtPhoneAddMember.Text = "";
                }
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void btnShowFactors_Click(object sender, RoutedEventArgs e)
        {
            PersonDataAccess.AllPersons.Clear();
            foreach (var item in PersonDataAccess.AllPersonsCopy)
            {
                PersonDataAccess.AllPersons.Add(item);
            }
            btnTasvie.Visibility = Visibility.Hidden;

            GridBody.Visibility = Visibility.Hidden;
            GridShowFactors.Visibility = Visibility.Visible;
            GridShowMembers.Visibility = Visibility.Hidden;
            GridAddMember.Visibility = Visibility.Hidden;
            GridBorrow.Visibility = Visibility.Hidden;
            GridFooter.Visibility = Visibility.Hidden;
            DtgShowFactorKol.Visibility = Visibility.Hidden;
            DtgShowFactor.Visibility = Visibility.Hidden;

        }

        private void btnShowAllFinallFactors_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<FactoKol> GeneralInvoices = new ObservableCollection<FactoKol>();
            DtgShowFactorKol.Visibility = Visibility.Visible;
            DtgShowFactor.Visibility = Visibility.Hidden;
            var connstr = "Server=localhost;Uid=root;Pwd=S13791381;database=centerbergamo";
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select IDPerson , Name , sum(Price) from partial_invoices group by IDPerson;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ii = reader.FieldCount;
                            string[] Values = new string[3];
                            for (int i = 0; i < ii; i++)
                            {
                                if (reader[i] is DBNull) { }
                                    
                                else
                                {
                                    string s = reader[i].ToString();
                                    Values[i] = s;
                                }
                            }
                            FactoKol f = new FactoKol(Convert.ToInt32(Values[0]) , Values[1] ,0, Convert.ToDecimal(Values[2]));
                            GeneralInvoices.Add(f);
                        }
                    }
                }
            }
                    DtgShowFactorKol.ItemsSource = GeneralInvoices;
        }

        private void btnShowAllFactors_Click(object sender, RoutedEventArgs e)
        {
            DtgShowFactorKol.Visibility = Visibility.Hidden;
            DtgShowFactor.Visibility = Visibility.Visible;

            ObservableCollection<ProductOfFactor> partialinvoices = new ObservableCollection<ProductOfFactor>();
            var connstr = "Server=localhost;Uid=root;Pwd=S13791381;database=centerbergamo";
            using (var conn = new MySqlConnection(connstr))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select * from partial_invoices;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ii = reader.FieldCount;
                            string[] Values = new string[7];
                            for (int i = 0; i < ii; i++)
                            {
                                if (reader[i] is DBNull) { }

                                else
                                {
                                    string s = reader[i].ToString();
                                    Values[i] = s;
                                }
                            }
                            ProductOfFactor f = new ProductOfFactor(Convert.ToInt32(Values[0]), Convert.ToInt32(Values[1]), Values[3],Values[4] , Values[5] , Convert.ToDecimal(Values[6]), Convert.ToInt32(Values[2]));
                            partialinvoices.Add(f);
                        }
                    }
                }
            }
            DtgShowFactor.ItemsSource = partialinvoices;
        }

        


        private void btnShowMembers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                
                dtgShowMembers.Items.Refresh();
                GridBody.Visibility = Visibility.Hidden;
                GridFooter.Visibility = Visibility.Visible;
                btnTasvie.Visibility = Visibility.Hidden;
                DtgFactor.Visibility = Visibility.Hidden;
                DtgFactorKol.Visibility = Visibility.Hidden;
                GridShowFactors.Visibility = Visibility.Hidden;
                GridAddMember.Visibility = Visibility.Hidden;
                GridBorrow.Visibility = Visibility.Hidden;
                GridShowMembers.Visibility = Visibility.Visible;
                PersonDataAccess.AllPersons.Clear();
                dtgShowMembers.Items.Refresh();
                foreach (var item in PersonDataAccess.AllPersonsCopy)
                {
                    bool val = true;
                    foreach (var item2 in PersonDataAccess.AllPersons)
                    {
                        if (item == item2)

                        {
                            val = false;
                            break;
                        }
                    }
                    if(val)
                        PersonDataAccess.AllPersons.Add(item);
                }
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void btnBorrow_Click(object sender, RoutedEventArgs e)
        {
            btnTasvie.Visibility = Visibility.Hidden;
            GridBody.Visibility = Visibility.Hidden;
            GridFooter.Visibility = Visibility.Visible;
            DtgFactor.Visibility = Visibility.Hidden;
            DtgFactorKol.Visibility = Visibility.Hidden;
            GridShowFactors.Visibility = Visibility.Hidden;
            GridAddMember.Visibility = Visibility.Hidden;
            GridShowMembers.Visibility = Visibility.Hidden;
            GridBorrow.Visibility = Visibility.Visible;
            hour12.IsChecked = false;
            hour12.IsEnabled = true;
            hour24.IsChecked = false;
            hour24.IsEnabled = true;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtNameBorrow.Text == "")
                {
                    MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                if (txtCartMashin.IsChecked == false && txtCartMeli.IsChecked == false && txtShenasname.IsChecked == false)
                {
                    MessageBox.Show("ضمانت را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                if(hour12.IsChecked == true)
                {
                    string whatborrow = "";
                    if(txtCartMashin.IsChecked == true)
                    {
                        whatborrow = "کارت ماشین";
                    }
                    if (txtShenasname.IsChecked == true)
                    {
                        whatborrow = "شناسنامه";
                    }
                    if (txtCartMeli.IsChecked == true)
                    {
                        whatborrow = "کارت ملی";
                    }
                    PersonDataAccess.IDB++;
                    PersonsBorrow p = new PersonsBorrow(PersonDataAccess.IDB, txtNameBorrow.Text , 12 , whatborrow, DateTime.Now.ToString("HH:mm:ss tt") , DateTime.Today.ToString("yyyy-MM-dd") , txtAddress.Text ,txtPhoneNumber.Text);
                    PersonDataAccess.Borrows.Add(p);
                }
                else
                {
                    string whatborrow = "";
                    if (txtCartMashin.IsChecked == true)
                    {
                        whatborrow = "کارت ماشین";
                    }
                    if (txtShenasname.IsChecked == true)
                    {
                        whatborrow = "شناسنامه";
                    }
                    if (txtCartMeli.IsChecked == true)
                    {
                        whatborrow = "کارت ملی";
                    }
                    PersonDataAccess.IDB++;
                    PersonsBorrow p = new PersonsBorrow(PersonDataAccess.IDB, txtNameBorrow.Text, 24, whatborrow, DateTime.Now.ToString("HH:mm:ss tt"), DateTime.Today.ToString("yyyy-MM-dd"), txtAddress.Text, txtPhoneNumber.Text);
                    PersonDataAccess.Borrows.Add(p);

                }
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);

            }
        }

        private void hour12_Checked(object sender, RoutedEventArgs e)
        {
            hour24.IsChecked = false;
            hour24.IsEnabled = false;
        }

        private void hour24_Checked(object sender, RoutedEventArgs e)
        {
            hour12.IsChecked = false;
            hour12.IsEnabled = false;
        }

        private void DtgFactorKol_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                decimal totall = 0;
                TextBox t = e.EditingElement as TextBox;
                string editedCellValue = t.Text.ToString();
                FactoKol customer = (FactoKol)(sender as DataGrid).SelectedItem;
                foreach (var item in PersonDataAccess.ListOfinvoicesFinall)
                {
                    if (customer.IDPerson == item.IDPerson)
                    {
                        if (Convert.ToDecimal(editedCellValue) <= item.FinallyPrice)
                        {
                            
                            item.Payment = 0;
                            totall = Convert.ToDecimal(editedCellValue);
                            foreach (var item2 in PersonDataAccess.AllPersons)
                            {
                                if (item.IDPerson == item2.ID)
                                {

                                    item2.Debt -= totall;
                                    break;
                                }
                            }
                            
                            PersonDataAccess.ListOfinvoicesFinall.Remove(item);

                        }
                        else
                        {
                            MessageBox.Show("پرداختی بیشتر از حساب روز است", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        
                        break;
                    }
                }
                
                
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }

            

        }

        private void DtgFactorKol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dtgShowMembers.Items.Refresh();
        }

        private void dtgShowMembers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                TextBox t = e.EditingElement as TextBox;
                string editedCellValue = t.Text.ToString();
                Persons customer = (Persons)(sender as DataGrid).SelectedItem;
                foreach (var item in PersonDataAccess.AllPersons)
                {
                    if(item.ID == customer.ID)
                    {
                        item.Debt = Convert.ToDecimal(editedCellValue);
                        break;
                    }
                }
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var message = "رفتی؟";
                var title = "بستن برنامه!";
                var result = MessageBox.Show(
                    message,                  // the message to show
                    title,                    // the title for the dialog box
                    MessageBoxButton.YesNo,  // show two buttons: Yes and No
                    MessageBoxImage.Question); // show a question mark icon

                // the following can be handled as if/else statements as well
                switch (result)
                {
                    case MessageBoxResult.Yes:   // Yes button pressed
                        PersonDataAccess.PushData();
                        MessageBox.Show("میموندی باش ور میرفتی");
                        Close();
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        PersonDataAccess.PushData();
                        MessageBox.Show("میموندی باش ور میرفتی");
                        Close();
                        break;
                }
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            PersonDataAccess.AllPersons.Clear();
            foreach (var item in PersonDataAccess.AllPersonsCopy)
                {
                    bool val = true;
                    foreach (var item2 in PersonDataAccess.AllPersons)
                    {
                        if (item == item2)

                        {
                            val = false;
                            break;
                        }
                    }
                    if (val)
                    {
                        if (item.Name.ToString().Contains(txtsearch.Text))
                            PersonDataAccess.AllPersons.Add(item);
                    }
                }
            dtgShowMembers.Items.Refresh();
        }

        private void txtdelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtgShowMembers.SelectedItem == null)
                {
                    MessageBox.Show("کاربر مورد نظر را انتخاب کن", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string name = (dtgShowMembers.SelectedItem as Persons).Name;
                    name = ReplaceWhitespace(name, "");
                    foreach (var item in PersonDataAccess.ListOfinvoices.ToList())
                    {
                        if (item.Name == name)
                            PersonDataAccess.ListOfinvoices.Remove(item);
                    }
                    PersonDataAccess.NameOfPerson.Remove(name);
                    var connstr = "Server=localhost;Uid=root;Pwd=S13791381;database=centerbergamo";
                    using (var conn = new MySqlConnection(connstr))
                    {
                        conn.Open();

                        
                        string cmdText = "DELETE FROM persons WHERE Name = @Name;";
                        MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.ExecuteNonQuery();
                                
                    }
                            foreach (var item in PersonDataAccess.AllPersonsCopy.ToList())
                    {
                        if (item.Name == name)
                        {
                            PersonDataAccess.AllPersonsCopy.Remove(item);
                        }
                    }
                    foreach (var item in PersonDataAccess.AllPersons.ToList())
                    {
                        if (item.Name == name)
                        {
                            PersonDataAccess.AllPersons.Remove(item);
                        }
                    }
                }
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void btnsum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblsum.Content = PersonDataAccess.sumtoday;
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void btnTotallSum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Values = "";
                var connstr = "Server=localhost;Uid=root;Pwd=S13791381;database=centerbergamo";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        

                        cmd.CommandText = "select sum(price) from partial_invoices;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ii = reader.FieldCount;
                                for (int i = 0; i < ii; i++)
                                {
                                    if (reader[i] is DBNull)
                                        ;
                                    else
                                    {
                                        Values = reader[i].ToString();
                                    }
                                }
                            }

                        }
                    }
                }
                MessageBox.Show(Values, "فروش کل :", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        private void btnTasvie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (DtgFactorKol.SelectedItem == null)
                {
                    MessageBox.Show("فاکتور مورد نظر را انتخاب کن", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    FactoKol F = (DtgFactorKol.SelectedItem as FactoKol);
                    if (F == null)
                    {
                        DataRowView dataRow = (DataRowView)DtgFactorKol.SelectedItem;
                        for (int i = 0; i < 4; i++)
                        {
                            string cellValue = dataRow.Row.ItemArray[i].ToString();
                            switch (i)
                            {
                                case 0:
                                    F.IDFactors = Convert.ToInt32(cellValue);
                                    break;
                                case 1:
                                    F.Name = cellValue;
                                    break;
                                case 2:
                                    F.FinallyPrice = Convert.ToDecimal(cellValue);
                                    break;
                                case 3:
                                    F.Payment = Convert.ToDecimal(cellValue);
                                    break;
                                default:
                                    break;
                            }
                        }
                        F.Name = ReplaceWhitespace(F.Name, "");
                        foreach (var item in PersonDataAccess.AllPersons.ToList())
                        {
                            if (item.Name == F.Name)
                            {
                                F.IDPerson = item.ID;
                                break;
                            }
                        }
                    }
                    foreach (var item in PersonDataAccess.ListOfinvoicesFinall.ToList())
                    {
                      
                        if (F.IDPerson == item.IDPerson)
                        {

                            item.Payment = item.FinallyPrice;
                            foreach (var item2 in PersonDataAccess.AllPersons.ToList())
                            {
                                if (item.IDPerson == item2.ID)
                                {

                                    item2.Debt -= item.Payment;
                                    break;
                                }
                            }

                            PersonDataAccess.ListOfinvoicesFinall.Remove(item);


                        }
                                
                    }
                }
            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

       
        public void BeginButton_Click(string H , string M)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "بیلیارد")
                    {
                        s = item;
                        break;
                    }
                }
            
                TimeSpan duration = TimeSpan.Parse(H+":"+ M+":0");
                dispatcherTimer.Tick +=dispatcherTimerbiliard_Tick;
                dispatcherTimer.Interval = new TimeSpan(Convert.ToInt32(H), Convert.ToInt32(M), 0);
                dispatcherTimer.Start();
                if (btnbiliard.Background == Brushes.Black)
                {
                    lblEndLivebiliard.Visibility = Visibility.Visible;
                    lblEndbiliard.Visibility = Visibility.Collapsed;
                    lblStartbiliard.Content = DateTime.Now.ToString("HH:mm:ss");
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLive;
                    LiveTime.Start();
                    s.timer.Start();
                    btnbiliard.Background = Brushes.DarkSeaGreen;
                    btnbiliard.IsEnabled = false;
                }


            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void dispatcherTimerbiliard_Tick(object sender, EventArgs e)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "بیلیارد")
                    {
                        s = item;
                        break;
                    }
                }
                Console.Beep(1000 , 2000);
                dispatcherTimer.Tick -= dispatcherTimerbiliard_Tick;

                if (btnbiliard.Background == Brushes.DarkSeaGreen)
                {
                    if (txtbiliard.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtbiliard.Text = ReplaceWhitespace(txtbiliard.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtbiliard.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtbiliard.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtbiliard.Text, 0, 3, 0, 0);
                        }


                        lblEndLivebiliard.Visibility = Visibility.Collapsed;
                        lblEndbiliard.Visibility = Visibility.Visible;

                        lblEndbiliard.Content = DateTime.Now.ToString("HH:mm:ss");
                        s.timer.Stop();
                        decimal price = CalculatePrice(dispatcherTimer.Interval.ToString(), s, p, 1);
                        btnbiliard.Background = Brushes.Black;
                        if (price > 0)
                            MessageBox.Show(Person.AddFactor(p.ID, txtbiliard.Text, lblStartbiliard.Content.ToString(), lblEndLivebiliard.Content.ToString(), price, s.ID));
                        else
                            MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                        txtbiliard.Text = "بیلیارد";
                        s.timer.Reset();
                        dispatcherTimer.Stop();
                    }
                }
                    MessageBox.Show("بیلیارد تمومه", "اتمام تایم", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void BeginButtonps4N1_Click(string H, string M)
        {
            try
            {
          
                TimeSpan duration = TimeSpan.Parse(H + ":" + M + ":0");
                dispatcherTimerps4N1.Tick += dispatcherTimerps4N1_Tick;
                dispatcherTimerps4N1.Interval = new TimeSpan(Convert.ToInt32(H), Convert.ToInt32(M), 0);
                dispatcherTimerps4N1.Start();
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N1")
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps4N1.Background == Brushes.Black)
                {
                    lblEndLiveps4N1.Visibility = Visibility.Visible;
                    lblEndps4N1.Visibility = Visibility.Collapsed;
                    lblStartps4N1.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps4N1;
                    LiveTime.Start();
                    btnps4N1.Background = Brushes.DarkSeaGreen;
                    btnps4N1.IsEnabled = false;
                    Controller3Ps4N1.IsEnabled = true;
                    Controller4Ps4N1.IsEnabled = true;

                    Controller3Ps4N1.IsChecked = false;
                    Controller4Ps4N1.IsChecked = false;
                }

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void dispatcherTimerps4N1_Tick(object sender, EventArgs e)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N1")
                    {
                        s = item;
                        break;
                    }
                }
                Console.Beep(1000, 2000);
                dispatcherTimerps4N1.Tick -= dispatcherTimerps4N1_Tick;
                if (btnps4N1.Background == Brushes.DarkSeaGreen)
                {
                    if (txtps4N1.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps4N1.Text = ReplaceWhitespace(txtps4N1.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps4N1.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps4N1.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps4N1.Text, 0, 3, 0, 0);
                        }
                        int NumberofController = 2;

                        if (Controller3Ps4N1.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps4N1.IsChecked == true)
                        {
                            NumberofController = 4;
                        }


                        
                                lblEndLiveps4N1.Visibility = Visibility.Collapsed;
                                lblEndps4N1.Visibility = Visibility.Visible;
                                lblEndps4N1.Content = DateTime.Now.ToString("HH:mm:ss");
                                s.timer.Stop();
                               decimal price = CalculatePrice(dispatcherTimerps4N1.Interval.ToString(), s, p, NumberofController);
                                btnps4N1.Background = Brushes.Black;
                                if (price > 0)
                                    MessageBox.Show(Person.AddFactor(p.ID, txtps4N1.Text, lblStartps4N1.Content.ToString(), lblEndLiveps4N1.Content.ToString(), price, s.ID));
                                else
                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                txtps4N1.Text = "سیستم1";
                                s.timer.Reset();
                                btnps4N1.IsEnabled = true;

                                Controller3Ps4N1.IsEnabled = true;
                                Controller4Ps4N1.IsEnabled = true;

                                Controller3Ps4N1.IsChecked = false;
                                Controller4Ps4N1.IsChecked = false;
                             
                        

                    }
                }
                MessageBox.Show("دستگاه 1 تمومه", "اتمام تایم", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void BeginButtonps4N2_Click(string H, string M)
        {
            try
            {
                dispatcherTimerps4N2.Tick += dispatcherTimerps4N2_Tick;
                dispatcherTimerps4N2.Interval = new TimeSpan(Convert.ToInt32(H), Convert.ToInt32(M), 0);
                dispatcherTimerps4N2.Start();
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N2")
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps4N2.Background == Brushes.Black)
                {
                    lblEndLiveps4N2.Visibility = Visibility.Visible;
                    lblEndps4N2.Visibility = Visibility.Collapsed;
                    lblStartps4N2.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps4N2;
                    LiveTime.Start();
                    btnps4N2.Background = Brushes.DarkSeaGreen;
                    btnps4N2.IsEnabled = false;
                    Controller3Ps4N2.IsEnabled = true;
                    Controller4Ps4N2.IsEnabled = true;

                    Controller3Ps4N2.IsChecked = false;
                    Controller4Ps4N2.IsChecked = false;
                }

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void dispatcherTimerps4N2_Tick(object sender, EventArgs e)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N2")
                    {
                        s = item;
                        break;
                    }
                }
                Console.Beep(1000, 2000);
                dispatcherTimerps4N2.Tick -= dispatcherTimerps4N2_Tick;
                if (btnps4N2.Background == Brushes.DarkSeaGreen)
                {
                    if (txtps4N2.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps4N2.Text = ReplaceWhitespace(txtps4N2.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps4N2.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps4N2.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps4N2.Text, 0, 3, 0, 0);
                        }
                        int NumberofController = 2;

                        if (Controller3Ps4N2.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps4N2.IsChecked == true)
                        {
                            NumberofController = 4;
                        }



                        lblEndLiveps4N2.Visibility = Visibility.Collapsed;
                        lblEndps4N2.Visibility = Visibility.Visible;
                        lblEndps4N2.Content = DateTime.Now.ToString("HH:mm:ss");
                        s.timer.Stop();
                        decimal price = CalculatePrice(dispatcherTimerps4N2.Interval.ToString(), s, p, NumberofController);
                        btnps4N2.Background = Brushes.Black;
                        if (price > 0)
                            MessageBox.Show(Person.AddFactor(p.ID, txtps4N2.Text, lblStartps4N2.Content.ToString(), lblEndLiveps4N2.Content.ToString(), price, s.ID));
                        else
                            MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                        txtps4N2.Text = "سیستم2";
                        s.timer.Reset();
                        btnps4N2.IsEnabled = true;

                        Controller3Ps4N2.IsEnabled = true;
                        Controller4Ps4N2.IsEnabled = true;
                        Controller3Ps4N2.IsChecked = false;
                        Controller4Ps4N2.IsChecked = false;



                    }
                }
                MessageBox.Show("دستگاه 2 تمومه", "اتمام تایم", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }

        }
        public void BeginButtonps4N3_Click(string H, string M)
        {
            try
            {
                
                dispatcherTimerps4N3.Tick += dispatcherTimerps4N3_Tick;
                dispatcherTimerps4N3.Interval = new TimeSpan(Convert.ToInt32(H), Convert.ToInt32(M), 0);
                dispatcherTimerps4N3.Start();
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N3")
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps4N3.Background == Brushes.Black)
                {
                    lblEndLiveps4N3.Visibility = Visibility.Visible;
                    lblEndps4N3.Visibility = Visibility.Collapsed;
                    lblStartps4N3.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps4N3;
                    LiveTime.Start();
                    btnps4N3.Background = Brushes.DarkSeaGreen;
                    btnps4N3.IsEnabled = false;
                    Controller3Ps4N3.IsEnabled = true;
                    Controller4Ps4N3.IsEnabled = true;
                    Controller3Ps4N3.IsChecked = false;
                    Controller4Ps4N3.IsChecked = false;
                }

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void dispatcherTimerps4N3_Tick(object sender, EventArgs e)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N3")
                    {
                        s = item;
                        break;
                    }
                }
                Console.Beep(1000, 2000);
                dispatcherTimerps4N3.Tick -= dispatcherTimerps4N3_Tick;

                if (btnps4N3.Background == Brushes.DarkSeaGreen)
                {
                    if (txtps4N3.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps4N3.Text = ReplaceWhitespace(txtps4N3.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps4N3.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps4N3.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps4N3.Text, 0, 3, 0, 0);
                        }
                        int NumberofController = 2;

                        if (Controller3Ps4N3.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps4N3.IsChecked == true)
                        {
                            NumberofController = 4;
                        }



                        lblEndLiveps4N3.Visibility = Visibility.Collapsed;
                        lblEndps4N3.Visibility = Visibility.Visible;
                        lblEndps4N3.Content = DateTime.Now.ToString("HH:mm:ss");
                        s.timer.Stop();
                        decimal price = CalculatePrice(dispatcherTimerps4N3.Interval.ToString(), s, p, NumberofController);
                        btnps4N3.Background = Brushes.Black;
                        if (price > 0)
                            MessageBox.Show(Person.AddFactor(p.ID, txtps4N3.Text, lblStartps4N3.Content.ToString(), lblEndLiveps4N3.Content.ToString(), price, s.ID));
                        else
                            MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                        txtps4N3.Text = "سیستم3";
                        s.timer.Reset();
                        btnps4N3.IsEnabled = true;
                        Controller3Ps4N3.IsEnabled = true;
                        Controller4Ps4N3.IsEnabled = true;
                        Controller3Ps4N3.IsChecked = false;
                        Controller4Ps4N3.IsChecked = false;



                    }
                }
                MessageBox.Show("دستگاه 3 تمومه", "اتمام تایم", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void BeginButtonps4N4_Click(string H, string M)
        {
            try
            {
               
                dispatcherTimerps4N4.Tick += dispatcherTimerps4N4_Tick;
                dispatcherTimerps4N4.Interval = new TimeSpan(Convert.ToInt32(H), Convert.ToInt32(M), 0);
                dispatcherTimerps4N4.Start();
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N4")
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps4N4.Background == Brushes.Black)
                {
                    lblEndLiveps4N4.Visibility = Visibility.Visible;
                    lblEndps4N4.Visibility = Visibility.Collapsed;
                    lblStartps4N4.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps4N4;
                    LiveTime.Start();
                    btnps4N4.Background = Brushes.DarkSeaGreen;
                    btnps4N4.IsEnabled = false;
                    Controller3Ps4N4.IsEnabled = true;
                    Controller4Ps4N4.IsEnabled = true;
                    Controller3Ps4N4.IsChecked = false;
                    Controller4Ps4N4.IsChecked = false;
                }

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void dispatcherTimerps4N4_Tick(object sender, EventArgs e)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N4")
                    {
                        s = item;
                        break;
                    }
                }
                Console.Beep(1000, 2000);
                dispatcherTimerps4N4.Tick -= dispatcherTimerps4N4_Tick;

                if (btnps4N4.Background == Brushes.DarkSeaGreen)
                {
                    if (txtps4N4.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps4N4.Text = ReplaceWhitespace(txtps4N4.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps4N4.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps4N4.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps4N4.Text, 0, 3, 0, 0);
                        }
                        int NumberofController = 2;

                        if (Controller3Ps4N4.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps4N4.IsChecked == true)
                        {
                            NumberofController = 4;
                        }



                        lblEndLiveps4N4.Visibility = Visibility.Collapsed;
                        lblEndps4N4.Visibility = Visibility.Visible;
                        lblEndps4N4.Content = DateTime.Now.ToString("HH:mm:ss");
                        s.timer.Stop();
                        decimal price = CalculatePrice(dispatcherTimerps4N4.Interval.ToString(), s, p, NumberofController);
                        btnps4N4.Background = Brushes.Black;
                        if (price > 0)
                            MessageBox.Show(Person.AddFactor(p.ID, txtps4N4.Text, lblStartps4N4.Content.ToString(), lblEndLiveps4N4.Content.ToString(), price, s.ID));
                        else
                            MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                        txtps4N4.Text = "سیستم4";
                        s.timer.Reset();
                        btnps4N4.IsEnabled = true;
                        Controller3Ps4N4.IsEnabled = true;
                        Controller4Ps4N4.IsEnabled = true;
                        Controller3Ps4N4.IsChecked = false;
                        Controller4Ps4N4.IsChecked = false;



                    }
                }
                MessageBox.Show("دستگاه 4 تمومه", "اتمام تایم", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void BeginButtonps5_Click(string H, string M)
        {
            try
            {
               
                dispatcherTimerps5.Tick += dispatcherTimerps5_Tick;
                dispatcherTimerps5.Interval = new TimeSpan(Convert.ToInt32(H), Convert.ToInt32(M), 0);
                dispatcherTimerps5.Start();
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps4N5")
                    {
                        s = item;
                        break;
                    }
                }
                if (btnps5.Background == Brushes.Black)
                {
                    lblEndLiveps5.Visibility = Visibility.Visible;
                    lblEndps5.Visibility = Visibility.Collapsed;
                    lblStartps5.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveps5;
                    LiveTime.Start();
                    btnps5.Background = Brushes.DarkSeaGreen;
                    btnps5.IsEnabled = false;
                    Controller3Ps5.IsEnabled = true;
                    Controller4Ps5.IsEnabled = true;
                    Controller3Ps5.IsChecked = false;
                    Controller4Ps5.IsChecked = false;
                }

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void dispatcherTimerps5_Tick(object sender, EventArgs e)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "Ps5")
                    {
                        s = item;
                        break;
                    }
                }
                Console.Beep(1000, 2000);
                dispatcherTimerps5.Tick -= dispatcherTimerps5_Tick;

                if (btnps5.Background == Brushes.DarkSeaGreen)
                {
                    if (txtps5.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtps5.Text = ReplaceWhitespace(txtps5.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtps5.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtps5.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtps5.Text, 0, 3, 0, 0);
                        }
                        int NumberofController = 2;

                        if (Controller3Ps5.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4Ps5.IsChecked == true)
                        {
                            NumberofController = 4;
                        }



                        lblEndLiveps5.Visibility = Visibility.Collapsed;
                        lblEndps5.Visibility = Visibility.Visible;
                        lblEndps5.Content = DateTime.Now.ToString("HH:mm:ss");
                        s.timer.Stop();
                        decimal price = CalculatePrice(dispatcherTimerps5.Interval.ToString(), s, p, NumberofController);
                        btnps5.Background = Brushes.Black;
                        if (price > 0)
                            MessageBox.Show(Person.AddFactor(p.ID, txtps5.Text, lblStartps5.Content.ToString(), lblEndLiveps5.Content.ToString(), price, s.ID));
                        else
                            MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                        txtps5.Text = "سیستم5";
                        s.timer.Reset();
                        btnps5.IsEnabled = true;
                        Controller3Ps5.IsEnabled = true;
                        Controller4Ps5.IsEnabled = true;
                        Controller3Ps5.IsChecked = false;
                        Controller4Ps5.IsChecked = false;



                    }
                }
                MessageBox.Show("دستگاه 5 تمومه", "اتمام تایم", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void BeginButtonVIP4_Click(string H, string M)
        {
            try
            {
                
                dispatcherTimerVIP4.Tick += dispatcherTimerVIP4_Tick;
                dispatcherTimerVIP4.Interval = new TimeSpan(Convert.ToInt32(H), Convert.ToInt32(M), 0);
                dispatcherTimerVIP4.Start();
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "VIP4")
                    {
                        s = item;
                        break;
                    }
                }
                if (btnVIP4.Background == Brushes.Black)
                {
                    lblEndLiveVIP4.Visibility = Visibility.Visible;
                    lblEndVIP4.Visibility = Visibility.Collapsed;
                    lblStartVIP4.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveVIP4;
                    LiveTime.Start();
                    btnVIP4.Background = Brushes.DarkSeaGreen;
                    btnVIP4.IsEnabled = false;
                    Controller3VIP4.IsEnabled = true;
                    Controller4VIP4.IsEnabled = true;
                    Controller3VIP4.IsChecked = false;
                    Controller4VIP4.IsChecked = false;
                }

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void dispatcherTimerVIP4_Tick(object sender, EventArgs e)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "VIP4")
                    {
                        s = item;
                        break;
                    }
                }
                Console.Beep(1000, 2000);
                dispatcherTimerVIP4.Tick -= dispatcherTimerVIP4_Tick;

                if (btnVIP4.Background == Brushes.DarkSeaGreen)
                {
                    if (txtVIP4.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtVIP4.Text = ReplaceWhitespace(txtVIP4.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtVIP4.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtVIP4.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtVIP4.Text, 0, 3, 0, 0);
                        }
                        int NumberofController = 2;

                        if (Controller3VIP4.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4VIP4.IsChecked == true)
                        {
                            NumberofController = 4;
                        }



                        lblEndLiveVIP4.Visibility = Visibility.Collapsed;
                        lblEndVIP4.Visibility = Visibility.Visible;
                        lblEndVIP4.Content = DateTime.Now.ToString("HH:mm:ss");
                        s.timer.Stop();
                        decimal price = CalculatePrice(dispatcherTimerVIP4.Interval.ToString(), s, p, NumberofController);
                        btnVIP4.Background = Brushes.Black;
                        if (price > 0)
                            MessageBox.Show(Person.AddFactor(p.ID, txtVIP4.Text, lblStartVIP4.Content.ToString(), lblEndLiveVIP4.Content.ToString(), price, s.ID));
                        else
                            MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                        txtVIP4.Text = "وی آی پی4";
                        s.timer.Reset();
                        btnVIP4.IsEnabled = true;
                        Controller3VIP4.IsEnabled = true;
                        Controller4VIP4.IsEnabled = true;
                        Controller3VIP4.IsChecked = false;
                        Controller4VIP4.IsChecked = false;



                    }
                }
                MessageBox.Show("دستگاه وی آی پی 4 تمومه", "اتمام تایم", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void BeginButtonVIP5_Click(string H, string M)
        {
            try
            {
               
                dispatcherTimerVIP5.Tick += dispatcherTimerVIP5_Tick;
                dispatcherTimerVIP5.Interval = new TimeSpan(Convert.ToInt32(H), Convert.ToInt32(M), 0);
                dispatcherTimerVIP5.Start();
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "VIP5")
                    {
                        s = item;
                        break;
                    }
                }
                if (btnVIP5.Background == Brushes.Black)
                {
                    lblEndLiveVIP5.Visibility = Visibility.Visible;
                    lblEndVIP5.Visibility = Visibility.Collapsed;
                    lblStartVIP5.Content = DateTime.Now.ToString("HH:mm:ss");
                    s.timer.Start();
                    DispatcherTimer LiveTime = new DispatcherTimer();
                    LiveTime.Interval = TimeSpan.FromSeconds(1);
                    LiveTime.Tick += TimeNowLiveVIP5;
                    LiveTime.Start();
                    btnVIP5.Background = Brushes.DarkSeaGreen;
                    btnVIP5.IsEnabled = false;
                    Controller3VIP5.IsEnabled = true;
                    Controller4VIP5.IsEnabled = true;
                    Controller3VIP5.IsChecked = false;
                    Controller4VIP5.IsChecked = false;
                }

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
        public void dispatcherTimerVIP5_Tick(object sender, EventArgs e)
        {
            try
            {
                Systems s = new Systems();
                foreach (var item in PersonDataAccess.AllSystems)
                {
                    if (item.Name == "VIP5")
                    {
                        s = item;
                        break;
                    }
                }
                Console.Beep(1000, 2000);
                dispatcherTimerVIP5.Tick -= dispatcherTimerVIP5_Tick;

                if (btnVIP5.Background == Brushes.DarkSeaGreen)
                {
                    if (txtVIP5.Text == "")
                    {
                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        txtVIP5.Text = ReplaceWhitespace(txtVIP5.Text, "");
                        Persons p = new Persons();
                        bool Valid = true;
                        foreach (var item in PersonDataAccess.AllPersons)
                        {
                            if (item.Name == txtVIP5.Text)
                            {
                                p = item;
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            p.ID = PersonDataAccess.IDP;
                            p.Grade = 3;
                            p.Name = txtVIP5.Text;
                            p.PhoneNumber = 0;
                            Person.AddMember(PersonDataAccess.IDP, txtVIP5.Text, 0, 3, 0, 0);
                        }
                        int NumberofController = 2;

                        if (Controller3VIP5.IsChecked == true)
                        {
                            NumberofController = 3;
                        }
                        if (Controller4VIP5.IsChecked == true)
                        {
                            NumberofController = 4;
                        }



                        lblEndLiveVIP5.Visibility = Visibility.Collapsed;
                        lblEndVIP5.Visibility = Visibility.Visible;
                        lblEndVIP5.Content = DateTime.Now.ToString("HH:mm:ss");
                        s.timer.Stop();
                        decimal price = CalculatePrice(dispatcherTimerVIP5.Interval.ToString(), s, p, NumberofController);
                        btnVIP5.Background = Brushes.Black;
                        if (price > 0)
                            MessageBox.Show(Person.AddFactor(p.ID, txtVIP5.Text, lblStartVIP5.Content.ToString(), lblEndLiveVIP5.Content.ToString(), price, s.ID));
                        else
                            MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                        txtVIP5.Text = "وی آی پی5";
                        s.timer.Reset();
                        btnVIP5.IsEnabled = true;
                        Controller3VIP5.IsEnabled = true;
                        Controller4VIP5.IsEnabled = true;
                        Controller3VIP5.IsChecked = false;
                        Controller4VIP5.IsChecked = false;



                    }
                }
                MessageBox.Show("دستگاه وی آی پی 5 تمومه", "اتمام تایم", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }


        private void btnopentime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vw = new Cafe() { Owner = this };
                vw.ShowDialog();
                switch (vw.State)
                {
                    case 1:
                        switch ((int)vw.SystemID)
                        {
                            case 8:
                                BeginButton_Click(vw.Hour.ToString(), vw.Min.ToString());
                                break;
                            case 7:
                                BeginButtonVIP5_Click(vw.Hour.ToString(), vw.Min.ToString());
                                break;
                            case 6:
                                BeginButtonVIP4_Click(vw.Hour.ToString(), vw.Min.ToString());
                                break;
                            case 5:
                                BeginButtonps5_Click(vw.Hour.ToString(), vw.Min.ToString());
                                break;
                            case 4:
                                BeginButtonps4N4_Click(vw.Hour.ToString(), vw.Min.ToString());
                                break;
                            case 3:
                                BeginButtonps4N3_Click(vw.Hour.ToString(), vw.Min.ToString());
                                break;
                            case 2:
                                BeginButtonps4N2_Click(vw.Hour.ToString(), vw.Min.ToString());
                                break;
                            case 1:
                                BeginButtonps4N1_Click(vw.Hour.ToString(), vw.Min.ToString());
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        Systems s = new Systems();

                        switch ((int)vw.SystemID)
                        {

                            case 8:
                                foreach (var item in PersonDataAccess.AllSystems)
                                {
                                    if (item.Name == "بیلیارد")
                                    {
                                        s = item;
                                        break;
                                    }
                                }
                                if (txtbiliard.Text == "")
                                    {
                                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                                    }
                                    else
                                    {
                                        txtbiliard.Text = ReplaceWhitespace(txtbiliard.Text, "");
                                        Persons p = new Persons();
                                        bool Valid = true;
                                        foreach (var item in PersonDataAccess.AllPersons)
                                        {
                                            if (item.Name == txtbiliard.Text)
                                            {
                                                p = item;
                                                Valid = false;
                                                break;
                                            }
                                        }
                                        if (Valid)
                                        {
                                            p.ID = PersonDataAccess.IDP;
                                            p.Grade = 3;
                                            p.Name = txtbiliard.Text;
                                            p.PhoneNumber = 0;
                                            Person.AddMember(PersonDataAccess.IDP, txtbiliard.Text, 0, 3, 0, 0);
                                        }
                                    MessageBox.Show(s.timer.Elapsed.ToString(@"h\:mm"));
                                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, 1).ToString() + "تومن";
                                        var title = "بستن تایم!";
                                        var result = MessageBox.Show(
                                            message,                  // the message to show
                                            title,                    // the title for the dialog box
                                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                            MessageBoxImage.Question); // show a question mark icon

                                        // the following can be handled as if/else statements as well
                                        switch (result)
                                        {
                                            case MessageBoxResult.Yes:   // Yes button pressed

                                                lblEndLivebiliard.Visibility = Visibility.Collapsed;
                                                lblEndbiliard.Visibility = Visibility.Visible;

                                                lblEndbiliard.Content = DateTime.Now.ToString("HH:mm:ss");
                                                s.timer.Stop();
                                                decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, 1);
                                                btnbiliard.Background = Brushes.Black;
                                                if (price > 0)
                                                    MessageBox.Show(Person.AddFactor(p.ID, txtbiliard.Text, lblStartbiliard.Content.ToString(), lblEndLivebiliard.Content.ToString(), price, s.ID));
                                                else
                                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                                txtbiliard.Text = "بیلیارد";
                                                s.timer.Reset();
                                                dispatcherTimer.Stop();
                                            btnbiliard.Background = Brushes.Black;
                                            btnbiliard.IsEnabled = true;
                                            dispatcherTimer.Tick -= dispatcherTimerbiliard_Tick;
                                            break;
                                            case MessageBoxResult.No:    // No button pressed
                                                break;
                                            default:                 // Neither Yes nor No pressed (just in case)

                                                break;
                                        }
                                    }
                                

                                break;
                            case 7:
                                foreach (var item in PersonDataAccess.AllSystems)
                                {
                                    if (item.Name == "VIP5")
                                    {
                                        s = item;
                                        break;
                                    }
                                }
                                dispatcherTimerVIP5.Tick -= dispatcherTimerVIP5_Tick;
                                if (txtVIP5.Text == "")
                                {
                                    MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                                else
                                {
                                    txtVIP5.Text = ReplaceWhitespace(txtVIP5.Text, "");

                                    Persons p = new Persons();
                                    bool Valid = true;
                                    foreach (var item in PersonDataAccess.AllPersons)
                                    {
                                        if (item.Name == txtVIP5.Text)
                                        {
                                            p = item;
                                            Valid = false;
                                            break;
                                        }
                                    }
                                    if (Valid)
                                    {
                                        p.ID = PersonDataAccess.IDP;
                                        p.Grade = 3;
                                        p.Name = txtVIP5.Text;
                                        p.PhoneNumber = 0;
                                        Person.AddMember(PersonDataAccess.IDP, txtVIP5.Text, 0, 3, 0, 0);
                                    }
                                    int NumberofController = 2;

                                    if (Controller3VIP5.IsChecked == true)
                                    {
                                        NumberofController = 3;
                                    }
                                    if (Controller4VIP5.IsChecked == true)
                                    {
                                        NumberofController = 4;
                                    }
                                    var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController).ToString() + "تومن";
                                    var title = "بستن تایم!";
                                    var result = MessageBox.Show(
                                        message,                  // the message to show
                                        title,                    // the title for the dialog box
                                        MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                        MessageBoxImage.Question); // show a question mark icon

                                    // the following can be handled as if/else statements as well
                                    switch (result)
                                    {
                                        case MessageBoxResult.Yes:   // Yes button pressed

                                            lblEndLiveVIP5.Visibility = Visibility.Collapsed;
                                            lblEndVIP5.Visibility = Visibility.Visible;

                                            lblEndVIP5.Content = DateTime.Now.ToString("HH:mm:ss");
                                            s.timer.Stop();
                                            decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController);
                                            btnVIP5.Background = Brushes.Black;
                                            if (price > 0)
                                                MessageBox.Show(Person.AddFactor(p.ID, txtVIP5.Text, lblStartVIP5.Content.ToString(), lblEndLiveVIP5.Content.ToString(), price, s.ID));
                                            else
                                                MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                            txtVIP5.Text = "وی ای پی5";
                                            s.timer.Reset();
                                            btnVIP5.IsEnabled = true;

                                            Controller3VIP5.IsEnabled = true;
                                            Controller4VIP5.IsEnabled = true;

                                            Controller3VIP5.IsChecked = false;
                                            Controller4VIP5.IsChecked = false;
                                            break;
                                        case MessageBoxResult.No:    // No button pressed
                                            break;
                                        default:                 // Neither Yes nor No pressed (just in case)

                                            break;
                                    }
                                }
                                break;
                            case 6:
                                foreach (var item in PersonDataAccess.AllSystems)
                                {
                                    if (item.Name == "VIP4")
                                    {
                                        s = item;
                                        break;
                                    }
                                }
                                dispatcherTimerVIP4.Tick -= dispatcherTimerVIP4_Tick;
                                if (txtVIP4.Text == "")
                                {
                                    MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                                else
                                {
                                    txtVIP4.Text = ReplaceWhitespace(txtVIP4.Text, "");
                                    Persons p = new Persons();
                                    bool Valid = true;
                                    foreach (var item in PersonDataAccess.AllPersons)
                                    {
                                        if (item.Name == txtVIP4.Text)
                                        {
                                            p = item;
                                            Valid = false;
                                            break;
                                        }
                                    }
                                    if (Valid)
                                    {
                                        p.ID = PersonDataAccess.IDP;
                                        p.Grade = 3;
                                        p.Name = txtVIP4.Text;
                                        p.PhoneNumber = 0;
                                        Person.AddMember(PersonDataAccess.IDP, txtVIP4.Text, 0, 3, 0, 0);
                                    }
                                    int NumberofController = 2;

                                    if (Controller3VIP4.IsChecked == true)
                                    {
                                        NumberofController = 3;
                                    }
                                    if (Controller4VIP4.IsChecked == true)
                                    {
                                        NumberofController = 4;
                                    }
                                    var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController).ToString() + "تومن";
                                    var title = "بستن تایم!";
                                    var result = MessageBox.Show(
                                        message,                  // the message to show
                                        title,                    // the title for the dialog box
                                        MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                        MessageBoxImage.Question); // show a question mark icon

                                    // the following can be handled as if/else statements as well
                                    switch (result)
                                    {
                                        case MessageBoxResult.Yes:   // Yes button pressed

                                            lblEndLiveVIP4.Visibility = Visibility.Collapsed;
                                            lblEndVIP4.Visibility = Visibility.Visible;

                                            lblEndVIP4.Content = DateTime.Now.ToString("HH:mm:ss");
                                            s.timer.Stop();
                                            decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController);
                                            btnVIP4.Background = Brushes.Black;
                                            if (price > 0)
                                                MessageBox.Show(Person.AddFactor(p.ID, txtVIP4.Text, lblStartVIP4.Content.ToString(), lblEndLiveVIP4.Content.ToString(), price, s.ID));
                                            else
                                                MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                            txtVIP4.Text = "وی ای پی4";
                                            s.timer.Reset();
                                            btnVIP4.IsEnabled = true;

                                            Controller3VIP4.IsEnabled = true;
                                            Controller4VIP4.IsEnabled = true;

                                            Controller3VIP4.IsChecked = false;
                                            Controller4VIP4.IsChecked = false;
                                            break;
                                        case MessageBoxResult.No:    // No button pressed
                                            break;
                                        default:                 // Neither Yes nor No pressed (just in case)

                                            break;
                                    }
                                }
                                break;
                            case 5:
                                foreach (var item in PersonDataAccess.AllSystems)
                                {
                                    if (item.Name == "Ps5")
                                    {
                                        s = item;
                                        break;
                                    }
                                }
                                dispatcherTimerps5.Tick -= dispatcherTimerps5_Tick;
                                if (txtps5.Text == "")
                                {
                                    MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                                else
                                {
                                    txtps5.Text = ReplaceWhitespace(txtps5.Text, "");
                                    Persons p = new Persons();
                                    bool Valid = true;
                                    foreach (var item in PersonDataAccess.AllPersons)
                                    {
                                        if (item.Name == txtps5.Text)
                                        {
                                            p = item;
                                            Valid = false;
                                            break;
                                        }
                                    }
                                    if (Valid)
                                    {
                                        p.ID = PersonDataAccess.IDP;
                                        p.Grade = 3;
                                        p.Name = txtps5.Text;
                                        p.PhoneNumber = 0;
                                        Person.AddMember(PersonDataAccess.IDP, txtps5.Text, 0, 3, 0, 0);
                                    }
                                    int NumberofController = 2;

                                    if (Controller3Ps5.IsChecked == true)
                                    {
                                        NumberofController = 3;
                                    }
                                    if (Controller4Ps5.IsChecked == true)
                                    {
                                        NumberofController = 4;
                                    }
                                    var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController).ToString() + "تومن";
                                    var title = "بستن تایم!";
                                    var result = MessageBox.Show(
                                        message,                  // the message to show
                                        title,                    // the title for the dialog box
                                        MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                        MessageBoxImage.Question); // show a question mark icon

                                    // the following can be handled as if/else statements as well
                                    switch (result)
                                    {
                                        case MessageBoxResult.Yes:   // Yes button pressed

                                            lblEndLiveps5.Visibility = Visibility.Collapsed;
                                            lblEndps5.Visibility = Visibility.Visible;

                                            lblEndps5.Content = DateTime.Now.ToString("HH:mm:ss");
                                            s.timer.Stop();
                                            decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController);
                                            btnps5.Background = Brushes.Black;
                                            if (price > 0)
                                                MessageBox.Show(Person.AddFactor(p.ID, txtps5.Text, lblStartps5.Content.ToString(), lblEndLiveps5.Content.ToString(), price, s.ID));
                                            else
                                                MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                            txtps5.Text = "سیستم5";
                                            s.timer.Reset();
                                            btnps5.IsEnabled = true;

                                            Controller3Ps5.IsEnabled = true;
                                            Controller4Ps5.IsEnabled = true;

                                            Controller3Ps5.IsChecked = false;
                                            Controller4Ps5.IsChecked = false;
                                            break;
                                        case MessageBoxResult.No:    // No button pressed
                                            break;
                                        default:                 // Neither Yes nor No pressed (just in case)

                                            break;
                                    }
                                }
                                break;
                            case 4:
                                foreach (var item in PersonDataAccess.AllSystems)
                                {
                                    if (item.Name == "Ps4N4")
                                    {
                                        s = item;
                                        break;
                                    }
                                }
                                dispatcherTimerps4N4.Tick -= dispatcherTimerps4N4_Tick;
                                if (txtps4N4.Text == "")
                                {
                                    MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                                else
                                {
                                    txtps4N4.Text = ReplaceWhitespace(txtps4N4.Text, "");

                                    Persons p = new Persons();
                                    bool Valid = true;
                                    foreach (var item in PersonDataAccess.AllPersons)
                                    {
                                        if (item.Name == txtps4N4.Text)
                                        {
                                            p = item;
                                            Valid = false;
                                            break;
                                        }
                                    }
                                    if (Valid)
                                    {
                                        p.ID = PersonDataAccess.IDP;
                                        p.Grade = 3;
                                        p.Name = txtps4N4.Text;
                                        p.PhoneNumber = 0;
                                        Person.AddMember(PersonDataAccess.IDP, txtps4N4.Text, 0, 3, 0, 0);
                                    }
                                    int NumberofController = 2;

                                    if (Controller3Ps4N4.IsChecked == true)
                                    {
                                        NumberofController = 3;
                                    }
                                    if (Controller4Ps4N4.IsChecked == true)
                                    {
                                        NumberofController = 4;
                                    }
                                    var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController).ToString() + "تومن";
                                    var title = "بستن تایم!";
                                    var result = MessageBox.Show(
                                        message,                  // the message to show
                                        title,                    // the title for the dialog box
                                        MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                        MessageBoxImage.Question); // show a question mark icon

                                    // the following can be handled as if/else statements as well
                                    switch (result)
                                    {
                                        case MessageBoxResult.Yes:   // Yes button pressed

                                            lblEndLiveps4N4.Visibility = Visibility.Collapsed;
                                            lblEndps4N4.Visibility = Visibility.Visible;

                                            lblEndps4N4.Content = DateTime.Now.ToString("HH:mm:ss");
                                            s.timer.Stop();
                                            decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController);
                                            btnps4N4.Background = Brushes.Black;
                                            if (price > 0)
                                                MessageBox.Show(Person.AddFactor(p.ID, txtps4N4.Text, lblStartps4N4.Content.ToString(), lblEndLiveps4N4.Content.ToString(), price, s.ID));
                                            else
                                                MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                            txtps4N4.Text = "سیستم4";
                                            s.timer.Reset();
                                            btnps4N4.IsEnabled = true;

                                            Controller3Ps4N4.IsEnabled = true;
                                            Controller4Ps4N4.IsEnabled = true;

                                            Controller3Ps4N4.IsChecked = false;
                                            Controller4Ps4N4.IsChecked = false;
                                            break;
                                        case MessageBoxResult.No:    // No button pressed
                                            break;
                                        default:                 // Neither Yes nor No pressed (just in case)

                                            break;
                                    }
                                }
                                break;
                            case 3:
                                foreach (var item in PersonDataAccess.AllSystems)
                                {
                                    if (item.Name == "Ps4N3")
                                    {
                                        s = item;
                                        break;
                                    }
                                }
                                dispatcherTimerps4N3.Tick -= dispatcherTimerps4N3_Tick;
                                if (txtps4N3.Text == "")
                                {
                                    MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                                else
                                {
                                    txtps4N3.Text = ReplaceWhitespace(txtps4N3.Text, "");

                                    Persons p = new Persons();
                                    bool Valid = true;
                                    foreach (var item in PersonDataAccess.AllPersons)
                                    {
                                        if (item.Name == txtps4N3.Text)
                                        {
                                            p = item;
                                            Valid = false;
                                            break;
                                        }
                                    }
                                    if (Valid)
                                    {
                                        p.ID = PersonDataAccess.IDP;
                                        p.Grade = 3;
                                        p.Name = txtps4N3.Text;
                                        p.PhoneNumber = 0;
                                        Person.AddMember(PersonDataAccess.IDP, txtps4N3.Text, 0, 3, 0, 0);
                                    }
                                    int NumberofController = 2;

                                    if (Controller3Ps4N3.IsChecked == true)
                                    {
                                        NumberofController = 3;
                                    }
                                    if (Controller4Ps4N3.IsChecked == true)
                                    {
                                        NumberofController = 4;
                                    }
                                    var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController).ToString() + "تومن";
                                    var title = "بستن تایم!";
                                    var result = MessageBox.Show(
                                        message,                  // the message to show
                                        title,                    // the title for the dialog box
                                        MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                        MessageBoxImage.Question); // show a question mark icon

                                    // the following can be handled as if/else statements as well
                                    switch (result)
                                    {
                                        case MessageBoxResult.Yes:   // Yes button pressed

                                            lblEndLiveps4N3.Visibility = Visibility.Collapsed;
                                            lblEndps4N3.Visibility = Visibility.Visible;

                                            lblEndps4N3.Content = DateTime.Now.ToString("HH:mm:ss");
                                            s.timer.Stop();
                                            decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController);
                                            btnps4N3.Background = Brushes.Black;
                                            if (price > 0)
                                                MessageBox.Show(Person.AddFactor(p.ID, txtps4N3.Text, lblStartps4N3.Content.ToString(), lblEndLiveps4N3.Content.ToString(), price, s.ID));
                                            else
                                                MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                            txtps4N3.Text = "سیستم3";
                                            s.timer.Reset();
                                            btnps4N3.IsEnabled = true;

                                            Controller3Ps4N3.IsEnabled = true;
                                            Controller4Ps4N3.IsEnabled = true;

                                            Controller3Ps4N3.IsChecked = false;
                                            Controller4Ps4N3.IsChecked = false;
                                            break;
                                        case MessageBoxResult.No:    // No button pressed
                                            break;
                                        default:                 // Neither Yes nor No pressed (just in case)

                                            break;
                                    }
                                }
                                break;
                            case 2:
                                foreach (var item in PersonDataAccess.AllSystems)
                                {
                                    if (item.Name == "Ps4N2")
                                    {
                                        s = item;
                                        break;
                                    }
                                }
                                dispatcherTimerps4N2.Tick -= dispatcherTimerps4N2_Tick;
                                if (txtps4N2.Text == "")
                                {
                                    MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                                else
                                {
                                    txtps4N2.Text = ReplaceWhitespace(txtps4N2.Text, "");

                                    Persons p = new Persons();
                                    bool Valid = true;
                                    foreach (var item in PersonDataAccess.AllPersons)
                                    {
                                        if (item.Name == txtps4N2.Text)
                                        {
                                            p = item;
                                            Valid = false;
                                            break;
                                        }
                                    }
                                    if (Valid)
                                    {
                                        p.ID = PersonDataAccess.IDP;
                                        p.Grade = 3;
                                        p.Name = txtps4N2.Text;
                                        p.PhoneNumber = 0;
                                        Person.AddMember(PersonDataAccess.IDP, txtps4N2.Text, 0, 3, 0, 0);
                                    }
                                    int NumberofController = 2;

                                    if (Controller3Ps4N2.IsChecked == true)
                                    {
                                        NumberofController = 3;
                                    }
                                    if (Controller4Ps4N2.IsChecked == true)
                                    {
                                        NumberofController = 4;
                                    }
                                    var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController).ToString() + "تومن";
                                    var title = "بستن تایم!";
                                    var result = MessageBox.Show(
                                        message,                  // the message to show
                                        title,                    // the title for the dialog box
                                        MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                        MessageBoxImage.Question); // show a question mark icon

                                    // the following can be handled as if/else statements as well
                                    switch (result)
                                    {
                                        case MessageBoxResult.Yes:   // Yes button pressed
                                            lblEndLiveps4N2.Visibility = Visibility.Collapsed;
                                            lblEndps4N2.Visibility = Visibility.Visible;
                                            lblEndps4N2.Content = DateTime.Now.ToString("HH:mm:ss");
                                            s.timer.Stop();
                                            decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController);
                                            btnps4N2.Background = Brushes.Black;
                                            if (price > 0)
                                                MessageBox.Show(Person.AddFactor(p.ID, txtps4N2.Text, lblStartps4N2.Content.ToString(), lblEndLiveps4N2.Content.ToString(), price, s.ID));
                                            else
                                                MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                            txtps4N2.Text = "سیستم2";
                                            s.timer.Reset();
                                            btnps4N2.IsEnabled = true;

                                            Controller3Ps4N2.IsEnabled = true;
                                            Controller4Ps4N2.IsEnabled = true;

                                            Controller3Ps4N2.IsChecked = false;
                                            Controller4Ps4N2.IsChecked = false;
                                            break;
                                        case MessageBoxResult.No:    // No button pressed
                                            break;
                                        default:                 // Neither Yes nor No pressed (just in case)

                                            break;
                                    }
                                }
                                break;
                            case 1:
                                try
                                {
                                    foreach (var item in PersonDataAccess.AllSystems)
                                    {
                                        if (item.Name == "Ps4N1")
                                        {
                                            s = item;
                                            break;
                                        }
                                    }
                                    dispatcherTimerps4N1.Tick -= dispatcherTimerps4N1_Tick;
                                    if (txtps4N1.Text == "")
                                    {
                                        MessageBox.Show("ابتدا نام مشتری را وارد کنید", "error", MessageBoxButton.OK, MessageBoxImage.Error);

                                    }
                                    else
                                    {
                                        dispatcherTimerps4N1.Stop();
                                        txtps4N1.Text = ReplaceWhitespace(txtps4N1.Text, "");
                                        Persons p = new Persons();
                                        bool Valid = true;
                                        foreach (var item in PersonDataAccess.AllPersons)
                                        {
                                            if (item.Name == txtps4N1.Text)
                                            {
                                                p = item;
                                                Valid = false;
                                                break;
                                            }
                                        }
                                        if (Valid)
                                        {
                                            p.ID = PersonDataAccess.IDP;
                                            p.Grade = 3;
                                            p.Name = txtps4N1.Text;
                                            p.PhoneNumber = 0;
                                            Person.AddMember(PersonDataAccess.IDP, txtps4N1.Text, 0, 3, 0, 0);
                                        }
                                        int NumberofController = 2;

                                        if (Controller3Ps4N1.IsChecked == true)
                                        {
                                            NumberofController = 3;
                                        }
                                        if (Controller4Ps4N1.IsChecked == true)
                                        {
                                            NumberofController = 4;
                                        }


                                        var message = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController).ToString() + "تومن";
                                        var title = "بستن تایم!";
                                        var result = MessageBox.Show(
                                            message,                  // the message to show
                                            title,                    // the title for the dialog box
                                            MessageBoxButton.YesNo,  // show two buttons: Yes and No
                                            MessageBoxImage.Question); // show a question mark icon

                                        // the following can be handled as if/else statements as well
                                        switch (result)
                                        {
                                            case MessageBoxResult.Yes:   // Yes button pressed
                                                lblEndLiveps4N1.Visibility = Visibility.Collapsed;
                                                lblEndps4N1.Visibility = Visibility.Visible;
                                                lblEndps4N1.Content = DateTime.Now.ToString("HH:mm:ss");
                                                s.timer.Stop();
                                                decimal price = CalculatePrice(s.timer.Elapsed.ToString(@"h\:mm"), s, p, NumberofController);
                                                btnps4N1.Background = Brushes.Black;
                                                if (price > 0)
                                                    MessageBox.Show(Person.AddFactor(p.ID, txtps4N1.Text, lblStartps4N1.Content.ToString(), lblEndLiveps4N1.Content.ToString(), price, s.ID));
                                                else
                                                    MessageBox.Show("تایم باز شده کمتر از حد مجاز است");
                                                txtps4N1.Text = "سیستم1";
                                                s.timer.Reset();
                                                btnps4N1.IsEnabled = true;

                                                Controller3Ps4N1.IsEnabled = true;
                                                Controller4Ps4N1.IsEnabled = true;

                                                Controller3Ps4N1.IsChecked = false;
                                                Controller4Ps4N1.IsChecked = false;
                                                break;
                                            case MessageBoxResult.No:    // No button pressed
                                                break;
                                            default:                 // Neither Yes nor No pressed (just in case)
                                                break;
                                        }

                                    }

                                }
                                catch (Exception et)
                                {
                                    MessageBox.Show(et.Message);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }
    }
}

