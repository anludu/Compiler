using System;
using System.Collections;
using System.IO;
using System.Text;

namespace grammar_analyzer.lexer
{
    public class Lexer : SystemException
    {
        private char _peek;
        private Hashtable _words = new Hashtable();
        private StreamReader _input;
        public static int Line = 1;
        private string[] _reserved = new string []{
            "program",
            "constant",
            "var",
            "begin",
            "end",
            "integer",
            "real",
            "boolean",
            "string",
            "writeln",
            "readln",
            "while",
            "do",
            "repeat",
            "until",
            "for",
            "to",
            "downto",
            "if",
            "then",
            "else",
            "not",
            "div",
            "mod",
            "and",
            "or",};

        public Lexer(StreamReader input)
        {
            this._peek = ' ';
            this._input = input;
            foreach (string word in _reserved)
            {
                _words.Add(word, new Word(word, Tag.Reserved));
            }
            

        }

        private void Readch()
        {
            try
            {
                _peek = (char) _input.Read();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.GetType().Name);
            }

        }

        public Token Scan()
        {
            for ( ; ; Readch() ) {
                if (_peek == ' ' || _peek == '\t') {
                    continue;
                } else if (_peek == '\n') {
                    Line = Line + 1;
                } else {
                    break;
                }
            }
            
			
            if (Char.IsDigit(_peek)) {
                int v = 0;
                do {
                    v = (10 * v) + Convert.ToInt32(_peek.ToString(), 10);
                    Readch();
                } while ( Char.IsDigit(_peek) );
                return new Number(v);
            }
		
            if (Char.IsLetter(_peek)) {
                StringBuilder b = new StringBuilder();
			
                do {
                    b.Append(Char.ToLower(_peek));
                    Readch();
                } while ( Char.IsLetterOrDigit(_peek) ) ;
                String s = b.ToString();
                Word w = (Word) _words[s];
                if (w != null) 
                    return w;
		
                w = new Word(s, Tag.Id);
                _words.Add(s, w);
                return w;
            }

            if (_peek == '(')
            {
                if (_input.Peek() == '*')
                {
                    Readch();
                    Readch();
                    while (_peek != '*')
                    {
                        Readch();
                        
                    }
                    Readch();
                    Readch();
                    return Scan();
                }
            }
            
            if (_peek == '"')
            {
                Readch();
                while (_peek != '"')
                {
                    Readch();
                    
                }
                Readch();
                return new Word("string", Tag.Reserved);
            }


            Token tok = new Token(_peek); 
            _peek = ' ';
            return tok;
        }
    }
}