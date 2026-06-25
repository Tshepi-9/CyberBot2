using System.Windows;

namespace CyberBot2
{
    public partial class MainWindow : Window
    {
        // CORE SYSTEMS
        private TaskManager taskManager;
        private ActivityLogger logger;
        private QuizManager quiz;
        private ChatBot bot;

        // USER
        private string userName = "";

        public MainWindow()
        {
            InitializeComponent();

            taskManager = new TaskManager();
            logger = new ActivityLogger();
            quiz = new QuizManager();

            bot = new ChatBot(taskManager, logger, quiz);

            LoadQuestion();
            QuizScoreText.Text = quiz.GetFinalScore();
        }

        // ---------------- CHAT START ----------------

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            userName = NameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Please enter your name.");
                return;
            }

            ChatDisplay.AppendText(bot.GetLogo() + "\n\n");
            ChatDisplay.AppendText($"Bot: Welcome {userName}! I am your Cybersecurity Bot.\n");
            ChatDisplay.AppendText("Bot: Ask me anything about cybersecurity.\n\n");

            logger.Log($"User started chat: {userName}");
        }

        // ---------------- CHAT SEND ----------------

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

            ChatDisplay.AppendText($"{userName}: {input}\n");

            string response = bot.ProcessInput(input, userName);

            ChatDisplay.AppendText($"Bot: {response}\n\n");

            logger.Log($"User message: {input}");

            UserInput.Clear();
        }

        // ---------------- TASK ASSISTANT ----------------

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleTextBox.Text.Trim();
            string description = TaskDescriptionTextBox.Text.Trim();
            string reminder = TaskReminderTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Enter a task title.");
                return;
            }

            taskManager.AddTask(title, description, reminder);

            logger.Log($"Task added manually: {title}");

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

            var tasks = taskManager.GetAllTasks();
            var task = tasks[TaskListBox.SelectedIndex];

            taskManager.MarkAsComplete(task.Id);

            logger.Log($"Task completed: {task.Title}");

            RefreshTaskList();
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select a task first.");
                return;
            }

            var tasks = taskManager.GetAllTasks();
            var task = tasks[TaskListBox.SelectedIndex];

            taskManager.DeleteTask(task.Id);

            logger.Log($"Task deleted: {task.Title}");

            RefreshTaskList();
        }

        private void RefreshTaskList()
        {
            TaskListBox.Items.Clear();

            var tasks = taskManager.GetAllTasks();

            foreach (var t in tasks)
            {
                string status = t.IsComplete ? "Completed" : "Pending";

                TaskListBox.Items.Add(
                    $"{t.Title} - {t.Description} - Reminder: {t.Reminder} [{status}]"
                );
            }
        }

        // ---------------- QUIZ ----------------

        private void LoadQuestion()
        {
            if (quiz.IsFinished())
            {
                QuizQuestionText.Text = "Quiz Finished!";
                QuizFeedbackText.Text = quiz.GetFinalScore() + "\n" + quiz.GetFinalMessage();

                SubmitAnswerButton.IsEnabled = false;
                NextQuestionButton.Visibility = Visibility.Collapsed;

                logger.Log("Quiz completed");

                return;
            }

            var q = quiz.GetCurrentQuestion();

            QuizQuestionText.Text = q.Question;

            OptionA.Content = q.Options[0];
            OptionB.Content = q.Options[1];
            OptionC.Content = q.Options.Count > 2 ? q.Options[2] : "";
            OptionD.Content = q.Options.Count > 3 ? q.Options[3] : "";

            OptionA.IsChecked = false;
            OptionB.IsChecked = false;
            OptionC.IsChecked = false;
            OptionD.IsChecked = false;

            QuizFeedbackText.Text = "";
            NextQuestionButton.Visibility = Visibility.Collapsed;

            QuizScoreText.Text = quiz.GetFinalScore();
        }

        private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string answer = "";

            if (OptionA.IsChecked == true) answer = "A";
            else if (OptionB.IsChecked == true) answer = "B";
            else if (OptionC.IsChecked == true) answer = "C";
            else if (OptionD.IsChecked == true) answer = "D";
            else
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            var currentQuestion = quiz.GetCurrentQuestion();
            bool correct = quiz.SubmitAnswer(answer);

            if (correct)
            {
                QuizFeedbackText.Text = "Correct! " + currentQuestion.Explanation;
            }
            else
            {
                QuizFeedbackText.Text = "Incorrect! " + currentQuestion.Explanation;
            }

            QuizScoreText.Text = quiz.GetFinalScore();

            NextQuestionButton.Visibility = Visibility.Visible;
            SubmitAnswerButton.IsEnabled = false;

            logger.Log("Quiz question answered");
        }

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            SubmitAnswerButton.IsEnabled = true;
            LoadQuestion();
        }

        // ---------------- ACTIVITY LOG ----------------

        private void ShowRecentLogButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityLogBox.Text = logger.GetRecentLog(10);
        }

        private void ShowFullLogButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityLogBox.Text = logger.GetFullLog();
        }

        private void ClearLogViewButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityLogBox.Clear();
        }
    }
}