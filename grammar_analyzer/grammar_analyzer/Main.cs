using grammar_analyzer;
using System;
using System.IO;

namespace grammar_analyzer
{
    public class Program
    {
        public static void Main (string[] args){
            if (args.Length != 1) {
                Console.WriteLine("usage: C# ConsoleApp file");
                Environment.Exit(0);
            }
            string filePath = args[0];
            var input = new StreamReader(filePath); 
            Parser parser = new Parser(input);
            parser.Analyze();
        }
    }
}