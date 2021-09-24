using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomReservation
{
    abstract class ViewPanel:Panel
    {
        protected int userId;
        public int UserId { get => this.userId; }

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
