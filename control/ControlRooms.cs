using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RoomReservation
{
    class ControlRooms : IControl<Room>
    {
        Lista<Room> allRooms;
        private string readingUrl = Application.StartupPath + @"\rooms.txt";
        private HomeView view;

        public ControlRooms()
        {
            this.allRooms = new Lista<Room>();
            read();
        }
        public ControlRooms(HomeView view) : this()
        {
            this.view = view;
        }

        public void read()
        {
            this.allRooms.clear();
            StreamReader reader = new StreamReader(this.readingUrl);
            string line = string.Empty;
            while((line = reader.ReadLine()) != null)
            {
                Room room = new Room(int.Parse(line.Split('|')[0]), double.Parse(line.Split('|')[1]));
                this.allRooms.addLast(room);
            }
            reader.Close();
        }
        public void write()
        {
            string result = string.Empty;
            StreamWriter writer = new StreamWriter(this.readingUrl);
            Node<Room> head = this.allRooms.First;
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
            this.allRooms.print();
        }
        public Room getById(int id)
        {
            Node<Room> head = this.allRooms.First;
            while (head != null)
            {
                if (head.Data.ID == id)
                    return head.Data;
                head = head.Next;
            }
            return new Room(-1, 0);
        }
        public void add(Room room)
        {
            //
        }
        public void remove(int id)
        {
            //
        }

        public void loadAllRooms()
        {
            this.view.Main.Controls.Clear();
            int x = 20, y = 20;
            Node<Room> head = this.allRooms.First;
            while(head != null)
            {
                RoomPreView preView = new RoomPreView(head.Data, this.view);
                preView.setLocation(x, y);
                preView.loadPreview();
                y += 210;
                head = head.Next;
            }
        }
    }
}
