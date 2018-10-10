using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Asteroids
{
    class Asteroid : BaseObject, ICloneable, IComparable<Asteroid>
    {
        /// <summary>
        /// Картинка астероида
        /// </summary>
       
        private static Random random = new Random();


        public int Power { get; set; } = 3;

        /// <summary>
        /// В конструкторе добавляем к каждому астероиду картинку
        /// </summary>
        /// <param name="pos">Позиция на экране</param>
        /// <param name="dir">Приращение</param>
        /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            obj = new Bitmap(AddAsteroid(),size);
            Power = 1;
        }
        /// <summary>
        /// Реализованный абстрактный метод для обновления объекта на экране
        /// </summary>
        public override void Update()
        {
            _pos.X = _pos.X + _dir.X;
            _pos.Y = _pos.Y + _dir.Y;
            if (_pos.X < 0) _dir.X = -_dir.X;
            if (_pos.X + _size.Width > Game.Width) _dir.X = -_dir.X;
            if (_pos.Y < 0) _dir.Y = -_dir.Y;
            if (_pos.Y + _size.Height > Game.Height) _dir.Y = -_dir.Y;
        }

        /// <summary>
        /// Метод загрузки случайной картинки астероида
        /// </summary>
       private static Image AddAsteroid()
        {
            Image i= Image.FromFile($"{Application.StartupPath}\\0{random.Next(1, 5)}.png");
            return i;
        }

        /// <summary>
        /// Метод клонирования астероида
        /// </summary>
        /// <returns>Возвращает копию объекта</returns>
        public object Clone()
        {
            Asteroid asteroid = new Asteroid(new Point(_pos.X, _pos.Y), new Point(_dir.X, _dir.Y),
               new Size(_size.Width, _size.Height))
            { Power = Power };
            return asteroid;

        }

        public int CompareTo(Asteroid obj)
        {
            if (Power > obj.Power)
                return 1;
            if (Power < obj.Power)
                return -1;
            return 0;

        }
    }
}
