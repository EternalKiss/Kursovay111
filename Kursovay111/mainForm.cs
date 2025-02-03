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

namespace Kursovay111
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        Deleted
    }

    public partial class mainForm : Form
    {

        DBConnect dbConnection = new DBConnect();

        int selectedRow;

        public mainForm()
        {
            InitializeComponent();
            CreateColumns();
            RefreshDataGridView(dataGridView1);
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id","id");
            dataGridView1.Columns.Add("Student", "Студент");
            dataGridView1.Columns.Add("Lesson1", "Первый предмет");
            dataGridView1.Columns.Add("Lesson2", "Второй предмет");
            dataGridView1.Columns.Add("Lesson3", "Третий предмет");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }

        private void ReadSingleRows(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetInt32(2), record.GetInt32(3),
                record.GetInt32(4), RowState.Modified);
        }

        private void RefreshDataGridView(DataGridView dgw)
        {
            dgw.Rows.Clear();

            string queryString = $"select * from tableOcenki1";
            SqlCommand command = new SqlCommand(queryString, dbConnection.GetConnection());

            dbConnection.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                ReadSingleRows(dgw, reader);
            }

            reader.Close();
        }

    }
}
