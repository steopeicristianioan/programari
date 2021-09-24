using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RoomReservation
{
    class ControlCustomers : IControl<Customer>
    {
        Lista<Customer> allCustomers;
        private string readingUrl = Application.StartupPath + @"\customers.txt";
        private int lastId;
        public int LastId { get => this.lastId; }
        public ControlCustomers()
        {
            this.allCustomers = new Lista<Customer>();
            read();
        }

        public void read()
        {
            this.allCustomers.clear();
            StreamReader reader = new StreamReader(this.readingUrl);
            string line = string.Empty;
            while((line = reader.ReadLine()) != null)
            {
                Customer customer = new Customer(int.Parse(line.Split('|')[0]), line.Split('|')[1], line.Split('|')[2], line.Split('|')[3], line.Split('|')[4]);
                this.allCustomers.addLast(customer);
                this.lastId = customer.ID;
            }
            reader.Close();
        }
        public void write()
        {
            string result = string.Empty;
            StreamWriter writer = new StreamWriter(this.readingUrl);
            Node<Customer> head = this.allCustomers.First;
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
            this.allCustomers.print();
        }
        public Customer getById(int id)
        {
            Node<Customer> head = this.allCustomers.First;
            while(head != null)
            {
                if (head.Data.ID == id)
                    return head.Data;
                head = head.Next;
            }
            return new Customer(-1, "", "", "", "");
        }
        public void add(Customer customer)
        {
            //
        }
        public void remove(int id)
        {
            //
        }
        public void update(int id, Customer newCustomer)
        {
            Node<Customer> head = this.allCustomers.First;
            while(head != null)
            {
                if(head.Data.ID == id)
                {
                    head.Data = newCustomer;
                    break;
                }
            }
            write();
        }
        public int getIdByName(string name)
        {
            Node<Customer> head = this.allCustomers.First;
            while(head != null)
            {
                if (head.Data.FirstName + head.Data.LastName == name)
                    return head.Data.ID;
                head = head.Next;
            }
            return -1;
        }
    }
}
