using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CarCareManagementSystem
{
    public class ManageInventoryForm : Form
    {
        private DataGridView dgvInventory;
        private TextBox txtPartName, txtQuantity;
        private Button btnRequestParts, btnUseParts, btnCheckShortage;

        private string connectionString = "Data Source=KZ070BRO12;Initial Catalog=CarCareDB;Integrated Security=True";

        public ManageInventoryForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Inventory";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "Manage Parts Inventory",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            dgvInventory = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(750, 300),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgvInventory);

            txtPartName = CreateTextBox("Part Name", new Point(20, 380));
            txtQuantity = CreateTextBox("Quantity", new Point(250, 380));
            this.Controls.AddRange(new Control[] { txtPartName, txtQuantity });

            btnRequestParts = new Button
            {
                Text = "Request Parts",
                Location = new Point(20, 420),
                Size = new Size(150, 30)
            };
            btnRequestParts.Click += BtnRequestParts_Click;
            this.Controls.Add(btnRequestParts);

            btnUseParts = new Button
            {
                Text = "Use Parts",
                Location = new Point(180, 420),
                Size = new Size(150, 30)
            };
            btnUseParts.Click += BtnUseParts_Click;
            this.Controls.Add(btnUseParts);

            btnCheckShortage = new Button
            {
                Text = "Check Shortage",
                Location = new Point(340, 420),
                Size = new Size(150, 30)
            };
            btnCheckShortage.Click += BtnCheckShortage_Click;
            this.Controls.Add(btnCheckShortage);
        }

        private TextBox CreateTextBox(string placeholder, Point location)
        {
            TextBox textBox = new TextBox
            {
                Location = location,
                Size = new Size(200, 30),
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

        private void BtnRequestParts_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO PartsInventory (PartName, Quantity) VALUES (@PartName, @Quantity)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PartName", txtPartName.Text);
                    command.Parameters.AddWithValue("@Quantity", int.Parse(txtQuantity.Text));

                    command.ExecuteNonQuery();
                    MessageBox.Show("Parts requested successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error requesting parts: {ex.Message}");
                }
            }
        }

        private void BtnUseParts_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE PartsInventory SET Quantity = Quantity - @Quantity WHERE PartName = @PartName";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PartName", txtPartName.Text);
                    command.Parameters.AddWithValue("@Quantity", int.Parse(txtQuantity.Text));

                    int rowsAffected = command.ExecuteNonQuery();
                    MessageBox.Show(rowsAffected > 0 ? "Parts updated successfully!" : "Part not found.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error using parts: {ex.Message}");
                }
            }
        }

        private void BtnCheckShortage_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT PartName, Quantity FROM PartsInventory WHERE Quantity < 5";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvInventory.DataSource = table;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking shortage: {ex.Message}");
                }
            }
        }
    }
}
