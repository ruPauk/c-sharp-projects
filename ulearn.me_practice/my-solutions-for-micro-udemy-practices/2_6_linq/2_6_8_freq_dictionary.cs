using System;
using System.Text.RegularExpressions;

/*
Частотным словарём текста называют список пар: для каждого слова количество раз, которое оно встретилось в тексте.
В этой задаче нужно по тексту вернуть не весь частотный словарь, а только count слов, встретившихся в тексте больше всего раз.
Среди слов, встречающихся одинаково часто, при выводе отдавать предпочтение лексикографически меньшим словам.
Например, если все слова в тексте встретились только по одному разу, то вывести нужно count лексикографически первых слов.
При этом слова сравнивайте без учёта регистра, а возвращайте в нижнем регистре.

Напомним сигнатуры некоторых LINQ-методов, которые могут понадобиться в этом упражнении:
IEnumerable<IGrouping<K, T>>    GroupBy(this IEnumerable<T> items, Func<T, K> keySelector)
IOrderedEnumerable<T>           OrderBy(this IEnumerable<T> items, Func<T, K> keySelector)
IOrderedEnumerable<T> OrderByDescending(this IEnumerable<T> items, Func<T, K> keySelector)
IOrderedEnumerable<T>            ThenBy(this IOrderedEnumerable<T> items, Func<T, K> keySelector)
IOrderedEnumerable<T>  ThenByDescending(this IOrderedEnumerable<T> items, Func<T, K> keySelector)
IEnumerable<T>                     Take(this IEnumerable<T> items, int count)
* */

//TASK - LINQ practicing - Groups
//https://ulearn.me/course/basicprogramming2/Sozdanie_chastotnogo_slovarya_0535734d-d258-44c6-99f3-f96258bcca6f

namespace Tests
{
    class Program
    {
        public static void Main()
        {
            var arr = GetMostFrequentWords("abc fv dabc abc af ef af gt aaaaaaa a a a ge af", 5);

            foreach (var tuple in arr)
                Console.WriteLine(tuple);

        }

        public static (string, int)[] GetMostFrequentWords(string text, int count)
        {
            return Regex.Split(text, @"\W+")
                .Where(word => word != "")
                .GroupBy(w => w.ToLower())
                .Select(x => (x.Key, x.Count()))
                .OrderByDescending(x => x.Item2)
                .ThenBy(x => x)
                .Take(count)
                .ToArray();
        }
    }
}