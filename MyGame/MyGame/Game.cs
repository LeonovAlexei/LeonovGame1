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
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
           
            //Добавим в Init таймер и обработчик таймера, в котором заставим вызываться Draw и Update.
            Timer timer = new Timer { Interval = 100 };
            timer.Start();
            timer.Tick += Timer_Tick;
            Load();
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
            Buffer.Render();

        }
        //также добавим метод Update для изменения состояния объектов.
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }


        //Внесем изменения в класс с игрой.Здесь создадим массив объектов BaseObject. 
        //Чтобы не загромождать метод Init, добавим дополнительно метод Load, 
        //в котором реализуем инициализацию наших объектов
       public static BaseObject[] _objs;
        public static void Load()
        {
            Random rnd = new Random();//Распределяем наши объекты случайным образом в пределах видимости

            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length / 2; i++)
                _objs[i] = new BaseObject(new Point((rnd.Next(1,800)), (rnd.Next(1, 600))), new Point(5, -5), new Size(1, 1));
            for (int n=0,i = (_objs.Length / 2)-1; i < _objs.Length; i++,++n)
                _objs[i] = new Star(new Point((rnd.Next(1, 800)), (rnd.Next(1, 600))), new Point(3, -1), new Size(4,5));


            _objs[_objs.Length-1] = new Galaxy(new Point((rnd.Next(50, 750)), (rnd.Next(50, 550))), new Point(10, 0), new Size(3, 3));



        }
        
    }
}
