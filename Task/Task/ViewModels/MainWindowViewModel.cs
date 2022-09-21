using Simplified;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Task.Models;

namespace Task.ViewModels
{

    public class MainWindowViewModel : BaseInpc
    {
        /// <summary>Список Функций.</summary>
        public ObservableCollection<FunctionModel> Functions { get; }
            = new ObservableCollection<FunctionModel>();

        private FunctionModel _selectedFunction;
        /// <summary>Выбранная Функция.</summary>
        public FunctionModel SelectedFunction { get => _selectedFunction; set => Set(ref _selectedFunction, value); }

        public MainWindowViewModel()
        {
            Functions.Add(new FunctionModel("Линейная",
                new List<double> { 1, 2, 3, 4, 5 },
                (a, b, c, x, y) => a * x + b * 1 + c)
            { A = 0, B = 0, C = 1 });
            Functions.Add(new FunctionModel("Квадратичная",
                new List<double> { 10, 20, 30, 40, 50 },
                (a, b, c, x, y) => a * Math.Pow(x, 2) + b * Math.Pow(y, 1) + c)
            { A = 0, B = 0, C = 10 });
            Functions.Add(new FunctionModel("Кубическая",
                new List<double> { 100, 200, 300, 400, 500 },
                (a, b, c, x, y) => a * Math.Pow(x, 3) + b * Math.Pow(y, 2) + c)
            { A = 0, B = 0, C = 100 });
            Functions.Add(new FunctionModel("4-ой степени",
                new List<double> { 1000, 2000, 3000, 4000, 5000 },
                (a, b, c, x, y) => a * Math.Pow(x, 4) + b * Math.Pow(y, 3) + c)
            { A = 0, B = 0, C = 1000 });
            Functions.Add(new FunctionModel("5-ой степени",
                new List<double> { 10000, 20000, 30000, 40000, 50000 },
                (a, b, c, x, y) => a * Math.Pow(x, 5) + b * Math.Pow(y, 4) + c)
            { A = 0, B = 0, C = 10000 });

            CalculatedFunctions.CollectionChanged += OnRowsChanged;
            IsNewFunction = true;
        }


        private void OnRowsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Добавление выбранной Функции в новую строку.
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (FunctionRowTable row in e.NewItems)
                {
                    row.SetFunction(SelectedFunction);
                }
        }

        /// <summary>Строки вычисленных значений.</summary>
        public ObservableCollection<FunctionRowTable> CalculatedFunctions { get; }
            = new ObservableCollection<FunctionRowTable>();

        private bool _isNewFunction;
        /// <summary>Режим изменения Функции.</summary>
        public bool IsNewFunction { get => _isNewFunction; set => Set(ref _isNewFunction, value); }

        private RelayCommand _inputFunctionCommand;
        /// <summary>Команда перехода в режим изменения Функции.</summary>
        public RelayCommand InputFunctionCommand => _inputFunctionCommand
         ?? (_inputFunctionCommand = new RelayCommand
        (
             () => { IsNewFunction = true; CalculatedFunctions.Clear(); NameSelectedFunction = string.Empty; },
             () => !IsNewFunction
        ));

        private RelayCommand _inputXyCommand;
        /// <summary>Команда перехода в режим ввода X и Y.</summary>
        public RelayCommand InputXyCommand => _inputXyCommand
         ?? (_inputXyCommand = new RelayCommand
        (
             () => { IsNewFunction = false; CalculatedFunctions.Clear(); NameSelectedFunction = SelectedFunction?.Name; },
             () => IsNewFunction
        ));

        private string _nameSelectedFunction;
        /// <summary>Имя Функции для которой вводятся X и Y.</summary>
        public string NameSelectedFunction { get => _nameSelectedFunction; private set => Set(ref _nameSelectedFunction, value); }
    }
}
