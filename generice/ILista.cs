using System;
using System.Collections.Generic;
using System.Text;

namespace RoomReservation
{
    interface ILista<T> where T:IComparable<T>
    {
        void create(T data);
        void print();
        void add(int position, T data);
        void remove(int position);
        void addFirst(T data);
        void addLast(T data);
        void removeFirst();
        void removeLast();
        bool has(T data);
        bool isEmpty();
        void clear();
    }
}
