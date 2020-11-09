using System.Linq;

namespace grammar_analyzer
{
    public class Production
    {
        private string _head;
        private string[] _body;

        public Production(string head, string[] body)
        {
            this._head = head;
            this._body = body;
        }

        public string GetHead()
        {
            return this._head;
            return this._head;
        }

        public string[] GetBody()
        {
            return this._body;
        }
        
        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (!(obj.GetType() == typeof(Production)))
            {
                return false;
            }

            Production other = (Production) obj;
            if (!this._body.SequenceEqual( other._body))
            {
                return false;
            }

            if (_head == null)
            {
                if (other._head != null)
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return "Production [head=" + this._head + ", body=" + this._body.ToString() + "]";
        }
        
    }  
}