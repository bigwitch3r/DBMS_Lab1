using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DbLib
{
    public class Db : IDb
    {
        private string connectionString = "Data Source=LAPTOP-R0PUGM2G;Initial Catalog = dbms_lab1; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection cnn;

        public Db()
        {
            cnn = new SqlConnection(connectionString);
        }

        public void AddPerson(Person person)
        {
            var sql = $"insert into person (person_id, fio, gender, age, profession) " +
                $"values (N'{person.PersonId}', N'{person.Fio}', N'{person.Gender}'," +
                $" N'{person.Age}', N'{person.Profession}')";
            ExecCommand(sql);
        }

        public void DeletePerson(int id)
        {
            var sql = $"delete from person where person_id = {id}";
            ExecCommand(sql);
        }

        public void CreateDbObject(int personId, int professionId)
        {
            var sql = $"EXEC Create {personId}, {professionId}";
            ExecCommand(sql);
        }

        public List<Person> GetPersons()
        {
            cnn.Open();

            var da = new SqlDataAdapter("select * from person", cnn);
            var ds = new DataSet();
            da.Fill(ds);
            var tablePerson = ds.Tables[0];

            var person = new List<Person>();

            foreach (DataRow row in tablePerson.Rows)
            {
                var newPerson = new Person((int)row["person_id"],
                                           (string)row["fio"],
                                           (string)row["gender"],
                                           (int)row["age"],
                                           (string)row["profession"]);

                person.Add(newPerson);
            }

            cnn.Close();
            return person;
        }

        /*public List<Profession> GetProfessions(string searchQuery = "")
        {
            string sql;

            if (searchQuery == "")
            {
                sql = "SELECT * FROM professions";
            }
            else
            {
                sql = $"SELECT * FROM professions WHERE name = N'{searchQuery}'";
            }

            cnn.Open();

            var da = new SqlDataAdapter(sql, cnn);
            var ds = new DataSet();
            da.Fill(ds);
            var tableProfessions = ds.Tables[0];

            var core = new List<Core>();

            foreach (DataRow row in tableProfessions.Rows)
            {
                var newCore = new Core(
                    (string)row["MechSystem"],
                    (int)row["Power"],
                    (int)row["Id"]);
                core.Add(newCore);
            }

            cnn.Close();

            return core;
        }*/

        private void ExecCommand(string sql)
        {
            cnn.Open();
            var cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
