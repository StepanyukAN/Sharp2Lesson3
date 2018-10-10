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
    class FirstAid : BaseObject
    {
        public int Power { get; set; } = 3;
        public FirstAid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            obj = new Bitmap(Image.FromFile($@"{Application.StartupPath}\aid.png"), size);
            Power = 1;
        }

        public override void Update()
        {
            _pos.X = _pos.X + _dir.X;
            _pos.Y = _pos.Y + _dir.Y;
            if (_pos.X < 0) _dir.X = -_dir.X;
            if (_pos.X + _size.Width > Game.Width) _dir.X = -_dir.X;
            if (_pos.Y < 0) _dir.Y = -_dir.Y;
            if (_pos.Y + _size.Height > Game.Height) _dir.Y = -_dir.Y;
        }
    }
}
