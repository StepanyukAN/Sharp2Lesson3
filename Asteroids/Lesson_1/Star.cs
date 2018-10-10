using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    class Star : BaseObject
    {
        /// <summary>
        /// Для картинки звезды.
        /// </summary>
        private Bitmap star;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">Позиция на экране</param>
        /// <param name="dir">Изменение координат</param>
        /// <param name="size">Размер объекта</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            star = new Bitmap(Image.FromFile($"{Application.StartupPath}\\star.png"), size);
        }

        /// <summary>
        /// Реализованный абстрактный метод для рисования объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(star, _pos);
        }

        /// <summary>
        /// Реализованный абстрактный метод для обновления объекта на экране
        /// </summary>
        public override void Update()
        {
            _pos.X = _pos.X + _dir.X;
            if (_pos.X <0) _pos.X = Game.Width;
        }

       
    }
}
