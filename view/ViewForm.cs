using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace RoomReservation
{
    public abstract  class View:Form
    {
        protected int userId;
        public int UserId { get => this.userId; set => this.userId = value; }

        public View()
        {
            this.MaximumSize = new Size(1000, 1000);
            this.MinimumSize = new Size(1000, 1000);

            this.CenterToScreen();
            this.Text = "Hello";
        }
       

        protected Panel header;
        public Panel Header { get => this.header; }

        protected Panel main;
        public Panel Main { get => this.main; }

        protected Panel footer;
        public Panel Footer { get => this.footer; }


        protected abstract void setHeader();
        protected abstract void setMain();
        protected abstract void setFooter();
        protected abstract void loadHeader();
        protected abstract void loadMain();
        protected abstract void loadFooter();

        

    }
}
