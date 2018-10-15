using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Asteroids
{
    public static class Game
    {
        /// <summary>
        /// Делегат для ведения лога
        /// </summary>
        /// <param name="message">Сообщение</param>
        public delegate void Log(string message);

        private static Log log;
        private static Log cons = Console.WriteLine;
        private static Log file = AddToFile;

        /// <summary>
        /// Количество астероидов (растет в зависимости от уровня)
        /// </summary>
        private static int asters =3;

        /// <summary>
        /// Уровень
        /// </summary>
        private static int level = 0;

        /// <summary>
        /// Счетчик для подсчета очков
        /// </summary>
        private static int count;

        /// <summary>
        /// Предоставляет доступ к главному буферу графического контекста
        /// для текущего приложения
        /// </summary>
        private static BufferedGraphicsContext _context;

        /// <summary>
        /// Буфер для рисования
        /// </summary>
        public static BufferedGraphics Buffer;

        /// <summary>
        /// Массив объектов
        /// </summary>
        private static BaseObject[] _objs;

        /// <summary>
        /// таймер
        /// </summary>
        private static Timer _timer = new Timer() { Interval = 40 };

        /// <summary>
        /// рандом
        /// </summary>
        private static Random rand = new Random();

        /// <summary>
        /// Пуля
        /// </summary>
        private static List<Bullet> _bullet;

        /// <summary>
        /// Массив астероидов
        /// </summary>
        private static List<Asteroid>_asteroids;

        /// <summary>
        /// Аптечка
        /// </summary>
        private static FirstAid _aid;

        /// <summary>
        /// Ширина игрового поля
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        /// Высота игрового поля
        /// </summary>
        public static int Height { get; set; }
        
        static Image image = Image.FromFile($@"{Application.StartupPath}\Galactic.jpg");

        /// <summary>
        /// Фоновая картинка.
        /// </summary>
        static Bitmap background;

        /// <summary>
        /// Космический корабль
        /// </summary>
        private static Ship _ship = new Ship(new Point(10, 400), new Point(10, 10), new Size(100,100));


        static Game()
        {
            
        }

        /// <summary>
        /// Целевой метод для события Timer.Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }



        /// <summary>
        /// Загрузка графики формы
        /// </summary>
        /// <param name="form">Экземпляр формы</param>
        public static void Init(Form form)
        {
           
            // Графическое устройство для вывода графики
            
            Graphics g;


            
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();


            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            
            background = new Bitmap(image, Width, Height);

            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в
            //буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();

            
            
            _timer.Start();

            //Подписываем метод Timer_Tick на событие timer.Tick
            _timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
        }

        /// <summary>
        /// Метод для обработки нажатия клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                _bullet.Add(new Bullet(new Point(_ship.Rect.X + 50, _ship.Rect.Y + 44), new Point(50, 0), new Size(50, 12)));
            }
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
        }

        /// <summary>
        /// Рисование объектов
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.DrawImage(background, 0, 0);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            foreach (Bullet b in _bullet)
            {
                b?.Draw();
            }
            _ship?.Draw();
            if (_ship != null)
                Buffer.Graphics.DrawString($"Энергия: { _ship.Energy}", new Font(FontFamily.GenericSansSerif, 20), Brushes.White, 0, 0);
            Buffer.Graphics.DrawString($"Уровень: {level}", new Font(FontFamily.GenericSansSerif, 20), Brushes.White, Game.Width-200, 0);
            Buffer.Graphics.DrawString($"Cчет: {count}", new Font(FontFamily.GenericSansSerif, 20),Brushes.White, Game.Width/2, 0);
            Buffer.Render();
        }

        /// <summary>
        /// Обновление объектов
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs) obj.Update();
            foreach (Bullet b in _bullet) b?.Update();
            foreach (var a in _asteroids) a?.Update();

            //Если не осталось астероидов, создаем новую коллекцию, где астероидов на 1 больше
            if (_asteroids.Count == 0)
            {
                asters++;
                Load();
            }

            //длинная проверка на столкновения
            for (int i = 0; i < _asteroids.Count; i++)
            {
                
                for (int j = 0; j < _bullet.Count; j++)
                {


                    if (_bullet[j].Collision(_asteroids[i]))
                    {
                        if (_asteroids[i] is FirstAid)
                        {
                            int r = rand.Next(1, 10);
                            _ship?.EnergyLow(-r);
                            _asteroids[i] = null;
                            _asteroids.RemoveAt(i);
                            i--;
                            _bullet[j] = null;
                            _bullet.RemoveAt(j);
                            log.Invoke($"Подлечились на {r} HP");
                            System.Media.SystemSounds.Exclamation.Play();
                            count++;
                            break;
                        }
                        else
                        {
                            System.Media.SystemSounds.Hand.Play();
                            _asteroids[i] = null;
                            _asteroids.RemoveAt(i);
                            i--;
                            _bullet[j] = null;
                            _bullet.RemoveAt(j);
                            log?.Invoke("Астероид уничтожен");
                            count++;
                            break;
                        }

                    }
                    if (_bullet[j].Pos.X > Game.Width) { _bullet[j] = null; _bullet.RemoveAt(j); break; }
                }

               // Костыль для ArgumentOutOfRangeException
                if (i < 0) break;


                if (_ship.Collision(_asteroids[i]))
                {
                    if (_asteroids[i] is FirstAid)
                    {
                        int r = rand.Next(1, 10);
                        _ship?.EnergyLow(-r);
                        _asteroids[i] = null;
                        _asteroids.RemoveAt(i);
                        log.Invoke($"Подлечились на {r} HP");
                        System.Media.SystemSounds.Exclamation.Play();
                        count++;
                        break;
                    }
                    else
                    {
                        int r = rand.Next(10, 50);
                        _ship?.EnergyLow(r);
                        _asteroids[i] = null;
                        _asteroids.RemoveAt(i);
                        log.Invoke($"Астероид жахнул на {r} HP");
                        System.Media.SystemSounds.Asterisk.Play();
                        break;
                    }

                }


            }
          
            if (_ship.Energy <= 0)
            {
                _ship?.Die();
                log.Invoke("Все, капец!!!");
            }

        }


        /// <summary>
        /// Загрузка объектов в игру
        /// </summary>
        public static void Load()
        {
            _objs = new BaseObject[30];
            _asteroids = new List<Asteroid>();
            _bullet = new List<Bullet>();
            _aid = new FirstAid(new Point(rand.Next(0, Game.Width - 100), rand.Next(0, Game.Height-200)), new Point(rand.Next(-15, 15), rand.Next(-15, 15)), new Size(70, 70));
            for (int i = 0; i < _objs.Length; i++)
            {
                int r = rand.Next(5, 50);
                _objs[i] = new Star(new Point(rand.Next(0, Game.Width), rand.Next(0, Game.Height-100)), new Point(-r, r), new Size(r, r));
            }
            for (int i = 0; i < asters; i++)
            {
                int r = rand.Next(5, 15);
                _asteroids.Add(new Asteroid(new Point(rand.Next(200, Game.Width - 100), rand.Next(0, Game.Height-200)), new Point(-r, r), new Size(50, 50)));
            }
            _asteroids.Add(_aid);

            level++;

            if (File.Exists($@"{Application.StartupPath}\log.dat"))
                File.Delete($@"{Application.StartupPath}\log.dat");

            log = cons;
            log += file;
        }

        /// <summary>
        /// Конец игры
        /// </summary>
        public static void Finish()
        {
            _timer.Stop();
            if (MessageBox.Show("К сожалению Вы проиграли. Начать заново?", "Все, капец!!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Restart();
            else Application.Exit();
        }

        /// <summary>
        /// Метод ведения лога в файл
        /// </summary>
        /// <param name="message">Сообщение</param>
        private static void AddToFile(string message)
        {
            using (FileStream fs = new FileStream($@"{Application.StartupPath}\log.dat", FileMode.Append))
            {

                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
