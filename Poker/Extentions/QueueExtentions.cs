using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Extentions
{
    public static class QueueExtensions
    {
        public static void Shuffle<T>(this Queue<T> source)
        {
            var tempArr = source.ToArray();

            var n = tempArr.Length;
            for (var i = 0; i < n; i++)
            {
                var r = i + RandomProvider.Instance.Next(0, n - i);
                var temp = tempArr[i];
                tempArr[i] = tempArr[r];
                tempArr[r] = temp;
            }

            source.Clear();
            foreach (var element in tempArr)
            {
                source.Enqueue(element);
            }
        }
    }
}
