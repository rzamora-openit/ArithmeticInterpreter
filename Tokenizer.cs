using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Tokenizer
    {
        public Token_Symbols_Set ignores;
        public Token_Symbols_Set separators;
        public Token_Symbols_Set symbols;

        public Tokenizer(Token_Symbols_Set separators, Token_Symbols_Set ignores)
        {
            this.separators = separators;
            this.ignores = ignores;
        }

        public Tokenizer(Token_Symbols_Set separators, Token_Symbols_Set ignores, Token_Symbols_Set symbols)
        {
            this.separators = separators;
            this.ignores = ignores;
            this.symbols = symbols;
        }

        public List<String> extractStatement(String str)
        {
            int n = str.Length; // total length of the string
            List<String> tokens = new List<String>();
            int i = 0; // indexer of current string
            for (int j = 0; j < n; j++) //j indexer of the string
            {
                if (separators.have(str[j] + ""))
                {
                    String temp = "";
                    for (int k = i; k < j; k++)
                    {
                        temp = temp + str[k];
                    }
                    if (temp.Length > 0)
                    {
                        tokens.Add(temp);
                    }
                    if (!ignores.have(str[j] + ""))
                    {
                        tokens.Add(str[j] + "");
                    }
                    i = j + 1;
                }
                else if (j == n - 1)  // end of line
                {
                    String temp = "";
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

        public List<String> extractTokens(String str) //hello world str[0] = hello 
        {
            int n = str.Length; // total length of the string
            List<String> tokens = new List<String>();
            int i = 0; // indexer of current string
            for (int j = 0; j < n; j++) //j indexer of the string
            {
                if (separators.have(str[j] + ""))
                {
                    String temp = "";
                    for (int k = i; k < j; k++)
                    {
                        temp = temp + str[k];
                    }
                    if (temp.Length > 0)
                    {
                        tokens.Add(temp);
                    }
                    if (!ignores.have(str[j] + ""))
                    {
                        tokens.Add(str[j] + "");
                    }
                    i = j + 1;
                }
                else if (symbols.have(str[j] + ""))// todo handle if symbols have str[j] -5-5-5-5 
                {
                    String temp = "";
                    for (int k = i; k < j; k++)
                    {
                        temp = temp + str[k];
                    }
                    if (temp.Length > 0)
                    {
                        tokens.Add(temp);
                    }
                    if (!ignores.have(str[j] + ""))
                    {
                        tokens.Add(str[j] + "");
                    }
                    i = j + 1;
                }
                else if (j == n - 1)  // end of line
                {
                    String temp = "";
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
    }
}
