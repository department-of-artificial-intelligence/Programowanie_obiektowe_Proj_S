namespace Calculator.Application
{
    public class MultiplicationOperation : IOperation
    {
        public MultiplicationOperation() { }

        public double Execute(params double[] args)
            => args[0] * args[1];
        }
}
