using CalculatorManager.Interfaces;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace CalculatorManager;

public class Calculator : IBusinessLogic
{
    IOtherFeatures _otherFeatures;
    ISubject<double> _resultSubject = new Subject<double>();

    public IObservable<double> Result => _resultSubject.AsObservable();

    public Calculator(IOtherFeatures otherFeatures)
    {
        _otherFeatures = otherFeatures;
    }

    #region Methods

    public async Task Add(double firstValue, double secondValue)
    {
        await Task.Run(() =>
        {
            _otherFeatures.DoSomething();
            _resultSubject.OnNext(firstValue + secondValue);
        });
    }

    public async Task Divide(double firstValue, double secondValue)
    {
        await Task.Run(() =>
        {
            _otherFeatures.DoSomething();
            _resultSubject.OnNext(firstValue / secondValue);
        });
    }

    public async Task Multiply(double firstValue, double secondValue)
    {
        await Task.Run(() =>
        {
            _otherFeatures.DoSomething();
            _resultSubject.OnNext(firstValue * secondValue);
        });
    }

    public async Task Substract(double firstValue, double secondValue)
    {
        await Task.Run(() =>
        {
            _otherFeatures.DoSomething();
            _resultSubject.OnNext(firstValue - secondValue);
        });
    }

    #endregion
}