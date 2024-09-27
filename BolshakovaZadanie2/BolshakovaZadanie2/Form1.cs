using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BolshakovaZadanie2
{
    public partial class Form1 : Form
    {

        private bool dragging = false; // Флаг для отслеживания перетаскивания
        private Point dragCursorPoint; // Точка, в которой курсор был захвачен
        private Point dragFormPoint;   // Начальная позиция формы

        public Form1()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializeComponent();
            this.ClientSize = new Size(400, 400); // Установка размеров формы
            this.Text = "Фиолетовая звезда"; // Установка заголовка формы

            SetFormToStar(); // Установка формы звезды
            this.Paint += new PaintEventHandler(MainForm_Paint); // Подписка на событие Paint

            // Подписка на события мыши для перетаскивания формы
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        private void SetFormToStar()
        {
            // Создаем звездообразную область (Region) для формы
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(CreateStarPoints(5, new PointF(200, 200), 180, 80));
            this.Region = new Region(path);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = Brushes.Violet; // Фиолетовая кисть
            g.FillPolygon(brush, CreateStarPoints(5, new PointF(200, 200), 180, 80)); // Рисуем звезду
        }

        // Метод для создания точек звезды
        private PointF[] CreateStarPoints(int numPoints, PointF center, float outerRadius, float innerRadius)
        {
            PointF[] points = new PointF[numPoints * 2];
            double angleStep = Math.PI / numPoints;
            double angle = -Math.PI / 2; // Начальная точка сверху

            for (int i = 0; i < numPoints * 2; i++)
            {
                float radius = (i % 2 == 0) ? outerRadius : innerRadius;
                points[i] = new PointF(
                    center.X + radius * (float)Math.Cos(angle),
                    center.Y + radius * (float)Math.Sin(angle)
                );
                angle += angleStep;
            }

            return points;
        }

        // Обработчик события MouseDown для начала перетаскивания
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Проверяем, нажата ли левая кнопка мыши
            {
                dragging = true; // Устанавливаем флаг перетаскивания
                dragCursorPoint = Cursor.Position; // Получаем текущую позицию курсора
                dragFormPoint = this.Location; // Получаем текущую позицию формы
            }
        }

        // Обработчик события MouseMove для перетаскивания формы
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging) // Если форма перетаскивается
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // Разница между текущей и начальной позицией курсора
                this.Location = Point.Add(dragFormPoint, new Size(dif)); // Устанавливаем новую позицию формы
            }
        }

        // Обработчик события MouseUp для завершения перетаскивания
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Проверяем, отпущена ли левая кнопка мыши
            {
                dragging = false; // Сбрасываем флаг перетаскивания
            }
        }
    }


}
