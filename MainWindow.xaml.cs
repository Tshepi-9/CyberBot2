
using System.Windows;

namespace CyberBot2
{
    public partial class MainWindow : Window
    {
        private ChatBot bot;
        private string userName = "";

        public MainWindow()
        {
            InitializeComponent();

            bot = new ChatBot();

            Audio.PlayGreeting();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            userName = NameTextBox.Text;

            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Please enter your name.");
                return;
            }

            ChatDisplay.AppendText(bot.GetLogo() + "\n\n");

            ChatDisplay.AppendText(
                $"Welcome {userName}! I am your Cybersecurity Awareness Bot.\n");

            ChatDisplay.AppendText(
                "Ask me anything about cybersecurity.\n\n");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInput.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

            ChatDisplay.AppendText("You: " + input + "\n");

            string response = bot.GetResponse(input, userName);

            ChatDisplay.AppendText("Bot: " + response + "\n\n");

            UserInput.Clear();
        }
    }
}

