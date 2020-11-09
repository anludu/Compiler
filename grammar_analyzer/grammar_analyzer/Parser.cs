using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using grammar_analyzer.lexer;

namespace grammar_analyzer
{
    public class Parser
    { 
	    private Dictionary<string, Action[]> _actionTable;
	    private Dictionary<string, Dictionary<string, string>> _goToTable;
		private Grammar _g;
		private Stack<string> _stack;
		private Stack<string> _symbols;
		private Lexer _lexer;
		
		public Parser(StreamReader input) {
			_g = new Grammar();
			_lexer = new Lexer(input);
			_stack = new Stack<string>();
			_symbols = new Stack<string>();
			
			LoadActionTable();
			LoadGoToTable();
		}
		
		private void LoadActionTable() {
			string line= "0", state, terminal, newState;
			string[] tokens;
			int size;
			ActionType type;
			string[][] mat;
			_actionTable = new Dictionary<string, Action[]>();
			
			try {
				using (StreamReader reader = new StreamReader("table2.csv"))
				{
					
					//line = reader.ReadLine();
					mat = new string[155][];
					var p = 0;
					while ((line = reader.ReadLine()) != null)
					{
						mat[p]=line.Split(',');
						p++;
					}
					
					Action[] actions;
					string actionText;
					for (int i = 0; mat[0][i] != "START'" ; i++)
					{
						actions = new Action[mat.Length];
						for (int j = 1; j < mat.Length; j++)
						{

							actionText = mat[j][i];
							if(actionText == "")
							{
								actions[j - 1] = null;
							}
							else if (actionText[0] == 'r')
							{
								actions[j - 1] = new Action(ActionType.Reduce, actionText.Substring(1));
							}
							else if (actionText[0] == 's')
							{
								actions[j - 1] = new Action(ActionType.Shift, actionText.Substring(1));
							}
							else if (actionText[0] == 'a')
							{
								actions[j - 1] = new Action(ActionType.Accept, actionText.Substring(1));
							}
							
						}
						
						_actionTable.Add(mat[0][i], actions);
					}
					
				}
			} catch (IOException e) {
				Console.WriteLine(e);
			}
		}
	
		private void LoadGoToTable() {
			string line, state, nonTerminal, newState;
			string[] tokens;
			int size;
		
			try {
				using (StreamReader reader = new StreamReader("goto.csv"))
				{
					line = reader.ReadLine();
					size = Int32.Parse(line);
					_goToTable = new Dictionary<string, Dictionary<string, string>>();
					for (int i = 0; i < size; i++)
					{
						_goToTable.Add("" + i, new Dictionary<string,string>());
					}

					while ((line = reader.ReadLine()) != null)
					{
						tokens = line.Split(',');

						state = tokens[0];
						nonTerminal = tokens[1];
						newState = tokens[2];
						_goToTable[state].Add(nonTerminal, newState);
					}
				}

			} catch (IOException e) {
				Console.WriteLine(e);
			}
		}


        public void Analyze()
        {
            bool found=false, error=false;
            string state;
            Action action;
            Token token;
            int line = 0;
            
            _stack.Push("0");
            _symbols.Push("$");
            for (int i = 1; i < 20; i++)
            {
	            try
                {
	                token = _lexer.Scan();
	                while (!found)
                    {
	                    state = _stack.Peek();
                        action = _actionTable[token.ToString()][int.Parse(state)];
                        if (action.GetType() == ActionType.Reduce)
                        {
	                        Production p = _g.GetProductionWithId(action.GetOperand());
	                        if (p.GetBody() != null)
                            {
                                string[] aux = p.GetBody();
                                for (int j = 0; j < aux.Length; j++)
                                { 
	                                _stack.Pop();
	                                _symbols.Pop();
                                }
                            }
	                        _symbols.Push(p.GetHead());
                            string newState = _goToTable[_stack.Peek()][_symbols.Peek()];


							_stack.Push(newState);
                        }
                        else if (action.GetType() == ActionType.Shift)
                        {
                            _stack.Push(action.GetOperand());
                            _symbols.Push(token.ToString());
                            token = _lexer.Scan();
                            if (token.ToString() == "EOF")
                            {
	                            token = new Word("$", Tag.Reserved);
                            }

                        }
                        else if (action.GetType() == ActionType.Accept)
                        {
                            found = true;
                        }
                    }
                    
                    if (found)
                    {
	                    _symbols.Pop();
                        Console.WriteLine("ACCEPTED");
                        break;
                    }

                }
                catch (Exception e)
                {
	                Console.WriteLine("Error en la linea: " + Lexer.Line);
	                Console.WriteLine(e.StackTrace);
                    break;
                }
            }

        }
        
    }
}