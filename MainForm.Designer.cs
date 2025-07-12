using System.Drawing;
using System.Windows.Forms;

namespace AutoNexSmartManager
{
    partial class MainForm
    {
        private TextBox txtTaskSearch;
        private ComboBox cmbStatusFilter;
        private ComboBox cmbSortOrder;
        private DataGridView dgvTasks;
        private ProgressBar progressBarTasks;
        private Label lblProgress;
        private Button btnAddTask;
        private Button btnEditTask;
        private Button btnDeleteTask;

        private void InitializeComponent()
        {
            this.Text = "AutoNex – Smart Project Manager";
            this.Size = new Size(800, 600);

            txtTaskSearch = new TextBox
            {
                Location = new Point(20, 20),
                Width = 200,
                Name = "txtTaskSearch"
            };

            cmbStatusFilter = new ComboBox
            {
                Location = new Point(240, 20),
                Width = 150,
                Name = "cmbStatusFilter",
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatusFilter.Items.AddRange(new string[] { "All", "To Do", "In Progress", "Done" });
            cmbStatusFilter.SelectedIndex = 0;

            cmbSortOrder = new ComboBox
            {
                Location = new Point(410, 20),
                Width = 180,
                Name = "cmbSortOrder",
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSortOrder.Items.AddRange(new string[]
            {
                "None", "Due Date Asc", "Due Date Desc",
                "Priority High → Low", "Priority Low → High"
            });
            cmbSortOrder.SelectedIndex = 0;
            cmbSortOrder.SelectedIndexChanged += CmbSortOrder_SelectedIndexChanged;

            dgvTasks = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(740, 350),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                Name = "dgvTasks"
            };

            progressBarTasks = new ProgressBar
            {
                Location = new Point(20, 430),
                Size = new Size(400, 20),
                Name = "progressBarTasks"
            };

            lblProgress = new Label
            {
                Location = new Point(430, 430),
                Size = new Size(150, 20),
                Name = "lblProgress",
                Text = "Progress: 0%"
            };

            btnAddTask = new Button
            {
                Location = new Point(20, 470),
                Size = new Size(100, 30),
                Text = "Add Task"
            };
            btnAddTask.Click += BtnAddTask_Click;

            btnEditTask = new Button
            {
                Location = new Point(140, 470),
                Size = new Size(100, 30),
                Text = "Edit Task"
            };
            btnEditTask.Click += BtnEditTask_Click;

            btnDeleteTask = new Button
            {
                Location = new Point(260, 470),
                Size = new Size(100, 30),
                Text = "Delete Task"
            };
            btnDeleteTask.Click += BtnDeleteTask_Click;

            this.Controls.AddRange(new Control[]
            {
                txtTaskSearch, cmbStatusFilter, cmbSortOrder,
                dgvTasks, progressBarTasks, lblProgress,
                btnAddTask, btnEditTask, btnDeleteTask
            });

            this.AcceptButton = btnAddTask;
            this.Load += MainForm_Load;
        }
    }
}