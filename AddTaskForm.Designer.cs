using System.Drawing;
using System.Windows.Forms;

namespace AutoNexSmartManager
{
    partial class AddTaskForm
    {
        private TextBox txtTaskName;
        private ComboBox cmbStatus;
        private ComboBox cmbPriority;
        private DateTimePicker dtpDueDate;
        private TextBox txtAssignedTo;
        private TextBox txtDescription;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.Text = "Add New Task";
            this.Size = new Size(400, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblTaskName = new Label { Text = "Task Name:", Location = new Point(20, 20), Size = new Size(80, 20) };
            txtTaskName = new TextBox { Location = new Point(120, 20), Size = new Size(230, 20) };

            Label lblStatus = new Label { Text = "Status:", Location = new Point(20, 60), Size = new Size(80, 20) };
            cmbStatus = new ComboBox { Location = new Point(120, 60), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new string[] { "To Do", "In Progress", "Done" });
            cmbStatus.SelectedIndex = 0;

            Label lblPriority = new Label { Text = "Priority:", Location = new Point(20, 100), Size = new Size(80, 20) };
            cmbPriority = new ComboBox { Location = new Point(120, 100), Size = new Size(150, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbPriority.Items.AddRange(new string[] { "Low", "Medium", "High" });
            cmbPriority.SelectedIndex = 1;

            Label lblDueDate = new Label { Text = "Due Date:", Location = new Point(20, 140), Size = new Size(80, 20) };
            dtpDueDate = new DateTimePicker { Location = new Point(120, 140), Size = new Size(150, 20), Format = DateTimePickerFormat.Short };

            Label lblAssignedTo = new Label { Text = "Assigned To:", Location = new Point(20, 180), Size = new Size(90, 20) };
            txtAssignedTo = new TextBox { Location = new Point(120, 180), Size = new Size(230, 20) };

            Label lblDescription = new Label { Text = "Description:", Location = new Point(20, 220), Size = new Size(80, 20) };
            txtDescription = new TextBox { Location = new Point(120, 220), Size = new Size(230, 60), Multiline = true };

            btnSave = new Button { Text = "Save", Location = new Point(120, 300), Size = new Size(80, 30) };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button { Text = "Cancel", Location = new Point(220, 300), Size = new Size(80, 30) };
            btnCancel.Click += BtnCancel_Click;

            this.Controls.AddRange(new Control[] {
                lblTaskName, txtTaskName,
                lblStatus, cmbStatus,
                lblPriority, cmbPriority,
                lblDueDate, dtpDueDate,
                lblAssignedTo, txtAssignedTo,
                lblDescription, txtDescription,
                btnSave, btnCancel
            });

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }
}