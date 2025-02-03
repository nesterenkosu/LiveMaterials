using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;


namespace MyUITestSamp
{

    [TestClass]
    public class UnitTest1
    {
        public static WindowsDriver<WindowsElement> windowsDriver;

        public static WindowsElement tb_a, tb_b, btn_Go, lb_Result;

        [TestInitialize]
        public void TestInitialize()
        {
            var desiredCapabilities = new AppiumOptions();

            //Задание параметров тестирующей библиотеки  (в данном случае параметр единственный - путь к тестируемой программе)
            desiredCapabilities.AddAdditionalCapability("app", @"D:\G_GITHUBMIRROR\nesterenkosu\pi\ProgramsToTest\MyCalcDesktop\MyCalculator\bin\Debug\MyCalculator.exe");

            //Запуск тестируемой программы и тестирующей библиотеки
            windowsDriver = new WindowsDriver<WindowsElement>(
                    new Uri("http://127.0.0.1:4723"),
                    desiredCapabilities
                );

            //Временная задержка, для уверенности в том, что приложение успело запуститься
            System.Threading.Thread.Sleep(1000);

            //Получить доступ к элементам управления, объявленным на форме
            tb_a = windowsDriver.FindElementByAccessibilityId("tb_a");
            tb_b = windowsDriver.FindElementByAccessibilityId("tb_b");
            btn_Go = windowsDriver.FindElementByAccessibilityId("btn_Go");
            lb_Result = windowsDriver.FindElementByAccessibilityId("lb_Result");

        }

        [TestMethod]
        public void TestMethod1()
        {
            tb_a.SendKeys("10");
            tb_b.SendKeys("5");
            btn_Go.Click();
            Assert.AreEqual("2",lb_Result.Text);
        }

        [TestMethod]
        public void TestMethod2()
        {
            tb_a.SendKeys("10");
            tb_b.SendKeys("0");
            btn_Go.Click();
            Assert.AreEqual("ОШИБКА! Деление на ноль!", lb_Result.Text);
        }

        [TestCleanup]
        public void TestFinalize()
        {
            windowsDriver.Close();
        }
    }
}
