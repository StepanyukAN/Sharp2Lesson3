using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Asteroids
{
    abstract class BaseObject:ICollision
    {
        /// <summary>
        /// Делегат для события смерть корабля
        /// </summary>
        public delegate void Message();

        /// <summary>
        /// Картинка объекта
        /// </summary>
        protected Bitmap obj;

       /// <summary>
       /// Позиция на экране
       /// </summary>
        protected Point _pos;

        /// <summary>
        /// Изменение координат
        /// </summary>
        protected Point _dir;

        /// <summary>
        /// Размер объекта
        /// </summary>
        protected Size _size;

        /// <summary>
        /// Свойство для определения новой позиции на экране
        /// </summary>
        public Point Pos
        {
            get => _pos;
            set
            {
                if (value.X < 0 || value.Y < 0)
                {
                    throw new GameObjectException("Координата не может быть отрицательной.");
                }
                else if (value.X > Game.Width || value.Y > Game.Height)
                {
                    throw new GameObjectException("Объект выходит за пределы игрового поля.");
                }
                else _pos = value;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">Позиция на экране</param>
        /// <param name="dir">Изменение координат</param>
        /// <param name="size">Размер объекта</param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            _pos = pos;
            _dir = dir;
            _size = size;

        }

       //реализация интерфейса ICollision
        public Rectangle Rect => new Rectangle(_pos,_size);
        public bool Collision(ICollision obj) => obj.Rect.IntersectsWith(this.Rect);


        /// <summary>
        /// Абстрактный метод для рисования объекта
        /// </summary>
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(obj, _pos);
        }

        /// <summary>
        /// Абстрактный метод для обновления объекта на экране
        /// </summary>
        public abstract void Update();
    }
}
