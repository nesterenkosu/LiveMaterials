using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalcLibrary
{

    [Serializable]
    public class MyDivideOnZeroException : Exception
    {
        public MyDivideOnZeroException() { }
        public MyDivideOnZeroException(string message) : base(message) { }
        public MyDivideOnZeroException(string message, Exception inner) : base(message, inner) { }
        protected MyDivideOnZeroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public static class MyCalc
    {
        public static double Sum(double a, double b)
        {
            return a + b;
        }

        public static double Divide(double a, double b)
        {
            if (b == 0)
                throw new MyDivideOnZeroException();

            return a / b;
        }

        public static void Test()
        {
            throw new Exception("samp"); 
        }
    }
}
