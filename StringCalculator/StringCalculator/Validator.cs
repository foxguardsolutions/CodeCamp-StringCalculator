using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Validator
    {
        private const string NEGATIVESNOTALLOWED = "Negatives not allowed: ";

        public IEnumerable<int> Validate(IEnumerable<int> numbers)
        {
            CheckForNegatives(numbers);
            return RemoveTooLarge(numbers);
        }

        private IEnumerable<int> RemoveTooLarge(IEnumerable<int> numbers)
        {
            return numbers.Where(number => number < 1001);
        }

        private void CheckForNegatives(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0);
            if (negativeNumbers.Any())
            {
                throw new ArgumentException(NEGATIVESNOTALLOWED + string.Join(", ", negativeNumbers));
            }
        }
    }
}
