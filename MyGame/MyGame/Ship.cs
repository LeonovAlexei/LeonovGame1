using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс корабль который представляет наш космический корабль.
    /// Класс корабль является наследником  класса BaseObject.
    /// </summary>
    class Ship : BaseObject
    {
       
        /// <summary>
        ///  закрытое поле Энергия корабля = 100
        /// </summary>
        private int _energy = 100;
        private int maxEnergy = 100;
        /// <summary>
        /// Устанавливаем свойство только для чтения для поля _energy(Энергия корабля)
        /// </summary>
        public int Energy => _energy;
         /// <summary>
        /// Метод рассчитывает уменьшение енергии корабля на величину входящей переменной
        /// _energy=_energy-n
        /// </summary>
        /// <param name="n"></param>
        public void EnergyLow(int n)
        {
            
            _energy -= n;
            
            if (LogBook.listOfHandlers != null) LogBook.listOfHandlers($" Повреждения корабля: - {n} енргии");
        }
        public void EnergyUp(int n)
        {

            if (_energy < maxEnergy)
            {
                _energy += n;
                if (LogBook.listOfHandlers != null) LogBook.listOfHandlers($" Произведен ремонт корабля: + {n} енргии");
            }
            else _energy = maxEnergy;
            

        }
        Image s = Image.FromFile("../../Ship.png");
        /// <summary>
        /// Назначаем пустой конструктор по умолчанию который наследуется от базового
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="dir"></param>
        /// <param name="size"></param>
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }
        /// <summary>
        /// Метод перегруженный рисует наш корабль
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(s, Pos.X, Pos.Y, Size.Width, Size.Height);
            //Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y,Size.Width, Size.Height);
        }
        /// <summary>
        /// Метод перегруженный обновление для класса корабль пустой
        /// </summary>
        public override void Update()
        {
        }
        /// <summary>
        /// Метод движения вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }
        /// <summary>
        /// Метод движение вниз
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }
        /// <summary>
        /// Событие вызывается когда погибает корабль
        /// </summary>
        public static event Message MessageDie;
        /// <summary>
        /// Метод гибели для корабля
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
            
            if (LogBook.listOfHandlers != null) LogBook.listOfHandlers(" Корабль уничтожен! ");
        }
        
    }
}
