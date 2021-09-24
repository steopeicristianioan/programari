using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RoomReservation
{
    class ProfileView:ViewPanel
    {
        private ProfileService service;
        private Customer customer;
        public HomeView home;
        public HomeView Home { get => this.home; set => this.home = value; }
        public Customer Customer { get => this.customer; set => this.customer = value; }
        public ProfileView(HomeView view)
        {
            this.home = view;
            this.home.Controls.Clear();
            this.home.MinimumSize = new Size(470, 550);
            this.home.MaximumSize = this.home.MinimumSize;
            this.home.SetDesktopLocation(400, 70);
            this.userId = view.UserId;
            this.Parent = view;
            this.Location = new Point(1, 1);
            this.Width = 985;
            this.Height = 985;
            this.service = new ProfileService(this);
            setHeader();
            setMain();
            setFooter();
            ControlCustomers customers = new ControlCustomers();
            this.customer = customers.getById(this.userId);

            loadHeader();
            loadMain();
            loadFooter();
        }

        protected override void setHeader()
        {
            this.header = new Panel();
            this.header.Parent = this;
            this.header.Location = new Point(10, 10);
            this.header.Width = 435;
            this.header.Height = 50;
        }
        protected override void setMain()
        {
            this.main = new Panel();
            this.main.Parent = this;
            this.main.Location = new Point(10, 70);
            this.main.Width = 430;
            this.main.Height = 400;
            this.main.AutoScroll = true;
        }
        protected override void setFooter()
        {
            this.footer = new Panel();
            this.footer.Parent = this;
            this.footer.Location = new Point(10, 450);
            this.footer.Width = 965;
            this.footer.Height = 50;
        }

        private void loadWelcomeLabel()
        {
            ControlCustomers customers = new ControlCustomers();
            Customer customer = customers.getById(this.userId);

            Label welcome = new Label();
            welcome.Parent = this.header;
            welcome.Location = new Point(0, 13);
            welcome.Width = 550;
            welcome.Height = 90;
            welcome.Text = "Here you can manage your account, " + customer.FirstName + " " + customer.LastName + "!";
            welcome.Font = new Font("Microsoft San Serif", 13, FontStyle.Regular);
            welcome.TextAlign = ContentAlignment.TopLeft;
        }
        protected override void loadHeader()
        {
            loadWelcomeLabel();
        }

        private void loadSettings()
        {
            Label label = new Label();
            label.BackColor = Color.Transparent;
            label.ForeColor = ColorTranslator.FromHtml("#3D84B8");
            label.Parent = this.main;
            label.Location = new Point(100, 75);
            label.Width = 280;
            label.Height = 100;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = "My settings";
            label.Font = new Font("Microsoft Sitka Small", 14, FontStyle.Bold);
            label.Click += new EventHandler(this.service.settings_Click);

            PictureBox box = new PictureBox();
            box.Location = new Point(0, 15);
            box.Parent = label;
            box.Image = Image.FromFile(Application.StartupPath + "\\resources\\s70.png");
            box.Width = 70;
            box.Height = 70;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.Click += new EventHandler(this.service.settings_Click);
        }
        private void loadMyBookings()
        {
            Label label = new Label();
            label.BackColor = Color.Transparent;
            label.ForeColor = ColorTranslator.FromHtml("#3D84B8");
            label.Parent = this.main;
            label.Location = new Point(100, 230);
            label.Width = 280;
            label.Height = 100;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = "My bookings";
            label.Font = new Font("Microsoft Sitka Small", 15, FontStyle.Bold);
            label.Click += new EventHandler(service.booking_Click);

            PictureBox box = new PictureBox();
            box.Location = new Point(0, 15);
            box.Parent = label;
            box.Image = Image.FromFile(Application.StartupPath + "\\resources\\book.png");
            box.Width = 70;
            box.Height = 70;
            box.SizeMode = PictureBoxSizeMode.StretchImage;
            box.Click += new EventHandler(this.service.booking_Click);
        }
        private void loadBackHome()
        {
            Label home = new Label();
            home.BackColor = Color.Transparent;
            home.Parent = this.main;
            home.Location = new Point(0, 0);
            home.Width = 50;
            home.Height = 50;
            home.Image = Image.FromFile(Application.StartupPath + "\\resources\\h.png");
            home.Click += new EventHandler(service.home_Click);
        }

        protected override void loadMain()
        {
            loadSettings();
            loadMyBookings();
            loadBackHome();
        }
        protected override void loadFooter()
        {
            Label label = new Label();
            label.Parent = this.footer;
            label.Location = new Point(1, 1);
            label.Width = 350;
            label.Height = 46;
            label.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
            label.Text = "Cristi's booking service, all rights reserved";
            label.TextAlign = ContentAlignment.BottomLeft;
        }

        public void refresh()
        {
            this.home.MinimumSize = new Size(470, 550);
            this.home.MaximumSize = this.home.MinimumSize;
            this.home.SetDesktopLocation(400, 70);

            this.header.Controls.Clear();
            this.main.Controls.Clear();
            this.footer.Controls.Clear();

            loadHeader();
            loadMain();
            loadFooter();
        }

        public void resizeHeader()
        {
            this.header.Controls.Clear();

            Label welcome = new Label();
            welcome.Parent = this.header;
            welcome.Location = new Point(170, 13);
            welcome.Width = 550;
            welcome.Height = 90;
            welcome.Text = "Here you can manage your account, " + customer.FirstName + " " + customer.LastName + "!";
            welcome.Font = new Font("Microsoft San Serif", 13, FontStyle.Regular);
            welcome.TextAlign = ContentAlignment.TopCenter;
        }
    }
}
