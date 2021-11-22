using System;
using System.Collections.Generic;

//оптимален път - граф със стойности за всеки път
//работи в дървовидна стуктура - пробва всеки възмоен път и намира най-добрия
namespace Upr7
{
    class Program
    {
        //costs - стълбовидна матрица за стойностите на пътищата
        //from - на кой възел се намираме
        //list visited - всички посетени вече възли
        public static int Route
            (
            int[,] costs,
            int from,
            List<int> visited,
            out string route
            )
        {
            route = "";

            //по правило щом ще правим рекурсия - 1во трябва да помислим кога ще сложим край на рекурсията
            //рекусрсията ще приключи, когато са посетвни всички възли 
            if (visited.Count == costs.GetLength(0) - 1)
            {
                route = from.ToString();
                return costs[from, 0];
            }
            //при обхожданео на възлите не тярбва да се въщаш назад
            //следователно трябва да добавим сегашнния възел като минат
            visited.Add(from);

            var min = int.MaxValue;
            for (int to = 0; to < costs.GetLength(0); to++)
                if (!visited.Contains(to) && costs[from, to] != -1)
                {
                    //стойността на целия път 
                    var subSolutin = Route(costs, to, visited, out string subRoute);

                    if (subSolutin == -1)
                        continue;

                    //стойността на началната до крайната и стойността на целия път
                    var value = costs[from, to] + subSolutin;

                    if (value < min)
                    {
                        route = from + " " + subRoute;
                        min = value;
                    }

                }
            //когато обходим един възможен път, при слеващия път трябва да се взима предвид добавения по-рано възел
            //затова го махаме след обхождането 
            visited.Remove(from);

            return min == int.MaxValue
                ? -1
                : min;

        }

        static void Main(string[] args)
        {
            var costs = new int[,]
            {
                {-1, 1, 3, -1, 6},
                {1, -1, 2, -1, 8},
                {3, 2, -1, 8, 2},
                {-1, 1, 8, -1, 1},
                {3, 8, 2, 1, -1},
            };

            var routeValue = Route(costs, 0, new List<int>(), out string route);
            Console.WriteLine($"Route: {route} = {routeValue}");
            Console.ReadLine();
        }
    }
}
