using System;
using System.Collections.Generic;

namespace Calculator
{
    public class SimpleCalculator
    {
        #region Решение выражения в префиксной записи

        public double EvaluatePrefix(string expression)
        {
            var tokens = Parser.ParseExpression(expression);

            return EvaluatePrefix(tokens);
        }

        private static double EvaluatePrefix(List<Token> tokens)
        {
            Stack<TokenNumber> numbers = new();

            for (var i = tokens.Count - 1; i >= 0; --i)
            {
                if (tokens[i] is TokenNumber number)
                {
                    numbers.Push(number);
                    continue;
                }

                if (numbers.Count < 2)
                    throw new Exception("Invalid input");

                var operation = tokens[i] as TokenOperation;
                operation.Left = numbers.Pop();
                operation.Right = numbers.Pop();

                numbers.Push(new TokenNumber(operation.Evaluate()));
            }

            if (numbers.Count != 1)
                throw new Exception("Invalid format");

            return numbers.Peek().Evaluate();
        }

        #endregion

        #region Решение выражения в инфиксной записи

        public double Evaluate(string expression)
        {
            var tokens = Parser.ParseExpression(expression);

            return EvaluateInfix(tokens);
        }

        private static double EvaluateInfix(List<Token> tokens)
        {
            Stack<TokenNumber> numbers = new();
            Stack<TokenOperation> operations = new();

            foreach (var token in tokens)
            {
                if (token is TokenNumber number)
                    numbers.Push(number);
                else
                {
                    if (numbers.Count == 0)
                        throw new Exception("Invalid format");

                    var operation = token as TokenOperation;

                    RollUp(numbers, operations, operation.Priority);

                    operations.Push(operation);
                }
            }

            RollUp(numbers, operations, int.MinValue);

            if (operations.Count != 0 || numbers.Count != 1)
                throw new Exception("Invalid input format");

            return numbers.Pop().Evaluate();
        }

        private static void RollUp(Stack<TokenNumber> numbers, Stack<TokenOperation> operations, int priority)
        {
            while (operations.Count != 0 && numbers.Count > 1 && operations.Peek().Priority >= priority)
            {
                var operation = operations.Pop();

                operation.Right = numbers.Pop();
                operation.Left = numbers.Pop();

                numbers.Push(new TokenNumber(operation.Evaluate()));
            }
        }

        #endregion
    }
}
