using System.Diagnostics;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("[FakeRat] Loding commands");

        //Full path to the JSON file containing commands

        String path = "C:\\Users\\**\\source\\repos\\05-FakeRAT-JSONCommandRunner\\commands.json";

        // Check if the json  file exists
        if (!File.Exists(path))
        {

            Console.WriteLine("Missng path " + path);
            return;
        }

        //Read the content of the json file into a string

        String json = File.ReadAllText(path);


        // Parse the JSON strings into JsonDocument
        // which allows us to navigate the JSON structure as an object tree.
        using JsonDocument doc = JsonDocument.Parse(json);
        //Get the root element of the JSON 
        // In our case, it's the entire JSON: { "commands": [...] }
        JsonElement root = doc.RootElement;

        // From the root, try to get the "commands" property,
        // which is expected to be an array of strings.

        // Try to get the "commands" array from the root object
        if (root.TryGetProperty("commands", out JsonElement commandsElement))
        {
            // Loop through each command in the array
            foreach (JsonElement cmd in commandsElement.EnumerateArray())
            {
                // Convert command to lowercase string
                String commands = cmd.GetString()?.ToLower();
                // Execute the command
                ExecuteCommand(commands);
            }
        }


    }
    // Executes a given command string
    static void ExecuteCommand(String command)
    {
        switch (command)
        {
            case "open_calc":
                Console.WriteLine("[Action] Opening Calculator...");
                Process.Start("calc.exe");
                break;
            case "print_time":
                Console.WriteLine("[Action] Time: " + DateTime.Now);
                break;
            case "write_log":
               
                Console.WriteLine("[Action] Writing log...");
                /*
               File.WriteAllText("rat_log.txt", $"FakeRAT ran at {DateTime.Now}\n");
               Console.WriteLine("Current directory: " + Environment.CurrentDirectory);
               */
                /*
                String timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                String filelog = $"rat_log{timestamp}.txt";
                File.WriteAllText(filelog, $"FakeRAT ran at {DateTime.Now}");
                Console.WriteLine($"[Info] Log saved as: {filelog}");
                */
                // Get current user name
                String user = Environment.UserName;
                // Create a log line with timestamp and username
                String logLine = $"FakeRAT ran at {DateTime.Now} as user: {user}\n";
                // Append the log line to the file (creates it if not exists)
                File.AppendAllText("rat_log.txt", logLine);
                Console.WriteLine("[Info] Log added to rat_log.txt");


                break;
            case "invalid_command":
                Console.WriteLine($"[Unknown] Command not recognized: {command}");
                break;








        }
    }
}

