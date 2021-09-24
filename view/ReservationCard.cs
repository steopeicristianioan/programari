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
    class ReservationCard:Panel
    {
        private HomeService service;
        public HomeService Service { get => this.service; }
        private DateTime date;
        private Panel parent;
        private int status;
        public int Status { get => this.status; set => this.status = value; }
        public DateTime Date { get => this.date; }

        public ReservationCard(DateTime date, Panel parent, HomeService service)
        {
            this.date = date;
            this.parent = parent;
            this.service = service;

            this.Parent = this.parent;
            this.Width = 200;
            this.Height = 200;
            this.BackColor = ColorTranslator.FromHtml("#3D84B8");
        }

        public void setLocation(int x, int y)
        {
            this.Location = new Point(x, y);
        }  

        private void loadTitle()
        {
            Label title = new Label();
            title.Parent = this;
            title.Location = new Point(1, 1);
            title.Width = 196;
            title.Height = 30;
            if (this.status == 0)
                title.Text = "Book date";
            else title.Text = "Release date";
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
        }
        private void loadGeneralPanel()
        {
            Label titles = new Label();
            titles.Parent = this;
            titles.Location = new Point(1, 40);
            titles.Width = 50;
            titles.Height = 85;
            titles.Text = "Year:\n\nMont:\n\nDay:";
            titles.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
        }
        private void loadTextBoxes()
        {
            TextBox year = new TextBox();
            TextBox month = new TextBox();
            TextBox day = new TextBox();

            year.Enabled = false;
            month.Enabled = false;
            day.Enabled = false;

            year.Parent = this;
            month.Parent = this;
            day.Parent = this;

            year.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
            month.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);
            day.Font = new Font("Microsoft Sitka Small", 9, FontStyle.Regular);

            year.Location = new Point(55, 40);
            month.Location = new Point(55, 70);
            day.Location = new Point(55, 100);

            year.Width = 135;
            month.Width = 135;
            day.Width = 135;

            year.Text = this.date.Year.ToString();
            month.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.date.Month);
            day.Text = this.date.Day.ToString();

            year.TabStop = false;
            month.TabStop = false;
            day.TabStop = false;
        }
        private void loadRemoveButton()
        {
            Label remove = new Label();
            remove.Parent = this;
            remove.Location = new Point(55, 130);
            remove.Width = 100;
            remove.Height = 50;
            remove.Image = Image.FromFile(Application.StartupPath + "\\resources\\remove.png");
            remove.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Bold);
            remove.Click += new EventHandler(this.remove_Click);
            remove.TabStop = false;
        }
        private void remove_Click(Object sender, EventArgs e)
        {
            this.service.ConfirmPanel.Visible = false;
            this.Visible = false;
            if (this.status == 0)
                this.service.BookDate = DateTime.MinValue;
            else this.service.ReleaseDate = DateTime.MaxValue;
        }
        public void loadCard()
        {
            this.Controls.Clear();
            this.Visible = true;
            loadTitle();
            loadGeneralPanel();
            loadTextBoxes();
            loadRemoveButton();
        }
    }
}
