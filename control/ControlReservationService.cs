using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace RoomReservation
{
    class ControlReservationService : IControl<ReservationService>
    {
        private ProfileView view;
        private Lista<ReservationService> allReservations;
        private string readingUrl = Application.StartupPath + @"\reservationServices.txt";
        private int lastId;
        public int LastId { get => this.lastId; }

        public ControlReservationService()
        {
            this.allReservations = new Lista<ReservationService>();
            read();
        }
        public ControlReservationService(ProfileView view) : this()
        {
            this.view = view;
        }

        public void read()
        {
            this.allReservations.clear();
            StreamReader reader = new StreamReader(this.readingUrl);
            string line = string.Empty;
            while((line  = reader.ReadLine()) != null)
            {
                ReservationService service = new ReservationService(int.Parse(line.Split('|')[0]), int.Parse(line.Split('|')[1]), int.Parse(line.Split('|')[2]), int.Parse(line.Split('|')[3]));
                this.allReservations.addLast(service);
                this.lastId = service.ID;
            }
            reader.Close();
        }
        public void write()
        {
            string result = string.Empty;
            StreamWriter writer = new StreamWriter(this.readingUrl);
            Node<ReservationService> head = this.allReservations.First;
            while (head != null)
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
        public ReservationService getById(int id)
        {
            Node<ReservationService> head = this.allReservations.First;
            while(head != null)
            {
                if (head.Data.ID == id)
                    return head.Data;
                head = head.Next;
            }
            return new ReservationService(-1, -1, -1, -1);
        }
        public void add(ReservationService reservation)
        {
            this.allReservations.addLast(reservation);
            write();
        }
        public void remove(int id)
        {
            Node<ReservationService> head = this.allReservations.First;
            int k = 0, position = 0;
            while(head != null)
            {
                if (head.Data.ID == id)
                {
                    position = k;
                    break;
                }
                k++;
                head = head.Next;
            }
            this.allReservations.remove(position);
            write();
        }

        public int lastActiveReservationId(int roomId)
        {
            ControlReservations reservations = new ControlReservations();
            Node<ReservationService> head = this.allReservations.First;
            int id = -1;
            while(head != null)
            {
                Reservation reservation = reservations.getById(head.Data.ReservationId);
                if (head.Data.RoomId == roomId && reservation.IsActive == 1)
                    return reservation.ID;
                head = head.Next;
            }
            return id;
        }
        public Lista<Reservation> allRoomReservations(int roomId)
        {
            ControlReservations reservations = new ControlReservations();
            Node<ReservationService> head = this.allReservations.First;
            Lista<Reservation> result = new Lista<Reservation>();
            while(head != null)
            {
                if (head.Data.RoomId == roomId)
                    result.addLast(reservations.getById(head.Data.ReservationId));
                head = head.Next;
            }
            return result;
        }
        
        public Lista<ReservationService> allClientServices(int customerId)
        {
            Lista<ReservationService> ids = new Lista<ReservationService>();
            Node<ReservationService> head = this.allReservations.First;
            while(head != null)
            {
                if (head.Data.CustomerId == customerId)
                    ids.addLast(head.Data);
                head = head.Next;
            }
            return ids;
        }
        public int loadBookings(Panel parent, Lista<ReservationService> services)
        {
            parent.Controls.Clear();

            int y = 70;
            Node<ReservationService> head = services.First;
            while(head != null)
            {
                RServiceCard card = new RServiceCard(parent, head.Data);
                card.view = this.view;
                card.setLocation(100, y);
                card.loadCard();
                y += 80;
                head = head.Next;
            }

            Label title = new Label();
            title.BackColor = Color.Transparent;
            title.Font = new Font("Microsoft Sitka Small", 11, FontStyle.Regular);
            title.Parent = parent;
            title.Location = new Point(250, 10);
            title.Width = 250;
            title.Height = 50;
            title.Text = "These are all your reservations!";
            title.TextAlign = ContentAlignment.MiddleCenter;

            return y;
        }
        public void sort(Lista<ReservationService> services, int index)
        {
            ControlReservations reservations = new ControlReservations();
            bool flag;
            do
            {
                flag = false;
                for (Node<ReservationService> node = services.First; node.Next != null; node = node.Next)
                {
                    ReservationService r = node.Data;
                    Reservation reservation = reservations.getById(r.ReservationId);
                    Reservation reservation1 = reservations.getById(node.Next.Data.ReservationId);
                            if (reservation.CompareTo(reservation1) > 0 && index == 0)
                            {
                                ReservationService aux = r;
                                node.Data = node.Next.Data;
                                node.Next.Data = aux;
                                flag = true;
                            }
                            if (reservation.CompareTo(reservation1) < 0 && index == 1)
                            {
                                ReservationService aux = r;
                                node.Data = node.Next.Data;
                                node.Next.Data = aux;
                                flag = true;
                            }
                            if (reservation.Duration().CompareTo(reservation1.Duration()) > 0 && index == 2)
                            {
                                ReservationService aux = r;
                                node.Data = node.Next.Data;
                                node.Next.Data = aux;
                                flag = true;
                            }
                            if (reservation.Duration().CompareTo(reservation1.Duration()) < 0 && index == 3)
                            {
                                ReservationService aux = r;
                                node.Data = node.Next.Data;
                                node.Next.Data = aux;
                                flag = true;
                            }
                }
            } while (flag);
        }
        public int display(Panel parent, Lista<ReservationService> services, int index)
        {
            sort(services, index);
            return loadBookings(parent, services);
        }
    }
}
