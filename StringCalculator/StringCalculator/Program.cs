namespace StringCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var addingMachine = new Calculator();
            foreach (string input in args)
            {
                addingMachine.Add(input);
            }
        }
    }
}
