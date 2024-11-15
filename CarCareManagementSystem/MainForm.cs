using System;
using System.Drawing;
using System.Windows.Forms;

namespace CarCareManagementSystem
{
    public class MainForm : Form
    {
        private Button btnViewTasks, btnRecordProgress, btnManageInventory, btnUpdateProfile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Настройки формы
            this.Text = "CarCare Service Center - Main Dashboard";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Заголовок
            Label lblTitle = new Label
            {
                Text = "Welcome to CarCare Service Center",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(80, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            // Кнопка "View Tasks"
            btnViewTasks = CreateButton("View Tasks", new Point(200, 80), BtnViewTasks_Click);
            this.Controls.Add(btnViewTasks);

            // Кнопка "Record Progress"
            btnRecordProgress = CreateButton("Record Progress", new Point(200, 140), BtnRecordProgress_Click);
            this.Controls.Add(btnRecordProgress);

            // Кнопка "Manage Inventory"
            btnManageInventory = CreateButton("Manage Inventory", new Point(200, 200), BtnManageInventory_Click);
            this.Controls.Add(btnManageInventory);

            // Кнопка "Update Profile"
            btnUpdateProfile = CreateButton("Update Profile", new Point(200, 260), BtnUpdateProfile_Click);
            this.Controls.Add(btnUpdateProfile);
        }

        // Метод для создания кнопок
        private Button CreateButton(string text, Point location, EventHandler onClick)
        {
            Button button = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(200, 40)
            };
            button.Click += onClick;
            return button;
        }

        // Переход на форму "View Tasks"
        private void BtnViewTasks_Click(object sender, EventArgs e)
        {
            ViewTasksForm viewTasksForm = new ViewTasksForm();
            viewTasksForm.Show();
        }

        // Переход на форму "Record Progress"
        private void BtnRecordProgress_Click(object sender, EventArgs e)
        {
            RecordProgressForm recordProgressForm = new RecordProgressForm();
            recordProgressForm.Show();
        }

        // Переход на форму "Manage Inventory"
        private void BtnManageInventory_Click(object sender, EventArgs e)
        {
            ManageInventoryForm manageInventoryForm = new ManageInventoryForm();
            manageInventoryForm.Show();
        }

        // Переход на форму "Update Profile"
        private void BtnUpdateProfile_Click(object sender, EventArgs e)
        {
            UpdateProfileForm updateProfileForm = new UpdateProfileForm();
            updateProfileForm.Show();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
