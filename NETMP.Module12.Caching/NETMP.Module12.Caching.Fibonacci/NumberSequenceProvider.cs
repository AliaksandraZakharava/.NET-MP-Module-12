using System;
using System.Collections.Generic;
using System.Linq;
using NETMP.Module12.Caching.Common;

namespace NETMP.Module12.Caching.Fibonacci
{
    public class NumberSequenceProvider
    {
        private readonly ICache<IEnumerable<int>> _cache;

        public NumberSequenceProvider(ICache<IEnumerable<int>> cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public IEnumerable<int> GetFibonacciNumbers(int numbersQuantity)
        {
            if (numbersQuantity < 1)
            {
                throw new ArgumentException("Invalid numbers quantity param.");
            }

            var result = _cache.TryGetFromCache(numbersQuantity.ToString());

            if (!result.Any())
            {
                result = CountFibonacciNumbers(numbersQuantity);

                _cache.AddToCache(numbersQuantity.ToString(), result);
            }

            return result;
        }

        #region Private methods

        private IEnumerable<int> CountFibonacciNumbers(int numbersQuantity)
        {
            int firstNumber = 1;
            int secondNumber = 1;

            for (int i = 1; i <= numbersQuantity; i++)
            {
                yield return secondNumber;

                var next = firstNumber + secondNumber;

                firstNumber = secondNumber;
                secondNumber = next;
            }
        }

        #endregion
    }
}
