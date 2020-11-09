using System;

namespace grammar_analyzer.lexer
{
    public class Number:Token
    {
        private int _value;
	
        public Number(int value) : base(Tag.Number)
        {
            this._value = value;
        }
	
        public int GetValue() {
            return _value;
        }
	
        public override string ToString() {
            return "number";
        }
    }
}