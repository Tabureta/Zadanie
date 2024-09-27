using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BolshakovaZadanie2
{
    public partial class Form1 : Form
    {

        private bool dragging = false; // ���� ��� ������������ ��������������
        private Point dragCursorPoint; // �����, � ������� ������ ��� ��������
        private Point dragFormPoint;   // ��������� ������� �����

        public Form1()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializeComponent();
            this.ClientSize = new Size(400, 400); // ��������� �������� �����
            this.Text = "���������� ������"; // ��������� ��������� �����

            SetFormToStar(); // ��������� ����� ������
            this.Paint += new PaintEventHandler(MainForm_Paint); // �������� �� ������� Paint

            // �������� �� ������� ���� ��� �������������� �����
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        private void SetFormToStar()
        {
            // ������� �������������� ������� (Region) ��� �����
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(CreateStarPoints(5, new PointF(200, 200), 180, 80));
            this.Region = new Region(path);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = Brushes.Violet; // ���������� �����
            g.FillPolygon(brush, CreateStarPoints(5, new PointF(200, 200), 180, 80)); // ������ ������
        }

        // ����� ��� �������� ����� ������
        private PointF[] CreateStarPoints(int numPoints, PointF center, float outerRadius, float innerRadius)
        {
            PointF[] points = new PointF[numPoints * 2];
            double angleStep = Math.PI / numPoints;
            double angle = -Math.PI / 2; // ��������� ����� ������

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

        // ���������� ������� MouseDown ��� ������ ��������������
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // ���������, ������ �� ����� ������ ����
            {
                dragging = true; // ������������� ���� ��������������
                dragCursorPoint = Cursor.Position; // �������� ������� ������� �������
                dragFormPoint = this.Location; // �������� ������� ������� �����
            }
        }

        // ���������� ������� MouseMove ��� �������������� �����
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging) // ���� ����� ���������������
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // ������� ����� ������� � ��������� �������� �������
                this.Location = Point.Add(dragFormPoint, new Size(dif)); // ������������� ����� ������� �����
            }
        }

        // ���������� ������� MouseUp ��� ���������� ��������������
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // ���������, �������� �� ����� ������ ����
            {
                dragging = false; // ���������� ���� ��������������
            }
        }
    }


}
