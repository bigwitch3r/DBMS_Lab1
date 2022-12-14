using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_test
{
    public partial class FormTransact : Form
    {
        public FormTransact()
        {
            InitializeComponent();

            using (SqlConnection connection = new SqlConnection("Data Source=LAPTOP-R0PUGM2G;Initial Catalog = dbms_lab1; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    // выполняем две отдельные команды
                    command.CommandText = "INSERT INTO person (fio, gender, age, profession)" +
                        " VALUES('Гигачад Г. Г.', 'М', '100', 'Гигачад')";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO person (fio, gender, age, profession)" +
                        " VALUES('Наночад Н. Н.', 'М', '33', 'Наночад')";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO person (fio, gender, age, profession)" +
                        " VALUES('Петачад П. П.', 'М', '300', 'Петачад')";
                    command.ExecuteNonQuery();

                    // подтверждаем транзакцию
                    transaction.Commit();
                    Console.WriteLine("Данные добавлены в базу данных");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }


            SqlConnection cnn = new SqlConnection("Data Source=LAPTOP-R0PUGM2G;Initial Catalog = dbms_lab1; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            cnn.Open();
            SqlDataAdapter de = new SqlDataAdapter("select * from person where gender = 'М' ", cnn);
            DataSet dq = new DataSet();
            de.Fill(dq);
            cnn.Close();
            dataGridView1.DataSource = dq.Tables[0];
        }

        private void FormTransact_Load(object sender, EventArgs e)
        {

        }
    }
}
