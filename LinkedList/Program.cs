using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            Task[] tasks = new Task[6];

            // Pseudo linked list?
            //                 id name next first
            tasks[0] = new Task(1, "1", 2, true);
            tasks[1] = new Task(2, "2", 5, false);
            tasks[2] = new Task(3, "3", 4, false);
            tasks[3] = new Task(4, "4", 6, false);
            tasks[4] = new Task(5, "5", 3, false); 
            tasks[5] = new Task(6, "6", 0, false);

            Task[] orderedTasks = new Task[tasks.Length];
            int x = 0;
            while (x < tasks.Length)
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i].First == true && x == 0)
                    {
                        orderedTasks[x] = tasks[i];
                        x++;
                    }
                    else if (x > 0)
                    {
                        orderedTasks[x] = tasks[orderedTasks[x-1].NextTask-1];
                        x++;
                    }
                }
            }

            foreach (Task orderedTask in orderedTasks)
            {
                Console.WriteLine("{0}", orderedTask.Name);
            }

            Console.ReadKey();

        }
    }

    class Task {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NextTask { get; set; }
        public bool First { get; set; }
        public Task(int _id, string _name, int _nextTask, bool _first)
        {
            Id = _id;
            Name = _name;
            NextTask = _nextTask;
            First = _first;
        }
    }
}
