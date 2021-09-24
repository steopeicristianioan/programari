using System;
using System.Collections.Generic;
using System.Text;

namespace RoomReservation
{
    class ReservationService : IComparable<ReservationService>
    {
        private int id;
        public int ID { get => this.id; set => this.id = value; }

        private int customerId;
        public int CustomerId { get => this.customerId; set => this.customerId = value; }

        private int roomId;
        public int RoomId { get => this.roomId; set => this.roomId = value; }

        private int reservationId;
        public int ReservationId { get => this.reservationId; set => this.reservationId = value; }

        public ReservationService(int id, int customerId, int roomId, int reservationId)
        {
            this.id = id;
            this.customerId = customerId;
            this.roomId = roomId;
            this.reservationId = reservationId;
        }

        public int CompareTo(ReservationService other)
        {
            return 1;
        }
        public override bool Equals(object obj)
        {
            ReservationService other = (ReservationService)obj;
            return this.id == other.id;
        }
        public override string ToString()
        {
            return this.id.ToString() + "|" + this.customerId.ToString() + "|" + this.roomId.ToString() + "|" + this.reservationId.ToString();
        }
    }
}
