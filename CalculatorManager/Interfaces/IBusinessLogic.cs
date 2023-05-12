namespace CalculatorManager.Interfaces;

public interface IBusinessLogic
{
    IObservable<double> Result { get; }
    Task Add(double firstValue, double secondValue);
    Task Substract(double firstValue, double secondValue);
    Task Multiply(double firstValue, double secondValue);
    Task Divide(double firstValue, double secondValue);
}
