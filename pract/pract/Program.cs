using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract
{
    class Program
    {
        static string SumDistribution(string typeDistr, float Sum, string StrSums)
        {
            //Преобразуем строку сумм в список
            List<float> ListOfSums = new List<float>(Array.ConvertAll(StrSums.Split(';'), float.Parse));
            string Result = "";
            switch (typeDistr) {
                case "ПРОП":
                    //Сложим суммы
                    float totalSum = ListOfSums.Sum();
                    //Остаток от округления
                    float remainder = 0;
                    for (int i=0;i<ListOfSums.Count();i++)
                    {
                        //Распределяем суммы пропорционально
                        float procent = ListOfSums[i] * 100 / totalSum;
                        float res = Sum * procent / 100;
                        //Округляем полученную сумму
                        float roundRes = (float)Math.Round(res * 100f) / 100f;
                        // Console.WriteLine(res % 0.01 + " "+(res - res % 0.001) + " "+ (roundRes - (res - res % 0.01)) + " "+ostatok);
                        // Console.WriteLine(roundRes+" " +res+" "+res % 0.01 + " " +(roundRes - res) + " " + ostatok);
                        //Console.WriteLine("-" + roundRes+"-"+res);
                        /*Console.WriteLine("-"+res % 1);
                        Console.WriteLine(res % 0.01);
                        Console.WriteLine("*"+(roundRes-res));*/
                        //Если это последняя сумма в списке - прибавляем к сумме остаток, если же нет - высчитываем остаток
                        if (i == ListOfSums.Count() - 1) roundRes += remainder;
                        //else if (res % 0.01 >= 0.005) ostatok += roundRes - res;
                        else if (roundRes-res > 0) remainder += 0.01f;
                        //else ostatok += res % 0.01f;
                       // Console.WriteLine(ostatok);
                        //Прибавляем строке result полученную округленную сумму
                        Result += roundRes + ";";                        
                    }
                    //Удаляем последний символ ";" в строке
                    Result = Result.Remove(Result.Length - 1);
                    break;
                case "ПЕРВ":
                    //Распределяем сумму по порядку
                    for (int i = 0; i < ListOfSums.Count(); i++)
                    {                        
                        if (Sum==0) Result += "0;";
                        else if (Sum-ListOfSums[i]>=0)
                        {
                            Result += ListOfSums[i] + ";";
                            Sum -= ListOfSums[i];
                        }
                        else
                        {
                            Result += Sum + ";";
                            Sum = 0;
                        }
                    }
                    //Удаляем последний символ ";" в строке
                    Result = Result.Remove(Result.Length - 1);
                    break;
                case "ПОСЛ":
                    //Распределяем сумму в обратном порядке
                    float[] newListOfSums = new float[ListOfSums.Count()];
                    for (int i = ListOfSums.Count()-1; i >-1 ; i--)
                    {
                        if (Sum == 0) newListOfSums[i]=0;
                        else if (Sum - ListOfSums[i] >= 0)
                        {
                            newListOfSums[i] = ListOfSums[i];
                            Sum -= ListOfSums[i];
                        }
                        else
                        {
                            newListOfSums[i] = Sum;
                            Sum = 0;
                        }
                    }
                    Result = string.Join(";", newListOfSums);
                    break;
            }
            return Result;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(SumDistribution("ПРОП", 10000, "1000;2000;3000;5000;8000;5000"));
            Console.ReadLine();
        }
    }
}
