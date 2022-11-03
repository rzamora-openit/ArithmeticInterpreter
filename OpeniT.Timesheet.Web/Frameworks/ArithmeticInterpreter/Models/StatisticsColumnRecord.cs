using System;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models
{
    public class StatisticsColumnRecord
    {
        public DateTime Month { get; set; }
        public double Hours { get; set; }

        public double RequiredHours { get; set; }

        public StatisticsColumn[] StatisticsColumns { get; set; }
    }
}
