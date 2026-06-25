using System.Media;

public static void Playgreeting()
{
    try
    {
        string path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Assets",
            "greeting.wav");

        Console.WriteLine("Looking for audio at: " + path);

        if (!File.Exists(path))
        {
            Console.WriteLine("Audio file not found!");
            return;
        }

        using (SoundPlayer player = new SoundPlayer(path))
        {
            player.Load();
            player.Play();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Audio error: " + ex.Message);
    }
}