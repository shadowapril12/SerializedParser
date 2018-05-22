using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Add_Class_To_Parse
{
    /// <summary>
    /// Сериализуемый класс
    /// </summary>
    [DataContract]
    class Serializer
    {
        //Переменная хранящая вводимое с консоли выражение
        [DataMember]
        public string stringOfExpression;

        //Продолжительность выполнения математического выражения
        [DataMember]
        public double dur;

        //Количество математический операций в выражении
        [DataMember]
        public int countOfOperations;

        //Результат математического выражения
        [DataMember]
        public string result;
    }
}
