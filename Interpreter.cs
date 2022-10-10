using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Interpreter
{
    class Interpreter
    {
        Lexical_Analysis lexical_analysis;
        Syntax_Analysis syntax_analysis;
        Semantic_Analysis semantic_analysis;


        public Token_Symbols_Set statements_ignore;
        public Token_Symbols_Set statements_separator;
        public Tokenizer statements_extractor;

        public Token_Symbols_Set tokens_separator;
        public Token_Symbols_Set tokens_ignore;
        public Token_Symbols_Set tokens_symbols;
        public Tokenizer tokens_extractor;

        public Token_Symbols_Set semantic_ignores;
        public Token_Symbols_Set semantic_separator;
        public Tokenizer semantic_extractor;

        public Interpreter()
        {
            this.statements_separator = new Token_Symbols_Set(new String[] { "~" });
            this.statements_ignore = new Token_Symbols_Set(new String[] { "~" });
            this.statements_extractor = new Tokenizer(statements_separator, statements_ignore);

            this.tokens_separator = new Token_Symbols_Set(new String[] { " ", "\n", ";", });
            this.tokens_ignore = new Token_Symbols_Set(new String[] { " ", "\n" });
            this.tokens_symbols = new Token_Symbols_Set(new string[] { "+", "-", "*", "/", "%", "(", ")", "=" });
            this.tokens_extractor = new Tokenizer(tokens_separator, tokens_ignore, tokens_symbols);

            this.semantic_separator = new Token_Symbols_Set(new string[] { "=" });
            this.semantic_ignores = new Token_Symbols_Set(new string[] { " " });
            this.semantic_extractor = new Tokenizer(semantic_separator, semantic_ignores);

            this.lexical_analysis = new Lexical_Analysis();
            this.syntax_analysis = new Syntax_Analysis();
            this.semantic_analysis = new Semantic_Analysis();
        }

        public void runtime(String source_code)
        {
            source_code = source_code.Replace(";", ";~");

            // partition source code into list of statement
            List<String> statements = statements_extractor.extractStatement(source_code);

            // tokenize each statement
            List<List<String>> tokenized_statements = new List<List<String>>();
            foreach (var statement in statements) {
                tokenized_statements.Add(tokens_extractor.extractTokens(statement));
            }

            // lexical expression
            List<List<String>> lexical_statements = new List<List<String>>();
            foreach (var tokenized in tokenized_statements) {
                lexical_statements.Add(lexical_analysis.extractLexicon(tokenized));
            }

            // syntax status statements
            List<List<String>> syntaxes = new List<List<String>>();
            // checks if there is syntax error and returns its index
            var syntaxErrorIndex = syntax_analysis.isValidParen(lexical_statements);
            if (syntaxErrorIndex == -1) {
                foreach (List<String> statement in lexical_statements) {
                    syntaxes.Add(syntax_analysis.extractSyntaxes(statement));
                }
            }
            else { // invalid syntax for parenthesis
                throw new Exception("Invalid syntax at : " + string.Join(" ", tokenized_statements[syntaxErrorIndex])); 
            }

            // semantics
            syntaxErrorIndex = syntaxes.FindIndex(x => x.Contains("Syntax error"));
            var t = false;
            if (syntaxErrorIndex == -1) {
                t = semantic_analysis.extractSemantics(tokenized_statements);
            } 
            else { // invalid syntax 
                throw new Exception("Invalid syntax at : " + string.Join(" ", tokenized_statements[syntaxErrorIndex]));
            }

            // interpreter part (evaluation, arithmetic, save in the heap, chchchuu...)
            if (t) {
                Console.WriteLine("hello");
            }
            Console.WriteLine("hi");
            // continue here if ok semantically
            //var t = -1;
            //if (t) {
            //    Console.WriteLine("hello");
            //}

        }

    }
}
