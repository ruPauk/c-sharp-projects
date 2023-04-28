using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Diagnostics;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            return null;
        }

        public static int FindFirstIndexByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return index;
            return -1;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            // тут стоит использовать написанный ранее класс LeftBorderTask
            var firstElement = FindFirstIndexByPrefix(phrases, prefix);
            TestContext.WriteLine("Prefix = " + prefix + " firstElementIndex = " + firstElement);
            if (firstElement > -1)
            {
                var result = new List<string>();
                for (int i = firstElement; i < firstElement + count; i++)
                {
                    if (i <= phrases.Count - 1 && phrases[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(phrases[i]);
                    }
                    else
                        break;
                }
                return result.ToArray();
            }
            else
                return new string[0];
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            // тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
            var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, leftIndex, phrases.Count);
            var result = (rightIndex + 2) - (leftIndex + 2) - 1;
            Debug.WriteLine("Prefix - " + prefix + "LeftIndex - " + leftIndex + " RightIndex - " + rightIndex);
            TestContext.WriteLine("Prefix - " + prefix + " LeftIndex - " + leftIndex + " RightIndex - " + rightIndex);
            TestContext.WriteLine("Result - " + result);
            return result;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [TestCase("a", 2, new string[1] { "aa" }, new string[1] { "aa" })]
        [TestCase("a", 3, new string[5] { "aa", "ab", "ac", "ad", "bd" }, new string[3] { "aa", "ab", "ac" })]
        [TestCase("z", 2, new string[3] { "aa", "ab", "ac" }, null)]
        public void TopByPrefix_IsEmpty_WhenNoPhrases(string prefix, int count, string[] phrases, string[] result)
        {
            var res = Autocomplete.AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            CollectionAssert.AreEqual(res, result);
        }

        // ...

        [TestCase("ab", 2)]
        [TestCase("a", 4)]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix(string value, int expectedCount)
        {
            List<string> test = new List<string> {
                "abc", "abd", "dcp", "gft", "ayd", "dip", "pot", "aaa", "zz", "gsst" };
            test.Sort();
            Assert.AreEqual(expectedCount, AutocompleteTask.GetCountByPrefix(test, value));
        }
    }
}
