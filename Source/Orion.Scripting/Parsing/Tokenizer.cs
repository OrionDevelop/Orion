using System;
using System.Collections.Generic;
using System.Linq;

namespace Orion.Scripting.Parsing
{
    public static class Tokenizer
    {
        public static IEnumerable<Token> Tokenize(string query)
        {
            var strPos = 0;
            if (string.IsNullOrWhiteSpace(query))
                yield break;
            do
            {
                switch (query[strPos])
                {
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        break;

                    case '!': // !, !=
                        if (CheckNext(query, strPos, '='))
                        {
                            yield return new Token(TokenType.OperatorNotEqual, "!=");
                            strPos++;
                        }
                        else
                        {
                            yield return new Token(TokenType.OperatorNot, "!");
                        }
                        break;

                    case '+':
                        yield return new Token(TokenType.OperatorAdd, "+");
                        break;

                    case '-':
                        yield return new Token(TokenType.OperatorSubtract, "-");
                        break;

                    case '*':
                        yield return new Token(TokenType.OperatorMultiply, "*");
                        break;

                    case '/':
                        yield return new Token(TokenType.OperatorDivide, "/");
                        break;

                    case '<': // <, <=
                        if (CheckNext(query, strPos, '='))
                        {
                            yield return new Token(TokenType.OperatorLessThanOrEqual, "<=");
                            strPos++;
                        }
                        else
                        {
                            yield return new Token(TokenType.OperatorLessThan, "<");
                        }
                        break;

                    case '>': // >, >=
                        if (CheckNext(query, strPos, '='))
                        {
                            yield return new Token(TokenType.OperatorGreaterThanOrEqual, ">=");
                            strPos++;
                        }
                        else
                        {
                            yield return new Token(TokenType.OperatorGreaterThan, ">");
                        }
                        break;

                    case '=': // =, ==
                        if (CheckNext(query, strPos, '='))
                        {
                            yield return new Token(TokenType.OperatorEqual, "==");
                            strPos++;
                        }
                        else
                        {
                            yield return new Token(TokenType.OperatorEqual, "=");
                        }
                        break;

                    case '&': // &, &&
                        if (CheckNext(query, strPos, '&'))
                        {
                            yield return new Token(TokenType.OperatorAnd, "&&");
                            strPos++;
                        }
                        else
                        {
                            yield return new Token(TokenType.OperatorAnd, "&");
                        }
                        break;

                    case '|': // |, ||
                        if (CheckNext(query, strPos, '|'))
                        {
                            yield return new Token(TokenType.OperatorOr, "||");
                            strPos++;
                        }
                        else
                        {
                            yield return new Token(TokenType.OperatorOr, "|");
                        }
                        break;

                    case '"': // String
                        yield return new Token(TokenType.String, GetString(query, ref strPos));
                        break;

                    case '.':
                        yield return new Token(TokenType.Period, ".");
                        break;

                    case '(':
                        yield return new Token(TokenType.BracketsStart, "(");
                        break;

                    case ')':
                        yield return new Token(TokenType.BracketsEnd, ")");
                        break;

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9': // Numeric
                        yield return new Token(TokenType.Numeric, GetNumeric(query, ref strPos));
                        break;

                    default:
                        yield return ParseOtherToken(query, ref strPos);
                        break;
                }
            } while (++strPos < query.Length);
        }

        private static string GetString(string query, ref int strPos)
        {
            var begin = strPos;
            strPos++;

            while (strPos < query.Length)
            {
                if (query[strPos] == '\\')
                    if (CheckNext(query, strPos, '\\') || CheckNext(query, strPos, '"'))
                        strPos++;
                    else if (strPos + 1 >= query.Length)
                        throw new QueryParsingException("文字列は \" で終了する必要があります。");
                    else
                        throw new QueryParsingException("不正な文字列です: \\");
                else if (query[strPos] == '"')
                    return query.Substring(begin + 1, strPos - begin - 1);
                strPos++;
            }
            throw new QueryParsingException("文字列は \" で終了する必要があります。");
        }

        private static string GetNumeric(string query, ref int strPos)
        {
            var begin = strPos;

            while (strPos < query.Length)
                if ('0' <= query[strPos] && query[strPos] <= '9')
                {
                    strPos++;
                }
                else
                {
                    strPos--;
                    return query.Substring(begin, strPos + 1 - begin);
                }
            return query.Substring(begin, strPos - begin);
        }

        private static Token ParseOtherToken(string query, ref int strPos)
        {
            const string splitters = " \t\r\n!+-*/<>=&|\".()";
            var begin = strPos;

            while (strPos < query.Length)
            {
                if (splitters.Contains(query[strPos]))
                    return CreateToken(query.Substring(begin, strPos-- - begin));
                strPos++;
            }
            return CreateToken(query.Substring(begin, strPos - begin));
        }

        private static Token CreateToken(string value)
        {
            string[] boolValues = {"true", "false"};
            if (boolValues.Contains(value))
                return new Token(TokenType.Boolean, value);

            string[] operators = {"contains", "containsignorecase", "startswith", "startswithignorecase", "endswith", "endswithignorecase"};
            if (!operators.Contains(value.ToLower()))
                return new Token(TokenType.Literal, value);
            switch (value.ToLower())
            {
                case "contains":
                    return new Token(TokenType.OperatorContains, value);

                case "containsignorecase":
                    return new Token(TokenType.OperatorContainsIgnoreCase, value);

                case "startswith":
                    return new Token(TokenType.OperatorStartsWith, value);

                case "startswithignorecase":
                    return new Token(TokenType.OperatorStartsWithIgnoreCase, value);

                case "endswith":
                    return new Token(TokenType.OperatorEndsWith, value);

                case "endswithignorecase":
                    return new Token(TokenType.OperatorEndsWithIgnoreCase, value);
            }
            throw new ArgumentException();
        }

        private static bool CheckNext(string query, int strPos, char next)
        {
            if (strPos + 1 >= query.Length)
                return false;
            return query[strPos + 1] == next;
        }
    }
}