using System;
using System.Diagnostics;
using System.IO;

class WavToMp3Converter
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the full path to the .wav file:");
        string? inputFilePath = Console.ReadLine()?.Trim()?.Trim('\"', '\'');

        if (string.IsNullOrEmpty(inputFilePath))
        {
            Console.WriteLine("No file path provided.");
            return;
        }

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("The file does not exist: " + inputFilePath);
            return;
        }

        string outputFilePath = Path.ChangeExtension(inputFilePath, ".mp3");

        try
        {
            Console.WriteLine($"Starting conversion: {inputFilePath} -> {outputFilePath}");

            Process ffmpeg = new Process();
            ffmpeg.StartInfo.FileName = "ffmpeg";
            ffmpeg.StartInfo.Arguments = $"-i \"{inputFilePath}\" \"{outputFilePath}\"";
            ffmpeg.StartInfo.RedirectStandardOutput = true;
            ffmpeg.StartInfo.RedirectStandardError = true;
            ffmpeg.StartInfo.UseShellExecute = false;
            ffmpeg.Start();

            string ffmpegOutput = ffmpeg.StandardError.ReadToEnd();
            ffmpeg.WaitForExit();

            Console.WriteLine("FFmpeg output:");
            Console.WriteLine(ffmpegOutput);

            if (ffmpeg.ExitCode == 0)
            {
                Console.WriteLine("Conversion complete. MP3 saved at: " + outputFilePath);
            }
            else
            {
                Console.WriteLine("FFmpeg failed. Check the output for details.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}
