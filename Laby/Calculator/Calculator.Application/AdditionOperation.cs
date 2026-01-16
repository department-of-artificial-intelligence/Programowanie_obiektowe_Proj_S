
namespace Calculator.Application
{
    public class AdditionOperation : IOperation
    {
        public AdditionOperation() { }
        public double Execute(params double[] args)
            => args[0] + args[1];
    }

}
