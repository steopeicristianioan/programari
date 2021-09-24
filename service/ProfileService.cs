using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RoomReservation
{
    class ProfileService
    {
        private ProfileView view;

        private CustomTextBox first;
        private TextBox last;
        private TextBox mail;
        private TextBox password;

        private Label save;
        public ProfileService() { }
        public ProfileService(ProfileView view)
        {
            this.view = view;
        }
        private void loadMessage()
        {
            Label label = new Label();
            label.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
            label.BackColor = Color.Transparent;
            label.Parent = this.view.Main;
            label.Location = new Point(50, 30);
            label.Width = 300;
            label.Height = 70;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = "These is all of your profile's data.\nEdit any field you want by clicking on it!";
        }
        private void loadDescriptions()
        {
            Label label = new Label();
            label.BackColor = Color.Transparent;
            label.Parent = this.view.Main;
            label.Location = new Point(50,100);
            label.Width = 120;
            label.Height = 200;
            label.Text = "First name:\n\n\nLast name:\n\n\nEmali adress:\n\n\nPassword:";
            label.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
        }
        private void loadFirstName()
        {
            this.first = new CustomTextBox();
            this.first.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
            this.first.Parent = this.view.Main;
            this.first.Location = new Point(170, 100);
            this.first.Width = 200;
            this.first.Text = view.Customer.FirstName;
            this.first.TextChanged += new EventHandler(this.textBox_TextChanged);
        }
        private void loadLastName()
        {
            this.last = new TextBox();
            this.last.Font = this.first.Font;
            this.last.Parent = this.view.Main;
            this.last.Location = new Point(170, 150);
            this.last.Width = 200;
            this.last.Text = view.Customer.LastName;
            this.last.TextChanged += new EventHandler(this.textBox_TextChanged);
        }
        private void loadPassword() 
        {
            this.password = new TextBox();
            this.password.Font = this.first.Font;
            this.password.Parent = this.view.Main;
            this.password.Location = new Point(170, 250);
            this.password.Width = 200;
            this.password.Text = view.Customer.Password;
            this.password.TextChanged += new EventHandler(this.textBox_TextChanged);
        }
        private void loadEmail()
        {
            this.mail = new TextBox();
            this.mail.Font = this.first.Font;
            this.mail.Parent = this.view.Main;
            this.mail.Location = new Point(170, 200);
            this.mail.Width = 200;
            this.mail.Text = view.Customer.Email;
            this.mail.TextChanged += new EventHandler(this.textBox_TextChanged);
        }
        private void loadSaveButton()
        {
            this.save = new Label();
            this.save.Font = this.first.Font;
            this.save.Image = Image.FromFile(Application.StartupPath + "\\resources\\save.png");
            this.save.Parent = this.view.Main;
            this.save.Location = new Point(110, 285);
            this.save.Width = 200;
            this.save.Height = 100;
            this.save.Text = "Save";
            this.save.Visible = false;
            this.save.Click += new EventHandler(this.save_Click);
            this.save.TextAlign = ContentAlignment.BottomCenter;
            this.save.BackColor = Color.Transparent;
        }
        private void loadExitButton(int x, int y, int k, Panel parent)
        {
            Label exit = new Label();
            exit.BackColor = Color.Transparent;
            exit.Parent = parent;
            exit.Location = new Point(x, y);
            exit.Width = 50;
            exit.Height = 50;
            exit.Image = Image.FromFile(Application.StartupPath + "\\resources\\bkk.png");
            exit.Click += delegate (object sender2, EventArgs e2) { exit_Click(sender2, e2, k); };
        }
        private void loadSettings()
        {
            this.view.Main.BackgroundImage = null;
            this.view.Main.Controls.Clear();
            loadExitButton(0, 0, 1, this.view.Main);
            loadMessage();
            loadDescriptions();
            loadFirstName();
            loadLastName();
            loadEmail();
            loadPassword();
            loadSaveButton();
            
        }
        private void loadFilterComboBox(string text, Panel parent)
        {
            Label title = new Label();
            title.BackColor = Color.Transparent;
            title.Parent = parent;
            title.Location = new Point(700, 10);
            title.Width = 100;
            title.Height = 50;
            title.Text = "Filtered by";
            title.Font = new Font("Microsoft Sitka Small", 11, FontStyle.Regular);
            title.TextAlign = ContentAlignment.MiddleCenter;

            ComboBox box = new ComboBox();
            box.Parent = parent;
            box.Location = new Point(685, 70);
            box.Width = 130;
            box.Height = 100;
            box.TabStop = false;
            box.Items.Add("oldest -> newest");
            box.Items.Add("newest -> oldest");
            box.Items.Add("shortest -> longest");
            box.Items.Add("longest -> shortest");
            box.Text = box.Items[0].ToString();
            box.Text = text;
            box.SelectedIndexChanged += delegate (Object sender2, EventArgs e2) { this.comboBox_ItemChanged(sender2, e2, box); };
            
        }

        private void comboBox_ItemChanged(Object sender, EventArgs e, ComboBox box)
        {
            ControlReservationService service = new ControlReservationService(this.view);
            Lista<ReservationService> lista = service.allClientServices(this.view.UserId);
            int y = 0;
            if (box.SelectedIndex == 0)
            {
                y = service.display(this.view.Main, lista, 0);
                loadFilterComboBox(box.Items[0].ToString(), this.view.Main);
            }
            else if(box.SelectedIndex == 1)
            {
                y = service.display(this.view.Main, lista, 1);
                loadFilterComboBox(box.Items[1].ToString(), this.view.Main);
            }
            else if (box.SelectedIndex == 2)
            {
                y = service.display(this.view.Main, lista, 2);
                loadFilterComboBox(box.Items[2].ToString(), this.view.Main);
            }
            else if (box.SelectedIndex == 3)
            {
                y = service.display(this.view.Main, lista, 3);
                loadFilterComboBox(box.Items[3].ToString(), this.view.Main);
            }
            loadExitButton(0,0, 0, this.view.Main);
        }
        private void textBox_TextChanged(Object sender, EventArgs e)
        {
            this.save.Visible = true;
        }

        private void save_Click(object sender, EventArgs e)
        {
            Customer newCustomer = new Customer(this.view.Customer.ID, first.Text, last.Text, mail.Text, password.Text);
            this.view.Customer = newCustomer;
            ControlCustomers customers = new ControlCustomers();
            customers.update(newCustomer.ID, newCustomer);
            this.view.Customer = newCustomer;
            this.view.refresh();
        }
        private void exit_Click(object sender, EventArgs e, int k)
        {
            if (k == 1)
            {
                Customer customer = this.view.Customer;
                if (first.Text != customer.FirstName || last.Text != customer.LastName || mail.Text != customer.Email || password.Text != customer.Password)
                {
                    DialogResult result = MessageBox.Show("Are you sure that you don't want to save your changes?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                        this.view.refresh();
                    else return;
                }
                else this.view.refresh();
            }
            else if (k == 0) this.view.refresh();
        }
        public void settings_Click(object sender, EventArgs e)
        {
            this.loadSettings();
        }
        public void home_Click(object sender, EventArgs e)
        {
            this.view.Home.refresh();
        }
        public void booking_Click(object sender, EventArgs e)
        {
            this.view.home.MinimumSize = new Size(900, 550);
            this.view.home.SetDesktopLocation(200, 70);
            this.view.home.MaximumSize = this.view.home.MinimumSize;
            this.view.Main.Width = 850;
            this.view.Header.Width = 850;
            this.view.resizeHeader();
            ControlReservationService service = new ControlReservationService(this.view);
            Lista<ReservationService> lista = service.allClientServices(this.view.UserId);
            int y = service.loadBookings(this.view.Main, lista);
            loadExitButton(0, 0, 0, this.view.Main);
            loadFilterComboBox("oldest -> newest", this.view.Main);
        }

        public void removeReservation(ReservationService service)
        {
            ControlReservationService ctrl = new ControlReservationService(this.view);
            Lista<ReservationService> lista = ctrl.allClientServices(service.CustomerId);
            int y = ctrl.loadBookings(this.view.Main, lista);
            loadExitButton(0, 0, 0, this.view.Main);
            loadFilterComboBox("oldest -> newest", this.view.Main);
        }

    }
}
