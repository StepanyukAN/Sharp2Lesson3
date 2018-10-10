using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    /// <summary>
    /// Класс для внутриигровых ошибок
    /// </summary>
    class GameObjectException : Exception
    {
        public GameObjectException(string message) : base(message)
        {
        }
    }
}
