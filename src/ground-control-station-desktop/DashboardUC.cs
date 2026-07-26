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
    public partial class DashboardUC : UserControl
    {
        public DashboardUC()
        {
            InitializeComponent();
        }

        private void DashboardUC_Load(object sender, EventArgs e)
        {
            cartesianChart1.Series.Clear();

            // Arka planı WPF mimarisine uygun olarak transparan (şeffaf) yapıyoruz
            cartesianChart1.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);

            // X ve Y Eksenlerini tasarlama (Çizgileri gizleyip yazıları açık gri yapma)
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Time",
                Labels = new[] { "1", "2", "3", "4", "5" }, // Şimdilik varsayılan
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(139, 141, 155)), // #Gri
                ShowLabels = true,
                Separator = new LiveCharts.Wpf.Separator
                {
                    StrokeThickness = 0,
                    Step = 1 // Tüm yazıları atlamadan gösterir
                },
                LabelsRotation = 15     // Yazıları 15 derece eğik yazar
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Speed (m/s)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(139, 141, 155)),
                Separator = new LiveCharts.Wpf.Separator
                {
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 255, 255, 255)) // Y ekseni çizgileri çok hafif transparan beyaz
                }
            });

            // Neon Yeşil renkli çizgi (Seri) ekleme
            cartesianChart1.Series.Add(new LiveCharts.Wpf.LineSeries
            {
                Title = "Speed",
                Values = new LiveCharts.ChartValues<double> { 0.5, 1.2, 1.8, 1.5, 2.1 }, // Şimdilik örnek veri
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 102)), // #00FF66 Neon Yeşil Çizgi
                StrokeThickness = 3, // Çizgi kalınlığı
                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 0, 255, 102)), // Çizginin altını hafif yeşile boyar
                PointGeometrySize = 10 // Noktaların boyutu
            });



            // --- GRAFİK 2: YAPAY ZEKA TESPİT DAĞILIMI (AI DETECTIONS) ---
            cartesianChart2.Series.Clear();
            cartesianChart2.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);

            // X Ekseni (Tespit Edilen Nesne Sınıfları)
            cartesianChart2.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Detected Classes",
                Labels = new[] { "Person", "Vehicle", "Sign", "Obstacle" }, // YOLO'nun bulacağı şeyler
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(139, 141, 155)),
                Separator = new LiveCharts.Wpf.Separator
                {
                    StrokeThickness = 0,
                    Step = 1
                },
                LabelsRotation = 15     // Yazıları 15 derece eğik yazar
            });

            // Y Ekseni (Adet / Sayı)
            cartesianChart2.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Count",
                MinValue = 0, // Grafik eksi değerlere inmesin diye 0'a sabitliyoruz
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(139, 141, 155)),
                Separator = new LiveCharts.Wpf.Separator
                {
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(50, 255, 255, 255))
                }
            });

            // Sütun (Bar) Serisi - Neon Pembe/Kırmızı Vurgu
            cartesianChart2.Series.Add(new LiveCharts.Wpf.ColumnSeries
            {
                Title = "Detections",
                Values = new LiveCharts.ChartValues<int> { 12, 5, 2, 8 }, // Örnek tespit sayıları
                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 85)), // #FF0055 Neon Pembe
                MaxColumnWidth = 40, // Sütunlar çok şişman durmasın diye inceltiyoruz
                DataLabels = true // Sütunların tam tepesinde rakamlar yazsın
            });



            // --- GRAFİK 3: SİSTEM YÜKÜ (RASPBERRY PI CPU GAUGE) ---

            // Başlangıç ve Bitiş değerleri (Yüzdelik dilim)
            solidGauge1.From = 0;
            solidGauge1.To = 100;

            // O anki değer (Örneğin Raspberry Pi şu an %65 kapasiteyle çalışıyor)
            solidGauge1.Value = 65;

            // Arka planı transparan yapma (Eğer hata verirse bu satırı silip Properties'den BackColor'ı şeffaf yapabilirsin)
            solidGauge1.BackColor = System.Drawing.Color.Transparent;

            // İçindeki rakamın (65) ve köşelerdeki min/max değerlerinin rengini Bembeyaz yapıyoruz:
            solidGauge1.Base.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
        }
    }
}
