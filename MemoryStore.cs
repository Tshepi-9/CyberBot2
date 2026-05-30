using System.Collections.Generic;

public class MemoryStore
{
    private Dictionary<string, string> memory =
        new Dictionary<string, string>();

    public void Save(string key, string value)
    {
        memory[key] = value;
    }

    public string Get(string key)
    {
        if (memory.ContainsKey(key))
        {
            return memory[key];
        }

        return "";
    }
}