namespace UnitTestingClassLibrary
{
    public class SimpleCalculator
    {
        public int Operand1 { get; set; }
        public int Operand2 { get; set; }

        public SimpleCalculator(int num1, int num2)
        {
            Operand1 = num1;
            Operand2 = num2;
        }

        public int Add()
        {
            return Operand1 + Operand2;
        }

        public int Subtract()
        {
            return Operand1 - Operand2;
        }

        public int Multiply()
        {
            return Operand1 * Operand2;
        }

        public decimal Divide()
        {
            return (decimal) Operand1 / Operand2;
        }
    }
}