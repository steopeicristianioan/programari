using System;
using System.Collections.Generic;
using System.Text;

namespace RoomReservation
{
    interface IControl<T> where T : IComparable<T>
    {
        void read();
        void write();
        void log();
        T getById(int id);
        void add(T data);
        void remove(int id);

    }
}
