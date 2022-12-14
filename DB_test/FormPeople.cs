using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DbLib;

namespace DB_test
{
    public partial class FormPeople : Form
    {
        IDb db;
        private List<Person> person;

        public FormPeople(IDb db)
        {
            InitializeComponent();
            this.db = db;
            person = new List<Person>();
            Reload(this.db);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            db.AddPerson(new Person("Гончаров Д. А.", "М", 21, "Погромист"));
            Reload(db);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idx = gridPerson.CurrentCell.RowIndex;
            int id = person[idx].PersonId;

            db.DeletePerson(id);
            Reload(db);
        }

        // Загрузить данные и отобразить их в таблице
        private void Reload(IDb db)
        {
            person = db.GetPersons();
            gridPerson.DataSource = person;
        }
    }
}
