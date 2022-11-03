using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter
{
    public static class Token_Types
    {
        public const string KEYWORD = "KEYWORD";
        public const string IDENTIFIER = "IDENTIFIER";
        public const string ASSIGNMENT = "ASSIGNMENT";
        public const string NUMBER = "NUMBER";
        public const string PLUS = "PLUS";
        public const string MINUS = "MINUS";
        public const string MULTIPLY = "MULTIPLY";
        public const string DIVIDE = "DIVIDE";
        public const string MODULO = "MODULO";
        public const string LPAREN = "LPAREN";
        public const string RPAREN = "RPAREN";
        public const string SEMICOLON = "SEMICOLON";
    }
}
