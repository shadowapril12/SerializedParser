using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;

namespace Add_Class_To_Parse
{
    class Program
    {
        static void Main(string[] args)
        {
            Collection col = new Collection();
            col.Storage = new List<Data>();

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
