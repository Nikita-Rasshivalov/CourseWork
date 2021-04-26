using Microsoft.Win32;
using Npgsql;
using System.Windows;
using System.Xml.Linq;
using CourseApp.Utility;

namespace CourseApp
{
    public class Reports
    {
        public Reports()
        {

        }
        public void GetReportCA()
        {
            DbConnection connection = new DbConnection();
            try
            {
                string query = "SELECT stocks.stock_name, products.product_name, sum(receipt_invoices.count_product)" +
                                " FROM receipt_invoices" +
                                " JOIN stocks ON stocks.stock_id = receipt_invoices.stock_id" +
                                " JOIN products ON products.product_id = receipt_invoices.product_id" +
                                " GROUP BY stocks.stock_name, products.product_name";

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
                        XElement istockSumElem = new XElement("Количество", reader["sum"].ToString());
                        stock.Add(istockNameAttr);
                        stock.Add(istockProductElem);
                        stock.Add(istockSumElem);
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

        public void GetReportCK(string dataFrom, string dataTo)
        {
            DbConnection connection = new DbConnection();
            try
            {
                string query = "SELECT stocks.stock_name, sum(expenditure_invoices.price_product * expenditure_invoices.count_product)" +
                                " FROM expenditure_invoices" +
                                " INNER JOIN stocks ON stocks.stock_id = expenditure_invoices.stock_id" +
                                " WHERE expenditure_invoice_date BETWEEN '" + dataFrom + "' AND '" + dataTo + "'" +
                                " GROUP BY stocks.stock_name";

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



        public void GetReportCL()
        {
            DbConnection connection = new DbConnection();
            try
            {
                string query = "SELECT products.product_name, max(expenditure_invoices.price_product)" +
                                " FROM expenditure_invoices" +
                                " INNER JOIN products ON products.product_id = expenditure_invoices.product_id" +
                                " GROUP BY products.product_name";

                NpgsqlCommand command = new NpgsqlCommand(query, connection.GetConnection());
                NpgsqlDataReader reader = command.ExecuteReader();

                XDocument xdoc = new XDocument();
                XElement products = new XElement("Прибыльный_Товар");

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
