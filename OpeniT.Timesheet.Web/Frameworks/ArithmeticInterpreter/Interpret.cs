using OpeniT.Timesheet.Web.Frameworks.Automata;
using System;
using System.Collections.Generic;
using OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter.Models;

namespace OpeniT.Timesheet.Web.Frameworks.ArithmeticInterpreter
{
    class Interpret
    {
        public Token_Symbols_Set statements_ignore;
        public Token_Symbols_Set statements_separator;
        public Tokenizer statements_extractor;

        public Token_Symbols_Set tokens_separator;
        public Token_Symbols_Set tokens_ignore;
        public Tokenizer tokens_extractor;

        public Token_Symbols_Set semantic_ignores;
        public Token_Symbols_Set semantic_separator;
        public Tokenizer semantic_extractor;

        Lexical_Analysis lexical_analysis;
        Syntax_Analysis syntax_analysis;
        Semantic_Analysis semantic_analysis;

        public Interpreter interpreter;

        Dictionary<string, Heap> heap;

        public Interpret()
        {
            statements_separator = new Token_Symbols_Set(new string[] { "~" });
            statements_ignore = new Token_Symbols_Set(new string[] { "~" });
            statements_extractor = new Tokenizer(statements_separator, statements_ignore);

            tokens_separator = new Token_Symbols_Set(new string[] { " ", "\n", ";", "+", "-", "*", "/", "%", "(", ")", "=" });
            tokens_ignore = new Token_Symbols_Set(new string[] { " ", "\n" });
            tokens_extractor = new Tokenizer(tokens_separator, tokens_ignore);

            semantic_separator = new Token_Symbols_Set(new string[] { "=" });
            semantic_ignores = new Token_Symbols_Set(new string[] { " " });
            semantic_extractor = new Tokenizer(semantic_separator, semantic_ignores);

            lexical_analysis = new Lexical_Analysis();
            syntax_analysis = new Syntax_Analysis();
            semantic_analysis = new Semantic_Analysis();

            interpreter = new Interpreter();

            heap = new Dictionary<string, Heap>();
        }

        public StatisticsColumn[] Run(SystemVariable systemVariables, string script)
        {
            // Script code
            script = script.Replace(";", ";~");

            // partition source code into list of statement
            var statements = statements_extractor.ExtractStatements(script);

            // tokenize each statement
            var tokenized_statements = new List<List<string>>();
            foreach (var statement in statements)
            {
                tokenized_statements.Add(tokens_extractor.ExtractTokens(statement));
            }

            // lexical analysis
            var lexical_statements = new List<List<string>>();
            foreach (var tokenized in tokenized_statements)
            {
                lexical_statements.Add(lexical_analysis.ExtractLexicon(tokenized));
            }

            // syntax analysis
            var syntaxStates = new List<List<string>>();
            foreach (var statement in lexical_statements)
            {
                var isValid = syntax_analysis.IsValidParen(statement);

                if (isValid)
                {
                    syntaxStates.Add(syntax_analysis.ExtractSyntaxes(statement));
                }
                else
                {
                    var statementIndex = lexical_statements.IndexOf(statement);
                    throw new InterpreterException("Invalid syntax at : " + string.Join(" ", tokenized_statements[statementIndex]));
                }
            }
            var syntaxErrorIndex = syntaxStates.FindIndex(x => x.Contains("Syntax error"));

            // semantic analysis
            if (syntaxErrorIndex == -1)
            {
                semantic_analysis.ExtractSemantics(tokenized_statements, systemVariables);
            }
            else
            { // invalid syntax 
                throw new InterpreterException("Invalid syntax at " + string.Join(" ", tokenized_statements[syntaxErrorIndex]));
            }

            // proceed if okay semantics
            heap = interpreter.Runtime(tokenized_statements, systemVariables);

            var StatisticsColumn = new List<StatisticsColumn>();
            foreach (var kv in heap)
            {
                if (kv.Key != "System")
                {
                    StatisticsColumn.Add(new StatisticsColumn() { Name = kv.Key, Value = kv.Value.Value.ToString() });
                }
            }

            return StatisticsColumn.ToArray();
        }
    }
}
