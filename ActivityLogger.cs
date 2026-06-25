using System;
using System.Collections.Generic;
using System.Linq;

public class ActivityLogger
{
    private List<string> _log = new List<string>();

    public void Log(string action)
    {
        string entry = DateTime.Now.ToString("[HH:mm] ") + action;
        _log.Add(entry);
    }

    public string GetRecentLog(int count = 10)
    {
        var recent = _log
            .Skip(Math.Max(0, _log.Count - count))
            .Take(count)
            .ToList();

        string result = "Here's a summary of recent actions:\n";

        for (int i = 0; i < recent.Count; i++)
        {
            result += $"{i + 1}. {recent[i]}\n";
        }

        return result;
    }

    public string GetFullLog()
    {
        string result = "Full Activity Log:\n";

        for (int i = 0; i < _log.Count; i++)
        {
            result += $"{i + 1}. {_log[i]}\n";
        }

        return result;
    }

    public int GetCount()
    {
        return _log.Count;
    }
}