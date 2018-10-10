using System.Drawing;

namespace Asteroids
{
    interface ICollision
    {
        /// <summary>
        /// Метод для проверки столкновения объектов
        /// </summary>
        /// <param name="obj">Объект, реализующий интерфейс ICollision</param>
        /// <returns>Возвращает истину, если прямоугольники, в которые вписаны объекты, пересекутся</returns>
        bool Collision(ICollision obj);

        /// <summary>
        /// Прямоугольник, в который вписан объект
        /// </summary>
        Rectangle Rect { get; }
    }
}
