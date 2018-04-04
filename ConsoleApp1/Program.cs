using GoogleClosureProcessor.Models;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GoogleClosureProcessor.GoogleClosureProcessor gc = new GoogleClosureProcessor.GoogleClosureProcessor();
            GoogleClosureResponse script = gc.Compress(File.ReadAllText("test.js"));
            Console.ReadLine();
        }
    }
}
