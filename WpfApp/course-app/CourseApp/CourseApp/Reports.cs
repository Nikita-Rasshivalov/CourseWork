using Microsoft.Win32;
using Npgsql;
using System.Windows;
using System.Xml.Linq;
using CourseApp.Utility;
using System.Windows.Controls;

namespace CourseApp
{
    public class Reports
    {
        public Reports()
        {

        }
        /// <summary>
        /// Получение отчета по складу
        /// </summary>
        public void GerReportS(int stockId, ComboBox ReportComboBox)
        {
            if (ReportComboBox == null ||
                ReportComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите склад");
            }
            else
            {

                DbConnection connection = new DbConnection();
                try
                {
                    string query = "select stock_name, product_name,count_product,product_price from products_in_stock" +
                        " inner join stocks on products_in_stock.stock_id = stocks.stock_id " +
                         "inner join products on products_in_stock.product_id = products.product_id  where products_in_stock.stock_id =" + stockId;

                    NpgsqlCommand command = new NpgsqlCommand(query, connection.GetConnection());
                    NpgsqlDataReader reader = command.ExecuteReader();

                    XDocument xdoc = new XDocument();
                    XElement stocks = new XElement("Склад");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            XElement stock = new XElement("Склад");
                            XAttribute istockNameAttr = new XAttribute("Наименование", reader["stock_name"].ToString());
                            XElement istockProductElem = new XElement("Продукт", reader["product_name"].ToString());
                            XElement istockCountElem = new XElement("Количество", reader["count_product"].ToString());
                            XElement istockSumElem = new XElement("Цена", reader["product_price"].ToString());
                            stock.Add(istockNameAttr);
                            stock.Add(istockProductElem);
                            stock.Add(istockSumElem);
                            stock.Add(istockCountElem);
                            stocks.Add(stock);
                        }
                        reader.Close();

                    }
                    xdoc.Add(stocks);

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "XML-File | *.xml";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        xdoc.Save(saveFileDialog.FileName);
                        MessageBox.Show("Файл сохранен");
                    }
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    connection.CloseConnection();
                }
            }
        }
        /// <summary>
        /// Получение отчета по всем складам
        /// </summary>
        public void GetReportAS()
        {
            DbConnection connection = new DbConnection();
            try
            {
                string query = "select stock_name, product_name,count_product,product_price from products_in_stock" +
                    " inner join stocks on products_in_stock.stock_id = stocks.stock_id " +
                    "inner join products on products_in_stock.product_id = products.product_id ";

                NpgsqlCommand command = new NpgsqlCommand(query, connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                XDocument xdoc = new XDocument();
                XElement stocks = new XElement("Склады");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        XElement stock = new XElement("Склад");
                        XAttribute istockNameAttr = new XAttribute("Наименование", reader["stock_name"].ToString());
                        XElement istockProductElem = new XElement("Продукт", reader["product_name"].ToString());
                        XElement istockCountElem = new XElement("Количество", reader["count_product"].ToString());
                        XElement istockSumElem = new XElement("Цена", reader["product_price"].ToString());
                        stock.Add(istockNameAttr);
                        stock.Add(istockProductElem);
                        stock.Add(istockSumElem);
                        stock.Add(istockCountElem);
                        stocks.Add(stock);
                    }
                    reader.Close();

                }
                xdoc.Add(stocks);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML-File | *.xml";
                if (saveFileDialog.ShowDialog() == true)
                {
                    xdoc.Save(saveFileDialog.FileName);
                    MessageBox.Show("Файл сохранен");
                }
            }
            catch (NpgsqlException ex)
            {

            }
            finally
            {
                connection.CloseConnection();
            }
        }
        /// <summary>
        /// Полученная прибыль по складам
        /// </summary>
        /// <param name="dataFrom"></param>
        /// <param name="dataTo"></param>
        public void GetReportDS(string dataFrom, string dataTo)
        {
            DbConnection connection = new DbConnection();
            try
            {
                string query = "select stock_name, sum(product_price*count_product)  from expenditure_invoices" +
                    " inner join expenditure_positions on expenditure_invoices.expenditure_invoice_id = expenditure_positions.expenditure_invoice_id " +
                    "inner join stocks on stocks.stock_id = expenditure_invoices.stock_id" +
                    " WHERE expenditure_invoice_date BETWEEN '" + dataFrom + "' AND '" + dataTo + "'" +
                    " group by stock_name";

                NpgsqlCommand command = new NpgsqlCommand(query, connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                XDocument xdoc = new XDocument();
                XElement stocks = new XElement("Склады");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        XElement stock = new XElement("Склад");
                        XAttribute istockNameAttr = new XAttribute("Наименование", reader["stock_name"].ToString());
                        XElement istockPriceElem = new XElement("Прибыль", reader["sum"].ToString());
                        stock.Add(istockNameAttr);
                        stock.Add(istockPriceElem);
                        stocks.Add(stock);
                    }
                    reader.Close();

                }
                xdoc.Add(stocks);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML-File | *.xml";
                if (saveFileDialog.ShowDialog() == true)
                {
                    xdoc.Save(saveFileDialog.FileName);
                    MessageBox.Show("Файл сохранен");
                }
            }
            catch (NpgsqlException ex)
            {

            }
            finally
            {
                connection.CloseConnection();
            }
        }


        /// <summary>
        /// Наиболее доходные товары
        /// </summary>
        public void GetReportD()
        {
            DbConnection connection = new DbConnection();
            try
            {
                string query = "select products.product_name, max(count_product*expenditure_positions.product_price)" +
                    "from expenditure_positions" +
                    " inner join products on expenditure_positions.product_id = products.product_id" +
                    " group by products.product_name " +
                    "order by max desc ";

                NpgsqlCommand command = new NpgsqlCommand(query, connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                XDocument xdoc = new XDocument();
                XElement products = new XElement("Товары");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        XElement product = new XElement("Товар");
                        XAttribute iproductNameAttr = new XAttribute("Наименование", reader["product_name"].ToString());
                        XElement iproductPriceElem = new XElement("Стоимость", reader["max"].ToString());
                        product.Add(iproductNameAttr);
                        product.Add(iproductPriceElem);
                        products.Add(product);
                    }
                    reader.Close();

                }
                xdoc.Add(products);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML-File | *.xml";
                if (saveFileDialog.ShowDialog() == true)
                {
                    xdoc.Save(saveFileDialog.FileName);
                    MessageBox.Show("Файл сохранен");
                }
            }
            catch (NpgsqlException ex)
            {

            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}
