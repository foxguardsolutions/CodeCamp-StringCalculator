using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator {
    public class Calculator {
        public int Add(string numbers) {
            var addends = ParseStringToInts(numbers);
            return addends.Sum();
        }

        private IEnumerable<int> ParseStringToInts(string numbers) {
            var splitNumbers = numbers.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return ConvertMembersToInts(splitNumbers);
        }

        private IEnumerable<int> ConvertMembersToInts(string[] splitNumbers) {
            return from number in splitNumbers
                   select int.Parse(number);
        }
    }
}
