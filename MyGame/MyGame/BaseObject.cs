using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
	

namespace MyGame
{
    abstract class BaseObject:ICollision
    {
        
        //Переписываем этот класс абстрактным
        //Возможность подгонять формат картинок  на лету или переедкт в другой класс или убрать, а подкладавть фоматированные картинки
        ///// <summary>
        ///// Уменьшаем картинки
        ///// </summary>
        ///// <returns></returns>
        //public bool ThumbnailCallback()
        //{
        //    return true;
        //}
       
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public abstract void Draw();



        //Уменьшаем картинку
        //  
        //Image a = Image.FromFile("aster3.png");
        //Image.GetThumbnailImageAbort callback =
        //new Image.GetThumbnailImageAbort(ThumbnailCallback);
        //Image pThumbnail = a.GetThumbnailImage(20, 20, callback, new IntPtr());


        //Game.Buffer.Graphics.DrawImageUnscaled(pThumbnail, Pos.X, Pos.Y, Size.Width, Size.Height);


        public abstract void Update();
        //{
        //    Pos.X = Pos.X  + Dir.X;
        //                            // if (Pos.X <= 0) Pos.X = Game.Width; 
        //                                                     // Pos.Y = Pos.Y  + Dir.Y;
        //    if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        //                                                   ////  if (Pos.X > Game.Width) Pos.X = 0;
        //                                                    // if (Pos.Y < 0) Dir.Y = -Dir.Y;
        //                                                    // if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        //}
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);

    }
}
