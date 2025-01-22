using System;
/*using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        var testProjectPath = ".";

        // Create a process to run the xUnit tests
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"test {testProjectPath} --no-build",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (var process = Process.Start(startInfo))
        {
            using (var reader = process.StandardOutput)
            {
                string output = reader.ReadToEnd();
                Console.WriteLine(output);  // Output the test results to the console
            }

            process.WaitForExit();
        }
    }
}*/