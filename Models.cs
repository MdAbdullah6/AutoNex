using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace AutoNexSmartManager
{
    // Task Item Model
    public class TaskItem
    {
        public string Name { get; set; }
        public string Status { get; set; } = "To Do";
        public DateTime DueDate { get; set; }
        public string Description { get; set; } = "";
        public string Priority { get; set; } = "Medium";
        public string AssignedTo { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsOverdue => DueDate < DateTime.Now && Status != "Done";
    }

    // Project Model
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime Deadline { get; set; } = DateTime.Now.AddDays(30);
        public string Priority { get; set; } = "Medium";
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public double ProgressPercentage
        {
            get
            {
                if (Tasks.Count == 0) return 0;
                var completedTasks = Tasks.Count(t => t.Status == "Done");
                return (double)completedTasks / Tasks.Count * 100;
            }
        }
    }

    // Project Data Manager
    public class ProjectData
    {
        public List<Project> Projects { get; set; } = new List<Project>();
        private readonly string _dataFilePath = "projects.json";

        public ProjectData()
        {
            LoadData();
        }

        // Load data from JSON file
        public void LoadData()
        {
            try
            {
                if (File.Exists(_dataFilePath))
                {
                    string jsonData = File.ReadAllText(_dataFilePath);
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        Projects = JsonConvert.DeserializeObject<List<Project>>(jsonData) ?? new List<Project>();
                    }
                }
                else
                {
                    // Create sample data if file doesn't exist
                    CreateSampleData();
                }
            }
            catch (Exception ex)
            {
                // If there's an error loading data, start with sample data
                CreateSampleData();
                System.Windows.Forms.MessageBox.Show($"Error loading data: {ex.Message}. Starting with sample data.");
            }
        }

        // Save data to JSON file
        public void SaveData()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(Projects, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(_dataFilePath, jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        // Create sample data for testing
        private void CreateSampleData()
        {
            Projects = new List<Project>
            {
                new Project
                {
                    Name = "Sample Project 1",
                    Description = "This is a sample project",
                    StartDate = DateTime.Now,
                    Deadline = DateTime.Now.AddDays(30),
                    Priority = "High",
                    Tasks = new List<TaskItem>
                    {
                        new TaskItem
                        {
                            Name = "Task 1",
                            Status = "To Do",
                            DueDate = DateTime.Now.AddDays(5),
                            Priority = "High",
                            Description = "First task description"
                        },
                        new TaskItem
                        {
                            Name = "Task 2",
                            Status = "In Progress",
                            DueDate = DateTime.Now.AddDays(10),
                            Priority = "Medium",
                            Description = "Second task description"
                        },
                        new TaskItem
                        {
                            Name = "Task 3",
                            Status = "Done",
                            DueDate = DateTime.Now.AddDays(-2),
                            Priority = "Low",
                            Description = "Completed task"
                        }
                    }
                },
                new Project
                {
                    Name = "Sample Project 2",
                    Description = "Another sample project",
                    StartDate = DateTime.Now.AddDays(-10),
                    Deadline = DateTime.Now.AddDays(20),
                    Priority = "Medium",
                    Tasks = new List<TaskItem>
                    {
                        new TaskItem
                        {
                            Name = "Project 2 Task 1",
                            Status = "To Do",
                            DueDate = DateTime.Now.AddDays(3),
                            Priority = "Medium",
                            Description = "Task for project 2"
                        }
                    }
                }
            };

            SaveData();
        }

        // Helper methods
        public void AddProject(Project project)
        {
            Projects.Add(project);
            SaveData();
        }

        public void RemoveProject(string projectName)
        {
            Projects.RemoveAll(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));
            SaveData();
        }

        public Project GetProject(string projectName)
        {
            return Projects.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));
        }

        public void UpdateProject(Project project)
        {
            var existingProject = GetProject(project.Name);
            if (existingProject != null)
            {
                var index = Projects.IndexOf(existingProject);
                Projects[index] = project;
                SaveData();
            }
        }

        public List<TaskItem> GetOverdueTasks()
        {
            var overdueTasks = new List<TaskItem>();
            foreach (var project in Projects)
            {
                overdueTasks.AddRange(project.Tasks.Where(t => t.IsOverdue));
            }
            return overdueTasks;
        }

        public List<TaskItem> GetTasksDueToday()
        {
            var today = DateTime.Now.Date;
            var tasksDueToday = new List<TaskItem>();
            foreach (var project in Projects)
            {
                tasksDueToday.AddRange(project.Tasks.Where(t => t.DueDate.Date == today && t.Status != "Done"));
            }
            return tasksDueToday;
        }
    }
}