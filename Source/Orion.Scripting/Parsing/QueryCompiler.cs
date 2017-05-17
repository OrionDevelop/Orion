using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Orion.Scripting.Ast;

namespace Orion.Scripting.Parsing
{
    public static class QueryCompiler
    {
        private static List<FilterSourceBase> Source { get; } = new List<FilterSourceBase>();

        public static void RegisterSource(FilterSourceBase source)
        {
            if (Source.All(w => w.Key != source.Key))
                Source.Add(source);
        }

        public static FilterQuery Compile<T>(string query)
        {
            var reader = new TokenReader(Tokenizer.Tokenize(query));
            var filter = new FilterQuery();

            CompileSourceQuery(reader, filter);
            CompileFilterQuery<T>(reader, filter);

            return filter;
        }

        #region Source query

        private static void CompileSourceQuery(TokenReader reader, FilterQuery filter)
        {
            if (reader.IsEOT || !reader.LookAhead().Is(TokenType.Literal, "FROM"))
            {
                Debug.WriteLine("[WARN] ソースクエリが検出できませんでした。デフォルト値 '*' を使用します。");
                filter.Source = null;
                filter.SourceStr = "*";
            }
            else
            {
                reader.Read();
                if (reader.IsEOT)
                    throw new QueryParsingException("ソースを指定する必要があります。");

                var source = reader.Read();
                if (!source.Is(TokenType.Literal) && !source.Is(TokenType.OperatorMultiply))
                    throw new QueryParsingException("ソースが不正です。");

                filter.Source = Source.SingleOrDefault(w => w.Key == source.Value);
                filter.SourceStr = source.Value;
            }
        }

        #endregion

        #region Filter query

        private static void CompileFilterQuery<T>(TokenReader reader, FilterQuery filter)
        {
            if (reader.IsEOT)
                return;

            if (!reader.Read().Is(TokenType.Literal, "WHERE"))
                throw new QueryParsingException("WHERE 句がありません。");

            if (reader.IsEOT)
                throw new QueryParsingException("WHERE 句に式がありません。");

            filter.DebugInfo = CompileConditionalOrExpression(reader);
            filter.Delegate = filter.DebugInfo.EvaluateRootFunc<T>().Compile();
            if (!reader.IsEOT)
                Debug.WriteLine($"{reader.LookAhead().Type}: {reader.LookAhead().Value}");
        }

        private static AstNode CompileConditionalOrExpression(TokenReader reader)
        {
            var left = CompileConditionalAndExpression(reader);
            if (reader.IsEOT)
                return left;

            if (!reader.LookAhead().Is(TokenType.OperatorOr))
                return left; // EMPTY

            return CreateAstTree(new AstConditionalOrOperator(reader.Read().Value), left, CompileConditionalOrExpression(reader));
        }

        private static AstNode CompileConditionalAndExpression(TokenReader reader)
        {
            var left = CompileEqualityExpression(reader);
            if (reader.IsEOT)
                return left;

            if (!reader.LookAhead().Is(TokenType.OperatorAnd))
                return left; // EMPTY

            return CreateAstTree(new AstConditionalAndOperator(reader.Read().Value), left, CompileConditionalAndExpression(reader));
        }

        private static AstNode CompileEqualityExpression(TokenReader reader)
        {
            var left = CompileRelationalExpression(reader);
            if (reader.IsEOT)
                return left;

            switch (reader.LookAhead().Type)
            {
                case TokenType.OperatorEqual:
                    return CreateAstTree(new AstEqualOperator(reader.Read().Value), left, CompileEqualityExpression(reader));

                case TokenType.OperatorNotEqual:
                    return CreateAstTree(new AstNotEqualOperator(reader.Read().Value), left, CompileEqualityExpression(reader));

                case TokenType.OperatorContains:
                    return CreateAstTree(new AstContainsOperator(reader.Read().Value), left, CompileEqualityExpression(reader));

                case TokenType.OperatorContainsIgnoreCase:
                    return CreateAstTree(new AstContainsIgnoreCaseOperator(reader.Read().Value), left, CompileEqualityExpression(reader));

                case TokenType.OperatorStartsWith:
                    return CreateAstTree(new AstStartsWithOperator(reader.Read().Value), left, CompileEqualityExpression(reader));

                case TokenType.OperatorStartsWithIgnoreCase:
                    return CreateAstTree(new AstStartsWithIgnoreCaseOperator(reader.Read().Value), left, CompileEqualityExpression(reader));

                case TokenType.OperatorEndsWith:
                    return CreateAstTree(new AstEndsWithOperator(reader.Read().Value), left, CompileEqualityExpression(reader));

                case TokenType.OperatorEndsWithIgnoreCase:
                    return CreateAstTree(new AstEndsWithIgnoreCaseOperator(reader.Read().Value), left, CompileEqualityExpression(reader));
            }
            return left; // EMPTY
        }

        private static AstNode CompileRelationalExpression(TokenReader reader)
        {
            var left = CompileAdditiveExpression(reader);
            if (reader.IsEOT)
                return left;

            switch (reader.LookAhead().Type)
            {
                case TokenType.OperatorLessThan:
                    return CreateAstTree(new AstLessThanOperator(reader.Read().Value), left, CompileRelationalExpression(reader));

                case TokenType.OperatorLessThanOrEqual:
                    return CreateAstTree(new AstLessThanOrEqualOperator(reader.Read().Value), left, CompileRelationalExpression(reader));

                case TokenType.OperatorGreaterThan:
                    return CreateAstTree(new AstGreaterThanOperator(reader.Read().Value), left, CompileRelationalExpression(reader));

                case TokenType.OperatorGreaterThanOrEqual:
                    return CreateAstTree(new AstGreaterThanOrEqualOperator(reader.Read().Value), left, CompileRelationalExpression(reader));
            }
            return left; // EMPTY
        }

        private static AstNode CompileAdditiveExpression(TokenReader reader)
        {
            var left = CompileMultiplicativeExpression(reader);
            if (reader.IsEOT)
                return left;

            if (!reader.LookAhead().Is(TokenType.OperatorAdd) && !reader.LookAhead().Is(TokenType.OperatorSubtract))
                return left; // EMPTY

            return reader.LookAhead().Is(TokenType.OperatorAdd)
                ? CreateAstTree(new AstAddOperator(reader.Read().Value), left, CompileAdditiveExpression(reader))
                : CreateAstTree(new AstSubtractOperator(reader.Read().Value), left, CompileAdditiveExpression(reader));
        }

        private static AstNode CompileMultiplicativeExpression(TokenReader reader)
        {
            var left = CompilePrimary(reader);
            if (reader.IsEOT)
                return left;

            if (!reader.LookAhead().Is(TokenType.OperatorMultiply) && !reader.LookAhead().Is(TokenType.OperatorDivide))
                return left; // EMPTY

            return reader.LookAhead().Is(TokenType.OperatorMultiply)
                ? CreateAstTree(new AstMultiplyOperator(reader.Read().Value), left, CompileMultiplicativeExpression(reader))
                : CreateAstTree(new AstDivideOperator(reader.Read().Value), left, CompileMultiplicativeExpression(reader));
        }

        private static AstNode CompilePrimary(TokenReader reader)
        {
            if (reader.IsEOT)
                return null;

            var primary = reader.Read();
            switch (primary.Type)
            {
                case TokenType.Literal:
                    if (reader.IsEOT || !reader.LookAhead().Is(TokenType.Period))
                        return new AstFieldAccess(primary.Value);
                    reader.Read();
                    return new AstFieldAccess($"{primary.Value}.{reader.Read().Value}");

                case TokenType.BracketsStart:
                    var expression = CompileConditionalOrExpression(reader);
                    if (reader.IsEOT || !reader.Read().Is(TokenType.BracketsEnd))
                        throw new QueryParsingException("括弧が閉じられていません。");
                    return expression;

                case TokenType.Boolean:
                    return new AstPrimary<bool>(bool.Parse(primary.Value));

                case TokenType.String:
                    return new AstPrimary<string>(primary.Value);

                case TokenType.Numeric:
                    return new AstPrimary<int>(int.Parse(primary.Value));
            }

            return null;
        }

        private static AstNode CreateAstTree(AstOperator root, AstNode left, AstNode right)
        {
            root.Left = left ?? throw new QueryParsingException($"演算子 '{root.Value}' の左辺を検出できませんでした。");
            root.Right = right ?? throw new QueryParsingException($"演算子 '{root.Value}' の右辺を検出できませんでした。");
            return root;
        }

        #endregion
    }
}