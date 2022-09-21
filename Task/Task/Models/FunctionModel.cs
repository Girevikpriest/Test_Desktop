using System;
using System.Collections.Generic;
using System.Linq;

namespace Task.Models
{
    /// <summary>Используется для задания именованной функции от двух параметров.</summary>
    public class FunctionModel
    {
        /// <summary>Имя Функции.</summary>
        public string Name { get; }

        /// <summary>Делегат Функции.</summary>
        public Func<double, double, double> Function { get; }

        /// <summary>Коэффициент A.</summary>
        public double A { get; set; }

        /// <summary>Коэффициент B.</summary>
        public double B { get; set; }

        /// <summary>Коэффициент C.</summary>
        public double C { get; set; }

        public IReadOnlyList<double> Arguments { get; }

        /// <summary>Создаёт экземпляр <see cref="FunctionModel"/>.</summary>
        /// <param name="name">Имя Функции.</param>
        /// <param name="function">Делегат Функции.</param>
        public FunctionModel(string name, IEnumerable<double> arguments, Func<double, double, double, double, double, double> function)
        {
            Name = name;
            Arguments = arguments.ToList().AsReadOnly();
            this.function = function ?? throw new ArgumentNullException(nameof(function));
            Function = Calculate;
        }
        private readonly Func<double, double, double, double, double, double> function;
        private double Calculate(double x, double y)
            => function(A, B, C, x, y);
    }
}
