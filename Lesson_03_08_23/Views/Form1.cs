using Lesson_03_08_23.Helpers;
using Lesson_03_08_23.Model;

namespace Lesson_03_08_23
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DBCreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                DB_Helper.CreateDateBase("TestDB_Lesson_03082023");
                DB_Helper.CreateTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            try
            {
                DB_Helper.Insert();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateDataSet();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var ObjectsList = UsersDataGridView.SelectedCells;

                if (ObjectsList.Count > 0)
                {
                    int selectedrowindex = UsersDataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = UsersDataGridView.Rows[selectedrowindex];

                    int IdValue = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                    string NameValue = Convert.ToString(selectedRow.Cells["Name"].Value) ?? "";
                    string SurNameValue = Convert.ToString(selectedRow.Cells["SurName"].Value) ?? "";

                    var user = new User(IdValue, NameValue, SurNameValue);
                    DB_Helper.Update(user);
                }
                UpdateDataSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                var ObjectsList = UsersDataGridView.SelectedCells;

                if (ObjectsList.Count > 0)
                {
                    int selectedrowindex = UsersDataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = UsersDataGridView.Rows[selectedrowindex];

                    int IdValue = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                    string NameValue = Convert.ToString(selectedRow.Cells["Name"].Value) ?? "";
                    string SurNameValue = Convert.ToString(selectedRow.Cells["SurName"].Value) ?? "";

                    var user = new User(IdValue, NameValue, SurNameValue);
                    DB_Helper.Delete(user);
                }
                UpdateDataSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            UpdateDataSet();
        }

        private void UpdateDataSet()
        {
            try
            {
                var ds = DB_Helper.Select();
                UsersDataGridView.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}