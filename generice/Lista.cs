using System;
using System.Collections.Generic;
using System.Text;

namespace RoomReservation
{
    class Lista<T> : ILista<T>  where T : IComparable<T>  
    {
        private Node<T> first;
        private int lenght = 0;
        public int Length { get => this.lenght; }
        public Node<T> First { get => this.first; set => this.first = value; }

        public Lista() { this.first = null;  }

        public void create(T data)
        {
            this.first = new Node<T>(data);
            lenght++;
        }
        public void print() 
        {
            if (!this.isEmpty())
            {
                Node<T> i = this.first;
                while (i != null)
                {
                    Console.WriteLine(i.Data);
                    i = i.Next;
                }
            }
            else Console.WriteLine("The list is empty");
        }
        public void addFirst(T data)
        {
            Node<T> newHead = new Node<T>(data);
            newHead.Next = this.first;
            this.first = newHead;
            lenght++;
        }
        public void addLast(T data)
        {
            if (this.isEmpty())
            {
                create(data);
                return;
            }
            Node<T> i = this.first;
            while (i.Next != null)
                i = i.Next;
            Node<T> node = new Node<T>(data);
            node.Next = null;
            i.Next = node;
            lenght++;
        }
        public void add(int position, T data) 
        {
            if (position == 0)
                addFirst(data);
            else if (position == this.lenght)
                addLast(data);
            else
            {
                position--;
                int start = 0;
                Node<T> i = this.first;
                while (start < position)
                {
                    i = i.Next;
                    start++;
                }
                Node<T> next = i.Next;
                Node<T> node = new Node<T>(data);
                i.Next = node;
                node.Next = next;
                lenght++;
            }
        }
        public void removeFirst() 
        {
            this.first = this.first.Next;
            lenght--;
        }
        public void removeLast() 
        {
            Node<T> i = this.first;
            while (i.Next.Next != null)
                i = i.Next;
            i.Next = null;
            lenght--;
        }
        public void remove(int position) 
        {
            if (position == 0)
                removeFirst();
            else if (position == this.lenght)
                removeLast();
            else
            {
                position--;
                int start = 0;
                Node<T> i = this.first;
                while(start < position)
                {
                    i = i.Next;
                    start++;
                }
                i.Next = i.Next.Next;
                lenght--;
            }
        }
        public bool has(T data) 
        {
            Node<T> i = this.first;
            while (i != null)
            {
                if (i.Data.Equals(data))
                    return true;
                i = i.Next;
            }
            return false;
        }
        public bool isEmpty() 
        {
            return this.lenght == 0;
        }
        public void clear()
        {
            this.first = null;
            lenght = 0;
        }

        public int getPosition(T data)
        {
            int start = 0;
            Node<T> i = this.first;
            while (i != null)
            {
                if (i.Data.Equals(data))
                    return start;
                start++;
            }
            return -1;
        }
        public void sort()
        {
            bool flag;
            do
            {
                flag = false;
                for (Node<T> i = this.first; i.Next != null; i = i.Next)
                    if (i.Data.CompareTo(i.Next.Data) == 1)
                    {
                        T aux = i.Data;
                        i.Data = i.Next.Data;
                        i.Next.Data = aux;
                        flag = true;
                        break;
                    }
            } while (flag);
        }
        public Node<T> reverse()
        {
            Node<T> prev = null;
            Node<T> current = this.first;
            while(current != null)
            {
                Node<T> next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
            return prev;
        }
        public void swap(Lista<T> other)
        {
            Node<T> aux = this.first;
            this.first = other.First;
            other.first = aux;
        }
        
    }
}
