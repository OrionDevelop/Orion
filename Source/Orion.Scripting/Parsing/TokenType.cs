namespace Orion.Scripting.Parsing
{
    public enum TokenType
    {
        /// <summary>
        ///     +
        /// </summary>
        OperatorAdd,

        /// <summary>
        ///     &, &&
        /// </summary>
        OperatorAnd,

        /// <summary>
        ///     contains
        /// </summary>
        OperatorContains,

        /// <summary>
        ///     containsIgnoreCase
        /// </summary>
        OperatorContainsIgnoreCase,

        /// <summary>
        ///     /
        /// </summary>
        OperatorDivide,

        /// <summary>
        ///     endsWith
        /// </summary>
        OperatorEndsWith,

        /// <summary>
        ///     endsWithIgnoreCase
        /// </summary>
        OperatorEndsWithIgnoreCase,

        /// <summary>
        ///     =, ==
        /// </summary>
        OperatorEqual,

        /// <summary>
        ///     &gt;
        /// </summary>
        OperatorGreaterThan,

        /// <summary>
        ///     &gt;=
        /// </summary>
        OperatorGreaterThanOrEqual,

        /// <summary>
        ///     &lt;
        /// </summary>
        OperatorLessThan,

        /// <summary>
        ///     &lt;=
        /// </summary>
        OperatorLessThanOrEqual,

        /// <summary>
        ///     *
        /// </summary>
        OperatorMultiply,

        /// <summary>
        ///     !
        /// </summary>
        OperatorNot,

        /// <summary>
        ///     !=
        /// </summary>
        OperatorNotEqual,

        /// <summary>
        ///     |, ||
        /// </summary>
        OperatorOr,

        /// <summary>
        ///     startsWith
        /// </summary>
        OperatorStartsWith,

        /// <summary>
        ///     startsWithIgnoreCase
        /// </summary>
        OperatorStartsWithIgnoreCase,

        /// <summary>
        ///     -
        /// </summary>
        OperatorSubtract,

        /// <summary>
        ///     "STRING"
        /// </summary>
        String,

        /// <summary>
        ///     123456789
        /// </summary>
        Numeric,

        /// <summary>
        ///     .
        /// </summary>
        Period,

        /// <summary>
        ///     (
        /// </summary>
        BracketsStart,

        /// <summary>
        ///     )
        /// </summary>
        BracketsEnd,

        /// <summary>
        /// </summary>
        Literal
    }
}