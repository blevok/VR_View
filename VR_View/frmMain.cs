using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VR_View
{
    public partial class frmMain : Form
    {
        private bool isWorking;
        private bool isTakingScreenshots;
        private bool isPreview;
        private bool isMouseCapture;
        private object locker = new object();
        private ReaderWriterLock rwl = new ReaderWriterLock();
        private MemoryStream img;

        public frmMain()
        {
            InitializeComponent();

            // for movable semi-borderless form
            this.FormBorderStyle = FormBorderStyle.None;
            //

            CheckForIllegalCrossThreadCalls = false; // For Visual Studio Debuging Only !

            img = new MemoryStream();
            isPreview = true;
            isMouseCapture = true;

            foreach (var screen in Screen.AllScreens)
            {
                comboScreens.Items.Add(screen.DeviceName.Replace("\\","").Replace(".",""));
            }
            comboScreens.SelectedIndex = 0;
        }

        // begin make semi-borderless form movable and resizable
        const int WM_NCHITTEST = 0x0084;
        const int HTCLIENT = 1;
        const int HTCAPTION = 2;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    if (m.Result == (IntPtr)HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                    }
                    break;
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x40000;
                return cp;
            }
        }
        // end make semi-borderless form movable and resizable

        private void btnStartViewer_Click(object sender, EventArgs e)
        {
            if (btnStartViewer.Tag.ToString() != "start")
            {
                btnStartViewer.Tag = "start";
                btnStartViewer.Text = "Start Viewer";
                isWorking = false;
                isTakingScreenshots = false;
                return;
            }

            try
            {
                isTakingScreenshots = true;
                isWorking = true;
                Task.Factory.StartNew(() => CaptureScreenEvery((int)numShotEvery.Value)).Wait();
                btnStartViewer.Tag = "stop";
                btnStartViewer.Text = "Stop Viewer";
            }
            catch
            {

            }
        }

        private async Task CaptureScreenEvery(int msec)
        {
            while (isWorking)
            {
                if (isTakingScreenshots)
                {
                    TakeScreenshot(isMouseCapture);
                    msec = (int)numShotEvery.Value;
                    await Task.Delay(msec);
                }
            }
        }
        private void TakeScreenshot(bool captureMouse)
        {
            if (captureMouse)
            {
                var bmp = ScreenCapturePInvoke.CaptureSelectedScreen(true,comboScreens.SelectedIndex);
                rwl.AcquireWriterLock(Timeout.Infinite);
                bmp.Save(Application.StartupPath + "/" + "/VR_View.jpg", ImageFormat.Jpeg);
                rwl.ReleaseWriterLock();
                
                if (isPreview)
                {
                    img = new MemoryStream();
                    bmp.Save(img, ImageFormat.Jpeg);
                    imgPreview.Image = new Bitmap(img);
                    imgPreview2.Image = new Bitmap(img);
                }
                bmp.Dispose();
                bmp = null;
                return;
            }
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                rwl.AcquireWriterLock(Timeout.Infinite);
                bitmap.Save(Application.StartupPath + "/" + "/VR_View.jpg", ImageFormat.Jpeg);
                rwl.ReleaseWriterLock();

                if (isPreview)
                {
                    img = new MemoryStream();
                    bitmap.Save(img, ImageFormat.Jpeg);
                    imgPreview.Image = new Bitmap(img);
                    imgPreview2.Image = new Bitmap(img);
                }


            }
        }

        private void btnStopViewer_Click(object sender, EventArgs e)
        {
            isWorking = false;
            isTakingScreenshots = false;
            btnStartViewer.Enabled = true;
            btnStopServer.Enabled = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        // close button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isMaximized = false;

        // maximize button
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pictureBox5.Visible = false;
            pictureBox7.Visible = true;
            isMaximized = true;
        }

        //minimize button
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // unmaximize button
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            isMaximized = false;
            this.WindowState = FormWindowState.Normal;
            pictureBox5.Visible = true;
            pictureBox7.Visible = false;
        }

        // hide settings button (gear)
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            label7.Visible = false;
            comboScreens.Visible = false;
            numShotEvery.Visible = false;
            label6.Visible = false;
            btnStartViewer.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox4.Visible = true;
            pictureBox3.Visible = false;
            pictureBox6.Visible = false;
            pictureBox5.Visible = false;
            pictureBox7.Visible = false;
            label9.Visible = false;
            versionLabel.Visible = false;
        }

        // show settings button (arrow thingy)
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            label7.Visible = true;
            comboScreens.Visible = true;
            numShotEvery.Visible = true;
            label6.Visible = true;
            btnStartViewer.Visible = true;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
            pictureBox6.Visible = true;
            label9.Visible = true;
            versionLabel.Visible = true;

            if (isMaximized == true)
            {
                pictureBox7.Visible = true;
            }
            else
            {
                pictureBox5.Visible = true;
            }
        }

        // settings gear tooltip
        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            ToolTip ttshow = new ToolTip();
            ttshow.SetToolTip(this.pictureBox4, "Show Settings");
        }

        // settings arrows tooltip
        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            ToolTip tthide = new ToolTip();
            tthide.SetToolTip(this.pictureBox2, "Hide Settings");
        }
    }
}
