using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.Automata
{
    class Tokenizer
    {
        public Token_Symbols_Set ignores;
        public Token_Symbols_Set separators;
        public Tokenizer()
        {

        }

        public Tokenizer(Token_Symbols_Set separators, Token_Symbols_Set ignores)
        {
            this.separators = separators;
            this.ignores = ignores;
        }

        public List<string> ExtractStatements(string str)
        {
            int n = str.Length; // total length of the string
            List<string> tokens = new List<string>();
            int i = 0; // indexer of current string
            for (int j = 0; j < n; j++) //j indexer of the string
            {
                if (separators.Have(str[j] + ""))
                {
                    string temp = "";
                    for (int k = i; k < j; k++)
                    {
                        temp = temp + str[k];
                    }
                    if (temp.Length > 0)
                    {
                        tokens.Add(temp);
                    }
                    if (!ignores.Have(str[j] + ""))
                    {
                        tokens.Add(str[j] + "");
                    }
                    i = j + 1;
                }
                else if (j == n - 1)  // end of line
                {
                    string temp = "";
                    for (int k = i; k < n; k++)
                    {
                        temp = temp + str[k];
                    }
                    if (temp.Length > 0)
                    {
                        tokens.Add(temp);
                    }
                }
            }
            return tokens;
        }

        public List<string> ExtractTokens(string str) //hello world str[0] = hello 
        {
            int n = str.Length; // total length of the string
            List<string> tokens = new List<string>();
            int i = 0; // indexer of current string
            for (int j = 0; j < n; j++) //j indexer of the string
            {
                if (separators.Have(str[j] + ""))
                {
                    string temp = "";
                    for (int k = i; k < j; k++)
                    {
                        temp = temp + str[k];
                    }
                    if (temp.Length > 0)
                    {
                        tokens.Add(temp);
                    }
                    if (!ignores.Have(str[j] + ""))
                    {
                        tokens.Add(str[j] + "");
                    }
                    i = j + 1;
                }
                else if (j == n - 1)  // end of line
                {
                    string temp = "";
                    for (int k = i; k < n; k++)
                    {
                        temp = temp + str[k];
                    }
                    if (temp.Length > 0)
                    {
                        tokens.Add(temp);
                    }
                }
            }
            return tokens;
        }

        public List<string> ExtractTokenChars(string token)
        {
            var tokens = new List<string>();
            foreach (var t in token)
            {
                tokens.Add(t.ToString());
            }
            return tokens;
        }
    }
}
