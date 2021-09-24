using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RoomReservation
{
    class Customer : IComparable<Customer>
    {
        private int id;
        public int ID { get => this.id; set => this.id = value; }
        private string firstName;
        public string FirstName { get => this.firstName; set => this.firstName = value; }
        private string lastName;
        public string LastName { get => this.lastName; set => this.lastName = value; }
        private string email;
        public string Email { get => this.email; set => this.email = value; }
        private string password;
        public string Password { get => this.password; set => this.password = value; }

        public Customer(int id, string firstName, string lastName, string email, string password)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }

        public int CompareTo(Customer other)
        {
            return 1;
        }
        public override bool Equals(object obj)
        {
            Customer other = (Customer)obj;
            return this.id == other.id; 
        }
        public override string ToString()
        {
            return this.id.ToString() + "|" + this.firstName + "|" + this.lastName + "|" + this.email + "|" + this.password;
        }

        public void bookRoom(TextBox txtRoom, TextBox txtBook, TextBox txtRelease)
        {
            int roomNumber = 0;
            string bookDate = string.Empty, releaseDate = string.Empty;
            roomNumber = int.Parse(txtRoom.Text);
            bookDate = txtBook.Text;
            releaseDate = txtRelease.Text;
            ControlRooms rooms = new ControlRooms();
            Room room = rooms.getById(roomNumber);
            ControlReservationService service = new ControlReservationService();
            ControlReservations reservations = new ControlReservations();
            Reservation reservation = new Reservation(reservations.LastId + 1, txtBook.Text, txtRelease.Text, 0);
            if (reservations.canBeMade(reservation, service.allRoomReservations(roomNumber)))
            {
                reservations.add(reservation);
                MessageBox.Show("Room #" + roomNumber + " has been succesfully booked from " + reservation.BookDate.ToString("f") + " to " + reservation.ReleaseDate.ToString("f"));
            }
            else MessageBox.Show("Room #" + roomNumber + " is already booked in that period");
        }
    }
}
