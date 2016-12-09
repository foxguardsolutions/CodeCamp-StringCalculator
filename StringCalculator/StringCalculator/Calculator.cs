using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator {
    public class Calculator {
        private static char[] defaultDelimiters = { ',', '\n' };
        public const string NEGATIVESNOTALLOWED = "Negatives not allowed: -1";

        public int Add(string input) {
            var addends = ParseInputToInts(input);
            ValidateNumbers(addends);
            return addends.Sum();
        }

        private void ValidateNumbers(IEnumerable<int> addends) {
            if (HasNegativeNumbers(addends)) {
                throw new ArgumentException(NEGATIVESNOTALLOWED);
            }
        }

        private bool HasNegativeNumbers(IEnumerable<int> addends) {
            return addends.Any(a => a < 0);
        }

        private IEnumerable<int> ParseInputToInts(string input) {
            var splitNumbers = SplitInupt(input);
            return ConvertMembersToInts(splitNumbers);
        }

        private string[] SplitInupt(string input) {
            var delimiters = GetDelimiters(input);
            var numbers = GetNumbers(input);
            return numbers.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        private char[] GetDelimiters(string input) {
            if (SpecifiesDelimiters(input)) {
                return new char[] { input[2] };
            }
            return defaultDelimiters;
        }

        private string GetNumbers(string input) {
            if (SpecifiesDelimiters(input)) {
                return input.Substring(4);
            }
            return input;
        }

        private bool SpecifiesDelimiters(string input) {
            return input.StartsWith("//");
        }

        private IEnumerable<int> ConvertMembersToInts(string[] splitNumbers) {
            return from number in splitNumbers
                   select int.Parse(number);
        }
    }
}
