using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    /// <summary>
    /// Класс аптечка
    /// </summary>
    class FirstAid : Asteroid
    {
       
        public FirstAid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            obj = new Bitmap(Image.FromFile($@"{Application.StartupPath}\aid.png"), size);
            Power = 1;
        }


    }
}
