using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Semantic_Analysis
    {
        public Semantic_Analysis()
        {
            
        }
        public bool extractSemantics(List<List<String>> tokenized_statements) // [[double, x, =, 10], [double, y, =, 10]]
        {
            HashSet<String> identifiers = new HashSet<string>(); // 

            foreach (var statement in tokenized_statements) {
                var firstIdentifier = true; 
                foreach (var token in statement) {
                    if (isIdentifier(token)) {
                        if (firstIdentifier) {
                            firstIdentifier = false;
                            if (identifiers.Add(token)) { // double x = 10; double x = 11;
                                continue;
                            }
                            else {
                                throw new Exception($"variable {token} is already defined in this scope");
                            }
                        } 
                        else {
                            if (!identifiers.Contains(token)) { // double x = 10; double z = 10 + y;
                                throw new Exception($"variable {token} does not exist in the current context");
                            }
                            else if (identifiers.Contains(token) && identifiers.Last() == token) { // double x = x;
                                throw new Exception($"variable {token} cannot be used before it is declared");
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool isIdentifier(String token)
        {
            var firstLetter = token[0];
            if ((Char.IsLetter(firstLetter) && token != "double") || firstLetter.ToString() == "_")
            {
                return true;
            }
            return false;
        }
    }
}
