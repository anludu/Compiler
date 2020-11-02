namespace grammar_analyzer.lexer
{
    public class Token
    {
        private int tag;
	
        public Token(int tag) {
            this.tag = tag;
        }
	
        public int GetTag() {
            return tag;
        }
	
        public override string ToString() {
            string s = "TOKEN, symbol = ";
		
            switch (tag) {
                case Tag.EOF		: s += "EOF"; break;
                default				: s += (char) tag; break;
            }
		
            return s;
        }
    }
}