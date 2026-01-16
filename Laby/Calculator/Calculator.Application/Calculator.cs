namespace Calculator.Application
{
    public class Calculator
    {
        public Calculator() { }
        public static double Calculate(IOperation operation, params double[] args)
            => operation.Execute(args);
    }
}
