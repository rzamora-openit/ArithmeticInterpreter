using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Lexical_Analysis
    {
        public List<string> dataType = new List<string>() { "double" };
        public List<string> invalidIdentifiers = new List<string>() { "var", "abstract", "byte", "class", "delegate", "event", "fixed", "if", "internal", "new", "override", "readonly", "short", "struct", "try", "unsafe", "volatile", "as", "case", "const", "do", "explicit", "float", "implicit", "is", "null", "params", "ref", "sizeof", "switch", "typeof", "ushort", "while", "base", "catch", "continue", "double", "extern", "for", "in", "lock", "object", "private", "return", "stackalloc", "this", "uint", "using", "bool", "char", "decimal", "else", "false", "foreach", "int", "long", "operator", "protected", "sbyte", "static", "throw", "ulong", "virtual", "break", "checked", "default", "enum", "finally", "goto", "interface", "namespace", "out", "public", "sealed", "string", "true", "unchecked", "void" };

        public List<String> extractLexicon(List<String> tokenized)
        {
            var lexical = new List<String>();
            foreach (String token in tokenized)
            {
                if (isKeyword(token))
                {
                    lexical.Add("KEYWORD");
                }
                else if (isIdentifier(token))
                {
                    lexical.Add("IDENTIFIER");
                }
                else if (token == "=")
                {
                    lexical.Add("ASSIGNMENT");
                }
                else if (isNumber(token))
                {
                    lexical.Add("NUMBER");
                }
                else if (token == "+")
                {
                    lexical.Add("PLUS");
                }
                else if (token == "-")
                {
                    lexical.Add("MINUS");
                }
                else if (token == "*")
                {
                    lexical.Add("MULTIPLY");
                }
                else if (token == "/")
                {
                    lexical.Add("DIVIDE");
                }
                else if (token == "%")
                {
                    lexical.Add("MODULO");
                }
                else if (token == "(")
                {
                    lexical.Add("LPAREN");
                }
                else if (token == ")")
                {
                    lexical.Add("RPAREN");
                }
                else if (token == ";")
                {
                    lexical.Add("SEMICOLON");
                }
                else
                {
                    throw new Exception($"{token} is unsupported type");
                }
            }
            return lexical;
        }

        public bool isKeyword(String token)
        {
            return dataType.Contains(token);
        }

        public bool isIdentifier(String token)
        {
            var firstLetter = token[0];
            if ((Char.IsLetter(firstLetter) && !invalidIdentifiers.Contains(token)) || firstLetter.ToString() == "_")
            {
                return true;
            }
            return false;
        }

        public bool isNumber(String token)
        {
            try
            {
                Convert.ToDouble(token);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
