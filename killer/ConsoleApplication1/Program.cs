using System;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace ConsoleApplication1
{
    class Killer //класс для функций, связанных с "убийством" процессов
    {

        private int t_kill; //переменная для хранения времени жизни
        private int t_check;//переменная для хранения частоты проверки

        //конструктор класса
        public Killer(int tk, int tc) 
        {
            t_kill = tk;
            t_check = tc;
        }

        // функция обеспечивает проверку по таймеру
        public void ToMurder(Process i)
        {
            var ttt = DateTime.Now - i.StartTime;
            double add = Convert.ToDouble(ttt.Minutes); //вычисление времени работы

            while (add < t_kill) // пока время работы меньше положенного...
            {
                Thread.Sleep(1000*60* t_check); //приостановка выполнения программы на заданное время

                ttt = DateTime.Now - i.StartTime;
                add = Convert.ToDouble(ttt.Minutes); //вычисление времени работы

            }

            Murder(i); // "убийство" процесса
        }


        // функция обеспечивает выключение процесса
        private void Murder(Process i) 
        {
                i.Kill(); //непосредственно остановка


                // запись лога о событии. Лог хранится на рабочем столе
                string logpath = "C:/Users/" + Environment.UserName + "/Desktop/log.txt";
                FileStream fileStream;

                if (!File.Exists(logpath))
                    fileStream = File.Create(logpath);
                else
                    fileStream = File.Open(logpath, FileMode.Append);

                StreamWriter output = new StreamWriter(fileStream);
                output.Write(DateTime.Now.ToString() + " | " + i.ProcessName + " | killed " + "\n");
                output.Close();
        }
    }
    class Program
    {
        //функция просматривает активные процессы
        static public void Run(string name, int t_kill, int t_check)
        {
            DateTime currentTime = DateTime.Now;
            Killer prepar = new Killer(t_kill, t_check); //инициализация класса Killer с параметрами времени

            Process[] procList = Process.GetProcesses(); // List с процессами            
            foreach (Process i in procList) //перебор процессов
            {
                if (i.ProcessName == name) //если есть нужный процесс
                {
                    prepar.ToMurder(i);//переход к функциям его останова
                }
            }
                        
        }

        //вывод справки
        static public void Help()
        {
            Console.WriteLine("________________________________ ПОМОЩЬ ___________________________________");
            Console.WriteLine("На входе должно быть три параметра: название процесса, допустимое время жизни (в минутах) и частота проверки (в минутах)");
            Console.WriteLine("Пример - monitor.exe notepad 5 1");
            Console.WriteLine("С такими параметрами утилита раз в минуту проверяет, не живет ли процесс notepad больше пяти минут, и \"убивает\" его, если живет.");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            string name;
            int t_kill;
            int t_check;

            //проверка верности введенных параметров. Если что - то не так - вывод справки
            if (args.Length == 3)
            {
                try
                {
                    name = Convert.ToString(args[0]);
                    t_kill = Convert.ToInt32(args[1]);
                    t_check = Convert.ToInt32(args[2]);

                    Run(name, t_kill, t_check);
                }
                catch { Help(); }


            } else { Help(); }
        }
    }
}
