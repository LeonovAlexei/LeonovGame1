using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
	

namespace MyGame
{
    class BaseObject
    {
        

        /// <summary>
        /// Уменьшаем картинки
        /// </summary>
        /// <returns></returns>
        public bool ThumbnailCallback()
        {
            return true;
        }
       
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public virtual void Draw()
        {
           
          
                //Уменьшаем картинку
                   
                Image a = Image.FromFile("aster3.png");
                Image.GetThumbnailImageAbort callback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image pThumbnail = a.GetThumbnailImage(20, 20, callback, new IntPtr());
               
          
            Game.Buffer.Graphics.DrawImageUnscaled(pThumbnail, Pos.X, Pos.Y, Size.Width, Size.Height);
           
            

            

        }
        public virtual void Update()
        {
            Pos.X = Pos.X  + Dir.X;
            Pos.Y = Pos.Y  + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

    }
}
