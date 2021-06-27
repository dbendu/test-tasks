using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calculator
{
    internal abstract class Token
    {
        public static readonly Token Unknown = null;

        public abstract double Evaluate();

        #region Парсинг токена 

        public static Token Parse(ReadOnlySpan<char> expression, out int end)
        {
            var number = TryParseNumber(expression, out end);
            if (number != Token.Unknown)
                return number;

            foreach (var (operationStr, operationType) in ExpressionToOperations)
            {
                if (MemoryExtensions.SequenceEqual(operationStr, expression.Slice(0, operationStr.Length)))
                {
                    end = operationStr.Length;
                    return new TokenOperation(operationType);
                }
            }

            throw new Exception($"Unknown token: {expression.ToString()}");
        }

        private static Dictionary<string, ArithmeticOperationType> ExpressionToOperations = new()
        {
            { "+", ArithmeticOperationType.ADD },
            { "-", ArithmeticOperationType.SUB },
            { "*", ArithmeticOperationType.MUL },
            { "/", ArithmeticOperationType.DIV }
        };

        #region Парсинг числа

        private static Token TryParseNumber(ReadOnlySpan<char> expression, out int end)
        {
            if (char.IsDigit(expression[0]) ||
                (expression.Length > 1 && expression[0] == '-' && char.IsDigit(expression[1])))
            {
                var pos = char.IsDigit(expression[0]) ? 0 : 1;
                while (pos < expression.Length && char.IsDigit(expression[pos]))
                    pos += 1;

                var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                if (pos + separator.Length < expression.Length &&
                    expression.Slice(pos, separator.Length) == separator)
                {
                    pos += 1;
                    while (char.IsDigit(expression[pos]))
                        pos += 1;
                }

                end = pos;
                return new TokenNumber(double.Parse(expression.Slice(0, pos)));
            }

            end = -1;
            return Token.Unknown;
        }

        #endregion

        #endregion
    }

    internal class TokenNumber : Token
    {
        private readonly double Number;

        public TokenNumber(double number)
        {
            Number = number;
        }

        public override double Evaluate() => Number;
    }

    internal class TokenOperation : Token
    {
        private ArithmeticOperationType Operation { get; }

        public int Priority { get; }

        public Token Left { get; set; }

        public Token Right { get; set; }

        public TokenOperation(ArithmeticOperationType operation)
        {
            Operation = operation;
            Priority = Priorities[operation];
        }

        private static readonly Dictionary<ArithmeticOperationType, int> Priorities = new()
        {
            { ArithmeticOperationType.ADD, 1 },
            { ArithmeticOperationType.SUB, 1 },
            { ArithmeticOperationType.MUL, 2 },
            { ArithmeticOperationType.DIV, 2 }
        };

        public override double Evaluate()
        {
            var left = Left.Evaluate();
            var right = Right.Evaluate();

            switch (Operation)
            {
                case ArithmeticOperationType.ADD:
                    return left + right;

                case ArithmeticOperationType.SUB:
                    return left - right;

                case ArithmeticOperationType.MUL:
                    return left * right;

                case ArithmeticOperationType.DIV:
                    {
                        if (right == 0)
                            throw new Exception($"Division by zero: {left} / {right}");
                        return left / right;
                    }

                default:
                    throw new Exception($"Invalid operation: {Operation}");
            }
        }
    }

    internal enum TokenType
    {
        NUMBER,
        ARITHMETIC_OPERATION
    }

    internal enum ArithmeticOperationType
    {
        ADD,
        SUB,
        MUL,
        DIV
    }
}
