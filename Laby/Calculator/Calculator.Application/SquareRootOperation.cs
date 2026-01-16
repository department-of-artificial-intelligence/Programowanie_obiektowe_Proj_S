namespace Calculator.Application
{
    public class SquareRootOperation : IOperation
    {
        public SquareRootOperation() { }

        public double Execute(params double[] args)
            => Math.Sqrt(args[0]);
    }
}
