using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CarCareManagementSystem
{
    public class ViewTasksForm : Form
    {
        private DataGridView dgvTasks;
        private ComboBox cbStatusFilter;
        private Button btnLoadTasks;

        private string connectionString = "Data Source=KZ070BRO12;Initial Catalog=CarCareDB;Integrated Security=True";

        public ViewTasksForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "View Tasks";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "View Tasks Assigned to You",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            dgvTasks = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(750, 400),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvTasks);

            Label lblFilter = new Label
            {
                Text = "Filter by Status:",
                Location = new Point(20, 480),
                AutoSize = true
            };
            this.Controls.Add(lblFilter);

            cbStatusFilter = new ComboBox
            {
                Location = new Point(120, 475),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbStatusFilter.Items.AddRange(new string[] { "All", "Pending", "In Progress", "Completed" });
            cbStatusFilter.SelectedIndex = 0;
            this.Controls.Add(cbStatusFilter);

            btnLoadTasks = new Button
            {
                Text = "Load Tasks",
                Location = new Point(340, 475),
                Size = new Size(150, 30)
            };
            btnLoadTasks.Click += BtnLoadTasks_Click;
            this.Controls.Add(btnLoadTasks);
        }

        private void BtnLoadTasks_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Tasks WHERE MechanicID = @MechanicID";

                    if (cbStatusFilter.SelectedItem.ToString() != "All")
                    {
                        query += " AND Status = @Status";
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MechanicID", 1); // Укажите реальный ID механика

                    if (cbStatusFilter.SelectedItem.ToString() != "All")
                    {
                        command.Parameters.AddWithValue("@Status", cbStatusFilter.SelectedItem.ToString());
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvTasks.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading tasks: {ex.Message}");
                }
            }
        }
    }
}
