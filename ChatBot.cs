public class ChatBot
{
    private KeywordResponder responder;
    private SentimentDetector detector;
    private MemoryStore memory;

    private string currentTopic = "";

    public ChatBot()
    {
        responder = new KeywordResponder();
        detector = new SentimentDetector();
        memory = new MemoryStore();
    }

    // ✅ THIS FIXES GetLogo ERROR
    public string GetLogo()
    {
        return
@"   ____      _                      _   _
  / ___|   _| |__   ___ _ __ ___  | | | | ___  _ __
 | |  | | | | '_ \ / _ \ '__/ _ \ | |_| |/ _ \| '_ \
 | |__| |_| | |_) |  __/ | |  __/ |  _  | (_) | | | |
  \____\__, |_.__/ \___|_|  \___| |_| |_|\___/|_| |_|
       |___/";
    }

    // ✅ THIS FIXES GetResponse ERROR
    public string GetResponse(string input, string userName)
    {
        string lower = input.ToLower();

        if (lower.Contains("how are you"))
            return $"I'm doing well, {userName}! Staying secure online.";

        if (lower.Contains("purpose"))
            return "I help you learn how to stay safe from cyber threats.";

        if (lower.Contains("what can i ask"))
            return "You can ask me about passwords, phishing, browsing, scams and privacy.";

        string mood = detector.DetectSentiment(input);

        if (mood == "worried")
            return "It's okay to feel worried. Always verify links before clicking.";

        if (mood == "frustrated")
            return "Cybersecurity can be confusing. Take it step by step.";

        if (mood == "curious")
            return "Great curiosity! That helps you stay safe online.";

        if (lower.Contains("tell me more") ||
            lower.Contains("another tip") ||
            lower.Contains("explain more"))
        {
            if (currentTopic != "")
                return responder.GetKeywordResponse(currentTopic);

            return "Tell me which topic you want more info about.";
        }

        string[] keywords = { "password", "phishing", "privacy", "scam", "browsing" };

        foreach (string keyword in keywords)
        {
            if (lower.Contains(keyword))
            {
                currentTopic = keyword;
                return responder.GetKeywordResponse(keyword);
            }
        }

        return "I'm not sure I understand. Can you rephrase that?";
    }
}