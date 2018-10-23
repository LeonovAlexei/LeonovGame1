using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    class SplashScreen
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        static SplashScreen()
        {
        }
        public static void Init(FormSS form)
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
            //timer.Tick += Timer_Tick;
           // Load();
            //LoadS();
        }
    }
}
