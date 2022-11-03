using Microsoft.EntityFrameworkCore.Internal;
using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter;
using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models;
using OpeniT.Timesheet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers
{
    public class ContractHelper
    {
        public List<StatisticsColumnRecord> InterpretUser(string script, List<RecordSummary> recordSummaryThisYear, IEnumerable<UserExcessHours> userExcessHours)
        {
            var statisticsColumns = new List<StatisticsColumn[]>();
            var statisticsColumnRecords = new List<StatisticsColumnRecord>();

            var excessYear = recordSummaryThisYear[0].Month.Year - 1;
            var excessLastYear = userExcessHours.FirstOrDefault(x => x.Year == excessYear);

            foreach (var recordSummary in recordSummaryThisYear)
            {
                var interpretedColumns = InterpretToColumns(
                    script,
                    recordSummary.Hours,
                    recordSummary.RequiredHours,
                    statisticsColumns.Count == 0 
                        ? excessLastYear != null 
                            ? new StatisticsColumn[] { new StatisticsColumn() { Name = "ExcessHours", Value = excessLastYear.Hours.ToString() } }
                            : new StatisticsColumn[] { new StatisticsColumn() { Name = "ExcessHours", Value = "0" } }
                        : statisticsColumns.Last()
                );
                statisticsColumns.Add(interpretedColumns);

                var record = new StatisticsColumnRecord()
                {
                    Month = recordSummary.Month,
                    Hours = recordSummary.Hours,
                    RequiredHours = recordSummary.RequiredHours,
                    StatisticsColumns = interpretedColumns

                };
                statisticsColumnRecords.Add(record);
            }

            return statisticsColumnRecords;
        }

        public StatisticsColumn[] InterpretToColumns(string script, double recordedHours, double requiredHours, StatisticsColumn[] previousMonth)
        {
            var systemVariables = new SystemVariable()
            {
                RecordedHours = recordedHours,
                RequiredHours = requiredHours,
                PreviousMonth = previousMonth
            };

            var result = Interpret(systemVariables, script);

            return result;
        }

        public StatisticsColumn[] Interpret(SystemVariable systemVariables, string script)
        {
            var interpret = new Interpret();

            return interpret.Run(systemVariables, script);
        }
    }
}
