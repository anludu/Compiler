using System;

namespace grammar_analyzer.lexer
{
    public class Number:Token
    {
        private int value;
	
        public Number(int value) : base(Tag.NUMBER)
        {
            this.value = value;
        }
	
        public int GetValue() {
            return value;
        }
	
        public override string ToString() {
            Console.WriteLine("From Number");
            return "NUMBER, value = " + value;
        }
    }
}