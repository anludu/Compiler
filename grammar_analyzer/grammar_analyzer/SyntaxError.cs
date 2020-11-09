using System;

namespace grammar_analyzer
{
    
    public class SyntaxError : Exception
    {
        private const long SerialVersionUid = 1L;
        
        public SyntaxError():base(){
        }
    
        public SyntaxError(string s): base(s){
            
        }
    }
}