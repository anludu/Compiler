    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection.Metadata.Ecma335;

    namespace grammar_analyzer
    {
        public class Grammar
        {
            public Dictionary<string, Production> Productions;


            public Grammar()
            {
                Productions = new Dictionary<string, Production>();
                string[] lines = File.ReadAllLines("productions.txt");
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] splitted = lines[i].Split(' ');
                    string head = splitted[0];
                    string[] body;
                    if (splitted[2] == "''")
                    {
                        body = null;
                    }
                    else
                    {
                        body = new string[splitted.Length - 2];
                        Array.Copy(splitted, 2, body, 0, splitted.Length - 2);

                    }

                    Productions.Add("" +i,new Production(head, body));
                

                }
            }

            public Production GetProductionWithId(string id)
            {
                return Productions[id];
            }

            public int Size()
            {
                return Productions.Count;
            }

        }
    }

