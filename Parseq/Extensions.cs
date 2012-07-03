﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parseq
{
    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable,Action<T> action)
        {
            foreach (var i in enumerable)
                action(i);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, Int32> action)
        {
            var index = 0;
            foreach (var i in enumerable)
                action(i, index++);
        }

        public static TResult Match<T, TResult>(this IEnumerable<T> enumerable,
            Func<TResult> nil,
            Func<T, IEnumerable<T>, TResult> cons)
        {
            var enumerator = enumerable.GetEnumerator();
            return !(enumerator.MoveNext())
                ? nil()
                : cons(enumerator.Current, enumerator.ToEnumerable());
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, T second)
        {
            foreach (var t in first)
                yield return t;

            yield return second;
        }

        public static IEnumerable<T> Concat<T>(this T first,IEnumerable<T> second)
        {
            yield return first;

            foreach (var t in second)
                yield return t;
        }

        public static IEnumerable<T> ToEnumerable<T>(this T value)
        {
            yield return value;
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

    }
}