namespace Calculator.Application
{
    public class DivisionOperation : IOperation
    {
        public DivisionOperation() { }

        public double Execute(params double[] args)
        => args[1] != 0
            ? args[0] / args[1]
            : throw new DivideByZeroException();
    }
}
