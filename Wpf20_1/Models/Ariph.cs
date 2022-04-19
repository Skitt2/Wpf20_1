using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wpf20_1.Model
{
    public class Ariph
    {
        private string result;

        public Ariph(string firstOper, string secondOper, string operation)
        {
            FirstOper = firstOper;
            SecondOper = secondOper;
            Operation = operation;
            result = string.Empty;
        }

        public Ariph(string firstOperand, string operation)
        {
            FirstOper = firstOperand;
            SecondOper = string.Empty;
            Operation = operation;
            result = string.Empty;
        }

        public Ariph()
        {
            FirstOper = string.Empty;
            SecondOper = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }
   

        public string FirstOper { get; set; }
        public string SecondOper { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }

        public void CalculateResult()
        {
            switch (Operation)
            {
                case ("+"):
                    result = (Convert.ToDouble(FirstOper) + Convert.ToDouble(SecondOper)).ToString();
                    break;

                case ("-"):
                    result = (Convert.ToDouble(FirstOper) - Convert.ToDouble(SecondOper)).ToString();
                    break;

                case ("*"):
                    result = (Convert.ToDouble(FirstOper) * Convert.ToDouble(SecondOper)).ToString();
                    break;

                case ("/"):
                    result = (Convert.ToDouble(FirstOper) / Convert.ToDouble(SecondOper)).ToString();
                    break;
            }
        }
    }
}
