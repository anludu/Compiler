namespace grammar_analyzer.lexer
{
    public class Word: Token
    {
        private string lexeme;
	
        public Word(string lexeme, int tag) : base(tag)
        {
            this.lexeme = lexeme;
        }
	
        public string getLexeme() {
            return lexeme;
        }
	
        public override string ToString() {
            return "WORD, lexeme = " + lexeme;
        }
    }
}