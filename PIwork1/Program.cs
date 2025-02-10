using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIwork1
{
    internal class Program
    {
        //Задание 1 Вариант 9
        //Учебные занятия: дата, название аудитории (строка в характерном формате), имя преподавателя(строка)
        public class Lesson {
            public DateTime date { get; set; }
            public string room { get; set; }
            public string teacher { get; set; }

            public virtual void Read() { }
            public virtual void Write() { }

        }

        public class Practice : Lesson
        {
            public int numberOfTasks { get; set; }

            public override void Read() { }
            public override void Write() { }
        }
        public class Lab : Lesson 
        {
            public string equipment { get; set; }

            public override void Read() { }
            public override void Write() { }

        }
        public class Lecture : Lesson 
        {
            public int numberOfGroups { get; set; }

            public override void Read() { }
            public override void Write() { }
        }

        static void Main(string[] args)
        {
            Lesson[] lessons = new Lesson[] { };
            while (true) 
            {

            }

        }
    }
}
