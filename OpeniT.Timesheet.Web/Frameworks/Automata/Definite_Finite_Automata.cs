using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.Automata
{
    class Definite_Finite_Automata : Finite_Automata
    {
        public Definite_Finite_Automata(States states, Alphabet symbols, Transitions transitions, State initial_state, States accept_states) : base(states, symbols, transitions, initial_state, accept_states)
        {

        }

        public State Run(List<string> tokens)
        {
            State state = initial_state;
            foreach (string token in tokens)
            {
                state = transitions[state][symbols.GetToken(token)];
            }
            return state;
        }

        public bool IsAccept(State state)
        {
            return accept_states.Contains(state);
        }
    }
}
