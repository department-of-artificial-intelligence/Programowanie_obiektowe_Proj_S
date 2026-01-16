namespace Calculator.Application
{
    public class SubtractionOperation : IOperation
    {
        public SubtractionOperation() { }
        public double Execute(params double[] args)
            => args[0] - args[1];
    }
}
