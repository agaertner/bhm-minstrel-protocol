using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nekres.Musician
{
    internal static class DictionaryExtension
    {
        public static async Task<Dictionary<TKey, TResult>> ToResults<TKey,TResult>(this IEnumerable<KeyValuePair<TKey, Task<TResult>>> input)
        {
            var pairs = await Task.WhenAll
            (
                input.Select
                ( 
                    async pair => new {pair.Key, Value = await pair.Value }
                )
            );
            return pairs.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
