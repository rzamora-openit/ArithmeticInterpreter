using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter;
using OpeniT.Timesheet.Web.Frameworks.Automata;
using System.Collections.Generic;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter
{
    class Syntax_Analysis
    {
        public Definite_Finite_Automata dfa;

        public Syntax_Analysis()
        {
            State q0 = new State("q0", "initial state");
            State q1 = new State("q1", "state 1");
            State q2 = new State("q2", "state 2");
            State q3 = new State("q3", "state 3");
            State q4 = new State("q4", "state 4");
            State q5 = new State("q5", "state 5, accept state");
            State qdump = new State("qdump", "dump state");

            States syntax_states = new States();
            syntax_states.Add(q0);
            syntax_states.Add(q1);
            syntax_states.Add(q2);
            syntax_states.Add(q3);
            syntax_states.Add(q4);
            syntax_states.Add(q5);
            syntax_states.Add(qdump);

            Token_Symbol keyword = new Token_Symbol(Token_Types.KEYWORD);
            Token_Symbol identifier = new Token_Symbol(Token_Types.IDENTIFIER);
            Token_Symbol assignment = new Token_Symbol(Token_Types.ASSIGNMENT);
            Token_Symbol number = new Token_Symbol(Token_Types.NUMBER);
            Token_Symbol plus = new Token_Symbol(Token_Types.PLUS);
            Token_Symbol minus = new Token_Symbol(Token_Types.MINUS);
            Token_Symbol multiply = new Token_Symbol(Token_Types.MULTIPLY);
            Token_Symbol divide = new Token_Symbol(Token_Types.DIVIDE);
            Token_Symbol modulo = new Token_Symbol(Token_Types.MODULO);
            Token_Symbol lparen = new Token_Symbol(Token_Types.LPAREN);
            Token_Symbol rparen = new Token_Symbol(Token_Types.RPAREN);
            Token_Symbol semicolon = new Token_Symbol(Token_Types.SEMICOLON);

            Alphabet alphabet = new Alphabet();
            alphabet.Add(keyword);
            alphabet.Add(identifier);
            alphabet.Add(assignment);
            alphabet.Add(number);
            alphabet.Add(plus);
            alphabet.Add(minus);
            alphabet.Add(multiply);
            alphabet.Add(divide);
            alphabet.Add(modulo);
            alphabet.Add(lparen);
            alphabet.Add(rparen);
            alphabet.Add(semicolon);

            Transitions transitions = new Transitions();
            int[][] transitionTable = new int[][]
            {
                      //col idt num =  +  -  *  /  %  (  )  ;
                new int[]{ 1, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6},//q0
                new int[]{ 6, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6},//q1
                new int[]{ 6, 6, 3, 6, 6, 6, 6, 6, 6, 6, 6, 6},//q2
                new int[]{ 6, 4, 6, 4, 3, 3, 6, 6, 6, 3, 6, 6},//q3
                new int[]{ 6, 6, 6, 6, 3, 3, 3, 3, 3, 6, 4, 5},//q4
                new int[]{ 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6},//q5 accept state
                new int[]{ 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6}//qdump
            };

            transitions.Tabulate(syntax_states, alphabet, transitionTable);

            States accept_states = new States();
            accept_states.Add(q5);

            dfa = new Definite_Finite_Automata(syntax_states, alphabet, transitions, q0, accept_states);
        }

        public List<string> ExtractSyntaxes(List<string> statement)
        {
            var states = new List<string>();
            State state = dfa.Run(statement);
            if (dfa.IsAccept(state))
                states.Add(state.id);
            else
                states.Add("Syntax error");
            return states;
        }

        public bool IsValidParen(List<string> statement)
        {
            var lParenCount = 0;
            var rParenCount = 0;

            foreach (var token in statement) {
                if (token == Token_Types.LPAREN)
                    lParenCount++;
                if (token == Token_Types.RPAREN)
                    rParenCount++;
            }

            // if paren not equal, return the index of that statement
            if (lParenCount != rParenCount) {
                return false;
            }
            else {
                return true;
            } 
        }

    }
}
