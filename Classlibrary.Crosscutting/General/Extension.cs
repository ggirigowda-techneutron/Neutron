#region Copyright TechNeutron © 2019

//
// NAME:			Extension.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			
//

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Classlibrary.Crosscutting.General
{
    /// <summary>
    ///     Represents the <see cref="EnumerableExtension" /> class.
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        ///     To hashset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }

        /// <summary>
        ///     Returns all distinct elements of the given source, where "distinctness"
        ///     is determined via a projection and the default eqaulity comparer for the projected type.
        /// </summary>
        /// <remarks>
        ///     This operator uses deferred execution and streams the results, although
        ///     a set of already-seen keys is retained. If a key is seen multiple times,
        ///     only the first element with that key is returned.
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="keySelector">Projection for determining "distinctness"</param>
        /// <returns>
        ///     A sequence consisting of distinct elements from the source sequence,
        ///     comparing them by the specified key projection.
        /// </returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector, null);
        }

        /// <summary>
        ///     Returns all distinct elements of the given source, where "distinctness"
        ///     is determined via a projection and the specified comparer for the projected type.
        /// </summary>
        /// <remarks>
        ///     This operator uses deferred execution and streams the results, although
        ///     a set of already-seen keys is retained. If a key is seen multiple times,
        ///     only the first element with that key is returned.
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="keySelector">Projection for determining "distinctness"</param>
        /// <param name="comparer">
        ///     The equality comparer to use to determine whether or not keys are equal.
        ///     If null, the default equality comparer for <c>TSource</c> is used.
        /// </param>
        /// <returns>
        ///     A sequence consisting of distinct elements from the source sequence,
        ///     comparing them by the specified key projection.
        /// </returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (keySelector == null) throw new ArgumentNullException("keySelector");
            return DistinctByImpl(source, keySelector, comparer);
        }

        private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
#if !NO_HASHSET
            var knownKeys = new HashSet<TKey>(comparer);
            foreach (var element in source)
                if (knownKeys.Add(keySelector(element)))
                    yield return element;
#else
            //
            // On platforms where LINQ is available but no HashSet<T>
            // (like on Silverlight), implement this operator using 
            // existing LINQ operators. Using GroupBy is slightly less
            // efficient since it has do all the grouping work before
            // it can start to yield any one element from the source.
            //

            return source.GroupBy(keySelector, comparer).Select(g => g.First());
#endif
        }

        /// <summary>
        ///     Convert dynamic IEnumerable to datatable
        /// </summary>
        /// <param name="list"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IEnumerable<dynamic> list, string tableName)
        {
            // Bind data by converting IEnumerable<dynamic> to datatable
            var dataTable = new DataTable(tableName);
            var collection = list.ToList();

            var properties = ((IDictionary<string, object>) collection.First()).ToDictionary(x => x.Key, x => x.Value);
            foreach (var property in properties)
                dataTable.Columns.Add(property.Key);

            foreach (var item in collection.ToList())
            {
                var dataRow = dataTable.NewRow();
                var o = (IDictionary<string, object>) item;
                foreach (var p in properties)
                    dataRow[p.Key] = o[p.Key];
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}