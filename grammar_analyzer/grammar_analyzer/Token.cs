namespace grammar_analyzer.lexer
{
    public class Token
    {
        private int _tag;

	
        public Token(int tag) {
            this._tag = tag;

        }
	
        public int GetTag() {
            return _tag;
        }

        public override string ToString() {
            string s = "";
		
            switch (_tag) {
                case Tag.Eof		: s += "EOF"; break;
                default				: s += (char) _tag; break;
            }
		
            return s;
        }
    }
}