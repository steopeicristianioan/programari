using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RoomReservation
{
    class LoginBox : Panel
    {
        private TextBox firstName;
        private TextBox lastName;
        private TextBox password;
        private HomeView view;

        public LoginBox(HomeView view)
        {
            this.view = view;
            this.Parent = this.view.Main;
            this.view.SetDesktopLocation(450, 150);
            //this.Location = new Point(0, 0);
            this.Width = 305;
            this.Height = 400;
            this.view.MaximumSize = this.Size;
            this.view.MinimumSize = this.Size;
            this.view.Main.Location = new Point(0, 0);
            this.view.Main.Size = this.Size;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = ColorTranslator.FromHtml("#3F3697");
            this.ForeColor = Color.White;
            

            loadDescriptions();
            loadFirstName();
            loadLastName();
            loadPassword();
            loadWelcome();
            loadButton();
        }
        private void loadWelcome()
        {
            Label label = new Label();
            label.Parent = this;
            label.Location = new Point(75, 0);
            label.Width = 155;
            label.Height = 75;
            label.Font = this.firstName.Font;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = "Fill in your information to login";
        }
        private void loadDescriptions()
        {
            Label label = new Label();
            label.Parent = this;
            label.Location = new Point(5, 105);
            label.Width = 100;
            label.Height = 150;
            label.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
            label.Text = "First name: \n\n\nLast name: \n\n\nPassword: ";
        }
        private void loadFirstName()
        {
            this.firstName = new TextBox();
            this.firstName.Parent = this;
            this.firstName.Location = new Point(110, 105);
            this.firstName.Width = 170;
            this.firstName.TabStop = false;
            this.firstName.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
        }
        private void loadLastName()
        {
            this.lastName = new TextBox();
            this.lastName.Parent = this;
            this.lastName.Location = new Point(110, 155);
            this.lastName.Width = 170;
            this.lastName.TabStop = false;
            this.lastName.Font = new Font("Microsoft Sitka Small", 10, FontStyle.Regular);
        }
        private void loadPassword()
        {
            this.password = new TextBox();
            this.password.Parent = this;
            this.password.Location = new Point(110, 205);
            this.password.TabStop = false;
            this.password.Width = 170;
            this.password.Font = this.firstName.Font;
            this.password.PasswordChar = '*';
        }
        private void loadButton()
        {
            PictureBox button = new PictureBox();
            button.Parent = this;
            button.Location = new Point(100, 240);
            button.Width = 105;
            button.Height = 100;
            button.Image = Image.FromFile(Application.StartupPath + "\\resources\\lin.png");
            button.TabStop = false;

            ToolTip tip = new ToolTip();
            tip.AutoPopDelay = 5000;
            tip.InitialDelay = 1000;
            tip.ReshowDelay = 500;
            tip.ShowAlways = true;
            tip.SetToolTip(button, "Log in");
            button.Click += new EventHandler(this.button_Click);
        }

        private bool check()
        {
            if (firstName.Text == "" || lastName.Text == "" || password.Text == "")
            {
                MessageBox.Show("Please complete all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            ControlCustomers customers = new ControlCustomers();
            int id = customers.getIdByName(firstName.Text + lastName.Text);
            if(id == -1)
            {
                MessageBox.Show("Please check your data again", "Couldn't find account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            this.view.UserId = id;
            this.view.refresh();
            return true;
        }
        private void button_Click(object sender, EventArgs e)
        {
            check();
        }
    }
}
