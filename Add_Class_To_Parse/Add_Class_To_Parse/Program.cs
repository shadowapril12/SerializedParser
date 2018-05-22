using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Add_Class_To_Parse
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                //Создание экземпляра класса Parser
                ParserExt obj = new ParserExt();
                obj.GetExpression();
                obj.ShowResult();              
            }
        }
    }
}
