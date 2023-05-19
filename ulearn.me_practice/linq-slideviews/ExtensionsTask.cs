using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

/*
Создавая методы, работающие с IEnumerable стоит придерживаться следующих рекомендаций:
	Если это возможно, не перечисляйте входной IEnumerable до конца. Потому что IEnumerable может теоретически быть бесконечным.
	Не перечисляйте больше элементов, чем нужно для работы IEnumerable. Возможно, при перечислении лишнего элемента случится ошибка или другой нежелательный побочный эффект.
	Не полагайтесь на то, что IEnumerable можно будет перечислить дважды. Этого никто не гарантирует. Кстати, некоторые IDE, автоматически находят нарушение этого пункта.
		Например, подобные предупреждения умеют показывать JetBrains Rider и Visual Studio с установленным Resharper.
	Лучше использовать foreach, а не явные вызовы MoveNext и Current. Это лучше читается и сложнее допустить ошибку.
 */

public static class ExtensionsTask
{
	/// <summary>
	/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
	/// Медиана списка из четного количества элементов — это среднее арифметическое 
    /// двух серединных элементов списка после сортировки.
	/// </summary>
	/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
	public static double Median(this IEnumerable<double> items)
	{
		if (items.Count() < 1)
			throw new InvalidOperationException();
		if (items.Count() % 2 == 0)
			//четное количество
			return items.Sum(item => item) / items.Count();
		else
		//нечетное количество
		{
			int i = (int)Math.Ceiling((double)items.Count() / (double)2) - 1;
            return items.OrderBy(item => item).ElementAt(i);
        }
            
	}

	/// <returns>
	/// Возвращает последовательность, состоящую из пар соседних элементов.
	/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
	/// </returns>
	public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
	{

		if (items.Any(x => items.Count() > 1))
		{
			foreach(var item in items)
            yield return 

			//return null;
		}	
		else
			return null;
	}

}