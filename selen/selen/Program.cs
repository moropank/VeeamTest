using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace selen
{
    class Program
    {
        static void Engldev(IWebDriver driver)//поиск по заранее заданным параметрам
        {
            openbr(driver); //первостепенные операции с браузером

            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[1]/div/div[2]/div/div")).Click();
            driver.FindElement(By.LinkText("Разработка продуктов")).Click(); //обозначение отдела

            driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[1]/div/div[3]/div/div/button")).Click();
            driver.FindElement(By.XPath("//*[@class='custom-control custom-checkbox']//*[text()='Английский']")).Click();//обозначение языка

            int count = driver.FindElements(By.XPath("//*[@class='card card-no-hover card-sm']")).Count();//подсчет результатов

            Console.WriteLine("\nКоличество вакансий по заданным параметрам - " + count); //вывод результата
        }

        static void Givenparam(IWebDriver driver)//поиск по задаваемым параметрам
        {
            //ввод параметров
            Console.Write("\nВведите ключевые слова для поиска: ");
            string keyword = Console.ReadLine();

            Console.Write("\nВведите отдел: ");
            string depart = Console.ReadLine();

            Console.Write("\nВведите язык: ");
            string lang = Console.ReadLine();

            Console.Write("\nВведите опыт: ");
            string exper = Console.ReadLine();

            Console.Write("\nВведите регион: ");
            string region = Console.ReadLine();

            Console.WriteLine("\nПоиск начат...");

            openbr(driver); //первостепенные операции с браузером

            //выполнение поиска по заданным параметрам
            try {

                if (keyword.Length > 0)//параметр - ключевое слово
                {
                    driver.FindElement(By.XPath($"//*[@type='search']")).SendKeys(keyword);
                }

                if (depart.Length > 0) //параметр - отдел
                {
                    driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[1]/div/div[2]/div/div")).Click();
                    driver.FindElement(By.LinkText(depart)).Click();
                }

                if (lang.Length > 0)//параметр - язык
                {
                    driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[1]/div/div[3]/div/div/button")).Click();
                    driver.FindElement(By.XPath($"//*[@class='custom-control custom-checkbox']//*[text()='{lang}']")).Click();
                }

                if (exper.Length > 0)//параметр - регион
                {
                    driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[1]/div/div[4]/div/div")).Click();
                    driver.FindElement(By.LinkText(exper)).Click();
                }

                if (region.Length > 0)//параметр - опыт
                {
                    driver.FindElement(By.XPath("/html/body/div[1]/div/div[1]/div/div[2]/div[1]/div/div[5]/div")).Click();
                    driver.FindElement(By.LinkText(region)).Click();
                }

                int count = driver.FindElements(By.XPath("//*[@class='card card-no-hover card-sm']")).Count();//подсчет результатов

                Console.WriteLine("\nКоличество вакансий по заданным параметрам - " + count); //вывод результата

            } catch
            {
                Console.WriteLine("Ошибка при поиске. Неверно введенные данные");
            }

        }

            static void choise(string i, IWebDriver driver)//функция выбора типа поиска
        {
            switch (i)
            {
                case "1":
                    Engldev(driver);//по заранее заданным параметрам
                    break;
                case "2":
                    Givenparam(driver);//по задаваемым параметрам
                    break;
                default:
                    Console.WriteLine("Что - то пошло не так");
                    break;
            }
        }

        static void openbr(IWebDriver driver)//первостепенные операции с браузером
        {
            driver.Url = "https://careers.veeam.ru/vacancies";//ссылка
            driver.Manage().Window.Maximize();//разворачивание браузера на весь экран
        }
        static void Main(string[] args)
        {
            
            ChromeOptions opt = new ChromeOptions();
            opt.AddArgument("log-level=3"); //установка опций для вывода информации на консоль
            IWebDriver driver = new ChromeDriver(opt);//инициализация драйвера браузера


            Console.WriteLine("\nЗадайте тип поиска: 1 - с заранее известными параметрами, 2 - с задаваемыми параметрами");

            string ch = Console.ReadLine();

            //Проверка введенного параметра и переход к функции посика 
            if (ch == "1" || ch == "2" )
            {
                choise(ch, driver);
            }
            else
            {
                Console.WriteLine("\nВведены неверные данные");
            }          

            //завершение работы
            Console.WriteLine("\nНажмите Enter для выхода");
            Console.ReadLine();

            driver.Quit();
            Environment.Exit(0);

        }
    }
}
