using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RoomReservation
{
    class RoomPreView:Panel
    {
        private Room room;
        private HomeView view;

        public RoomPreView(Room room, HomeView view)
        {
            this.room = room; 
            this.view = view;
            this.Parent = this.view.Main;
            this.Width = 925;
            this.Height = 200;
            this.BackColor = ColorTranslator.FromHtml("#344FA1");
        }

        public void setLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        
        private void loadPictureBox()
        {
            string url = this.room.Url;
            PictureBox box = new PictureBox();
            box.Parent = this;
            box.Location = new Point(70, 20);
            box.Width = 180;
            box.Height = 160;
            box.Image = Image.FromFile(url);
            box.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void loadDescription()
        {
            Label description = new Label();
            description.Parent = this;
            description.ForeColor = Color.White;
            description.Location = new Point(330, 20);
            description.Width = 300;
            description.Height = 160;
            int status = this.room.Status;
            double perHour = Math.Round(this.room.PricePerNight / 24, 2);
            description.Text = "Room #" + this.room.ID + " is a " + status + " star hotel-level room.\nIts price/night is " + this.room.PricePerNight + " $ (" + perHour + " $/hour)";
            description.TextAlign = ContentAlignment.MiddleCenter;
            description.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
            description.BackColor = Color.Transparent;
        }
        private void loadBookingOptions()
        {
            Label booking = new Label();
            booking.Parent = this;
            booking.Location = new Point(700, 50);
            booking.Width = 150;
            booking.Height = 100;
            booking.BackColor = Color.Transparent;
            booking.Image = Image.FromFile(Application.StartupPath + "\\resources\\calendar.png");
            booking.TextAlign = ContentAlignment.MiddleCenter;

            HomeService service = new HomeService(this.view);
            booking.Click += delegate (object sender2, EventArgs e2) { service.seeBookingOptions_Click(sender2, e2, this.room.ID); };
        }
        public void loadPreview()
        {
            this.loadPictureBox();
            this.loadDescription();
            this.loadBookingOptions();        
        }
    }
}
