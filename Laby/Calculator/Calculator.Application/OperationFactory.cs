using System.Collections.Generic;

namespace Calculator.Application
{
    public class OperationFactory
    {
        public OperationFactory() { }

        private static readonly Dictionary<string, IOperation> Operations = new()
        {
            { "+", new AdditionOperation() },
            { "-", new SubtractionOperation() },
            { "*", new MultiplicationOperation() },
            { "/", new DivisionOperation() },
            { "pow", new SquareOperation() },
            { "sqrt", new SquareRootOperation() }
        };

        public static IOperation? GetOperation(string operationSymbol)
        {
            if(Operations.TryGetValue(operationSymbol, out IOperation? operation))
                return operation;
            else return null;
        }
    }
}
