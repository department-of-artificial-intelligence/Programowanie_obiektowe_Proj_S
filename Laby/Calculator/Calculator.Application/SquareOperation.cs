namespace Calculator.Application
{
    public class SquareOperation : IOperation
    {
        public SquareOperation() { }

        public double Execute(params double[] args)
            => Math.Pow(args[0], 2.0);
    }
}
