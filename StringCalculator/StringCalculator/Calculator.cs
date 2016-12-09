using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator {
    public class Calculator {
        public int Add(string numbers) {
            if (numbers.Length == 0) {
                return 0;
            }
            return int.Parse(numbers);
        }
    }
}
