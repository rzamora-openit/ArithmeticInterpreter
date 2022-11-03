using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.Automata
{
    class Alphabet : List<Token>
    {
        public Alphabet()
        {

        }

        public Alphabet(string[] symbols)
        {
            foreach (string symbol in symbols)
            {
                Add(new Token_Symbol(symbol));
            }
        }

        public Alphabet(List<string> symbols)
        {
            foreach (string symbol in symbols)
            {
                Add(new Token_Symbol(symbol));
            }
        }

        public Token GetToken(string symbol)
        {
            foreach (Token token in this)
            {
                if (token.Have(symbol))
                    return token;
            }
            throw new InterpreterException($"Symbol {symbol} not found in Alphabet");
        }

        public bool Contains(List<string> tokenChars)
        {
            foreach (string _char in tokenChars) {
                if (!Contains(_char)) {
                    return false;
                }
            }
            return true;
        }

        public bool Contains(string passed_token)
        {
            foreach (Token token in this) {
                if (token.Have(passed_token)) {
                    return true;
                }
            }
            return false;
        }
    }
}
