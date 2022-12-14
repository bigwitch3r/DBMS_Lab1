﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DbLib;


namespace DB_test
{
    public partial class FormMenu : Form
    {
        IDb db;

        public FormMenu(IDb db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
            (new FormPeople(db)).Show();
        }

        private void transact(object sender, EventArgs e)
        {
            (new FormTransact()).Show();
        }

        private void Procedure(object sender, EventArgs e)
        {
            (new Procedure()).Show();
        }
    }
}
