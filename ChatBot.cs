using System;

public class ChatBot
{
    private KeywordResponder responder;
    private SentimentDetector detector;
    private MemoryStore memory;

    private TaskManager taskManager;
    private ActivityLogger logger;
    private QuizManager quizManager;

    private string currentTopic = "";

    public string GetLogo()
{
    return @"
 ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗
██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝
██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   
██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   
╚██████╗   ██║   ██████╔╝███████╗██████╔╝██║  ██║╚██████╔╝   ██║   
 ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═════╝ ╚═╝  ╚═╝ ╚═════╝    ╚═╝   

        ╔══════════════════════════════════════╗
        ║   CYBERSECURITY AWARENESS BOT v3.0   ║
        ║   Protect • Learn • Stay Safe Online  ║
        ╚══════════════════════════════════════╝
";
}
    public ChatBot(TaskManager tm, ActivityLogger log, QuizManager qm)
    {
        responder = new KeywordResponder();
        detector = new SentimentDetector();
        memory = new MemoryStore();

        taskManager = tm;
        logger = log;
        quizManager = qm;
    }

    public string ProcessInput(string input, string userName)
    {
        string lower = input.ToLower();

        // ---------------- TASK INTENT ----------------
        if (lower.Contains("add task") || lower.Contains("create task"))
        {
            string title = input.Replace("add task", "").Replace("create task", "").Trim();

            taskManager.AddTask(title, "", "");

            logger.Log($"Task added: '{title}'");

            return $"Task added: '{title}'. Would you like a reminder?";
        }

        // ---------------- QUIZ INTENT ----------------
        if (lower.Contains("start quiz") || lower.Contains("quiz me"))
        {
            quizManager.ResetQuiz();
            logger.Log("Quiz started");

            return quizManager.GetCurrentQuestion().Question;
        }

        // ---------------- LOG INTENT ----------------
        if (lower.Contains("show activity log") || lower.Contains("what have you done"))
        {
            logger.Log("Activity log viewed");
            return logger.GetRecentLog();
        }

        // ---------------- SENTIMENT ----------------
        string mood = detector.DetectSentiment(input);

        if (mood == "worried")
            return "It's okay to feel worried. Always verify links.";

        if (mood == "frustrated")
            return "Take it step by step. Cybersecurity is learnable.";

        if (mood == "curious")
            return "Great curiosity! That keeps you safe.";

        // ---------------- KEYWORDS ----------------
        string[] keywords = { "password", "phishing", "privacy", "scam", "browsing" };

        foreach (string k in keywords)
        {
            if (lower.Contains(k))
            {
                currentTopic = k;
                logger.Log($"Keyword matched: {k}");
                return responder.GetKeywordResponse(k);
            }
        }

        return "I’m not sure I understand. Try rephrasing.";
    }
}