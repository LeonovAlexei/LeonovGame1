using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyGame
{
    class RepairKit : BaseObject
    {
        public int Power { get; set; }
        Image r = Image.FromFile("../../RepairKit.jpg");
        public RepairKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(r, Pos.X, Pos.Y, Size.Width, Size.Height);
             //Game.Buffer.Graphics.FillEllipse(Brushes.White, Pos.X, Pos.Y, Size.Width, Size.Height); ;
        }
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            // if (Pos.X <= 0) Pos.X = Game.Width; 
            // Pos.Y = Pos.Y  + Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
            ////  if (Pos.X > Game.Width) Pos.X = 0;
            // if (Pos.Y < 0) Dir.Y = -Dir.Y;
            // if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }
}
