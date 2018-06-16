using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Add_Class_To_Parse
{
    /// <summary>
    /// Parser - класс, содержащий методы, поля и свойства, предназначенные для
    /// парсирования строки, передаваемой через консоль в программу
    /// </summary>
    /// 
    class Parser
    {
        protected int index;
        public string res;
        //Свойство s - служит для возврата и передачи значения приватному полю s, 
        public string S { get; set; }
        /// <summary>
        /// CalcExpression - основной метод, обнаруживает позиции скобок, для сохранения
        /// приоритетности выполнения математических операций.
        /// </summary>
        /// <param name="s">Выражение передаваемое в виде строки</param>
        /// <returns>Возвращает результат вычисления</returns>
        public string CalcExpression(string s)
        {
            //Цикл, выполняющий до тех пор, пока в строке присутствую открывающиеся скобки
            while (s.Contains("("))
            {
                ///lvl - счетчик, увеличивающийся при обнаружении открывающейся скобки,
                ///и уменьшающийся при обнаружении закрывающейся
                int lvl = 1;

                //idx - индекс открывающейся скобки
                int idx = s.IndexOf("(");
                int i;
                for (i = idx + 1; lvl > 0 && i < s.Length; i++)
                {
                    if (s[i] == ')')
                    {
                        lvl--;
                    }
                    if (s[i] == '(')
                    {
                        lvl++;
                    }
                }

                ///localRes - строка, содержащая внутри внешних скобок, передаваемая в функцию повторно,
                ///для обнаружения внутренних скобок
                string localRes = CalcExpression(s.Substring(idx + 1, i - idx - 2));

                s = s.Substring(0, idx) + localRes +
                    (i < s.Length ? s.Substring(i, s.Length - i) : "");
            }
            res = s;
            return CalcPlusMinus() + "";
        }

        /// <summary>
        /// Приватный метод CalcFactorial - служит для нахождения факториала, передаваемого в функцию числа
        /// </summary>
        /// <param name="n">Передаваемое число</param>
        /// <returns>Возаращает значение факториала</returns>
        protected int CalcFactorial(int n)
        {
            //переменная, а которую будет передаваться значение факториала
            int factorial;

            if (n == 1)
            {
                return n;
            }
            else
            {
                factorial = CalcFactorial(n - 1) * n;
            }

            return factorial;
        }

        /// <summary>
        /// Приватный метод MulDivFac - служит для выполнения операция умножения с делением, а также для
        /// нахождения факториала
        /// </summary>
        /// <param name="s">Строка передаваемая в функцию</param>
        /// <param name="i">Индекс символа</param>
        /// <returns>Возвращает результат умножения/деления либо фактрориала</returns>
        protected virtual int MulDivFac()
        {
            //Парсированная строка, до первого знака математических операций
            int num = Num();
            //Проверка наличия знака восклицания, в конце числа
            if(index < res.Length && res[index] == '!')
            {
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

        /// <summary>
        /// Приватный метод CalcPlusMinus - служит для выполнения
        /// математических операция сложения и вычитания
        /// </summary>
        /// <param name="s">Передаваемая в функцию строка</param>
        /// <returns>Возвращает сумму или разность, в зависимости от знака</returns>
        protected int CalcPlusMinus()
        {
            //Индекс первого символа строки
            index = 0;
            int num = MulDivFac();

            while (index < res.Length)
            {
                if (res[index] == '+')
                {
                    index++;
                    int b = MulDivFac();
                    num += b;
                }
                else if (res[index] == '-')
                {
                    index++;
                    int b = MulDivFac();
                    num -= b;
                }
                else
                {
                    Console.WriteLine("Error");
                    return 0;
                }
            }

            return num;
        }
        /// <summary>
        /// Метод Num - служит для считывания числа из строки, останавливается
        /// на первом не числовом символе
        /// </summary>
        /// <param name="s">Передаваемая в функцию строка</param>
        /// <param name="index">Индекс символа строки</param>
        /// <returns>Возвращает парсированную строку</returns>
        protected virtual int Num()
        {
            string buff = "0";

            for (; index < res.Length && char.IsDigit(res[index]); index++)
            {
                buff += res[index];
            }
               
         return Convert.ToInt32(buff);
        }
        /// <summary>
        /// Приватный метод FindFac - служит для нахождения факториала
        /// </summary>
        /// <returns>Возвращает значение факториала</returns>
        protected int FindFac()
        {
            int facIdx = index - 1;
            int fac = CalcFactorial(Convert.ToInt32(res[facIdx] + ""));
            index++;

            return fac;
        }
        /// <summary>
        /// Публичный метод GetString служит для считывания строки
        /// </summary>
        public virtual void GetString()
        {
            S = Console.ReadLine();
        }
        /// <summary>
        /// Публичный метод ShowResult служит для вывода результата и вычисления выражения переданного в строке
        /// </summary>
        public virtual void ShowResult()
        {
            Console.WriteLine("Результат выражения {0}", CalcExpression(S) + "\n");
        }

    }
}
