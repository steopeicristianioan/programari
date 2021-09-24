using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RoomReservation
{
    class RServiceCard : Panel
    {
        public ProfileView view;
        private Panel parent;
        private int status;
        public Panel Parentt { get => this.parent; set => this.parent = value; }
        private ReservationService service;

        public RServiceCard(Panel parent, ReservationService service)
        {
            this.parent = parent;
            this.service = service;

            this.Parent = parent;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = ColorTranslator.FromHtml("#3D84B8");
            this.ForeColor = Color.White;
        }

        public void setLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        private void loadGeneral()
        {
            Label label = new Label();
            label.Parent = this;
            label.Location = new Point(5, 5);
            label.Width = 100;
            label.Height = 60;
            label.Text = "Reservation #" + this.service.ID;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void loadRoom()
        {
            Label label = new Label();
            label.Parent = this;
            label.Location = new Point(110, 5);
            label.Width = 60;
            label.Height = 60;
            label.Text = "Room #" + this.service.RoomId;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void loadTimeSpan()
        {
            ControlReservations reservations = new ControlReservations();
            Reservation reservation = reservations.getById(this.service.ReservationId);
            if (!reservation.hasStarted() && !reservation.hasEnded())
                this.status = 2;
            else if (reservation.IsActive == 1)
               this.status = 1;
            else if(status != 2) this.status = 0;

            Label label = new Label();
            label.Parent = this;
            label.Location = new Point(180, 5);
            label.Width = 200;
            label.Height = 60;
            label.Text = "Duration: \n" + reservation.BookDate.ToString("f") + "\n-\n" + reservation.ReleaseDate.ToString("f");
            label.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void loadStatus()
        {
            Label label = new Label();
            label.Parent = this;
            label.Location = new Point(390, 5);
            label.Width = 200;
            label.Height = 60;
            label.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);

            PictureBox box = new PictureBox();
            box.Parent = label;
            box.Location = new Point(15, 6);
            box.Height = 50;
            box.Width = 50;

            if (this.status == 2)
            {
                box.Image = Image.FromFile(Application.StartupPath + "\\resources\\cancel.png");

                Label text = new Label();
                text.Parent = label;
                text.Location = new Point(65, 0);
                text.Height = 60;
                text.Width = 135;
                text.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
                text.Text = "Not started\nClick here to remove";
                text.TextAlign = ContentAlignment.MiddleLeft;
                text.Click += new EventHandler(this.remove_ByClick);
                text.ForeColor = ColorTranslator.FromHtml("#EBE545");
                label.Click += new EventHandler(this.remove_ByClick);

            }
            else if (this.status == 1)
            {
                box.Image = Image.FromFile(Application.StartupPath + "\\resources\\active.png");

                label.ForeColor = ColorTranslator.FromHtml("#41CD0F");
                label.ForeColor = Color.Lime;
                label.Text = "Active";
                label.TextAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                box.Image = Image.FromFile(Application.StartupPath + "\\resources\\x.png");

                label.ForeColor = ColorTranslator.FromHtml("#CC3333");
                label.Text = "Expired";
                label.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        public void loadCard()
        {
            this.Width = 555;
            this.Height = 70;
            loadGeneral();
            loadRoom();
            loadTimeSpan();
            loadStatus();
        }

        public void remove_ByClick(object sender, EventArgs e)
        {
            ControlReservationService control = new ControlReservationService();
            control.remove(service.ID);
            ControlReservations reservations = new ControlReservations();
            reservations.remove(service.ReservationId);

            ProfileService profile = new ProfileService(this.view);
            profile.removeReservation(service);
        }
    }
}
