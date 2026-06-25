using System.Collections.Generic;

public class QuizManager
{
    private List<QuizQuestion> _questions;
    private int _currentIndex;
    private int _score;

    public QuizManager()
    {
        _questions = new List<QuizQuestion>();

        // 1
        _questions.Add(new QuizQuestion
        {
            Question = "What is phishing?",
            Options = new List<string> { "A scam to steal data", "A game", "A browser", "A virus update" },
            CorrectAnswer = "A",
            Explanation = "Phishing tricks users into giving sensitive information.",
            IsTrueFalse = false
        });

        // 2
        _questions.Add(new QuizQuestion
        {
            Question = "True or False: You should reuse passwords.",
            Options = new List<string> { "True", "False" },
            CorrectAnswer = "B",
            Explanation = "False. Always use unique passwords.",
            IsTrueFalse = true
        });

        // 3
        _questions.Add(new QuizQuestion
        {
            Question = "What is 2FA?",
            Options = new List<string> { "Two-factor authentication", "Fake app", "Firewall", "Virus scanner" },
            CorrectAnswer = "A",
            Explanation = "2FA adds extra security to accounts.",
            IsTrueFalse = false
        });

        // 4
        _questions.Add(new QuizQuestion
        {
            Question = "Safe browsing includes:",
            Options = new List<string> { "Clicking ads", "Using HTTPS sites", "Ignoring updates", "Sharing passwords" },
            CorrectAnswer = "B",
            Explanation = "HTTPS helps secure websites.",
            IsTrueFalse = false
        });

        // 5
        _questions.Add(new QuizQuestion
        {
            Question = "True or False: Public Wi-Fi is always safe.",
            Options = new List<string> { "True", "False" },
            CorrectAnswer = "B",
            Explanation = "Public Wi-Fi can be risky.",
            IsTrueFalse = true
        });

        // 6
        _questions.Add(new QuizQuestion
        {
            Question = "What is malware?",
            Options = new List<string> { "Helpful tool", "Malicious software", "Browser", "Email service" },
            CorrectAnswer = "B",
            Explanation = "Malware is harmful software.",
            IsTrueFalse = false
        });

        // 7
        _questions.Add(new QuizQuestion
        {
            Question = "Best password practice?",
            Options = new List<string> { "123456", "Reuse passwords", "Strong unique passwords", "Share with friends" },
            CorrectAnswer = "C",
            Explanation = "Strong unique passwords are safest.",
            IsTrueFalse = false
        });

        // 8
        _questions.Add(new QuizQuestion
        {
            Question = "Social engineering is:",
            Options = new List<string> { "Tricking people", "Coding", "Firewall", "Encryption" },
            CorrectAnswer = "A",
            Explanation = "It manipulates people into giving info.",
            IsTrueFalse = false
        });

        // 9
        _questions.Add(new QuizQuestion
        {
            Question = "True or False: You should click unknown links.",
            Options = new List<string> { "True", "False" },
            CorrectAnswer = "B",
            Explanation = "Never click suspicious links.",
            IsTrueFalse = true
        });

        // 10
        _questions.Add(new QuizQuestion
        {
            Question = "What helps protect accounts?",
            Options = new List<string> { "2FA", "Weak password", "Sharing login", "Ignoring alerts" },
            CorrectAnswer = "A",
            Explanation = "2FA improves account security.",
            IsTrueFalse = false
        });

        ResetQuiz();
    }

    public QuizQuestion GetCurrentQuestion()
    {
        return _questions[_currentIndex];
    }

    public bool SubmitAnswer(string answer)
    {
        bool correct = answer.ToUpper() == _questions[_currentIndex].CorrectAnswer;

        if (correct)
            _score++;

        _currentIndex++;
        return correct;
    }

    public bool IsFinished()
    {
        return _currentIndex >= _questions.Count;
    }

    public string GetFinalScore()
    {
        return $"Score: {_score} / {_questions.Count}";
    }

    public string GetFinalMessage()
    {
        return _score >= 7 ? "Great job!" : "Keep learning cybersecurity!";
    }

    public void ResetQuiz()
    {
        _currentIndex = 0;
        _score = 0;
    }
}