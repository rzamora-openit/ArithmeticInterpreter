using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Transitions : Dictionary<State, Transition>
    {
        public bool tabulate(States states, Alphabet alphabet, int[][] transfer_index)
        {
            if (!(transfer_index.Length * transfer_index[0].Length == states.Count * alphabet.Count))
                return false;
            for (int i = 0; i < transfer_index.Length; i++)
            {
                Transition transition = new Transition();
                for (int j = 0; j < transfer_index[i].Length; j++)
                {
                    transition.Add(alphabet[j], states[transfer_index[i][j]]);
                }
                Add(states[i], transition);
            }
            return true;
        }
    }
}
