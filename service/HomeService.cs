using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace RoomReservation
{
    class HomeService
    {
        private HomeView view;
        private Label[,] calendar;
        private Panel calendarPanel;
        private Panel confirmPanel = new Panel();
        public Panel ConfirmPanel { get => this.confirmPanel; set => this.confirmPanel = value; }
        private int roomNumber;

        private ReservationCard bookCard;
        private ReservationCard releaseCard;

        private int startI;
        private int startJ;
        private int endI;
        private int endJ;

        public int RoomNumber { get => this.roomNumber; set => this.roomNumber = value; }

        private int monthNumber = DateTime.Now.Month;
        private int year = DateTime.Now.Year;

        private DateTime bookDate = DateTime.MinValue;
        private DateTime releaseDate = DateTime.MaxValue;
        public DateTime BookDate { get => this.bookDate; set => this.bookDate = value; }
        public DateTime ReleaseDate { get => this.releaseDate; set => this.releaseDate = value; }

        public HomeService(HomeView view)
        {
            this.view = view;
            this.ConfirmPanel.Visible = false;
        }

        private void loadCalendarTitle()
        {
            Label title = new Label();
            this.calendarPanel.Controls.Add(title);
            title.Location = new Point(275, 10);
            title.Width = 253;
            title.Height = 30;
            title.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.monthNumber).ToUpper() + " - " + this.year;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Font = new Font("Microsoft Sitka Small", 11, FontStyle.Regular);
        }
        private void loadCalendar()
        {
            ControlReservationService service = new ControlReservationService();
            Lista<Reservation> allRoomReservations = service.allRoomReservations(this.roomNumber);
            ControlReservations reservations = new ControlReservations();
            
            int days = DateTime.DaysInMonth(this.year, this.monthNumber);
            int currentDay = 1, x = 20, y = 50;
            for(int i = 1; i<=days; i++)
            {
                for(int j = 1; j<=7; j++)
                {
                    Label label = new Label();
                    label.Name = currentDay.ToString();
                    label.Parent = this.calendarPanel;
                    label.Location = new Point(x, y);
                    label.Width = 110;
                    label.Height = 30;
                    DateTime temp = new DateTime(this.year, this.monthNumber, currentDay);
                    label.Text = currentDay.ToString() + " - " + temp.DayOfWeek.ToString();
                    label.ForeColor = Color.White;
                    label.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    label.BorderStyle = BorderStyle.FixedSingle;
                    if (reservations.dateIsValid(temp, allRoomReservations))
                        label.BackColor = ColorTranslator.FromHtml("#344FA1");
                    else label.BackColor = Color.Gray;
                    this.calendar[i, j] = label;
                    string name = calendar[i, j].Name;
                    int aux1 = i, aux2 = j;
                    if(calendar[i, j].BackColor != Color.Gray)
                        this.calendar[i, j].Click += delegate (Object sender2, EventArgs e2) { this.calendarDay_Click(sender2, e2, int.Parse(name), aux1, aux2); };
                    x += 110;
                    currentDay++;
                    if (currentDay > days)
                    {
                        i = days + 1;
                        j = 8;
                    }
                }
                x = 20;
                y += 30;
            }
        }
        private void loadWelcome()
        {
            Label welcome = new Label();
            welcome.Parent = this.view.Main;
            welcome.Location = new Point(200,240);
            welcome.Width = 550;
            welcome.Height = 40;
            welcome.Text = "Booking room #" + this.roomNumber + ".\nClick on any available day to select the duration of your reservation.";
            welcome.TextAlign = ContentAlignment.MiddleCenter;
            welcome.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
        }
        private void loadBackAndForward()
        {
            Label back = new Label();
            Label front = new Label();
            back.Parent = this.view.Main;
            front.Parent = back.Parent;
            back.Location = new Point(8, 120);
            front.Location = new Point(895, 120);
            back.Width = 60;
            front.Width = 60;
            back.Height = 60;
            front.Height = 60;
            back.Image = Image.FromFile(Application.StartupPath + "\\resources\\bk.png");
            front.Image = Image.FromFile(Application.StartupPath + "\\resources\\fw.png");
            back.Click += new EventHandler(this.back_Click);
            front.Click += new EventHandler(this.forward_Click);
        }
        private void loadCalendarPanel()
        {
            this.calendar = new Label[10, 10];
            this.calendarPanel = new Panel();
            this.calendarPanel.Parent = this.view.Main;
            this.calendarPanel.Location = new System.Drawing.Point(75, 10);
            this.calendarPanel.Width = 813;
            this.calendarPanel.Height = 227;
            loadCalendarTitle();
            loadCalendar();
        }
        private void loadConfirmButton()
        {
            this.confirmPanel.Visible = true;
            this.confirmPanel.Parent = this.view.Main;
            this.confirmPanel.Location = new Point(290, 285);
            this.confirmPanel.Width = 385;
            this.confirmPanel.Height = 200;
            Label button = new Label();
            button.Parent = this.confirmPanel;
            button.Location = new Point(135, 70);
            button.Width = 100;
            button.Height = 100;
            button.Image = Image.FromFile(Application.StartupPath + "\\resources\\okok.png");
            button.TextAlign = ContentAlignment.BottomCenter;
            button.Click += new EventHandler(this.confirm_Click);
            button.TabStop = false;
        }
        private void loadBack()
        {
            Label box = new Label();
            box.Parent = this.view.Main;
            box.Location = new Point(5, 5);
            box.Width = 50;
            box.Height = 50;
            box.Image = Image.FromFile(Application.StartupPath + "\\resources\\bkk.png");
            box.BackColor = Color.Transparent;
            box.Click += new EventHandler(this.back1_Click);
        }

        private void back1_Click(Object sender, EventArgs e)
        {
            if (this.bookDate != DateTime.MinValue || this.releaseDate != DateTime.MaxValue)
            {
                DialogResult result = MessageBox.Show("All reservation data will be lost, are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    this.view.refresh();
                else return;
            }
            else
                this.view.refresh();
        }
        private void confirm_Click(Object sender, EventArgs e)
        {
            for(int i = startI; i<=endI; i++)
            {
                for(int j = (i==startI) ? startJ : 1 ; j<=7; j++)
                {
                    if (calendar[i, j].BackColor == Color.Gray)
                    {
                        MessageBox.Show("The period you selected is invalid");
                        return;
                    }
                    if (int.Parse(calendar[i, j].Name) >= DateTime.DaysInMonth(this.year, this.monthNumber) || (i==endI && j>=endJ))
                        break;
                }
            }
            ControlReservations reservations = new ControlReservations();
            ControlReservationService service = new ControlReservationService();
            Reservation reservation = new Reservation(reservations.LastId + 1, this.bookDate, this.releaseDate, 0);
            ReservationService r = new ReservationService(service.LastId + 1, this.view.UserId, this.roomNumber, reservation.ID);
            reservations.add(reservation);
            service.add(r);
            MessageBox.Show("Reservation #" + reservation.ID + ", for room #" + this.roomNumber + " has been sucessfully registered");
            loadBookingOptions();
            this.bookDate = DateTime.MinValue;
            this.releaseDate = DateTime.MaxValue;
        }
        private void back_Click(Object sender, EventArgs e)
        {
            if (this.monthNumber == 1)
            {
                this.year--;
                this.monthNumber = 12;
            }
            else this.monthNumber--;
            this.calendarPanel.Controls.Clear();
            this.loadCalendarTitle();
            this.loadCalendar();
        }
        private void forward_Click(Object sender, EventArgs e)
        {
            if (this.monthNumber == 12)
            {
                this.year++;
                this.monthNumber = 1;
            }
            else this.monthNumber++;
            this.calendarPanel.Controls.Clear();
            this.loadCalendarTitle();
            this.loadCalendar();
        }
        private void calendarDay_Click(object sender, EventArgs e, int day, int i, int j)
        {
            DateTime time = new DateTime(this.year, this.monthNumber, day);
            if (this.bookDate.Equals(DateTime.MinValue))
            {
                this.startI = i;
                this.startJ = j;
                if(time.CompareTo(this.releaseDate) > -1)
                {
                    MessageBox.Show("Your reservation must be a valid one");
                    return;
                }
                this.bookCard = new ReservationCard(time, this.view.Main, this);
                bookCard.Status = 0;
                bookCard.setLocation(75, 285);
                bookCard.loadCard();
                this.bookDate = time;
            }
            else if(this.releaseDate.Equals(DateTime.MaxValue))
            {
                this.endI = i;
                this.endJ = j;
                if (time.CompareTo(this.bookDate) < 1)
                {
                    MessageBox.Show("Your reservation must be valid, minim 1 day");
                    return;
                }
                this.releaseCard = new ReservationCard(time, this.view.Main, this);
                releaseCard.Status = 1;
                releaseCard.setLocation(688, 285);
                releaseCard.loadCard();
                this.releaseDate = time;
            }
            if (!this.bookDate.Equals(DateTime.MinValue) && !this.releaseDate.Equals(DateTime.MaxValue))
                this.loadConfirmButton();
        }
        public void seeBookingOptions_Click(object sender, EventArgs e, int roomNumber)
        {
            this.roomNumber = roomNumber;
            this.loadBookingOptions();
        }

        public void directToProfile(Object sender, EventArgs e)
        {
            this.view.Profile = new ProfileView(this.view);
        }
        public void directToLogout(object sender, EventArgs e)
        {
            this.view.Main.Controls.Clear();
            this.view.Header.Size = new Size(0, 0);
            LoginBox login = new LoginBox(this.view);
        }
        public void loadProfileButton_Click(object sender, EventArgs e)
        {
            if (bookDate != DateTime.MinValue || releaseDate != DateTime.MaxValue)
            {
                DialogResult result = MessageBox.Show("All reservation data will be lost, are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    this.view.Profile = new ProfileView(this.view);
                else return;
            }
            else this.view.Profile = new ProfileView(this.view);
        }
        public void logout_Click(Object sender, EventArgs e)
        {
            if (bookDate != DateTime.MinValue || releaseDate != DateTime.MaxValue)
            {
                DialogResult result = MessageBox.Show("All reservation data will be lost, are you sure you want to proceed?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.view.Main.Controls.Clear();
                    this.view.Header.Size = new Size(0, 0);
                    LoginBox login = new LoginBox(this.view);
                }
                else return;
            }
            else
            {
                this.view.Main.Controls.Clear();
                this.view.Header.Size = new Size(0, 0);
                LoginBox login = new LoginBox(this.view);
            }
        }
        public void loadBookingOptions()
        {
            this.view.Main.Controls.Clear();
            loadBack();
            loadCalendarPanel();
            loadWelcome();
            loadBackAndForward();
            this.view.Header.Controls.RemoveAt(1);
            this.view.Header.Controls.RemoveAt(1);
            this.view.loadProfileButton(1);
            this.view.loadLogout(1);
            this.view.prof.Click += new EventHandler(this.loadProfileButton_Click);
            this.view.logout.Click += new EventHandler(this.logout_Click);
        }
    }
}
