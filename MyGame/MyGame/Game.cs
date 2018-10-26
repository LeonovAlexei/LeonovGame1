using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace MyGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина игрового поля
        public static int Width { get; set; }
        // Высота игрового поля
        public static int Height { get; set; }
        public static Bullet _bullet;
        public static Asteroid[] _asteroids;
        public static RepairKit[] _repairKit;
        public static BaseObject[] _objs;
        public static Random rnd = new Random();//Распределяем наши объекты случайным образом в пределах видимости
        private static int _scope = 0;
        public static int Scope => _scope;
        

        

        /// <summary>
        /// Выносим таймер из метода в класс чтобы заработал метод Finish
        /// </summary>
        private static Timer _timer = new Timer(); //{ Interval = 100 };
        /// <summary>
        /// Создаем статический объект корабля
        /// </summary>
        private static Ship _ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));

        

        static Game()
        {
        }
        
        
        /// <summary>
        /// Метод Finish останавливает таймер, выводит на экран конец из графического буфера
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(
                FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
        
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            //Задание 4 для вызова ошибки нужно поменять значения на 1000
            if (Width>10000||Height>10000)
            {
                throw new ArgumentOutOfRangeException($"Выводим исключение по задаче 4... Высота {Width} или ширина{Height} больше 1000 ");
            }
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
           
            //Добавим в Init таймер и обработчик таймера, в котором заставим вызываться Draw и Update.
            //Timer timer = new Timer { Interval = 100 };//выносим таймер из метода в класс
            _timer.Start();
            _timer.Tick += Timer_Tick;

            form.KeyDown += From_KeyDown;
            //Подписываемся на событие потери корабля и вызываем метод Finish
            Ship.MessageDie += Finish;

        }
        /// <summary>
        /// Метод управления кораблем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void From_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
                // Console.WriteLine(  $"{DateTime.Now} Произведен выстрел.");
                LogBook.listOfHandlers?.Invoke($" Произведен выстрел!");
                

            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();         
        }
        //Обработчик таймера:
        //Более подробно про события и обработчики событий мы поговорим на следующих уроках.
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
           
        }
        /// <summary>
        /// Метод выводит объекты, учитывает столкновения и выводит энергию корабля
        /// </summary>
        public static void Draw()
        {
            ///
           
            // Проверяем вывод графики
            // Buffer.Graphics.Clear(Color.Black);
            // Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            // Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            // Buffer.Render();
            //Добавим в метод Draw вывод всех этих объектов на экран,
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids) 
            {                                   
                a?.Draw();                      
            }
            foreach (RepairKit r in _repairKit)
            {
                r?.Draw();
            }
            _bullet?.Draw();
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy,
                    SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Graphics.DrawString("Scope: " + _scope,
                    SystemFonts.DefaultFont, Brushes.White, Game.Width-70, 0);
            Buffer.Render();

            
            //

        }


        
        /// <summary>
        /// Метод для изменения состояния объектов, а также учета столкновений
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            _bullet?.Update();
            for (int i = 0; i < _repairKit.Length; i++)
            {
                if (_repairKit[i] == null) continue;
                _repairKit[i].Update();
                if (!_ship.Collision(_repairKit[i])) continue;
                LogBook.listOfHandlers?.Invoke($" Найден рем. комплект!");
                _ship?.EnergyUp(rnd.Next(1, 10));
                _repairKit[i] = null;
                System.Media.SystemSounds.Question.Play();
            }

            for (int i = 0; i < _asteroids.Length; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                if (_bullet!=null && _bullet.Collision(_asteroids[i]))
                {
                    int r = rnd.Next(5, 50);
                    System.Media.SystemSounds.Hand.Play();
                    _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r)); //null;
                    _bullet = null;
                    _scope++;
                    LogBook.listOfHandlers?.Invoke(" Астероид сбит! ");
                    continue;
                }
                if (!_ship.Collision(_asteroids[i])) continue;
                LogBook.listOfHandlers?.Invoke($" Столкновение с астероидом!");
                _ship?.EnergyLow(rnd.Next(1, 10));
                System.Media.SystemSounds.Asterisk.Play();
                
                if (_ship.Energy <= 0) _ship.Die();
                
            }
            #region Старый код с задачи 3 закомментирован


            //foreach (Asteroid a in _asteroids)
            //{
            //    a.Update();
            //    if (a.Collision(_bullet)) { System.Media.SystemSounds.Hand.Play();

            //        //Задание 3 при столкновении пули с астероидом они регенирируются
            //        // Пуля повторно загружается, 
            //        //астероид -- находим индекс астероида в который попала пуля
            //        // и создаем по этому индексу в массиве новый астероид 
            //        var rnd = new Random();
            //        int r = rnd.Next(5, 50);
            //        var indexAster = Array.IndexOf(_asteroids, a);
            //        _asteroids [indexAster]   = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            //        _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            //    }
            //}
            //_bullet.Update();
            #endregion
        }


        
        /// <summary>
        /// реализуем инициализацию наших объектов
        /// </summary>
        public static void Load()
        {
            //Сообщаем методы для отправки сообщений
            LogBook.RegisterWithShipLog(new LogBook.ShipLogHandler(LogBook.ShipEventConsole));
            LogBook.RegisterWithShipLog(new LogBook.ShipLogHandler(LogBook.ShipEventFile));
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];
            _repairKit = new RepairKit[3];

            for (var i = 0; i < _objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0,Game.Height)), new Point(-r, r), new Size(3, 3));
            }
            for (var i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r/5, r), new Size(r, r));
            }
            for (var i = 0; i < _repairKit.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _repairKit[i] = new RepairKit(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
            }
            // throw new GameObjectException($"Выводим исключение по задаче 5... ","Что конкретно произошло ?",DateTime.Now);
            //галактику не запускаем
            // _objs[_objs.Length-1] = new Galaxy(new Point((rnd.Next(50, 750)), (rnd.Next(50, 550))), new Point(10, 0), new Size(3, 3));

            if (LogBook.listOfHandlers != null) LogBook.listOfHandlers(" Начало похода!");

        }
        
    }
}
