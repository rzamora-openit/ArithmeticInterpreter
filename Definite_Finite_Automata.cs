using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Definite_Finite_Automata : Finite_Automata
    {
        public Definite_Finite_Automata(States states, Alphabet symbols, Transitions transitions, State initial_state, States accept_states) : base(states, symbols, transitions, initial_state, accept_states)
        {

        }

        public State run(List<String> tokens)
        {
            State state = initial_state;
            foreach (String token in tokens)
            {
                state = transitions[state][symbols.getToken(token)]; //0
            }
            return state;
        }

        public bool is_accept(State state)
        {
            return accept_states.Contains(state);
        }
    }
}
