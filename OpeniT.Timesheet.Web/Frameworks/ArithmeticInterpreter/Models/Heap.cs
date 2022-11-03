using System.Collections.Generic;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models
{
    public class Heap
    {
        public List<Heap> Heaps { get; set; } = new List<Heap>();
        public string Key { get; set; }

        public double Value { get; set; }
    }
}
