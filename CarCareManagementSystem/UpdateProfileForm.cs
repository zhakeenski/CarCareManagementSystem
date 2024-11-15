using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CarCareManagementSystem
{
    public class UpdateProfileForm : Form
    {
        private TextBox txtName, txtContact, txtPassword;
        private Button btnUpdateProfile;

        private string connectionString = "Data Source=KZ070BRO12;Initial Catalog=CarCareDB;Integrated Security=True";

        public UpdateProfileForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Update Profile";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Update Your Profile",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            txtName = CreateTextBox("Name", new Point(20, 60));
            txtContact = CreateTextBox("Contact Info", new Point(20, 100));
            txtPassword = CreateTextBox("New Password", new Point(20, 140));

            this.Controls.AddRange(new Control[] { txtName, txtContact, txtPassword });

            btnUpdateProfile = new Button
            {
                Text = "Update Profile",
                Location = new Point(20, 180),
                Size = new Size(150, 30)
            };
            btnUpdateProfile.Click += BtnUpdateProfile_Click;
            this.Controls.Add(btnUpdateProfile);
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

        private void BtnUpdateProfile_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Mechanics SET Name = @Name, ContactInfo = @ContactInfo, Password = @Password WHERE ID = @MechanicID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", txtName.Text);
                    command.Parameters.AddWithValue("@ContactInfo", txtContact.Text);
                    command.Parameters.AddWithValue("@Password", txtPassword.Text);
                    command.Parameters.AddWithValue("@MechanicID", 1); // Укажите реальный ID

                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Profile updated successfully!" : "Error updating profile.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating profile: {ex.Message}");
                }
            }
        }
    }
}
