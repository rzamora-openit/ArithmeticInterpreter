using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class State
    {
        public String id;
        public String description;
        public State()
        {
            id = "";
            description = "";
        }

        public State(String id, String description)
        {
            this.id = id;
            this.description = description;
        }
    }
}
