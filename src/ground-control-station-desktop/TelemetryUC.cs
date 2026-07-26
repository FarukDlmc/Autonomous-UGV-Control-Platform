using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutonomousControlStation
{
    public partial class TelemetryUC : UserControl
    {
        // Aracın anlık yönü (Şimdilik örnek olarak 45 derece yani Kuzeydoğu verelim)
        float currentYaw = 45.0f;
        Random rnd = new Random();
        public TelemetryUC()
        {
            InitializeComponent();
        }

        private void panelMotorTicks_Paint(object sender, PaintEventArgs e)
        {
            // Çentiklerin rengi: Yarı saydam gri
            Pen tickPen = new Pen(Color.FromArgb(100, 255, 255, 255), 2);
            int gap = 20; // Çentikler arası boşluk (Piksel)

            for (int i = 0; i < ((Panel)sender).Height; i += gap)
            {
                // 0'dan başla, 8 piksel uzunluğunda yatay çizgi çek
                e.Graphics.DrawLine(tickPen, 0, i, 8, i);
            }
        }

        private void panelMotorTicks2_Paint(object sender, PaintEventArgs e)
        {
            // Çentiklerin rengi: Yarı saydam gri
            Pen tickPen = new Pen(Color.FromArgb(100, 255, 255, 255), 2);
            int gap = 20; // Çentikler arası boşluk (Piksel)

            for (int i = 0; i < ((Panel)sender).Height; i += gap)
            {
                // 0'dan başla, 8 piksel uzunluğunda yatay çizgi çek
                e.Graphics.DrawLine(tickPen, 0, i, 8, i);
            }
        }

        private void panelPitchTicks_Paint(object sender, PaintEventArgs e)
        {
            Pen tickPen = new Pen(Color.FromArgb(100, 255, 255, 255), 2);
            int gap = 20;

            for (int i = 0; i < ((Panel)sender).Width; i += gap)
            {
                // 0'dan başla, 8 piksel uzunluğunda dikey çizgi çek
                e.Graphics.DrawLine(tickPen, i, 0, i, 8);
            }
        }

        private void panelRollTicks_Paint(object sender, PaintEventArgs e)
        {
            Pen tickPen = new Pen(Color.FromArgb(100, 255, 255, 255), 2);
            int gap = 20;

            for (int i = 0; i < ((Panel)sender).Width; i += gap)
            {
                // 0'dan başla, 8 piksel uzunluğunda dikey çizgi çek
                e.Graphics.DrawLine(tickPen, i, 0, i, 8);
            }
        }

        private void TelemetryUC_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");

            // 2. TEST: Pusula ibresi her saniye 5 derece dönsün (Araç kendi etrafında dönüyor gibi)
            int sapma = rnd.Next(-10, 11); // -10 ile +10 arasında rastgele sayı üretir

            // Yönümüzü sıfır noktası (Kuzey) + sapma olarak ayarlıyoruz
            currentYaw = 0f + sapma;

            // 3. Pusula paneline "kendini baştan çiz" diyoruz.
            pnlCompass.Invalidate();
        }

        private void pnlCompass_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // Çizgilerin piksel piksel tırtıklı olmaması, jilet gibi (Anti-Alias) görünmesi için:
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int width = pnlCompass.Width;
            int height = pnlCompass.Height;
            int cx = width / 2; // Merkez X
            int cy = height / 2; // Merkez Y
            int radius = Math.Min(cx, cy) - 20; // Yarıçap

            // 1. Dış Çerçeve (Neon Mavi)
            Pen outerPen = new Pen(Color.FromArgb(0, 229, 255), 2);
            g.DrawEllipse(outerPen, cx - radius, cy - radius, radius * 2, radius * 2);

            // Fontlar ve Fırçalar
            Font fontDirection = new Font("Segoe UI", 14, FontStyle.Bold);
            Brush brushWhite = Brushes.White;

            // 2. 360 Derecelik Çentikleri ve Harfleri Çizme
            for (int i = 0; i < 360; i += 15)
            {
                // Matematiğin kalbi: Açıyı radyana çevir (0 derece Kuzey olacak şekilde -90 kaydırıyoruz)
                double rad = (i - 90) * Math.PI / 180.0;

                // Çizginin dış başlangıç noktası
                int x1 = cx + (int)((radius) * Math.Cos(rad));
                int y1 = cy + (int)((radius) * Math.Sin(rad));
                int x2, y2;

                if (i % 90 == 0) // Ana Yönler (0, 90, 180, 270)
                {
                    // Uzun çizgi
                    x2 = cx + (int)((radius - 15) * Math.Cos(rad));
                    y2 = cy + (int)((radius - 15) * Math.Sin(rad));
                    g.DrawLine(new Pen(Color.White, 2), x1, y1, x2, y2);

                    // N, E, S, W Harflerini yazma
                    string dir = "";
                    if (i == 0) dir = "N";
                    if (i == 90) dir = "E";
                    if (i == 180) dir = "S";
                    if (i == 270) dir = "W";

                    SizeF textSize = g.MeasureString(dir, fontDirection);
                    int tx = cx + (int)((radius - 35) * Math.Cos(rad)) - (int)(textSize.Width / 2);
                    int ty = cy + (int)((radius - 35) * Math.Sin(rad)) - (int)(textSize.Height / 2);

                    // Kuzey (N) harfini neon yeşil yapıyoruz ki vurgulu olsun
                    g.DrawString(dir, fontDirection, (i == 0) ? new SolidBrush(Color.FromArgb(0, 255, 102)) : brushWhite, tx, ty);
                }
                else if (i % 30 == 0) // Ara Dereceler (30, 60, 120...)
                {
                    x2 = cx + (int)((radius - 10) * Math.Cos(rad));
                    y2 = cy + (int)((radius - 10) * Math.Sin(rad));
                    g.DrawLine(new Pen(Color.FromArgb(139, 141, 155), 1.5f), x1, y1, x2, y2);
                }
                else // Ufak Çentikler (15, 45, 75...)
                {
                    x2 = cx + (int)((radius - 5) * Math.Cos(rad));
                    y2 = cy + (int)((radius - 5) * Math.Sin(rad));
                    g.DrawLine(new Pen(Color.FromArgb(80, 80, 80), 1), x1, y1, x2, y2);
                }
            }

            // 3. Aracın Anlık Yön İbresi (Needle)
            double yawRad = (currentYaw - 90) * Math.PI / 180.0;
            int nx = cx + (int)((radius - 40) * Math.Cos(yawRad));
            int ny = cy + (int)((radius - 40) * Math.Sin(yawRad));

            Pen needlePen = new Pen(Color.FromArgb(255, 0, 85), 3); // Neon Pembe İbre
            needlePen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor; // Ucunu ok işareti yap
            g.DrawLine(needlePen, cx, cy, nx, ny);

            // İbrenin merkezindeki mekanik vida/nokta
            g.FillEllipse(Brushes.White, cx - 5, cy - 5, 10, 10);
        }
    }
}
