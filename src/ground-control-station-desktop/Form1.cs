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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DashboardUC dsh = new DashboardUC(); //uygulama açıldığında dashboard sayfası gelsin diye
            AddUserControl(dsh);
        }
        private void AddUserControl(UserControl uc)
        {
            // Önce container içindeki eski sayfayı temizle
            pnlOrta.Controls.Clear();

            // Yeni sayfayı container'a tam sığacak şekilde ayarla
            uc.Dock = DockStyle.Fill;

            // Yeni sayfayı container'ın içine ekle ve öne getir
            pnlOrta.Controls.Add(uc);
            uc.BringToFront();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            DashboardUC dsh = new DashboardUC();
            AddUserControl(dsh);
        }

        private void btnVision_Click(object sender, EventArgs e)
        {
            AIVisionUC aiv = new AIVisionUC();
            AddUserControl(aiv);
        }

        private void btnTelemetry_Click(object sender, EventArgs e)
        {
            TelemetryUC tel = new TelemetryUC();
            AddUserControl(tel);
        }

        private void btnSystemLogs_Click(object sender, EventArgs e)
        {
            SystemLogsUC logs = new SystemLogsUC();
            AddUserControl (logs);
        }

        private void btnControlPanel_Click(object sender, EventArgs e)
        {
            ControlPanelUC conp = new ControlPanelUC();
            AddUserControl(conp);
        }
    }
}
