using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


namespace Add_Class_To_Parse
{
    
    class ParserExt : Parser
    {
        //begin - время начала вычисления выражения
        private DateTime begin;

        //end - время окончания вычисления выражения
        private DateTime end;

        
        //duration - продолжительночть вычисления выражения
        public TimeSpan duration;

        
        //Счетчик количества операций
        public int operCount;

        //Переменная, в которую возвращается результат метода CheckString
        private bool resOfCheck;


        /// <summary>
        /// Данный метод переопределен для установки начального времени вычисления выражения
        /// </summary>
        public void GetExpression()
        {
            S = Console.ReadLine();

            begin = DateTime.Now;
            //Возвращение результата метода
            resOfCheck = CheckString();
        }

        /// <summary>
        /// Метод CheckString проверяет строку на корректность ввода. Если были допущены ошибки,
        /// то строка выводится в консоли заново, с подсвеченными операндами или операторами. Также выводится
        /// время, возникшего исключения и номер колонки.
        /// </summary>
        /// <returns>Возвращает значение логического типа true или false. Если ошибок не было, то true, если же
        /// наоборот, то возвращается false</returns>
        private bool CheckString()
        {
            //Переменная i, счетчик в цикле
            int i = 0;

            //Переменная errorCount - счетчик количества ошибок в считываемой строке
            int errorCount = 0;

            //Коллекция errorColumns содержит номера колонок в которых были допущены ошибки
            ArrayList errorColumns = new ArrayList();

            //Коллекция errorTime содержит время допущенных ошибок
            ArrayList errorTime = new ArrayList();

            Console.WriteLine("Проверка выражения:");

            try
            {
                //Проход циклом по всему выражению
                for (; i < S.Length; i++)
                {
                    //Если символ число или скобка, то отображаем его в консоли
                    if (char.IsDigit(S[i]) || S[i] == '(' || S[i] == ')')
                    {
                        Console.Write(S[i]);
                    }
                    //Если символ знак +, -, *, / или ! то выводим его в консоли
                    else if (S[i] == '+' || S[i] == '-' || S[i] == '*' || S[i] == '/' || S[i] == '!')
                    {
                        Console.Write(S[i]);

                        //Переменная счетчика цикла по знакам математических операций
                        int j;
                        ///Тут же запускаем цикл, со следующего символа, если он один из перечисленных,
                        ///то выводим его красным цветом
                        for (j = i + 1; j < S.Length && !(char.IsDigit(S[j])); j++)
                        {
                            if (S[j] == '+' || S[j] == '-' || S[j] == '*' || S[j] == '/' || S[j] == '!')
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(S[j]);
                                Console.ResetColor();

                                //Увеличиваем счетчик с ошибками
                                errorCount++;

                                //Добавляем в коллекцию с номерами колонок индекс символа, увелечинный на единицу
                                errorColumns.Add(j+1);

                                //Добавляем в коллекцию время найденной ошибки
                                errorTime.Add(DateTime.Now);

                                //Увличиваем индекс i на единицу
                                i++;
                            }
                        }

                    }
                    else
                    {
                        ///Если символ не число и не скобка то выводим его красным цветом. Увеличиваем счетчик
                        ///ошибок на единицу. Добавляем в коллекции номер колонки и время ошибки
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(S[i]);
                        Console.ResetColor();
                        errorCount++;
                        errorColumns.Add(i);
                        errorTime.Add(DateTime.Now);
                    }
                }
                ///Если количество ошибок больше нуля - создается исключение. В него добаляется определенная
                ///информация, с причиной ошибки.
                if (errorCount > 0)
                {
                    Exception exc = new Exception();
                    exc.Data.Add("Причина: ", "Некорретно введеные значения, повторяющиеся математические операторы. Попробуйте ввести выражение снова");
                    throw exc;
                }
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //В цикле выводятся столбцы с ошибками и время возникновения ошибок
                for(int k = 0; k < errorColumns.Count; k++)
                {
                    Console.WriteLine($"\nОшибка в столбце {errorColumns[k]}. Время возникновения ошибки {errorTime[k]}");
                }

                Console.ResetColor();

                foreach (DictionaryEntry d in exc.Data)
                {
                    Console.WriteLine($"{d.Key} {d.Value}\n");
                }
            }
            ///Если количество ошибок больше нуля - возвращается 'false' еси нет - 'true'
            return errorCount > 0 ? false : true;

        }


        /// <summary>
        /// Данный метод переопределен для установки конечного времения вычисления выражения,
        /// а также расчета продолжительности выполнения программы
        /// </summary>
        public override void ShowResult()
        {
            //Если метод CheckString возвращает "true", то введеное выражение вычисляется
            if (resOfCheck)
            {
                end = DateTime.Now;
                duration = end - begin;

                string res = CalcExpression(S);

                Console.WriteLine("\nРезультат выражения {0}", res + "\n");

                Console.WriteLine("Продолжительность выполнения программы: {0}," +
                    " количество операций {1}\n", duration.TotalMilliseconds + "\n", operCount);

                Serializer s = new Serializer();

                //Передача значения переменной operCount(количество операций) - переменной класса Serializer для
                //дальнейшей сериализации
                s.countOfOperations = operCount;

                //Передача результата математического выражения переменной класса Serializer для
                //дальнейшей сериализации
                s.result = res;

                //Передача значения duration переменной класса Serializer для дальнейшей сериализации
                s.dur = duration.TotalMilliseconds;

                //Передача значения свойства 'S' - переменной класса Serializer для дальнейшей сериализации
                s.stringOfExpression = S;

                //Класс предназначенный для сериализации объектов
                DataContractJsonSerializer jSon = new DataContractJsonSerializer(typeof(Serializer));

                //Файл в котором будут сохраняться сериализованные объекты
                string path = "res.json";

                //Добавление сериализованных объектов в файл
                using (FileStream fs = new FileStream(path, FileMode.Append))
                {
                    jSon.WriteObject(fs, s);
                }
            }  
        }

        protected override int Num()
        {

            string buff = "0";

            for (; index < res.Length && char.IsDigit(res[index]); index++)
            {
                buff += res[index];
            }
            ///Проверка на количество операций в выражении, если символ строки не числовой,
            ///то это математический оператор
            if(index < res.Length)
            {
                if (!char.IsDigit(res[index]))
                {
                    operCount++;
                }
            }
            return Convert.ToInt32(buff);
        }
        /// <summary>
        /// Переопределен метод MulDivFac, для отслеживания знака факториала в выражении и увеличения счетчика операций
        /// </summary>
        /// <returns>Возвращает результат результат умножения, деления или вычисления факториала</returns>
        protected override int MulDivFac()
        {
            //Парсированная строка, до первого знака математических операций
            int num = Num();
            //Проверка наличия знака восклицания, в конце числа
            if (index < res.Length && res[index] == '!')
            {
                ///Увеличиваем счетчик количества операций
                operCount++;
                num = FindFac();
            }

            while (index < res.Length)
            {
                if (res[index] == '*')
                {

                    index++;
                    //Num - метод для считывания числа из строки
                    int b = Num();

                    if (index < res.Length && res[index] == '!')
                    {
                        operCount++;
                        num = FindFac();
                    }

                    num *= b;



                }
                else if (res[index] == '/')
                {
                    index++;
                    int b = Num();

                    if (index < res.Length && res[index] == '!')
                    {
                        operCount++;
                        num = FindFac();
                    }

                    num /= b;
                }
                else
                {
                    return num;
                }
            }

            return num;
        }
    }
}
