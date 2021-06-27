using System;
using System.Collections.Generic;

namespace Calculator
{
    internal static class Parser
    {
        public static List<Token> ParseExpression(string expression)
        {
            var tokens = new List<Token>();

            var span = expression.AsSpan();
            var start = SkipSpaces(expression, 0);

            while (start < expression.Length)
            {
                tokens.Add(Token.Parse(span.Slice(start), out int end));
                start = SkipSpaces(expression, start + end);
            }

            return tokens;
        }

        private static int SkipSpaces(string expression, int start)
        {
            while (start < expression.Length && char.IsWhiteSpace(expression[start]))
                start += 1;
            return start;
        }
    }
}
