using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;

namespace boutonuser_control_2015
{
    public partial class JaugeComposant : UserControl
    {
        //DialColor	Color Gets or Sets the background color for the gauge.
        private Color _dialColor = Color.PaleTurquoise;
        [Browsable(true), DefaultValue(0), Category("Jauge")]
        [Description("Fixe ou obtient la couleur de la gauge.")]
        public Color DialColor
        {
            get {   return _dialColor; }
            set {
                    _dialColor = value;
                    Invalidate();
                }
        }

        //DialText String Gets or Sets the Text displayed on the gauge dial.
        private string _dialText = "Ma belle jauge";
        [Browsable(true), DefaultValue(0), Category("Jauge")]
        [Description("Fixe ou obtient le text sur la gauge.")]
        public string DialText
        {
            get { return _dialText; }
            set { _dialText = value; Invalidate(); }
        }

        //MaxValue	float	Gets or Sets the maximum value shown on the gauge scale.
        private float _max = 100;
        [Browsable(true), DefaultValue(0), Category("Jauge")]
        [Description("Fixe ou obtient la valeur maximun qui se montre dans la gauge.")]
        [Required(ErrorMessage = "Max {0} est obligatoire")]
        public float Max
        {
            get { return _max; }
            set
            {
                    value = ((value < 0)      ? _max : value);
                    value = ((value == 0)     ? 100 : value);
                    value = ((value >= 10000) ? 100 : value);
                    value = ((value <= Min)   ? _max : value);
                    _valeur = ((_valeur > Max) ? 0 : _valeur);
                    _max = value;
                    Invalidate();
             }
        }

        //MinValue	float Gets or Sets the minimum value shown on the gauge scale.
        private float _min = 0;
        [Browsable(true), DefaultValue(0), Category("Jauge")]
        [Description("Fixe ou obtient la valeur minimum qui se montre dans la gauge.")]
        [Required(ErrorMessage = "Min {0} est obligatoire")]
        public float Min
        {
            get { return _min; }
            set
            {
                value = ((value < 0) ? _min : value);
                value = ((value == 0) ? 0 : value);
                value = ((value >= Max) ? _min : value);
                _valeur = ((_valeur < _min) ? 0 : _valeur-(int)_min);
                _min = value;
                Invalidate();
            }
        }

        //Value	float Gets or Sets the value to which the pointer will point.
        private int _valeur = 0;
        [Browsable(true), DefaultValue(0), Category("Jauge")]
        [Description("Fixe ou obtient la valeur (puissance) ou doit pointer la gauge.")]
        [Required(ErrorMessage = "Valeur {0} est obligatoire")]
        public int Valeur
        {
            get { return _valeur; }
            set
            {
                value = ((value > Max) ? 0 : value);
                value = ((value < Min) ? 0 : value);
                value = value - (int)Min;
                _valeur = ((value < 0) ? 0 : value);
                Invalidate();
            }
        }
        //NoOfDivisions	int	Gets or Sets the number of divisions on the gauge scale.
        private int _noOfDivisions = 5;
        [Browsable(true), DefaultValue(0), Category("Jauge")]
        [Description("Fixe ou obtient le nombre des divisions sur la gauge.")]
        public int NoOfDivisions
        {
            get { return _noOfDivisions; }
            set
            {
                value = ((value < 0) ? -value : value);
                value = ((value == 0) ? 1 : value);
                value = ((value >= Max) ? 5 : value);
                _noOfDivisions = value;
                Invalidate();
            }
        }

        //NoOfSubDivisions	int	Gets or Sets the number of subdivisions displayed on the scale for each division.
        private int _noOfSubDivisions = 5;
        [Browsable(true), DefaultValue(1), Category("Jauge")]
        [Description("Fixe ou obtient le nombre des soubdivisions sur la gauge.")]
        public int NoOfSubDivisions
        {
            get { return _noOfSubDivisions; }
            set
            {
                value = ((value < 0) ? -value : value);
                value = ((value == 0) ? 1 : value);
                value = ((value >= Max) ? 5 : value);
                _noOfSubDivisions = value;
                Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            //premier grand rectangle Cyan
            float hauteur1 = Height - 0.1f * Width;
            float largeur1 = 0.9f * Width;
            float x1 = 0.05f * Width;
            float y1 = 0.05f * Width;
            SolidBrush brush = new SolidBrush(DialColor);
            graphics.FillRectangle(brush, x1, y1, largeur1, hauteur1);

            //les 4 cercles qui decorent le grand rectangle
            Pen penCercles = new Pen(System.Drawing.Color.Black, 1);
            float diametreCercles = 0.06f * Width;

            //cercle 1 Blue
            Color backCercle1VColor = Color.Blue;
            SolidBrush brushCercle1 = new SolidBrush(backCercle1VColor);
            float xC1 = x1 - 0.6f * diametreCercles;
            float yC1 = x1 - 0.6f * diametreCercles;
            graphics.FillEllipse(brushCercle1, xC1, yC1, diametreCercles, diametreCercles);
            graphics.DrawEllipse(penCercles, xC1, yC1, diametreCercles, diametreCercles);

            //cercle 2 Jeune
            Color backCercle2VColor = Color.Yellow;
            SolidBrush brushCercle2 = new SolidBrush(backCercle2VColor);
            float xC2 = xC1 + largeur1;
            float yC2 = yC1;
            graphics.FillEllipse(brushCercle2, xC2, yC2, diametreCercles, diametreCercles);
            graphics.DrawEllipse(penCercles, xC2, yC2, diametreCercles, diametreCercles);

            //cercle 3 Vert
            Color backCercle3VColor = Color.Green;
            SolidBrush brushCercle3 = new SolidBrush(backCercle3VColor);
            float xC3 = xC1;
            float yC3 = yC1 + hauteur1;
            graphics.FillEllipse(brushCercle3, xC3, yC3, diametreCercles, diametreCercles);
            graphics.DrawEllipse(penCercles, xC3, yC3, diametreCercles, diametreCercles);

            //cercle 4 Rouge
            Color backCercle4VColor = Color.Red;
            SolidBrush brushCercle4 = new SolidBrush(backCercle4VColor);
            float xC4 = xC2;
            float yC4 = yC3;
            graphics.FillEllipse(brushCercle4, xC4, yC4, diametreCercles, diametreCercles);
            graphics.DrawEllipse(penCercles, xC4, yC4, diametreCercles, diametreCercles);

            //deuxieme moyen rectangle blanc
            float hauteur2 = 0.1f * Width;
            float largeur2 = 0.9f * largeur1;
            float x2 = x1 + 0.05f * largeur1;
            float y2 = y1 + 0.5f * hauteur1 - 0.5f * hauteur2;
            Color backRect2Color = Color.White;
            SolidBrush brushRec2 = new SolidBrush(backRect2Color);
            Pen penRec2 = new Pen(Color.Black);
            graphics.FillRectangle(brushRec2, x2, y2, largeur2, hauteur2);
            graphics.DrawRectangle(penRec2, x2, y2, largeur2, hauteur2);

            //l'etiquette qui s'affiche (puissance) 
            Color labelColor = Color.Red;
            SolidBrush brushLabel = new SolidBrush(labelColor);
            int sizeFontLabel = (int)(5 + (0.01* Width));
            Font fontLabel = new Font("Arial", sizeFontLabel, FontStyle.Bold);

            using (Font font1 = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point))
            {
                Rectangle rectLabel = new Rectangle((int)(xC3+(diametreCercles/2)), (int)yC3-50, (int)xC4, 40);

                // Create a StringFormat object with the each line of text, and the block
                // of text centered on the page.
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                // Draw the text and the surrounding rectangle.
                e.Graphics.DrawString(DialText, fontLabel, brushLabel, rectLabel, stringFormat);
                 e.Graphics.DrawRectangle(Pens.Transparent, rectLabel);
            }

            //l'etiquette des nombres
            Brush brushLabelNumbers = new SolidBrush(this.ForeColor);
            float sizeFontLabelNumbers = sizeFontLabel;
            Font fontLabelNumbers = new Font("Arial", sizeFontLabelNumbers, FontStyle.Bold);
            StringFormat strFormatLabelNumbers = new StringFormat(StringFormatFlags.NoClip);
            strFormatLabelNumbers.Alignment = StringAlignment.Center;

            float scale = Min;
            Pen penLinesDiv;
            Pen penLinesSousDiv = new Pen(Color.Yellow);
            penLinesSousDiv.Width = 2;

            float xL1, yL1, xL2, yL2;
            float xSL1, ySL1, xSL2, ySL2;

            for (int i = 0; i <= NoOfDivisions; i++)
            {
                //dessiner les divisions
                penLinesDiv = new Pen(Color.Red);
                penLinesDiv.Width = 4;

                xL1 = (float)(x2 + (i * largeur2) / NoOfDivisions);
                yL1 = (float)(y2 - hauteur1 / 10);

                xL2 = (float)(x2 + (i * largeur2) / NoOfDivisions);
                yL2 = (float)(y2 + largeur2 / 5);

                //dessiner les sous-divisions
                for (int j = 0; (j <= NoOfSubDivisions && i!= NoOfDivisions); j++)
                {
                    xSL1 = (float)(xL1 + ((j * largeur2) / NoOfDivisions)/NoOfSubDivisions);
                    ySL1 = (float)(y2 - hauteur1 / 25);

                    xSL2 = (float)(xL2  + ((j * largeur2) / NoOfDivisions)/ NoOfSubDivisions);
                    ySL2 = (float)(y2 + largeur2 / 7);

                    graphics.DrawLine(penLinesSousDiv, xSL1, ySL1, xSL2, ySL2);
                }

                if (i == 0 || i == NoOfDivisions)
                {
                    penLinesDiv = new Pen(Color.Black);
                    penLinesDiv.Width = 8;
                }

                graphics.DrawLine(penLinesDiv, xL1, yL1, xL2, yL2);

                scale += ((i != 0) ? (float)((Max - Min) / NoOfDivisions):0);

                int scaleInt = (int)scale;

                graphics.DrawString(scaleInt.ToString(), fontLabelNumbers, brushLabelNumbers, xL1, yL1-30, strFormatLabelNumbers);
            }

            //rectangle selon la valeur donc la puissance de la gauge Orange
            float hauteurV = 0.1f * hauteur1;
            float largeurV = (largeur2 * (Valeur) / (Max - Min));
            float xV = x2;
            float yV = y1 + 0.45f * hauteur1;
            Color backRectVColor = Color.Orange;
            SolidBrush brushRecV = new SolidBrush(backRectVColor);
            for (int i = 0; i < largeurV; i++)
            {
                graphics.FillRectangle(brushRecV, xV, yV, i, hauteurV);
                System.Threading.Thread.Sleep(1);
            }

        }
        public JaugeComposant()
        {
            InitializeComponent();
        }
    }
}
