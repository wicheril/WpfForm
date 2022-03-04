using System;
using System.Collections.Generic;
using System.Data;
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
using Npgsql;

namespace WpfSql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BdSql conn;
        string sql;
        public MainWindow()
        {
            InitializeComponent();
            conn = new BdSql();
            conn.OpenCon();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            box2.Items.Add("1.Найти покупателей, проживающих в городе Казань");
            box2.Items.Add("2.Найти покупателей, фамилии которых начинаются с заданного символа");
            box2.Items.Add("3.Найти покупателей, фамилии которых содержат заданную последовательность символов");
            box2.Items.Add("4.Найти покупателей, фамилии которых оканчиваются заданным символом");
            box2.Items.Add("5.Выдать список покупателей с указанием значения выражения Balance*100");
            box2.Items.Add("6.Определить число поставщиков каждого товара");
            box2.Items.Add("7.Найти минимальную цену заданного товара");
            box2.Items.Add("8.Выдать упорядоченный по возрастанию цен список поставщиков заданного товара");
            box2.Items.Add("9.Найти покупателей, некоторые заказы которых можно выполнить, не нарушая лимитирующего ограничения");
            box2.Items.Add("10.Найти всех покупателей указанного товара");
            box2.Items.Add("11.Найти максимальный по стоимости заказ");
            box2.Items.Add("12.Найти все тройки <покупатель,поставщик,заказ>, удовлетворяющие условию");
            box2.Items.Add("13.Вывести таблицу с заказами но вместо id фамилии");
            box2.Items.Add("14.Вывести города поставщиков без повторений");
            box2.Items.Add("15.Вывести список покупателей у которых фамилия заканчивается на 'ев' и проживают в Казани" +
                            "и их баланс больше 4000");
            box2.Items.Add("16.Вывести количество городов в которых есть поставщики");
            box2.Items.Add("17. Вывести города в которых проживают покупатели без повторений большими буквами");

            box1.Items.Add("1.Найти покупателей, проживающих в городе Казань");
            box1.Items.Add("2.Найти покупателей, фамилии которых начинаются с заданного символа");
            box1.Items.Add("3.Найти покупателей, фамилии которых содержат заданную последовательность символов");
            box1.Items.Add("4.Найти покупателей, фамилии которых оканчиваются заданным символом");
            box1.Items.Add("5.Выдать список покупателей с указанием значения выражения Balance*100");
            box1.Items.Add("6.Определить число поставщиков каждого товара");
            box1.Items.Add("7.Найти минимальную цену заданного товара");
            box1.Items.Add("8.Выдать упорядоченный по возрастанию цен список поставщиков заданного товара");
            box1.Items.Add("9.Найти покупателей, некоторые заказы которых можно выполнить, не нарушая лимитирующего ограничения");
            box1.Items.Add("10.Найти всех покупателей указанного товара");
            box1.Items.Add("11.Найти максимальный по стоимости заказ");
            box1.Items.Add("12.Найти все тройки <покупатель,поставщик,заказ>, удовлетворяющие условию");
            box1.Items.Add("13.Вывести таблицу с заказами но вместо id фамилии");
            box1.Items.Add("14.Вывести города поставщиков без повторений");
            box1.Items.Add("15.Вывести список покупателей у которых фамилия заканчивается на 'ев' и проживают в Казани" +
                            "и их баланс больше 4000");
            box1.Items.Add("16.Вывести количество городов в которых есть поставщики");
            box1.Items.Add("17. Вывести города в которых проживают покупатели без повторений большими буквами");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (box1.SelectedIndex)
            {
                case 0:
                    sql = "select * from \"Customers\" where \"Address\" like 'Казань%'";
                    break;
                case 1:
                    sql = "select * from \"Customers\" where \"Family\" like 'П%'";
                    break;
                case 2:
                    sql = "select * from \"Customers\" where \"Family\" like 'Кон%'";
                    break;
                case 3:
                    sql = "select * from \"Customers\" where \"Family\" like '%ов'";
                    break;
                case 4:
                    sql = "select \"Family\", \"Balance\" * 100 from \"Customers\"";
                    break;
                case 5:
                    sql = "select \"Commodity\", count(*) from \"Providers\" Group by \"Commodity\"";
                    break;
                case 6:
                    sql = "Select min(\"Price\"),\"Commodity\" from \"Providers\" where \"Commodity\" = 'Маски' Group by \"Commodity\", \"Price\"";
                    break;
                case 7:
                    sql = "select min(\"Price\"),\"Commodity\",\"Name\" from \"Providers\" where \"Commodity\" = 'Маски' group by \"Commodity\", \"Price\", \"Name\"";
                    break;
                case 8:
                    sql = "select \"Family\" from \"Customers\" where \"IdCs\" in (select \"IdCs\" from \"Orders\" inner join \"Providers\" on \"Orders\".\"Commodity\" = \"Providers\".\"Commodity\" and \"Orders\".\"Number\" * \"Providers\".\"Price\" < \"Orders\".\"Limit\")";
                    break;
                case 9:
                    sql = "select \"Family\" from \"Customers\" where \"IdCs\" in (select \"IdCs\" from \"Orders\" where \"Commodity\" = 'Стулья')";
                    break;
                case 10:
                    sql = "select max(\"Orders\".\"Number\"*\"Providers\".\"Price\"), \"Providers\".\"Commodity\" from \"Orders\" inner join \"Providers\" on \"Orders\".\"Commodity\" = \"Providers\".\"Commodity\" group by \"Providers\".\"Commodity\" order by max(\"Orders\".\"Number\" * \"Providers\".\"Price\") DESC limit 1";
                    break;
                case 11:
                    sql = "select \"Customers\".\"Family\",\"Providers\".\"Name\",\"Providers\".\"Address\",\"Orders\".\"Commodity\" from \"Orders\",\"Providers\",\"Customers\" where \"Customers\".\"IdCs\" = \"Orders\".\"IdCs\" and \"Providers\".\"Address\" = \"Customers\".\"Address\" and \"Orders\".\"Commodity\" = \"Providers\".\"Commodity\" and \"Orders\".\"Number\" * \"Providers\".\"Price\" <= \"Orders\".\"Limit\"";
                    break;
                case 12:
                    sql = "select \"Customers\".\"Family\",\"Orders\".\"Commodity\",\"Orders\".\"Number\",\"Orders\".\"Limit\" from \"Orders\",\"Customers\" where \"Customers\".\"IdCs\" = \"Orders\".\"IdCs\" group by \"Customers\".\"Family\", \"Orders\".\"Commodity\", \"Orders\".\"Number\", \"Orders\".\"Limit\"";
                    break;
                case 13:
                    sql = "select \"Providers\".\"Address\" from \"Providers\" group by \"Address\"";
                    break;
                case 14:
                    sql = "select * from \"Customers\" where \"Customers\".\"Family\" like '%ев' and \"Customers\".\"Address\" like '%нь%' and \"Balance\" > money(4000)";
                    break;
                case 15:
                    sql = "select count(distinct (\"Address\")) from \"Providers\"";
                    break;
                case 16:
                    sql = "select distinct (upper(\"Address\")) as Адрес from \"Customers\"";
                    break; 
            }
            TextBox1.Text = sql;
        }

        private void buttonAction_Click(object sender, RoutedEventArgs e)
        {
            sql = TextBox1.Text;
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sql, conn.Connect);
            DataTable data = new DataTable();
            adapter.Fill(data);
            sqlResult.ItemsSource = data.DefaultView;
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (box2.SelectedIndex)
            {
                case 0:
                    sql = $"select * from \"Customers\" where \"Address\" like '{TextBox2.Text}'";
                    break;
                case 1:
                    sql = "select * from \"Customers\" where \"Family\" like 'П%'";
                    break;
                case 2:
                    sql = "select * from \"Customers\" where \"Family\" like 'Кон%'";
                    break;
                case 3:
                    sql = "select * from \"Customers\" where \"Family\" like '%ов'";
                    break;
                case 4:
                    sql = "select \"Family\", \"Balance\" * 100 from \"Customers\"";
                    break;
                case 5:
                    sql = "select \"Commodity\", count(*) from \"Providers\" Group by \"Commodity\"";
                    break;
                case 6:
                    sql = "Select min(\"Price\"),\"Commodity\" from \"Providers\" where \"Commodity\" = 'Маски' Group by \"Commodity\", \"Price\"";
                    break;
                case 7:
                    sql = "select min(\"Price\"),\"Commodity\",\"Name\" from \"Providers\" where \"Commodity\" = 'Маски' group by \"Commodity\", \"Price\", \"Name\"";
                    break;
                case 8:
                    sql = "select \"Family\" from \"Customers\" where \"IdCs\" in (select \"IdCs\" from \"Orders\" inner join \"Providers\" on \"Orders\".\"Commodity\" = \"Providers\".\"Commodity\" and \"Orders\".\"Number\" * \"Providers\".\"Price\" < \"Orders\".\"Limit\")";
                    break;
                case 9:
                    sql = "select \"Family\" from \"Customers\" where \"IdCs\" in (select \"IdCs\" from \"Orders\" where \"Commodity\" = 'Стулья')";
                    break;
                case 10:
                    sql = "select max(\"Orders\".\"Number\"*\"Providers\".\"Price\"), \"Providers\".\"Commodity\" from \"Orders\" inner join \"Providers\" on \"Orders\".\"Commodity\" = \"Providers\".\"Commodity\" group by \"Providers\".\"Commodity\" order by max(\"Orders\".\"Number\" * \"Providers\".\"Price\") DESC limit 1";
                    break;
                case 11:
                    sql = "select \"Customers\".\"Family\",\"Providers\".\"Name\",\"Providers\".\"Address\",\"Orders\".\"Commodity\" from \"Orders\",\"Providers\",\"Customers\" where \"Customers\".\"IdCs\" = \"Orders\".\"IdCs\" and \"Providers\".\"Address\" = \"Customers\".\"Address\" and \"Orders\".\"Commodity\" = \"Providers\".\"Commodity\" and \"Orders\".\"Number\" * \"Providers\".\"Price\" <= \"Orders\".\"Limit\"";
                    break;
                case 12:
                    sql = "select \"Customers\".\"Family\",\"Orders\".\"Commodity\",\"Orders\".\"Number\",\"Orders\".\"Limit\" from \"Orders\",\"Customers\" where \"Customers\".\"IdCs\" = \"Orders\".\"IdCs\" group by \"Customers\".\"Family\", \"Orders\".\"Commodity\", \"Orders\".\"Number\", \"Orders\".\"Limit\"";
                    break;
                case 13:
                    sql = "select \"Providers\".\"Address\" from \"Providers\" group by \"Address\"";
                    break;
                case 14:
                    sql = "select * from \"Customers\" where \"Customers\".\"Family\" like '%ев' and \"Customers\".\"Address\" like '%нь%' and \"Balance\" > money(4000)";
                    break;
                case 15:
                    sql = "select count(distinct (\"Address\")) from \"Providers\"";
                    break;
                case 16:
                    sql = "select distinct (upper(\"Address\")) as Адрес from \"Customers\"";
                    break;
            }
            TextBox1.Text = sql;
        }
    }
}
