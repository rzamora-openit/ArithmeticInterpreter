using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers;
using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers.ContractHelper;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter
{
    class Semantic_Analysis
    {
        public Lexical_Analysis la = new Lexical_Analysis();

        public bool ExtractSemantics(List<List<string>> tokenizedStatement, SystemVariable systemVariable) // [[double, x, =, 10], [double, y, =, 10]]
        {
            var heap = new HeapHelper();
            heap.Add(systemVariable);

            foreach (var statement in tokenizedStatement)
            {
                var identifierTokens = statement.Where(x => la.IsIdentifier(x)).Skip(1);
                foreach (var token in identifierTokens)
                {
                    if (!heap.Contains(token))
                    {
                        throw new InterpreterException("Invalid semantics at " + string.Join(" ", statement).ToString() + $" Column '{token}' does not exist in the current context.");
                    }

                }

                var identifierDc = statement.Where(x => la.IsIdentifier(x)).First();
                if (!heap.Contains(identifierDc))
                {
                    heap.Add(identifierDc, 0);
                }
                else
                {
                    throw new InterpreterException("Invalid semantics at " + string.Join(" ", statement).ToString() + $" Column '{identifierDc}' is already defined in this scope.");
                }
            }

            return true;
        }
    }
}
