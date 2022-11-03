namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models
{
    public class SystemVariable
    {
        public double RecordedHours { get; set; }
        public double RequiredHours { get; set; }
        public StatisticsColumn[] PreviousMonth { get; set; }
    }
}
