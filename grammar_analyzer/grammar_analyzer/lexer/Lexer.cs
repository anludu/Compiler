using System;
using System.Collections;
using System.IO;
using System.Text;

namespace grammar_analyzer.lexer
{
    public class Lexer : SystemException
    {
        private char peek;
        private Hashtable words = new Hashtable();
        private StreamReader input;
        public static int line = 1;

        public Lexer(StreamReader input)
        {
            this.peek = ' ';
            this.input = input;
        }

        private void Readch()
        {
            try
            {
                peek = (char) input.Read();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.GetType().Name);
            }

        }

        public Token Scan()
        {
            for ( ; ; Readch() ) {
                if (peek == ' ' || peek == '\t') {
                    continue;
                } else if (peek == '\n') {
                    line = line + 1;
                } else {
                    break;
                }
            }
			
            if (Char.IsDigit(peek)) {
                int v = 0;
                do {
                    v = (10 * v) + Convert.ToInt32(peek.ToString(), 10);
                    Readch();
                } while ( Char.IsDigit(peek) );
                return new Number(v);
            }
		
            if (Char.IsLetter(peek)) {
                StringBuilder b = new StringBuilder();
			
                do {
                    b.Append(Char.ToLower(peek));
                    Readch();
                } while ( Char.IsLetterOrDigit(peek) ) ;
                String s = b.ToString();
                Word w = (Word) words[s];
                if (w != null) 
                    return w;
		
                w = new Word(s, Tag.ID);
                words.Add(s, w);
                return w;
            }
		
            Token tok = new Token(peek); peek = ' ';
            return tok;
        }
    }
}