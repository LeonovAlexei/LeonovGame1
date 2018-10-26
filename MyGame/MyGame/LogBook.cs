using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Класс для записи сообщений игры
    /// </summary>
    class LogBook
    {
        ///// <summary>
        ///// Определяем новый тип делегата который будет отправлять уведомления вызывающему коду
        ///// </summary>
        ///// <param name="msgForCaller">Сообщение строка</param>
        public delegate void ShipLogHandler(string msgForCaller);
        
        /// <summary>
        /// Переменная нашего делегата
        /// </summary>
        public static ShipLogHandler listOfHandlers;
        /// <summary>
        /// Регистрационная функция для вызывающего кода запись лога
        /// </summary>
        /// <param name="methodToCall"></param>
        public static void RegisterWithShipLog(ShipLogHandler methodToCall)
        {
            listOfHandlers += methodToCall;
        }
        /// <summary>
        /// Метод записи сообщений в консоль
        /// </summary>
        /// <param name="msg"></param>
        public static void ShipEventConsole (string msg)
        {
            Console.WriteLine($"***********{DateTime.Now}************");
            Console.WriteLine($"*{msg,-40}*");
            Console.WriteLine("******************************************\n");
        }
        /// <summary>
        /// Метод записи сообщений игры в файл
        /// </summary>
        /// <param name="msg"></param>
        public static void ShipEventFile(string msg)
        {
            FileInfo log = new FileInfo("logBook.txt");
            using (StreamWriter swriterAppend = log.AppendText()) 
            {
              swriterAppend.WriteLine($"{DateTime.Now}*{msg}");
            }
        }
    }
}
