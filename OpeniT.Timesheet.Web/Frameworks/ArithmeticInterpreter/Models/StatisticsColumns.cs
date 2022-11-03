using System.Collections;
using System.Collections.Generic;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models
{
    public class StatisticsCollection
    {
        public int Id { get; set; }
        public ICollection<StatisticsColumn> StatisticsColumns { get; set; }
    }
}
