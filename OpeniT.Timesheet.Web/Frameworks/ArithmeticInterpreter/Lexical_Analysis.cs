using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter;
using OpeniT.Timesheet.Web.Frameworks.Automata;
using System;
using System.Collections.Generic;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter
{
    class Lexical_Analysis
    {
        public Definite_Finite_Automata dfa;
        public Tokenizer get_character_tokens;
        public Alphabet alphabet;

        public List<string> dataType = new List<string>() { "Column" };
        public List<string> invalidIdentifiers = new List<string>() { "Column", "var", "abstract", "byte", "class", "delegate", "event", "fixed", "if", "internal", "new", "override", "readonly", "short", "struct", "try", "unsafe", "volatile", "as", "case", "const", "do", "explicit", "float", "implicit", "is", "null", "params", "ref", "sizeof", "switch", "typeof", "ushort", "while", "base", "catch", "continue", "double", "extern", "for", "in", "lock", "object", "private", "return", "stackalloc", "this", "uint", "using", "bool", "char", "decimal", "else", "false", "foreach", "int", "long", "operator", "protected", "sbyte", "static", "throw", "ulong", "virtual", "break", "checked", "default", "enum", "finally", "goto", "interface", "namespace", "out", "public", "sealed", "string", "true", "unchecked", "void" };


        public Lexical_Analysis()
        {
            get_character_tokens = new Tokenizer();

            State q0 = new State("q0", "initial state");
            State q1 = new State("q1", "state 1, accept state");
            State q2 = new State("q2", "state 2, accept state");
            State qdump = new State("qdump", "dump state");

            States lexical_states = new States();
            lexical_states.Add(q0);
            lexical_states.Add(q1);
            lexical_states.Add(q2);
            lexical_states.Add(qdump);

            Token_Symbol underscore = new Token_Symbol("_");
            Token_Symbol dot = new Token_Symbol(".");
            Token_Symbols_Set numbers = new Token_Symbols_Set(new string[]{
                "0","1","2","3","4","5","6","7","8","9" });
            Token_Symbols_Set letters = new Token_Symbols_Set(new string[]{
                "a","b","c","d","e","f","g","h","i","j",
                "k","l","m","n","o","p","q","r","s","t",
                "u","v","w","x","y","z",
                "A","B","C","D","E","F","G","H","I","J",
                "K","L","M","N","O","P","Q","R","S","T",
                "U","V","W","X","Y","Z" });

            alphabet = new Alphabet();
            alphabet.Add(underscore);
            alphabet.Add(dot);
            alphabet.Add(numbers);
            alphabet.Add(letters);

            Transitions transitions = new Transitions();
            int[][] transitionTable = new int[][]
            {
                        // _  .  n  l
                new int[]{ 2, 3, 3, 1},//q0
                new int[]{ 2, 2, 1, 1},//q1 accept state
                new int[]{ 3, 3, 3, 1},//q2 accept state
                new int[]{ 3, 3, 3, 3}//qdump
            };

            transitions.Tabulate(lexical_states, alphabet, transitionTable);

            States accept_states = new States();
            accept_states.Add(q1);
            accept_states.Add(q2);

            dfa = new Definite_Finite_Automata(lexical_states, alphabet, transitions, q0, accept_states);
        }

        public List<string> ExtractLexicon(List<string> tokenized)
        {
            var lexical = new List<string>();
            foreach (string token in tokenized)
            {
                if (IsKeyword(token))
                {
                    lexical.Add(Token_Types.KEYWORD);
                }
                else if (IsIdentifier(token))
                {
                    lexical.Add(Token_Types.IDENTIFIER);
                }
                else if (token == "=")
                {
                    lexical.Add(Token_Types.ASSIGNMENT);
                }
                else if (IsNumber(token))
                {
                    lexical.Add(Token_Types.NUMBER);
                }
                else if (token == "+")
                {
                    lexical.Add(Token_Types.PLUS);
                }
                else if (token == "-")
                {
                    lexical.Add(Token_Types.MINUS);
                }
                else if (token == "*")
                {
                    lexical.Add(Token_Types.MULTIPLY);
                }
                else if (token == "/")
                {
                    lexical.Add(Token_Types.DIVIDE);
                }
                else if (token == "%")
                {
                    lexical.Add(Token_Types.MODULO);
                }
                else if (token == "(")
                {
                    lexical.Add(Token_Types.LPAREN);
                }
                else if (token == ")")
                {
                    lexical.Add(Token_Types.RPAREN);
                }
                else if (token == ";")
                {
                    lexical.Add(Token_Types.SEMICOLON);
                }
                else
                {
                    throw new InterpreterException($"{token} is unsupported type");
                }
            }
            return lexical;
        }

        public bool IsKeyword(string token)
        {
            return dataType.Contains(token);
        }

        public bool IsIdentifier(string token)
        {
            if (alphabet.Contains(get_character_tokens.ExtractTokenChars(token))) {
                State state = dfa.Run(get_character_tokens.ExtractTokenChars(token));
                if (dfa.IsAccept(state) && !invalidIdentifiers.Contains(token)) {
                    return true;
                }

                return false;
            }
            else {
                return false;
            }
        }
        public bool IsNumber(string token)
        {
            try {
                Convert.ToDouble(token);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
