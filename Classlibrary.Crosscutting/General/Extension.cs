#region Copyright TechNeutron © 2019

//
// NAME:			Extension.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/21/2019
// PURPOSE:			Common extensions
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

    /// <summary>
    ///     Represents the regex patterns class.
    /// </summary>
    public class RegexPattern
    {
        public const string ALPHA = "[^a-zA-Z]";
        public const string ALPHA_NUMERIC = "[^a-zA-Z0-9]";
        public const string ALPHA_NUMERIC_SPACE = @"[^a-zA-Z0-9\s]";
        public const string CREDIT_CARD_AMERICAN_EXPRESS = @"^(?:(?:[3][4|7])(?:\d{13}))$";
        public const string CREDIT_CARD_CARTE_BLANCHE = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";
        public const string CREDIT_CARD_DINERS_CLUB = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";
        public const string CREDIT_CARD_DISCOVER = @"^(?:(?:6011)(?:\d{12}))$";
        public const string CREDIT_CARD_EN_ROUTE = @"^(?:(?:[2](?:014|149))(?:\d{11}))$";
        public const string CREDIT_CARD_JCB = @"^(?:(?:(?:2131|1800)(?:\d{11}))$|^(?:(?:3)(?:\d{15})))$";
        public const string CREDIT_CARD_MASTER_CARD = @"^(?:(?:[5][1-5])(?:\d{14}))$";
        public const string CREDIT_CARD_STRIP_NON_NUMERIC = @"(\-|\s|\D)*";
        public const string CREDIT_CARD_VISA = @"^(?:(?:[4])(?:\d{12}|\d{15}))$";
        public const string EMAIL = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
        //public const string EMAIL = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";

        public const string EMBEDDED_CLASS_NAME_MATCH = "(?<=^_).*?(?=_)";
        public const string EMBEDDED_CLASS_NAME_REPLACE = "^_.*?_";
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_MATCH = "(?<=^UNDERSCORE).*?(?=UNDERSCORE)";
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_REPLACE = "^UNDERSCORE.*?UNDERSCORE";
        public const string GUID = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";

        public const string IP_ADDRESS =
            @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        public const string LOWER_CASE = @"^[a-z]+$";
        public const string NUMERIC = "[^0-9]";
        public const string SOCIAL_SECURITY = @"^\d{3}[-]?\d{2}[-]?\d{4}$";
        public const string SQL_EQUAL = @"\=";
        public const string SQL_GREATER = @"\>";
        public const string SQL_GREATER_OR_EQUAL = @"\>.*\=";
        public const string SQL_IS = @"\x20is\x20";
        public const string SQL_IS_NOT = @"\x20is\x20not\x20";
        public const string SQL_LESS = @"\<";
        public const string SQL_LESS_OR_EQUAL = @"\<.*\=";
        public const string SQL_LIKE = @"\x20like\x20";
        public const string SQL_NOT_EQUAL = @"\<.*\>";
        public const string SQL_NOT_LIKE = @"\x20not\x20like\x20";

        ///// <summary>
        /////     Password format validation regex
        /////     ^           # Start of string
        /////     (?![a-z]*$) # Assert that it doesn't just contain lowercase alphas
        /////     (?![A-Z]*$) # Assert that it doesn't just contain uppercase alphas
        /////     (?!\d*$)    # Assert that it doesn't just contain digits
        /////     (?!\p{P
        /////     }*$) # Assert that it doesn't just contain punctuation
        /////     (?![^a-zA-Z\d\p{P}]*$) # or the inverse of the above
        /////     .{8,}       # Match at least eight characters
        /////     $           # End of string
        ///// </summary>
        //public const string STRONG_PASSWORD = @"^(?![a-z]*$)(?![A-Z]*$)(?!\d*$)(?!\p{P}*$)(?![^a-zA-Z\d\p{P}]*$).{8,}$";
        public const string STRONG_PASSWORD = @"(?=^.{8,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*";

        public const string UPPER_CASE = @"^[A-Z]+$";

        public const string URL = @"^^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";

        public const string US_CURRENCY = @"^\$(([1-9]\d*|([1-9]\d{0,2}(\,\d{3})*))(\.\d{1,2})?|(\.\d{1,2}))$|^\$[0](.00)?$";

        public const string US_TELEPHONE = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
        public const string US_ZIPCODE = @"^\d{5}$";
        public const string US_ZIPCODE_PLUS_FOUR = @"^\d{5}((-|\s)?\d{4})$";
        public const string US_ZIPCODE_PLUS_FOUR_OPTIONAL = @"^\d{5}((-|\s)?\d{4})?$";
    }
}