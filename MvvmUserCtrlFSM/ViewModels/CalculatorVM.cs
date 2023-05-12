using CalculatorManager;
using CalculatorManager.Interfaces;
using CommanLib;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvvmUserCtrlFSM.States;
using System;
using System.Threading.Tasks;

namespace MvvmUserCtrlFSM.ViewModels;

public partial class CalculatorVM : ObservableObject
{
    #region Fields

    State _state;
    string _preValue;
    string _nowValue;
    string _operator;

    [ObservableProperty]
    double result;

    [ObservableProperty]
    string historyOutput;

    IBusinessLogic calculator;

    StateMachine stateMachine;


    #endregion


    #region Properties

    public static IHost AppHost { get; private set; }

    #endregion


    public CalculatorVM()
    {

        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<IOtherFeatures, OtherFeatures>();
            services.AddSingleton<IBusinessLogic, Calculator>();

        }).Build();

        Task task = Task.Run(async () =>
        {
            await AppHost!.StartAsync();
        });

        calculator = AppHost.Services.GetRequiredService<IBusinessLogic>();
        calculator.Result.Subscribe(x =>
        {
            _preValue = x.ToString();
            Result = x;
        });

        stateMachine = new StateMachine();
    }


    #region Public Methods

    public AsyncRelayCommand<string> OperatorCommand => new(async Task (transSymbol) =>
    {
        //string symbol = string.Empty;

        //switch (_state)
        //{
        //    case State.Start:
        //        _preValue = long.Parse(_nowValue).ToString();
        //        _nowValue = "0";
        //        _state = State.First;
        //        break;
        //    case State.First:
        //        break;
        //    case State.Operator:
        //        break;
        //    case State.Second:

        //        Calculator();

        //        if (_operator == string.Empty)
        //        {
        //            _preValue = _nowValue;
        //        }
        //        else
        //        {
        //            _state = State.First;
        //        }

        //        break;
        //    case State.End:
        //        _nowValue = "0";
        //        _state = State.Second;
        //        break;
        //}

        //_operator = transSymbol;

        //HistoryOutput = $"{_preValue} {StrOperator()}";

        stateMachine.ProcessInput(transSymbol.ToString());
    });

    public AsyncRelayCommand<object> NumberCommand => new(async Task (obj) =>
    {
        switch (_state)
        {
            case State.Start:
                break;
            case State.First:
                _nowValue = "0";
                _state = State.Second;
                break;
            case State.Operator:
                break;
            case State.Second:
                break;
            case State.End:
                EqualsClear();
                break;
        }

        _nowValue += obj.ToString();

        Result = long.Parse(_nowValue);

        // 輸入 number 觸發狀態轉換
        stateMachine.ProcessInput(obj.ToString());
    });

    public AsyncRelayCommand EqualCommand => new(async Task () =>
    {
        // 輸入 'complete' 觸發狀態轉換
        stateMachine.ProcessInput("=");

        //string symbol = string.Empty;

        //Calculator();

        //HistoryOutput = _state == State.Start ? $"{long.Parse(_nowValue)} =" : $"{_preValue} {StrOperator()} {long.Parse(_nowValue)} =";

        //_state = State.End;
    });

    public AsyncRelayCommand ClearEntryCommand => new(async Task () => ClearEntry());

    public AsyncRelayCommand ClearCommand => new(async Task () => Clear());

    public AsyncRelayCommand BackCommand => new(async Task () =>
    {
        if (_state == State.Start || _state == State.Second)
        {
            if (_nowValue != string.Empty && _nowValue != "0")
            {
                _nowValue = _nowValue[..^1];
            }

            Result = long.Parse(_nowValue);
        }
    });

    public AsyncRelayCommand NegateCommand => new(async Task () =>
    {

        if (_nowValue[0] == '-')
        {
            _nowValue = $"{_nowValue[1..]}";
            Result = long.Parse(_nowValue);
        }
        else
        {
            _nowValue = $"-{_nowValue}";
            Result = long.Parse(_nowValue);
        }

        HistoryOutput = Result.ToString();
    });

    public AsyncRelayCommand DenominatorCommand => new(async Task () =>
    {
        _nowValue = (1 / double.Parse(_nowValue)).ToString();

        Result = double.Parse(_nowValue);

        HistoryOutput = Result.ToString();
    });

    public AsyncRelayCommand PowCommand => new(async Task () =>
    {
        _nowValue = Math.Pow(double.Parse(_nowValue), 2).ToString();

        Result = double.Parse(_nowValue);

        HistoryOutput = Result.ToString();
    });

    public AsyncRelayCommand SqrtCommand => new(async Task () =>
    {
        _nowValue = Math.Sqrt(double.Parse(_nowValue)).ToString();

        Result = double.Parse(_nowValue);

        HistoryOutput = Result.ToString();
    });


    #endregion


    #region Private Methods

    private void EqualsClear()
    {
        _state = State.Start;
        _nowValue = "0";
        _preValue = "0";
        //_operator = string.Empty;

        Result = 0;
        HistoryOutput = string.Empty;
    }

    private void ClearEntry()
    {
        _nowValue = "0";
        Result = 0;
    }

    private void Clear()
    {
        _state = State.Start;
        _nowValue = "0";
        _preValue = "0";
        _operator = string.Empty;

        Result = 0;
        HistoryOutput = string.Empty;
    }

    //private string StrOperator()
    //{
    //    switch (_operator)
    //    {
    //        case Operator.Add:
    //            return "+";
    //        case Operator.Substract:
    //            return "-";
    //        case Operator.Multiply:
    //            return "×";
    //        case Operator.Divide:
    //            return "÷";
    //        default:
    //            return string.Empty;
    //    }
    //}

    private async Task Calculator()
    {
        switch (_operator)
        {
            case "+":
                await Add();
                break;
            case "-":
                await Substract();
                break;
            case "×":
                await Multiply();
                break;
            case "÷":
                await Divide();
                break;
            default:

                break;
        }
    }

    private async Task Add()
    {
        await calculator.Add(double.Parse(_preValue.ToString()), double.Parse(_nowValue.ToString()));
    }

    private async Task Substract()
    {
        await calculator.Substract(double.Parse(_preValue.ToString()), double.Parse(_nowValue.ToString()));
    }

    private async Task Multiply()
    {
        await calculator.Multiply(double.Parse(_preValue.ToString()), double.Parse(_nowValue.ToString()));
    }

    private async Task Divide()
    {
        await calculator.Divide(double.Parse(_preValue.ToString()), double.Parse(_nowValue.ToString()));
    }

    #endregion

}
