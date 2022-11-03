using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.Automata
{
    class State
    {
        public string id;
        public string description;
        public State()
        {
            id = "";
            description = "";
        }

        public State(string id, string description)
        {
            this.id = id;
            this.description = description;
        }
    }
}
