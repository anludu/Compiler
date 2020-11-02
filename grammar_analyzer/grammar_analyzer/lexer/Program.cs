using System;
using System.IO;


namespace grammar_analyzer.lexer
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
            Lexer lex = new Lexer(input);
            Token token = lex.Scan();
            while (token.GetTag() != Tag.EOF) {
                //Console.WriteLine("From Program");
                Console.WriteLine(token.ToString());
                token = lex.Scan();
            }
        }
    }
}