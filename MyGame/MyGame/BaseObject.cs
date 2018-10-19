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
            var aster = new List<string> { "aster1.png", "aster2.png", "aster3.png", "aster4.png" };
            for (int i = 0; i < aster.Count; i++)
            {
                Image a = Image.FromFile(aster[i]);
                Image.GetThumbnailImageAbort callback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image pThumbnail = a.GetThumbnailImage(40, 40, callback, new IntPtr());
                Game.Buffer.Graphics.DrawImageUnscaled(pThumbnail, Pos.X, Pos.Y, Size.Width, Size.Height);

            }
            
            
           

            
            //Game.Buffer.Graphics.DrawRectangle(Pens.White, Pos.X+10, Pos.Y+30, Size.Width+10, Size.Height+15);

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
