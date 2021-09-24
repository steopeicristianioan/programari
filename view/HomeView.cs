using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RoomReservation
{
    class HomeView : View
    {
        private ProfileView profile;
        private LoginBox login;

        public PictureBox logout;
        public PictureBox prof;

        public ProfileView Profile { get => this.profile; set => this.profile = value; }
        private HomeService service;

        public HomeView(int userId)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.StartupPath + "\\resources\\bb.ico");
            this.Text = "Room Booking";
            this.userId = userId;
            this.service = new HomeService(this);

            this.main = new Panel();
            main.Parent = this;
            this.login = new LoginBox(this);
            this.BackColor = ColorTranslator.FromHtml("#F0EBCC");
        }

        private void loadWelcomeLabel()
        {
            ControlCustomers customers = new ControlCustomers();
            Customer customer = customers.getById(this.userId);

            Label welcome = new Label();
            welcome.Parent = this.header;
            welcome.Location = new Point(250, 13);
            welcome.Width = 450;
            welcome.Height = 90;
            welcome.Text = "Welcome to your account, " + customer.FirstName + " " + customer.LastName + "!";
            welcome.Font = new Font("Microsoft San Serif", 16, FontStyle.Regular);
            welcome.TextAlign = ContentAlignment.TopCenter;
        }
        public void loadProfileButton(int k)
        {
            PictureBox profile = new PictureBox();
            profile.Parent = this.header;
            profile.Location = new Point(910, 1);
            profile.Width = 50;
            profile.Height = 46;
            profile.Image = Image.FromFile(Application.StartupPath + "\\resources\\acc.png");
            profile.SizeMode = PictureBoxSizeMode.StretchImage;
            profile.TabStop = false;
            if(k==0)
                profile.Click += new EventHandler(this.service.directToProfile);

            ToolTip tip = new ToolTip();
            tip.AutoPopDelay = 5000;
            tip.InitialDelay = 1000;
            tip.ReshowDelay = 500;
            tip.ShowAlways = true;
            tip.SetToolTip(profile, "My profile");
            this.prof = profile;
        }
        public void loadLogout(int k)
        {
            PictureBox logout = new PictureBox();
            logout.Parent = this.header;
            logout.Location = new Point(5, 1);
            logout.Width = 45;
            logout.Height = 46;
            logout.Image = Image.FromFile(Application.StartupPath + "\\resources\\lgo.png");
            logout.SizeMode = PictureBoxSizeMode.StretchImage;
            if(k == 0)
                logout.Click += new EventHandler(this.service.directToLogout);


            ToolTip tip = new ToolTip();
            tip.AutoPopDelay = 5000;
            tip.InitialDelay = 1000;
            tip.ReshowDelay = 500;
            tip.ShowAlways = true;
            tip.SetToolTip(logout, "Logout");
            this.logout = logout;
        }
        public void loadLoggedOutMessage()
        {
            this.header.Controls.Clear();
            Label welcome = new Label();
            welcome.Parent = this.header;
            welcome.Location = new Point(250, 13);
            welcome.Width = 450;
            welcome.Height = 90;
            welcome.Text = "Welcome to Cristi's booking service, have fun!";
            welcome.Font = new Font("Microsoft San Serif", 13, FontStyle.Regular);
            welcome.TextAlign = ContentAlignment.TopCenter;
        }

        protected override void setHeader()
        {
            this.header = new Panel();
            this.header.Parent = this;
            this.header.Location = new Point(10, 10);
            this.header.Width = 965;
            this.header.Height = 50;
        }
        protected override void setMain()
        {
            this.main = new Panel();
            this.main.Parent = this;
            this.main.Location = new Point(10, 70);
            this.main.Width = 965;
            this.main.Height = 500;
            this.main.AutoScroll = true;
        }
        protected override void setFooter()
        {
            this.footer = new Panel();
            this.footer.Parent = this;
            this.footer.Location = new Point(10, 580);
            this.footer.Width = 965;
            this.footer.Height = 50;
        }
        protected override void loadHeader()
        {
            loadLoggedOutMessage();
        }
        protected override void loadMain()
        {
            this.login = new LoginBox(this);
        }
        protected override void loadFooter()
        {
            Label label = new Label();
            label.Parent = this.footer;
            label.Location = new Point(1, 1);
            label.Width = 500;
            label.Height = 46;
            label.Font = new Font("Microsoft Sitka Small", 13, FontStyle.Regular);
            label.Text = "Cristi's booking service, all rights reserved";
            label.TextAlign = ContentAlignment.BottomLeft;
        }

        public void refresh()
        {
            this.Controls.Clear();
            setHeader();
            setMain();
            setFooter();
            loadWelcomeLabel();
            loadProfileButton(0);
            loadLogout(0);
            ControlRooms rooms = new ControlRooms(this);
            rooms.loadAllRooms();
            loadFooter();
            this.MaximumSize = new Size(1000, 1000);
            this.MinimumSize = this.MaximumSize;
            this.CenterToScreen();
        }
    }
}
