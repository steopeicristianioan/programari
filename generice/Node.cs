using System;
using System.Collections.Generic;
using System.Text;

namespace RoomReservation
{
    class Node<T> where T : IComparable<T>
    {
        private T data;
        private Node<T> next;
        public T Data { get => this.data; set => this.data = value; }
        public Node<T> Next { get => this.next; set => this.next = value; }

        public Node(T data) { this.data = data; }



    }
}
