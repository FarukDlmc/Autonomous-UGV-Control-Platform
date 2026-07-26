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
    public partial class SystemLogsUC : UserControl
    {
        public SystemLogsUC()
        {
            InitializeComponent();
        }

        private void SystemLogsUC_Load(object sender, EventArgs e)
        {
            dgvLogs.Columns["clmnTime"].Width = 120;
            dgvLogs.Columns["clmnModule"].Width = 140;
            dgvLogs.Columns["clmnLevel"].Width = 100;
            dgvLogs.Columns["clmnMessage"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            
            LoadDummyLogs(); // Sahte logları tabloya ekle
            ColorizeTerminal(); // Log seviyelerine göre renkleri uygula
        }
        // 1. Tabloya sahte UGV (İnsansız Kara Aracı) logları ekleyen fonksiyon
        private void LoadDummyLogs()
        {
            // Tabloyu temizle (varsa eski verileri uçurur)
            dgvLogs.Rows.Clear();

            // dgvLogs.Rows.Add("SAAT", "MODÜL", "SEVİYE", "MESAJ");
            dgvLogs.Rows.Add(DateTime.Now.ToString("HH:mm:ss.fff"), "SYS_BOOT", "INFO", "System initialized. Boot sequence completed.");
            dgvLogs.Rows.Add(DateTime.Now.AddMilliseconds(150).ToString("HH:mm:ss.fff"), "GPS_MODULE", "INFO", "Satellites acquired: 12. Fix: 3D. Precision: 2.5m.");
            dgvLogs.Rows.Add(DateTime.Now.AddMilliseconds(420).ToString("HH:mm:ss.fff"), "AI_VISION", "INFO", "YOLOv8 engine started. Camera feed active at 30 FPS.");
            dgvLogs.Rows.Add(DateTime.Now.AddSeconds(2).ToString("HH:mm:ss.fff"), "MOTOR_CTRL", "WARNING", "Right track PWM sync delay detected (15ms). Compensating...");
            dgvLogs.Rows.Add(DateTime.Now.AddSeconds(5).ToString("HH:mm:ss.fff"), "TELEMETRY", "INFO", "Connection established with Ground Control Station.");
            dgvLogs.Rows.Add(DateTime.Now.AddSeconds(12).ToString("HH:mm:ss.fff"), "LIDAR_SENS", "ERROR", "Connection timeout on COM4. Scanning failed.");
            dgvLogs.Rows.Add(DateTime.Now.AddSeconds(13).ToString("HH:mm:ss.fff"), "AI_VISION", "CRITICAL", "OBSTACLE COLLISION IMMINENT! Distance < 1m.");
            dgvLogs.Rows.Add(DateTime.Now.AddSeconds(14).ToString("HH:mm:ss.fff"), "SYS_MAIN", "INFO", "Auto-braking engaged. Switching to manual override.");
        }

        // 2. Tablodaki verileri seviyelerine (LEVEL) göre boyayan efsanevi fonksiyon
        private void ColorizeTerminal()
        {
            // Tablodaki her bir satırı tek tek gezer
            foreach (DataGridViewRow row in dgvLogs.Rows)
            {
                // Eğer satır boşsa veya yeni satırsa işlem yapma
                if (row.IsNewRow || row.Cells["clmnLevel"].Value == null) continue;

                string level = row.Cells["clmnLevel"].Value.ToString();

                // LEVEL hücresindeki yazıya göre o satırın rengini değiştir
                switch (level)
                {
                    case "INFO":
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(0, 255, 102); // Matrix Yeşili
                        break;
                    case "WARNING":
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 215, 0); // Uyarı Sarısı
                        break;
                    case "ERROR":
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 0, 85); // Neon Kırmızı/Pembe
                        break;
                    case "CRITICAL":
                        // Kritik hatada satırın arka planı kırmızımsı, yazısı beyaz olur (Dikkat çeksin diye)
                        row.DefaultCellStyle.BackColor = Color.FromArgb(80, 20, 30);
                        row.DefaultCellStyle.ForeColor = Color.White;
                        break;
                    default:
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(192, 198, 209); // Bilinmeyen seviyede Gri kalsın
                        break;
                }
            }

            // Tabloda varsayılan seçili gelen o mavi satır rengini kaldırmak için seçimi iptal et
            dgvLogs.ClearSelection();
        }
    }
}
