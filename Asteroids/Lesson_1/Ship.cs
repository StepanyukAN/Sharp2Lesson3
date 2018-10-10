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
    /// Класс корабль
    /// </summary>
    class Ship : BaseObject
    {
        /// <summary>
        /// Событие при смерти корабля
        /// </summary>
        public static event Message MessageDie;
        
        /// <summary>
        /// Энергия корабля
        /// </summary>
        private int _energy = 100;
        public int Energy => _energy;

        public void EnergyLow(int n)
        {
            _energy -= n;
        }
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
           obj = new Bitmap(Image.FromFile($@"{Application.StartupPath}\Spaceship.png"), size);
        }

        public override void Update()
        {
        }

        /// <summary>
        /// Движение вверх
        /// </summary>
        public void Up()
        {
            if (_pos.Y > 0) _pos.Y = _pos.Y - _dir.Y;
        }

        /// <summary>
        /// Движение вниз
        /// </summary>
        public void Down()
        {
            if (_pos.Y < Game.Height) _pos.Y = _pos.Y + _dir.Y;
        }

        /// <summary>
        /// Метод смерти корабля
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
        }


    }
}
