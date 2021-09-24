using System;
using System.Collections.Generic;
using System.Text;

namespace RoomReservation
{
    class Reservation : IComparable<Reservation>
    {
        private int id;
        public int ID { get => this.id; set => this.id = value; }
        private DateTime bookDate;
        public DateTime BookDate { get => this.bookDate; set => this.bookDate = value; }
        private DateTime releaseDate;
        public DateTime ReleaseDate { get => this.releaseDate; set => this.releaseDate = value; }
        private int isActive;
        public int IsActive { get => this.isActive; set => this.isActive = value; }

        public Reservation(int id, DateTime bookDate, DateTime releaseDate, int isActive)
        {
            this.id = id;
            this.bookDate = bookDate;
            this.releaseDate = releaseDate;
            this.isActive = isActive;
        }
        public Reservation(int id, string bookDate, string releaseDate, int isActive)
        {
            this.id = id;
            if(bookDate != "" && releaseDate != "")
            {
                this.bookDate = new DateTime(int.Parse(bookDate.Split(',')[0]), int.Parse(bookDate.Split(',')[1]), int.Parse(bookDate.Split(',')[2]), int.Parse(bookDate.Split(',')[3]), int.Parse(bookDate.Split(',')[4]), 0);
                this.releaseDate = new DateTime(int.Parse(releaseDate.Split(',')[0]), int.Parse(releaseDate.Split(',')[1]), int.Parse(releaseDate.Split(',')[2]), int.Parse(releaseDate.Split(',')[3]), int.Parse(releaseDate.Split(',')[4]), 0);
            }
            this.isActive = isActive;
        }

        public TimeSpan Duration()
        {
            return releaseDate.Subtract(bookDate);
        }
        public string Description()
        {
            return "Your appointment starts " + this.bookDate.ToString("f") + " and it lasts " + this.Duration().TotalMinutes + " minutes";
        }
        public override string ToString()
        {
            string start = string.Empty, end = string.Empty;
            start = this.bookDate.Year.ToString() + "," + this.bookDate.Month.ToString() + "," + this.bookDate.Day.ToString() + "," + this.bookDate.Hour.ToString() + "," + this.bookDate.Minute.ToString();
            end = this.releaseDate.Year.ToString() + "," + this.releaseDate.Month.ToString() + "," + this.releaseDate.Day.ToString() + "," + this.releaseDate.Hour.ToString() + "," + this.releaseDate.Minute.ToString();
            return this.id.ToString() + "|" + start + "|" + end + "|" + this.isActive.ToString();
        }
        public int CompareTo(Reservation other)
        {
            return this.bookDate.CompareTo(other.bookDate);
        }
        public bool isValid(Reservation other)
        {
            if (other.releaseDate.CompareTo(this.bookDate) == -1 || this.releaseDate.CompareTo(other.bookDate) == -1)
                return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            return !this.isValid((Reservation)obj);
        }

        public bool hasStarted()
        {
            return this.bookDate.CompareTo(DateTime.Now) == -1;
        }
        public bool hasEnded()
        {
            return this.releaseDate.CompareTo(DateTime.Now) == -1;
        }
    }
}
