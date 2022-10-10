using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
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
            State q6 = new State("q6", "state 6");
            State q7 = new State("q7", "state 7");
            State qdump = new State("qdump", "dump state");

            States syntax_states = new States();
            syntax_states.Add(q0);
            syntax_states.Add(q1);
            syntax_states.Add(q2);
            syntax_states.Add(q3);
            syntax_states.Add(q4);
            syntax_states.Add(q5);
            syntax_states.Add(q6);
            syntax_states.Add(q7);
            syntax_states.Add(qdump);

            Token_Symbol keyword = new Token_Symbol("KEYWORD");
            Token_Symbol identifier = new Token_Symbol("IDENTIFIER");
            Token_Symbol assignment = new Token_Symbol("ASSIGNMENT");
            Token_Symbol number = new Token_Symbol("NUMBER");
            Token_Symbol plus = new Token_Symbol("PLUS");
            Token_Symbol minus = new Token_Symbol("MINUS");
            Token_Symbol multiply = new Token_Symbol("MULTIPLY");
            Token_Symbol divide = new Token_Symbol("DIVIDE");
            Token_Symbol modulo = new Token_Symbol("MODULO");
            Token_Symbol lparen = new Token_Symbol("LPAREN");
            Token_Symbol rparen = new Token_Symbol("RPAREN");
            Token_Symbol semicolon = new Token_Symbol("SEMICOLON");

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
                      //key idt num =  +  -  *  /  %  (  )  ;
                new int[]{ 1, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8},//q0
                new int[]{ 8, 2, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8},//q1
                new int[]{ 8, 8, 3, 8, 8, 8, 8, 8, 8, 8, 8, 8},//q2
                new int[]{ 8, 4, 8, 4, 3, 3, 8, 8, 8, 6, 8, 8},//q3
                new int[]{ 8, 8, 8, 8, 3, 3, 3, 3, 3, 8, 8, 5},//q4
                new int[]{ 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8},//q5
                new int[]{ 8, 7, 8, 7, 6, 6, 8, 8, 8, 6, 8, 8},//q6
                new int[]{ 8, 8, 8, 8, 6, 6, 6, 6, 6, 8, 7, 5},//q7
                new int[]{ 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8}//qdump
            };

            transitions.tabulate(syntax_states, alphabet, transitionTable);

            States accept_state = new States();
            accept_state.Add(q5);

            dfa = new Definite_Finite_Automata(syntax_states, alphabet, transitions, q0, accept_state);
        }

        public List<String> extractSyntaxes(List<String> statement)
        {
            var statement_types = new List<String>();
            State state = dfa.run(statement);
            if (dfa.is_accept(state))
                statement_types.Add(state.id);
            else
                statement_types.Add("Syntax error");
            return statement_types;
        }

        public int isValidParen(List<List<String>> lexicald_statements)
        {
            var lParenCount = 0;
            var rParenCount = 0;

            foreach (var statement in lexicald_statements)
            {

                foreach (var token in statement)
                {
                    if (token == "LPAREN")
                        lParenCount++;
                    if (token == "RPAREN")
                        rParenCount++;

                }

                if (lParenCount != rParenCount)
                {
                    // if paren not equal, return the index of that statement
                    return lexicald_statements.IndexOf(statement);
                }

                lParenCount = 0;
                rParenCount = 0;
            }


            return -1;
        }

    }
}
