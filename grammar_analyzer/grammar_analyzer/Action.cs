namespace grammar_analyzer
{
    public class Action
    {
        private ActionType _type;
        private string _operand;
	
        public Action(ActionType type, string operand) {
            this._type = type;
            this._operand = operand;
        }

        public new ActionType GetType() {
            return _type;
        }

        public string GetOperand() {
            return _operand;
        }
        
        public override string ToString() {
            return "Action [type=" + _type + ", operand=" + _operand + "]";
        }
    }
}