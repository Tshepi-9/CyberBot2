using System;
using System.Collections.Generic;
using System.Windows; 

namespace CyberBot2
{
    public partial class MainWindow : Window
    {
        // Chat variables
        private string userName = "";

        // Task list
        private List<TaskItem> tasks = new List<TaskItem>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // ---------------- CHATBOT ----------------

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            userName = NameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please enter your name first.");
                return;
            }

            ChatDisplay.Text += $"Bot: Hello {userName}! Welcome to Cybersecurity Awareness Bot.\n";
            ChatDisplay.Text += "Bot: Ask me anything about staying safe online.\n\n";
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = UserInput.Text.Trim();

            if (string.IsNullOrEmpty(message))
                return;

            ChatDisplay.Text += $"{userName}: {message}\n";

            string response = GetBotResponse(message);

            ChatDisplay.Text += $"Bot: {response}\n\n";

            UserInput.Clear();
        }

        private string GetBotResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("password"))
                return "Use strong passwords with letters, numbers, and symbols.";

            if (input.Contains("phishing"))
                return "Phishing is when attackers trick you into giving personal info.";

            if (input.Contains("virus") || input.Contains("malware"))
                return "Malware can harm your device. Avoid suspicious links and downloads.";

            if (input.Contains("privacy"))
                return "Keep your personal information private online.";

            if (input.Contains("hello") || input.Contains("hi"))
                return "Hello! How can I help you stay safe online?";

            return "Sorry, I don't understand. Try asking about passwords, phishing, malware, or privacy.";
        }

        // ---------------- TASK ASSISTANT ----------------

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text.Trim();
            string description = TaskDescriptionTextBox.Text.Trim();
            string reminder = TaskReminderTextBox.Text.Trim();

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Please enter a task title.");
                return;
            }

            TaskItem task = new TaskItem
            {
                Title = title,
                Description = description,
                Reminder = reminder,
                IsCompleted = false
            };

            tasks.Add(task);
            RefreshTaskList();

            TaskTitleTextBox.Clear();
            TaskDescriptionTextBox.Clear();
            TaskReminderTextBox.Clear();
        }

        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select a task first.");
                return;
            }

            tasks[TaskListBox.SelectedIndex].IsCompleted = true;
            RefreshTaskList();
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select a task first.");
                return;
            }

            tasks.RemoveAt(TaskListBox.SelectedIndex);
            RefreshTaskList();
        }

        private void RefreshTaskList()
        {
            TaskListBox.Items.Clear();

            foreach (var task in tasks)
            {
                string status = task.IsCompleted ? " Completed" : " Pending";

                TaskListBox.Items.Add(
                    $"{task.Title} - {task.Description} - Reminder: {task.Reminder} [{status}]"
                );
            }
        }
    }

    // ---------------- TASK CLASS ----------------

    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Reminder { get; set; }
        public bool IsCompleted { get; set; }
    }
}