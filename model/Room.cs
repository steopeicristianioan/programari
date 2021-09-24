using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RoomReservation
{
    class Room : IComparable<Room>
    {
        private int id;
        public int ID { get => this.id; set => this.id = value; }

        private double pricePerNight;
        public double PricePerNight { get => this.pricePerNight; set => this.pricePerNight = value; }

        public string Url;
        public int Status;

        public Room(int id, double price)
        {
            this.id = id;
            this.pricePerNight = price;
            getUrl();
            getStatus();
        }

        public int CompareTo(Room other)
        {
            return 1;
        }
        public override bool Equals(object obj)
        {
            Room other = (Room)obj;
            return this.id == other.id;
        }
        public override string ToString()
        {
            return this.id.ToString() + "|" + this.pricePerNight.ToString();
        }

        private void getUrl()
        {
            string url = Application.StartupPath + "\\resources";
            switch (this.PricePerNight)
            {
                case 50:
                    url += "\\r50.jpg";
                    break;
                case 100:
                    url += "\\r100.jpg";
                    break;
                case 200:
                    url += "\\r200.jpg";
                    break;
                case 300:
                    url += "\\r300.jpg";
                    break;
            }
            this.Url = url;
        }
        private void getStatus()
        {
            int status = 0;
            switch (this.PricePerNight)
            {
                case 50:
                    status = 2;
                    break;
                case 100:
                    status = 3;
                    break;
                case 200:
                    status = 4;
                    break;
                case 300:
                    status = 5;
                    break;
            }
            this.Status = status;
        }
    }
}
