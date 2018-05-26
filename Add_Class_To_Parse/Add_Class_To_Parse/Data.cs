using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

namespace Add_Class_To_Parse
{
    /// <summary>
    /// Сериализуемый класс
    /// </summary>
    [Serializable]
    class Data
    {
        //Переменная хранящая вводимое с консоли выражение

        public string StringOfExpression { get; set; }

        //Продолжительность выполнения математического выражения

        public double Dur { get; set; }

        //Количество математический операций в выражении

        public int CountOfOperations { get; set; }

        //Результат математического выражения

        public string Result { get; set; }
    }
}
