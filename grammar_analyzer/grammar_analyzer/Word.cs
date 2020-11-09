namespace grammar_analyzer.lexer
{
    public class Word: Token
    {
        private string _lexeme;

        public Word(string lexeme, int tag) : base(tag)
        {
            this._lexeme = lexeme;
        }
	
        public string GetLexeme() {
            return _lexeme;
        }

        public override string ToString() {
            if (this.GetTag() == Tag.Id)
            {
                return "identifier";
            }
            
            return "" + _lexeme;
        }
    }
}