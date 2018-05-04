using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities
{
    public static class Extensions
    {
        private static readonly double mWeightThreshold = 0.7;

        private static readonly int mNumChars = 4;
        /// <summary>
        /// Change every empty string member of the specified object to Null 
        /// </summary>
        /// <typeparam name="T">object</typeparam>
        /// <param name="entity">The object to change</param>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static void EmptyStringtoNull<T>(this T entity) where T : class
        {
            Type type = entity.GetType();
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic);
            for (int j = 0; j < fieldInfos.Length; j++)
            {
                FieldInfo propertyInfo = fieldInfos[j];
                if (propertyInfo.FieldType.Name == "String")
                {
                    object obj = propertyInfo.GetValue(entity);
                    if ((string)obj == "" || string.IsNullOrWhiteSpace((string)obj))
                        propertyInfo.SetValue(entity, null);

                }
            }
        }
        /// <summary>
        /// Indicate whether the indicate stirng is null or empty
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string entity)
        {
            return string.IsNullOrEmpty(entity);
        }

        public static bool IsAny<T>(this IEnumerable<T> entity)
        {
            return entity != null && entity.Any();
        }

        public static double IsSimilar(this string aString1, string aString2)
        {
            aString1 = aString1.ToLower();
            aString2 = aString2.ToLower();

            int lLen1 = aString1.Length;
            int lLen2 = aString2.Length;
            if (lLen1 == 0)
                return lLen2 == 0 ? 1.0 : 0.0;

            int lSearchRange = Math.Max(0, Math.Max(lLen1, lLen2) / 2 - 1);

            // default initialized to false
            bool[] lMatched1 = new bool[lLen1];
            bool[] lMatched2 = new bool[lLen2];

            int lNumCommon = 0;
            for (int i = 0; i < lLen1; ++i)
            {
                int lStart = Math.Max(0, i - lSearchRange);
                int lEnd = Math.Min(i + lSearchRange + 1, lLen2);
                for (int j = lStart; j < lEnd; ++j)
                {
                    if (lMatched2[j]) continue;
                    if (aString1[i] != aString2[j])
                        continue;
                    lMatched1[i] = true;
                    lMatched2[j] = true;
                    ++lNumCommon;
                    break;
                }
            }
            if (lNumCommon == 0) return 0.0;

            int lNumHalfTransposed = 0;
            int k = 0;
            for (int i = 0; i < lLen1; ++i)
            {
                if (!lMatched1[i]) continue;
                while (!lMatched2[k]) ++k;
                if (aString1[i] != aString2[k])
                    ++lNumHalfTransposed;
                ++k;
            }
            // System.Diagnostics.Debug.WriteLine("numHalfTransposed=" + numHalfTransposed);
            int lNumTransposed = lNumHalfTransposed / 2;

            // System.Diagnostics.Debug.WriteLine("numCommon=" + numCommon + " numTransposed=" + numTransposed);
            double lNumCommonD = lNumCommon;
            double lWeight = (lNumCommonD / lLen1
                             + lNumCommonD / lLen2
                             + (lNumCommon - lNumTransposed) / lNumCommonD) / 3.0;

            if (lWeight <= mWeightThreshold) return lWeight;
            int lMax = Math.Min(mNumChars, Math.Min(aString1.Length, aString2.Length));
            int lPos = 0;
            while (lPos < lMax && aString1[lPos] == aString2[lPos])
                ++lPos;
            if (lPos == 0) return lWeight;
            return lWeight + 0.1 * lPos * (1.0 - lWeight);
        }
    }
}
