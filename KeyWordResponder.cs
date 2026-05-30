using System;
using System.Collections.Generic;

public class KeywordResponder
{
    private Random random = new Random();

    private Dictionary<string, List<string>> responses =
        new Dictionary<string, List<string>>();

    public KeywordResponder()
    {
        responses.Add("password", new List<string>
        {
            "Use strong passwords with numbers and symbols.",
            "Never reuse passwords across accounts.",
            "Use a password manager."
        });

        responses.Add("phishing", new List<string>
        {
            "Don't click suspicious links.",
            "Check email sender carefully.",
            "Scammers pretend to be trusted companies."
        });

        responses.Add("privacy", new List<string>
        {
            "Adjust your privacy settings regularly.",
            "Don't share personal info online.",
            "Enable two-factor authentication."
        });

        responses.Add("scam", new List<string>
        {
            "Never send money to strangers online.",
            "If it sounds too good, it is a scam.",
            "Verify before trusting messages."
        });

        responses.Add("browsing", new List<string>
        {
            "Only use trusted websites.",
            "Avoid pop-up links.",
            "Keep your browser updated."
        });
    }

    public string? GetKeywordResponse(string keyword)
    {
        if (responses.ContainsKey(keyword))
        {
            Random r = new Random();
            int index = r.Next(responses[keyword].Count);
            return responses[keyword][index];
        }

        return null;
    }
}