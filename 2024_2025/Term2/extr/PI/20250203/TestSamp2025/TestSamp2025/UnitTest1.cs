using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyCalcLibrary;

namespace TestSamp2025
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSum5And4Eq9()
        {
            double result = MyCalc.Sum(5, 4);

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void TestDivide10On5Eq2()
        {
            double result = MyCalc.Divide(10, 5);

            Assert.AreEqual(2,result);
        }

        [TestMethod]
        public void TestDivideOnZero()
        {
            try
            {
                MyCalc.Divide(10, 0);
            }
            catch (MyDivideOnZeroException)
            {
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail("Сгенерировано неизвестное исключение "+ex.ToString());
            }

            Assert.Fail("Не сгенерировано исключение при делении на ноль");
        }

        [TestMethod]
        public void TestSamp()
        {
            //Assert.AreEqual(1,1);
            //Assert.Fail("Пусть будет ошибка!");
            //throw new Exception("Пусть будет исключение!");
           /* try
            {
                Assert.AreEqual(1, 2);
            }
            catch (Exception) { }*/
        }
    }
}
