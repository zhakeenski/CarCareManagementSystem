using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CarCareManagementSystem
{
    public class RecordProgressForm : Form
    {
        private TextBox txtTaskID, txtStatus, txtComments, txtCompletionTime;
        private Button btnUpdateProgress;

        private string connectionString = "Data Source=KZ070BRO12;Initial Catalog=CarCareDB;Integrated Security=True";

        public RecordProgressForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Record Progress";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Record Task Progress",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            txtTaskID = CreateTextBox("Task ID", new Point(20, 60));
            txtStatus = CreateTextBox("Status (e.g., Completed)", new Point(20, 100));
            txtComments = CreateTextBox("Comments", new Point(20, 140));
            txtCompletionTime = CreateTextBox("Completion Time (YYYY-MM-DD)", new Point(20, 180));

            this.Controls.AddRange(new Control[] { txtTaskID, txtStatus, txtComments, txtCompletionTime });

            btnUpdateProgress = new Button
            {
                Text = "Update Progress",
                Location = new Point(20, 220),
                Size = new Size(150, 30)
            };
            btnUpdateProgress.Click += BtnUpdateProgress_Click;
            this.Controls.Add(btnUpdateProgress);
        }

        private TextBox CreateTextBox(string placeholder, Point location)
        {
            TextBox textBox = new TextBox
            {
                Location = location,
                Size = new Size(300, 30),
                ForeColor = Color.Gray,
                Text = placeholder
            };

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };

            return textBox;
        }

        private void BtnUpdateProgress_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Tasks SET Status = @Status, CompletionTime = @CompletionTime, Comments = @Comments WHERE ID = @TaskID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Status", txtStatus.Text);
                    command.Parameters.AddWithValue("@CompletionTime", txtCompletionTime.Text);
                    command.Parameters.AddWithValue("@Comments", txtComments.Text);
                    command.Parameters.AddWithValue("@TaskID", txtTaskID.Text);

                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Progress updated successfully!" : "Task not found.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating progress: {ex.Message}");
                }
            }
        }
    }
}
