namespace Calculator.Application
{
    public interface IOperation
    {
        double Execute(params double[] args);
    }
}
