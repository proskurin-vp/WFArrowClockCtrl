using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace WFArrowClockCtrl
{
    public partial class ArrowClockCtrl: UserControl
    {
        const int DEGREES_IN_MINUTE = 6;
        const int DEGREES_IN_HOUR = 30;
        SolidBrush _minBrush = new SolidBrush(Color.Black);     

        [Category("Внешний вид")]  
        public Color MinColor
        {
            set { _minBrush = new SolidBrush(value); }
            get { return _minBrush.Color;  }
        }

        SolidBrush _hourBrush = new SolidBrush(Color.Black);
        [Category("Внешний вид")]
        public Color HourColor
        {
            set { _hourBrush = new SolidBrush(value); }
            get { return _hourBrush.Color; }
        }

        SolidBrush _secBrush = new SolidBrush(Color.Red);
        [Category("Внешний вид")]
        public Color SecColor
        {
            set { _secBrush = new SolidBrush(value); }
            get { return _secBrush.Color; }
        }
        public ArrowClockCtrl()
        {
            InitializeComponent();
            this.BackgroundImage = Resource1.system;
        }


        [Browsable(false), DesignerSerializationVisibility(
             DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage { get; set; }


        public enum BackImage
        {
            Cronometer, Diner, Flower, Modern, Square, System, Trad
        }

        BackImage _backPicture;
        [Category("Внешний вид")]
        public BackImage BackPicture
        {
            set
            {
                _backPicture = value;
                switch (value)
                {
                    case BackImage.Cronometer: BackgroundImage = Resource1.cronometer; break;
                    case BackImage.Diner: BackgroundImage = Resource1.diner; break;                       
                    case BackImage.Flower: BackgroundImage = Resource1.flower; break;
                    case BackImage.Modern: BackgroundImage = Resource1.modern; break;
                    case BackImage.Square: BackgroundImage = Resource1.square; break;
                    case BackImage.System: BackgroundImage = Resource1.system; break;
                    case BackImage.Trad:BackgroundImage = Resource1.trad; break;                   
                }
            }

            get
            {
                return _backPicture;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void ArrowClockCtrl_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TranslateTransform(Size.Width / 2, Size.Height / 2);

            Point[] points = new Point[] { new Point(-3, 0), new Point(0, -40), new Point(3, 0), new Point(0, 3) };
            float angle = DateTime.Now.Minute * DEGREES_IN_MINUTE + DateTime.Now.Second / 10.0f;
            DrawArrow(graphics, points, _minBrush, angle);

            points = new Point[] { new Point(-3, 0), new Point(0, -25), new Point(3, 0), new Point(0, 3) };
            angle = DateTime.Now.Hour * DEGREES_IN_HOUR + DateTime.Now.Minute / 2.0f;
            DrawArrow(graphics, points, _hourBrush, angle);

            points = new Point[] { new Point(-1, 0), new Point(0, -50), new Point(1, 0), new Point(0, 1) };
            angle = DateTime.Now.Second * DEGREES_IN_MINUTE;
            DrawArrow(graphics, points, _secBrush, angle);
        }

        private void DrawArrow(Graphics graphics, Point[] points, Brush brush, float angle)            
        {
            GraphicsPath arrow = new GraphicsPath();
            arrow.StartFigure();        
            arrow.AddPolygon(points);
            Matrix matrix = new Matrix();         
            matrix.Rotate(angle);
            //Debug.WriteLine(DateTime.Now.Minute + ":" + DateTime.Now.Second + " angle: " + minAngle);
            arrow.Transform(matrix);
            graphics.FillPolygon(brush, arrow.PathPoints);
        }
    }
}
