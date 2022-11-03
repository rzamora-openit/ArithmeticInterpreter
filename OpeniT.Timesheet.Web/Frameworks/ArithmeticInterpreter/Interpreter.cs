using OpeniT.Timesheet.Web.Frameworks.Automata;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers;
using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models;
using static OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Helpers.ContractHelper;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter
{
    class Interpreter
    {
        public Lexical_Analysis la = new Lexical_Analysis();

        public Dictionary<string, Heap> Runtime(List<List<string>> tokenized_statement, SystemVariable SystemVariable)
        {
            var heap = new HeapHelper();
            heap.Add(SystemVariable);

            for (int i = 0; i < tokenized_statement.Count; i++) 
            {
                var expression = new List<string>();

                var varName = tokenized_statement[i][1];

                // evaluate expression
                for (int j = 3; j < tokenized_statement[i].Count - 1; j++) 
                {
                    if (la.IsIdentifier(tokenized_statement[i][j])) 
                    {
                        var identifierName = tokenized_statement[i][j];
                        expression.Add(heap[identifierName].Value.ToString()); // get Value from the Heaps then add it to expression
                    }
                    else 
                    {
                        expression.Add(tokenized_statement[i][j]);
                    }
                }

                // save Value after evaluating expression
                if (expression.Count <= 1) 
                {
                    heap.Add(varName, Convert.ToDouble(expression[0]));
                }
                else {
                    heap.Add(varName, InfixToPostfix(HandleSign(expression)));
                }
            } 

            return heap.Heap;
        }

        public List<string> HandleSign(List<string> expression)
        {
            // handle signs of an expression
            for (int i = 0; i < expression.Count; i++) {
                if (expression[i] == "+" || expression[i] == "-") {
                    // handle if sign is used as an operator
                    if (i != 0) // its an operator if it is not placed at the beginning of an expression 
                    { 
                        if (IsNumber(expression[i - 1]) && (IsNumber(expression[i + 1]) || IsUsedAsOperator(expression[i + 1]))) // its an operator if the sign is placed after a number and is next to a number or operator +, -, (
                        { 
                            continue;
                        }
                        else if (expression[i - 1] == ")") // operator if placed after closing paren (5*2) +-*/% 2
                        { 
                            continue;
                        }
                    }

                    // handle if sign is used as positive or negative
                    var temp = 1; // temporary variable that will carry the sign
                    for (int j = i; j < expression.Count; j++) 
                    {
                        if (expression[j] == "+") 
                        {
                            temp = temp * 1;
                        }
                        else if (expression[j] == "-") 
                        {
                            temp = temp * -1;
                        }
                        else 
                        {
                            if (expression[j] == "(" || IsNumber(expression[j])) // minus at the beginning of the expression or next to a number ->  - ( x + y), - 10 + 2, 4 - pos 2 + (2 /2)
                            { 
                                for (int k = i; k < j; k++) // remove the signs
                                { 
                                    expression.RemoveAt(k);
                                    k--;
                                    j--;
                                }

                                if (temp > 0) 
                                {
                                    expression.Insert(j, "pos");
                                }
                                else if (temp < 0) 
                                {
                                    expression.Insert(j, "neg");
                                }

                                break;
                            }
                            else if (IsPosOrNeg(expression[i - 1])) // sign after operator -> 4 * - x  or sign after opening paren -> 2 * (- x + y)
                            { 
                                for (int k = i; k < j; k++) // remove the signs
                                { 
                                    expression.RemoveAt(k);
                                    k--;
                                    j--;
                                }
                                
                                expression[j] = Convert.ToString(Convert.ToDouble(expression[j]) * temp);
                                break;
                            }
                        }
                    }
                }
            } 

            return expression;
        }

        public double InfixToPostfix(List<string> expression)
        {
            // infix to postfix
            var stack = new Stack<string>();
            var postfix = new List<string>();

            for (int i = 0; i < expression.Count; i++) 
            {
                if (IsNumber(expression[i])) 
                {
                    postfix.Add(expression[i]);
                }
                else if (expression[i] == "(") 
                {
                    stack.Push(expression[i]);
                }
                else if (expression[i] == ")") 
                {
                    while (stack.Peek() != "(")  
                    {
                        postfix.Add(stack.Pop());
                    }
                    stack.Pop();
                }
                else if (IsOperator(expression[i])) 
                {
                    if (stack.Count == 0 || Precedence(expression[i]) > Precedence(stack.Peek())) 
                    {
                        stack.Push(expression[i]);
                    }
                    else 
                    {
                        while (stack.Count > 0 && Precedence(expression[i]) <= Precedence(stack.Peek())) 
                        {
                            postfix.Add(stack.Pop());
                        }
                        stack.Push(expression[i]);
                    }
                }
                else 
                {
                    if (expression[i] == "pos") 
                    {
                        postfix.Add("0");
                        stack.Push("+");
                    }
                    else if (expression[i] == "neg") 
                    {
                        postfix.Add("0");
                        stack.Push("-");
                    }
                }
            }

            // pop remaining operators in stack
            while (stack.Count > 0) 
            {
                postfix.Add(stack.Pop());
            }

            return EvaluateRPN(postfix);
        }

        public double EvaluateRPN(List<string> postfix)
        {
            var stack = new Stack<double>();

            foreach (string str in postfix)
            {
                if (IsNumber(str))
                {
                    stack.Push(Convert.ToDouble(str));
                }
                else if (IsOperator(str))
                {
                    if (str == "+")
                    {
                        var num2 = stack.Pop();
                        var num1 = stack.Pop();
                        stack.Push(Convert.ToDouble(num1 + num2));
                    }
                    else if (str == "-")
                    {
                        var num2 = stack.Pop();
                        var num1 = stack.Pop();
                        stack.Push(Convert.ToDouble(num1 - num2));
                    }
                    else if (str == "*")
                    {
                        var num2 = stack.Pop();
                        var num1 = stack.Pop();
                        stack.Push(Convert.ToDouble(num1 * num2));
                    }
                    else if (str == "/")
                    {
                        var num2 = stack.Pop();
                        var num1 = stack.Pop();
                        if (num2 == 0)
                        {
                            throw new Exception("Division by zero error");
                        }
                        stack.Push(Convert.ToDouble(num1 / num2));
                    }
                    else
                    {
                        var num2 = stack.Pop();
                        var num1 = stack.Pop();
                        stack.Push(Convert.ToDouble(num1 % num2));
                    }
                }
            }

            var finalAns = stack.Pop();
            return finalAns;
        }

        public bool IsNumber(string token)
        {
            try {
                Convert.ToDouble(token);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        public bool IsUsedAsOperator(string s)
        {
            var symbols = new List<string> { "+", "-", "(" };
            if (symbols.Contains(s)) 
            {
                return true;
            }
            return false;
        }

        public bool IsPosOrNeg(string c)
        {
            var symbols = new List<string>() { "*", "/", "%", "+", "-", "(" };
            if (symbols.Contains(c)) 
            {
                return true;
            }
            return false;
        }

        public bool IsOperator(string s)
        {
            var op = new List<string>() { "*", "/", "%", "+", "-" };
            if (op.Contains(s))
            {
                return true;
            }
            return false;
        }

        public int Precedence(string op)
        {
            if (op == "*" || op == "/" || op == "%") {
                return 2;
            }
            else if (op == "+" || op == "-") {
                return 1;
            }
            else { 
                return 0;
            }
        }


    }
}
