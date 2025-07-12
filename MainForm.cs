using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutoNexSmartManager
{
    public partial class MainForm : Form
    {
        private readonly ProjectData projectData = new ProjectData();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadTasks();

            var project = projectData.Projects.FirstOrDefault();
            var overdueCount = project?.Tasks.Count(t => t.DueDate < DateTime.Now && t.Status != "Done") ?? 0;

            if (overdueCount > 0)
            {
                MessageBox.Show($"You have {overdueCount} overdue task(s). Consider updating them!",
                    "Task Reminder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadTasks()
        {
            var project = projectData.Projects.FirstOrDefault();
            if (project == null) return;

            var filteredTasks = project.Tasks;

            if (cmbStatusFilter.SelectedItem?.ToString() != "All")
            {
                filteredTasks = filteredTasks
                    .Where(t => t.Status == cmbStatusFilter.SelectedItem.ToString())
                    .ToList();
            }

            // 🔽 Apply sorting
            switch (cmbSortOrder?.SelectedItem?.ToString())
            {
                case "Due Date Asc":
                    filteredTasks = filteredTasks.OrderBy(t => t.DueDate).ToList();
                    break;
                case "Due Date Desc":
                    filteredTasks = filteredTasks.OrderByDescending(t => t.DueDate).ToList();
                    break;
                case "Priority High → Low":
                    filteredTasks = filteredTasks.OrderByDescending(t => t.Priority).ToList();
                    break;
                case "Priority Low → High":
                    filteredTasks = filteredTasks.OrderBy(t => t.Priority).ToList();
                    break;
            }

            dgvTasks.DataSource = filteredTasks.Select(t => new
            {
                t.Name,
                t.Status,
                DueDate = t.DueDate.ToShortDateString(),
                t.Priority,
                t.AssignedTo,
                t.Description
            }).ToList();

            // 🔴 Highlight overdue tasks
            foreach (DataGridViewRow row in dgvTasks.Rows)
            {
                if (row.Cells["DueDate"] != null &&
                    DateTime.TryParse(row.Cells["DueDate"].Value.ToString(), out DateTime dueDate))
                {
                    string status = row.Cells["Status"].Value?.ToString();
                    if (dueDate < DateTime.Now && status != "Done")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                }
            }

            progressBarTasks.Value = (int)project.ProgressPercentage;
            lblProgress.Text = $"Progress: {project.ProgressPercentage:F1}%";
        }

        private void CmbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTasks();
        }

        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            var form = new AddTaskForm();
            var project = projectData.Projects.FirstOrDefault();

            if (project == null)
            {
                MessageBox.Show("No project found. Please add one first.");
                return;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                project.Tasks.Add(new TaskItem
                {
                    Name = form.TaskName,
                    Status = form.Status,
                    DueDate = form.DueDate,
                    Priority = form.Priority,
                    AssignedTo = form.AssignedTo,
                    Description = form.Description
                });

                projectData.SaveData();
                LoadTasks();
            }
        }

        private void BtnEditTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.CurrentRow == null)
            {
                MessageBox.Show("Select a task to edit.");
                return;
            }

            string taskName = dgvTasks.CurrentRow.Cells["Name"].Value.ToString();
            var project = projectData.Projects.FirstOrDefault();
            var task = project?.Tasks.FirstOrDefault(t => t.Name == taskName);

            if (task != null)
            {
                var form = new AddTaskForm();
                form.PopulateForm(task);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    task.Name = form.TaskName;
                    task.Status = form.Status;
                    task.DueDate = form.DueDate;
                    task.Priority = form.Priority;
                    task.AssignedTo = form.AssignedTo;
                    task.Description = form.Description;

                    projectData.SaveData();
                    LoadTasks();
                }
            }
        }

        private void BtnDeleteTask_Click(object sender, EventArgs e)
        {
            if (dgvTasks.CurrentRow == null)
            {
                MessageBox.Show("Select a task to delete.");
                return;
            }

            string taskName = dgvTasks.CurrentRow.Cells["Name"].Value.ToString();
            var project = projectData.Projects.FirstOrDefault();
            var task = project?.Tasks.FirstOrDefault(t => t.Name == taskName);

            if (task != null && project.Tasks.Remove(task))
            {
                projectData.SaveData();
                LoadTasks();
            }
        }
    }
}