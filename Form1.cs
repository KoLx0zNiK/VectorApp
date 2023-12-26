using System;
using System.Drawing;
using System.Windows.Forms;

namespace VectorApp
{
    public partial class VectorForm : Form
    {
        private VectorObject vectorA, vectorB, vectorC;

        public VectorForm()
        {
            InitializeComponent();

            vectorA = new VectorObject(Color.Red);
            vectorB = new VectorObject(Color.Blue);
            vectorC = new VectorObject(Color.Green);
        }

        private void DrawAxis(Graphics graphics)
        {
            graphics.DrawLine(new Pen(Brushes.Black, 2), new Point(0, 200), new Point(900, 200));
            graphics.DrawLine(new Pen(Brushes.Black, 2), new Point(190, 0), new Point(190, 700));
        }

        private void DrawVectors(Graphics graphics)
        {
            vectorA.Draw(graphics, 190, 200);
            vectorB.Draw(graphics, 190, 200);

            if (radioButton1.Checked)
            {
                vectorC.Draw(graphics, 190, 200);
            }
            else if (radioButton2.Checked)
            {
                vectorC.Draw(graphics, 190, 200);
            }
        }

        private void UpdateTextBoxes()
        {
            textBox1.Text = vectorA.Length.ToString();
            textBox2.Text = vectorB.Length.ToString();
            textBox3.Text = vectorC.X.ToString();
            textBox4.Text = vectorC.Y.ToString();
            textBox5.Text = vectorC.Length.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vectorA.SetValues(Convert.ToSingle(numericUpDown1.Value), Convert.ToSingle(numericUpDown2.Value));
            vectorB.SetValues(Convert.ToSingle(numericUpDown3.Value), Convert.ToSingle(numericUpDown4.Value));

            if (radioButton1.Checked)
            {
                vectorC = vectorA.Add(vectorB);
            }
            else if (radioButton2.Checked)
            {
                vectorC = vectorA.Subtract(vectorB);
            }

            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(SystemColors.Control);

            DrawAxis(graphics);
            DrawVectors(graphics);
            UpdateTextBoxes();
        }
    }

    public class VectorObject
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Length => (float)Math.Sqrt(X * X + Y * Y);
        public Point EndPoint => new Point((int)X, (int)Y);

        private Color color;

        public VectorObject(Color color)
        {
            this.color = color;
        }

        public void SetValues(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Draw(Graphics graphics, int startX, int startY)
        {
            Pen pen = new Pen(color);
            graphics.DrawLine(pen, startX, startY, EndPoint.X + startX, EndPoint.Y + startY);
        }

        public VectorObject Add(VectorObject other)
        {
            return new VectorObject(this.color)
            {
                X = this.X + other.X,
                Y = this.Y + other.Y
            };
        }

        public VectorObject Subtract(VectorObject other)
        {
            return new VectorObject(this.color)
            {
                X = this.X - other.X,
                Y = this.Y - other.Y
            };
        }
    }
}
