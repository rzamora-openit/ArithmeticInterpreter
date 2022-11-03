using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.Automata
{
    class Finite_Automata
    {
        public States states;
        public Alphabet symbols;
        public Transitions transitions;
        public State initial_state;
        public States accept_states;

        public Finite_Automata(States states, Alphabet symbols, Transitions transitions, State initial_state, States accept_states)
        {
            this.states = states;
            this.symbols = symbols;
            this.transitions = transitions;
            this.initial_state = initial_state;
            this.accept_states = accept_states;
        }
    }
}
