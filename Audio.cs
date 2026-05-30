using System;
using System.IO;
using System.Media;

public class Audio
{
    public static void PlayGreeting()
    {
        try
        {
            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Assets",
                "greeting.wav");

            if (!File.Exists(path))
                return;

            SoundPlayer player = new SoundPlayer(path);
            player.Load();
            player.Play();
        }
        catch (Exception)
        {
            // prevents crash if audio fails
        }
    }
}