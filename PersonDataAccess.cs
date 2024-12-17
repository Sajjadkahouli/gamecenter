using Game_Center_Bergamo.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game_Center_Bergamo
{
    public class PersonDataAccess
    {
        public static ObservableCollection<Systems> AllSystems = new ObservableCollection<Systems>();
        public static ObservableCollection<ProductOfFactor> ListOfinvoices = new ObservableCollection<ProductOfFactor>();
        public static ObservableCollection<FactoKol> ListOfinvoicesFinall = new ObservableCollection<FactoKol>();
        public static ObservableCollection<Persons> AllPersons = new ObservableCollection<Persons>();
        public static ObservableCollection<Persons> AllPersonsCopy = new ObservableCollection<Persons>();
        public static ObservableCollection<Persons> AllPersonsStart = new ObservableCollection<Persons>();
        public static ObservableCollection<PersonsBorrow> Borrows = new ObservableCollection<PersonsBorrow>();
        public static ObservableCollection<string> Mylist = new ObservableCollection<string>();
        public static List<string> NameOfPerson = new List<string>();
        public static int IDP { get; set; }
        public static int IDS { get; set; }
        public static int IDB { get; set; }
        public static int IDBS { get; set; }
        public static string sumtoday { get; set; } = "0";


        public PersonDataAccess()
        {
            IDP = 1;
            IDS = 1; 
            ProductOfFactor.IDFactor = 1;
            FactoKol.IDFactor = 1;
            ReadData();
            
        }
        private StringBuilder sb = new StringBuilder();

        private void ReadData()
        {
            try
            {
                var connstr = "Server=localhost;Uid=root;Pwd=S13791381;database=centerbergamo";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "select * from persons;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ii = reader.FieldCount;
                                string[] Values = new string[6];
                                for (int i = 0; i < ii; i++)
                                {
                                    if (reader[i] is DBNull)
                                        sb.AppendLine("null");
                                    else
                                    {
                                        string s = reader[i].ToString();
                                        sb.AppendLine(s);
                                        Values[i] = s;
                                    }
                                }
                                IDP = Convert.ToInt32(Values[0]);
                                AddMember(Convert.ToInt32(Values[0]), Values[1], Convert.ToUInt64(Values[2]), Convert.ToInt32(Values[3]), Convert.ToDecimal(Values[4]), Convert.ToDecimal(Values[5]));
                            }
                            foreach (var item in AllPersons)
                            {
                                AllPersonsStart.Add(item);
                                AllPersonsCopy.Add(item);
                            }
                        }

                        cmd.CommandText = "select * from systems;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ii = reader.FieldCount;
                                string[] Values = new string[3];
                                for (int i = 0; i < ii; i++)
                                {
                                    if (reader[i] is DBNull)
                                        sb.AppendLine("null");
                                    else
                                    {
                                        string s = reader[i].ToString();
                                        sb.AppendLine(s);
                                        Values[i] = s;
                                    }
                                }
                                AddSystem(Convert.ToInt32(Values[0]), Convert.ToDecimal(Values[2]), Values[1]);
                            }
                        }

                        cmd.CommandText = "select * from Cafe;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ii = reader.FieldCount;
                                string[] Values = new string[3];
                                for (int i = 0; i < ii; i++)
                                {
                                    if (reader[i] is DBNull)
                                        sb.AppendLine("null");
                                    else
                                    {
                                        string s = reader[i].ToString();
                                        sb.AppendLine(s);
                                        Values[i] = s;
                                    }
                                }
                                Mylist.Add(Values[1] + " " + Values[2]);
                            }
                        }

                        cmd.CommandText = "select * from borrow;";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ii = reader.FieldCount;
                                string[] Values = new string[9];
                                for (int i = 0; i < ii; i++)
                                {
                                    if (reader[i] is DBNull)
                                        sb.AppendLine("null");
                                    else
                                    {
                                        string s = reader[i].ToString();
                                        sb.AppendLine(s);
                                        Values[i] = s;
                                    }
                                }
                                PersonsBorrow p = new PersonsBorrow(Convert.ToInt32(Values[0]), Values[1], Convert.ToInt32(Values[2]), Values[3], Values[4], Values[6] , Values[8] , Values[7]);
                                Borrows.Add(p);
                            }
                            if (Borrows.Count() > 0 )
                                IDB = Borrows[Borrows.Count() - 1].ID;
                                IDBS = IDB;
                        }
                    }
                }
                /*AddMember(IDP , "سجادکهولی" , 09370216467 , 1);
                AddMember(IDP, "محمدموسوی", 09370211234, 2);
                AddMember(IDP, "امیرموسوی", 09370214321, 3);
                AddMember(IDP, "علیرضارجبی", 09370212389, 3);

                AddSystem(IDS , 30000 , "Ps4N1");
                AddSystem(IDS,  30000, "Ps4N2");
                AddSystem(IDS,  30000, "Ps4N3");
                AddSystem(IDS,  30000, "Ps4N4");
                AddSystem(IDS,  40000, "Ps5");
                AddSystem(IDS,  40000, "VIP4");
                AddSystem(IDS,  50000, "VIP5");
                AddSystem(IDS,  60000, "بیلیارد");

                Mylist.Add("اسپرسوسینگل 15000");
                Mylist.Add("اسپرسودبل 20000");
                Mylist.Add("باجیکا 30000");
                Mylist.Add("لیموناد 17000");
                Mylist.Add("بیگ بر 250000");
                Mylist.Add("رانی 22000");
                Mylist.Add("ویتامین سی 20000");
                Mylist.Add("هایپ 35000");
                Mylist.Add("آیس مانکی 25000");
                Mylist.Add("آب 7000");
                Mylist.Add("کیک معینی 15000");
                */
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void PushData()
        {
            try
            {
                var connstr = "Server=localhost;Uid=root;Pwd=S13791381;database=centerbergamo";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();

                    foreach (var item in AllPersons)
                    {
                        bool val = true;
                        foreach (var item1 in AllPersonsStart)
                        {
                            if (item.ID == item1.ID)
                            {
                                val = false;
                                string cmdText = "update persons set TotalPrice = @TotalPrice, Debt =@Debt  where ID = @ID;";
                                MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                                cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                                cmd.Parameters.AddWithValue("@Debt", item.Debt);
                                cmd.Parameters.AddWithValue("@ID", item.ID);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (val)
                        {
                            string cmdText = "insert into persons(Name , PhoneNumber , Grade , TotalPrice , Debt) values(@Name , @PhoneNumber , @Grade , @TotalPrice , @Debt);";
                            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                            cmd.Parameters.AddWithValue("@Name", item.Name);
                            cmd.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);
                            cmd.Parameters.AddWithValue("@Grade", item.Grade);
                            cmd.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                            cmd.Parameters.AddWithValue("@Debt", item.Debt);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    foreach (var item in ListOfinvoices)
                    {

                        string cmdText = "insert into partial_invoices(IDPerson , IDSystem , Name , Start , End , Price) values(@IDPerson , @IDSystem , @Name , @Start , @End , @Price);";
                        MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                        cmd.Parameters.AddWithValue("@IDPerson", item.IDPerson);
                        cmd.Parameters.AddWithValue("@IDSystem", item.IDSystem);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Start", item.Start);
                        cmd.Parameters.AddWithValue("@End", item.End);
                        cmd.Parameters.AddWithValue("@Price", item.Price);
                        cmd.ExecuteNonQuery();

                    }
                    foreach (var item in Borrows)
                    {
                        if (item.ID > IDBS)
                        {
                            string cmdText = "insert into borrow(Name , Period , Borrow , StartTime , EndTime , Date , PhoneNumber , Address) values(@Name , @Period, @Borrow , @Start , @End , @Date , @PhoneNumber , @Address);";
                            MySqlCommand cmd = new MySqlCommand(cmdText, conn);
                            cmd.Parameters.AddWithValue("@Name", item.Name);
                            cmd.Parameters.AddWithValue("@Period", item.Long);
                            cmd.Parameters.AddWithValue("@Borrow", item.Borrow);
                            cmd.Parameters.AddWithValue("@Start", item.StartTime);
                            cmd.Parameters.AddWithValue("@End", item.EndTime);
                            cmd.Parameters.AddWithValue("@Date", item.Date);
                            cmd.Parameters.AddWithValue("@PhoneNumber", item.PhoneNumber);
                            cmd.Parameters.AddWithValue("@Address", item.Address);
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch(Exception et)
            {
                MessageBox.Show(et.Message);
            }
        }

        public string AddMember(int ID, string Name, UInt64 Phone, int Grade , decimal TotallPrice , decimal Debt)
        {
            try
            {
                NameOfPerson.Add(Name);
                Persons p = new Persons(ID, Name, Phone, Grade , TotallPrice , Debt);
                AllPersons.Add(p);
                AllPersonsCopy.Add(p);
                IDP++;
                return "Succesfull";
            }
            catch (Exception t)
            {
                return t.Message.ToString();
            }
        }
        public string AddSystem(int ID, decimal PriceByHour, string Name) 
        {
            try
            {
                Systems p = new Systems(ID, PriceByHour, Name);
                AllSystems.Add(p);
                IDS++;
                return "Succesfull";
            }
            catch (Exception t)
            {
                return t.Message.ToString();
            }
        }

        

        public string AddFactor(int idp ,string name ,string start , string end , decimal price , int ids)
        {
            try
            {
                decimal sum = Convert.ToDecimal(PersonDataAccess.sumtoday) + price;
                sumtoday = sum.ToString();
                foreach (var item in AllPersons)
                {
                    if(item.ID == idp)
                    {
                        item.TotalPrice += price;
                        break;
                    }
                }
                ProductOfFactor pf = new ProductOfFactor(ProductOfFactor.IDFactor, idp, name,start , end ,price, ids);
                ListOfinvoices.Add(pf);
                AddFactorFinall(pf);
                ProductOfFactor.IDFactor += 1;
                return "فاکتور ثبت شد";
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        public string AddFactorFinall(ProductOfFactor New)
        {
            try
            {
                foreach (var item2 in AllPersons)
                {
                    if (New.IDPerson == item2.ID)
                    {
                        item2.Debt += New.Price;
                        break;
                    }
                }
                bool Value = true;
                decimal price = New.Price;
                foreach (FactoKol item in ListOfinvoicesFinall)
                {
                    if (item.IDPerson == New.IDPerson)
                    {
                    int x = ListOfinvoicesFinall.IndexOf(item);
                    Value = false;
                    int temporary = FactoKol.IDFactor;
                    FactoKol.IDFactor = x + 1;
                    FactoKol F = new FactoKol(New.IDPerson , New.Name , 0 , item.FinallyPrice + New.Price);
                        
                    ListOfinvoicesFinall[x] = F;
                    FactoKol.IDFactor = temporary;
                    break;
                    }
                }
                if (Value)
                    {
                        FactoKol F = new FactoKol(New.IDPerson , New.Name , 0 , New.Price);
                        ListOfinvoicesFinall.Add(F);
                        FactoKol.IDFactor++;
                    }
                
                return "Succesfull";
            }
            catch (Exception t)
            {
                return t.Message.ToString();
            }
        }
    }
}

