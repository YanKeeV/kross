using System;
using ClassLibrary1;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

class Program
{
    static void RunSubcommand(string subcommand, string input, string output)
    {
        if (string.IsNullOrWhiteSpace(subcommand))
        {
            Console.WriteLine("Please provide a subcommand (lab1, lab2, or lab3).");
            return;
        }

        string inputFilePath = input;

        Console.WriteLine(Environment.GetEnvironmentVariable("LAB_PATH"));

        if (string.IsNullOrWhiteSpace(input) && !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LAB_PATH")))
        {
            inputFilePath = Path.Combine(Environment.GetEnvironmentVariable("LAB_PATH"), $"{subcommand}_input.txt");
            Console.WriteLine(inputFilePath);
        }
        else if (string.IsNullOrWhiteSpace(input))
        {
            inputFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $"{subcommand}_input.txt");
            Console.WriteLine(inputFilePath);
        }

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("Input file not found.");
            return;
        }

        if (string.IsNullOrWhiteSpace(output))
        {
            output = $"{subcommand}_output.txt";
        }

        Class1 labs = new Class1();
        if(subcommand == "lab1")
        {
            labs.task1(inputFilePath, output);
        }
        if (subcommand == "lab2")
        {
            labs.task2(inputFilePath, output);
        }
        if (subcommand == "lab3")
        {
            labs.task3(inputFilePath, output);
        }

        // Implement the logic to run the specified subcommand with the determined input file and output options.
        Console.WriteLine($"Running {subcommand} with input file: {inputFilePath}, output: {output}");
    }

    static void ShowUsage()
    {
        Console.WriteLine("Usage: nazaiiy [command] [options]");
        Console.WriteLine("Commands:");
        Console.WriteLine("  version  - Display version information");
        Console.WriteLine("  run      - Run a lab (lab1, lab2, or lab3) with optional -I/--input and -o/--output options");
        Console.WriteLine("  set-path - Set the LAB_PATH environment variable with the -p/--path option");
    }

    static void ShowVersionInfo()
    {
        Console.WriteLine("Author: Nazarii Yankiv");
        Console.WriteLine("Version: 1.0");
    }

    static void SetLabPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Please provide a valid path.");
            return;
        }

        // Set the LAB_PATH environment variable with the provided path.
        Environment.SetEnvironmentVariable("LAB_PATH", path);
        Console.WriteLine($"LAB_PATH has been set to: {path}");
    }

    static int Main(string[] args)
    {

        var rootCommand = new RootCommand();


        var run = new Command("run");
        var subcommandOption = new Option<string>(new string[] { "--subcommand", "-s" }, "Subcommand (lab1, lab2, or lab3)");
        var inputOption = new Option<string>(new string[] { "--input", "-I" }, "Input file");
        var outputOption = new Option<string>(new string[] { "--output", "-o" }, "Output file");
        rootCommand.Add(run);
        run.Add(subcommandOption);
        run.Add(inputOption);
        run.Add(outputOption);


        var set_path = new Command("set-path");
        var pathOption = new Option<string>(new string[] { "--path", "-p" }, "Path to input and output files folder") { IsRequired = true };
        rootCommand.Add(set_path);
        set_path.Add(pathOption);

        var help = new Command("help");
        rootCommand.Add(help);

        var version = new Command("version");
        rootCommand.Add(version);

        version.SetHandler(() => { ShowVersionInfo(); });
        help.SetHandler(() => { ShowUsage(); });
        set_path.SetHandler((path) => { SetLabPath(path.ToString()); }, pathOption);
        run.SetHandler((subcommand, input, output) => { RunSubcommand(subcommand, input, output); }, subcommandOption, inputOption, outputOption);

        Console.WriteLine("Before command invoke");

        int result = rootCommand.Invoke(args);
        if (result != 0)
        {
            ShowUsage();
        }

        Console.WriteLine("After command invoke");

        return result;
    }

}