using OpeniT.Timesheet.Web.Frameworks.Automata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter
{
    class Lexical_DFA
    {
        public Alphabet alphabet;
        public Definite_Finite_Automata dfa;

        public Lexical_DFA()
        {
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
            alphabet.Add(numbers);
            alphabet.Add(letters);

            Transitions transitions = new Transitions();
            int[][] transitionTable = new int[][]
            {
                new int[]{ 2, 3, 1},//q0
                new int[]{ 2, 1, 1},//q1 accept state
                new int[]{ 3, 3, 1},//q2 accept state
                new int[]{ 3, 3, 3}//qdump
            };

            transitions.Tabulate(lexical_states, alphabet, transitionTable);

            States accept_states = new States();
            accept_states.Add(q1);
            accept_states.Add(q2);

            dfa = new Definite_Finite_Automata(lexical_states, alphabet, transitions, q0, accept_states);
        }
    }
}
