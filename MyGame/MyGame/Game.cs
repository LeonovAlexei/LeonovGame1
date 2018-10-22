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
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static Bullet _bullet;
        public static Asteroid[] _asteroids;
        static Game()
        {
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
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            //Load();
            //LoadS();
        }
        //Обработчик таймера:
        //Более подробно про события и обработчики событий мы поговорим на следующих уроках.
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
           
        }
        public static void Draw()
        {
            // Проверяем вывод графики
           // Buffer.Graphics.Clear(Color.Black);
           // Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
           // Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
           // Buffer.Render();
            //Добавим в метод Draw вывод всех этих объектов на экран,
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
                     
            Buffer.Render();

        }
        //также добавим метод Update для изменения состояния объектов.
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet)) { System.Media.SystemSounds.Hand.Play();

                    //Задание 3 при столкновении пули с астероидом они регенирируются
                    // Пуля повторно загружается, 
                    //астероид -- находим индекс астероида в который попала пуля
                    // и создаем по этому индексу в массиве новый астероид 
                    var rnd = new Random();
                    int r = rnd.Next(5, 50);
                    var indexAster = Array.IndexOf(_asteroids, a);
                    _asteroids [indexAster]   = new Asteroid(new Point(1000, rnd.Next(0, Game.Height)), new Point(-r / 5, r), new Size(r, r));
                    _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
                }
            }
            _bullet.Update();
        }


        //Внесем изменения в класс с игрой.Здесь создадим массив объектов BaseObject. 
        //Чтобы не загромождать метод Init, добавим дополнительно метод Load, 
        //в котором реализуем инициализацию наших объектов
       public static BaseObject[] _objs;
        public static void Load()
        {
            var rnd = new Random(20);//Распределяем наши объекты случайным образом в пределах видимости

            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[3];

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
           // throw new GameObjectException($"Выводим исключение по задаче 5... ","Что конкретно произошло ?",DateTime.Now);



            //галактику не запускаем

            // _objs[_objs.Length-1] = new Galaxy(new Point((rnd.Next(50, 750)), (rnd.Next(50, 550))), new Point(10, 0), new Size(3, 3));



        }
        
    }
}
