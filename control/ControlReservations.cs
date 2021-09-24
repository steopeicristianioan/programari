using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RoomReservation
{
    class ControlReservations : IControl<Reservation>
    {
        private Lista<Reservation> allReservations;
        private string readingUrl = Application.StartupPath + @"\reservations.txt";
        private int lastId;
        public int LastId { get => this.lastId; }

        public ControlReservations()
        {
            this.allReservations = new Lista<Reservation>();
            read();
            updateToPresent();
            read();
        }

        public void read()
        {
            this.allReservations.clear();
            StreamReader reader = new StreamReader(this.readingUrl);
            string line = string.Empty;
            while((line = reader.ReadLine()) != null)
            {
                Reservation reservation = new Reservation(int.Parse(line.Split('|')[0]), line.Split('|')[1], line.Split('|')[2], int.Parse(line.Split('|')[3]));
                this.allReservations.addLast(reservation);
                this.lastId = reservation.ID;
            }
            reader.Close();
        }
        public void write()
        {
            string result = string.Empty;
            StreamWriter writer = new StreamWriter(this.readingUrl);
            Node<Reservation> head = this.allReservations.First;
            while(head != null)
            {
                result += head.Data.ToString();
                result += "\n";
                head = head.Next;
            }
            writer.Write(result);
            writer.Close();
        }
        public void log()
        {
            this.allReservations.print();
        }
        public Reservation getById(int id)
        {
            Node<Reservation> head = this.allReservations.First;
            while(head != null)
            {
                if (head.Data.ID == id)
                    return head.Data;
                head = head.Next;
            }
            return new Reservation(-1, "", "", -2);
        }
        public void add(Reservation reservation)
        {
            this.allReservations.addLast(reservation);
            write();
        }
        public void remove(int id)
        {
            int k = 0, position = 0;
            Node<Reservation> head = this.allReservations.First;
            while(head != null)
            {
                if(head.Data.ID == id)
                {
                    position = k;
                    break;
                }
                head = head.Next;
                k++;
            }
            this.allReservations.remove(position);
            write();
        }
        
        public void updateToPresent()
        {
            Node<Reservation> head = this.allReservations.First;
            while(head != null)
            {
                if (head.Data.ReleaseDate.CompareTo(DateTime.Now) == -1 || head.Data.BookDate.CompareTo(DateTime.Now) == 1)
                    head.Data.IsActive = 0;
                else head.Data.IsActive = 1;
                head = head.Next;
            }
            write();
        }
        public bool canBeMade(Reservation reservation, Lista<Reservation> roomReservations)
        {
            Node<Reservation> head = roomReservations.First;
            while(head != null)
            {
                Console.WriteLine(reservation.isValid(head.Data));
                if (!reservation.isValid(head.Data))
                    return false;
                head = head.Next;
            }
            return true;
        }

        public bool dateIsValid(DateTime date, Lista<Reservation> reservations)
        {
            Node<Reservation> head = reservations.First;
            while (head != null)
            {
                DateTime aux = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if (date.CompareTo(aux) == -1)
                    return false;
                DateTime temp1 = new DateTime(head.Data.BookDate.Year, head.Data.BookDate.Month, head.Data.BookDate.Day);
                DateTime temp2 = new DateTime(head.Data.ReleaseDate.Year, head.Data.ReleaseDate.Month, head.Data.ReleaseDate.Day);
                if (date.CompareTo(temp1) >= 0 && date.CompareTo(temp2) <= 0)
                    return false;
                head = head.Next;
            }
            return true;
        }

        public Lista<Reservation> allCustomerReservations(Lista<ReservationService> services)
        {
            Lista<Reservation> result = new Lista<Reservation>();
            Node<ReservationService> head = services.First;
            while(head != null)
            {
                result.addLast(this.getById(head.Data.ReservationId));
                head = head.Next;
            }
            return result;
        }
    }
}
