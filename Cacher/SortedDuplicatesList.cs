﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cacher
{
    public class SortedDuplicatesList<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable
    {
        private List<KeyValuePair<TKey, TValue>> list;
        private IComparer<KeyValuePair<TKey, TValue>> comparer;
        public SortedDuplicatesList()
        {
            list = new List<KeyValuePair<TKey, TValue>>();
            comparer = new KvpKeyComparer<TKey, TValue>();
        }

        /// <summary>
        /// Add a new item with the specified key and value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            var kvp = new KeyValuePair<TKey, TValue>(key, value);
            var index = list.BinarySearch(kvp, comparer);
            //get the index of the item with this exact key, or the index of the closest item
            if (index < 0) index = ~index;
            list.Insert(index, kvp);
        }

        public List<KeyValuePair<TKey, TValue>> Items
        {
            get
            {
                return list;
            }
        }

        /// <summary>
        /// Remove the item with the specified key and value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Remove(TKey key, TValue value)
        {
            var kvp = new KeyValuePair<TKey, TValue>(key, value);
            var idx = list.BinarySearch(kvp, comparer);
            if (idx > -1)
            {
                list.RemoveAt(idx);
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }

    public class KvpKeyComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
     where TKey : IComparable
    {
        public int Compare(KeyValuePair<TKey, TValue> x,
                           KeyValuePair<TKey, TValue> y)
        {
            if (x.Key == null)
            {
                if (y.Key == null)
                    return 0;
                return -1;
            }

            if (y.Key == null)
                return 1;

            return x.Key.CompareTo(y.Key);
        }
    }

}
