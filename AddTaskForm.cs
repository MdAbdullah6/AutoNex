using System;
using System.Windows.Forms;

namespace AutoNexSmartManager
{
    public partial class AddTaskForm : Form
    {
        public AddTaskForm()
        {
            InitializeComponent();
        }

        public string TaskName => txtTaskName.Text.Trim();
        public DateTime DueDate => dtpDueDate.Value;
        public string Status => cmbStatus.SelectedItem?.ToString();
        public string Priority => cmbPriority.SelectedItem?.ToString();
        public string AssignedTo => txtAssignedTo.Text.Trim();
        public string Description => txtDescription.Text.Trim();

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text) || cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void PopulateForm(TaskItem task)
        {
            txtTaskName.Text = task.Name;
            cmbStatus.SelectedItem = task.Status;
            cmbPriority.SelectedItem = task.Priority;
            dtpDueDate.Value = task.DueDate;
            txtAssignedTo.Text = task.AssignedTo;
            txtDescription.Text = task.Description;
        }
    }
}