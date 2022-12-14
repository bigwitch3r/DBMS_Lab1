using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbLib;

namespace DbConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IDb db = new Db();

            //Transact();
            //db.DeletePerson(14);
            // var person = db.GetPersons();
            // PrintMech(person);

            string connectionString = "Data Source=LAPTOP-R0PUGM2G;Initial Catalog = dbms_lab1; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            await GetPersonAsync(connectionString);

            Console.ReadKey();
        }

        private static void Transact()
        {
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
                        " VALUES('Иванов И. И.', 'М', '54', 'А33')";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO person (fio, gender, age, profession)" +
                        " VALUES('Сидоров С. С.', 'М', '88', 'А24')";
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
        }

        private static void PrintMech(List<Person> persons)
        {
            Console.WriteLine("|   ID |              FIO |           Gender |              Age |       Profession |");
            Console.WriteLine("|------|------------------|------------------|------------------|------------------|");

            foreach (var person in persons)
            {
                Console.WriteLine($"| {person.PersonId,4} | {person.Fio,16} | {person.Gender,16} | {person.Age,16} | {person.Profession,16} |");
            }
        }

        private static async Task GetPersonAsync(string connectionString)
        {
            // название процедуры
            string sqlExpression = "sp_GetPerson";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(2)}\t{reader.GetName(1)}");

                        while (await reader.ReadAsync())
                        {
                            int id = reader.GetInt32(0);
                            string fio = reader.GetString(1);
                            string gender = reader.GetString(2);
                            int age = reader.GetInt32(3);
                            string profession = reader.GetString(4);
                            Console.WriteLine($"{id} \t{fio} \t{gender} \t{age} \t{profession}");
                        }
                    }
                }
            }
        }

        private static async Task ProcedureData(string connectionString)
        {
            string proc1 = @"CREATE PROCEDURE [dbo].[sp_InsertPerson]
                                @fio varchar(80),
                                @gender varchar(1),
                                @age int,
                                @profession varchar(30)
                            AS
                                INSERT INTO person (fio, gender, age, profession)
                                VALUES (@fio, @gender, @age, @profession)
   
                                SELECT SCOPE_IDENTITY()
                            GO";

            string proc2 = @"CREATE PROCEDURE [dbo].[sp_GetPerson]
                                AS
                                    SELECT * FROM person 
                                GO";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(proc1, connection);

                // добавляем первую процедуру
                await command.ExecuteNonQueryAsync();

                // добавляем вторую процедуру
                command.CommandText = proc2;
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("Хранимые процедуры добавлены в базу данных.");
            }
        }
    }
}
