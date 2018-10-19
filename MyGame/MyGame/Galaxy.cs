using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace MyGame
{
    class Galaxy:BaseObject
    {
        Image Galaxyjpg = Image.FromFile("galaxy.jpg");

        public Galaxy(Point pos, Point dir, Size size): base (pos, dir, size)
        {

        }
        public override void Draw()
        {

         
            Game.Buffer.Graphics.DrawImageUnscaled(Galaxyjpg, Pos.X, Pos.Y, Size.Width, Size.Height);


        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;

            if (Pos.X <= -Game.Width) Pos.X = Game.Width;
            //Galaxyjpg.RotateFlip(RotateFlipType.RotateNoneFlipXY );
           // Game.Buffer.Graphics.TranslateTransform(Pos.X, Pos.Y);

            

        }

    }
}
