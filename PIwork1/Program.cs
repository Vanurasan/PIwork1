using System;
using System.Collections.Generic;
using System.IO;
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
            private DateTime date { get; set; }
            private string room { get; set; }
            private string teacher { get; set; }

            public virtual void Read(string[] words) { }
            public virtual void Write() { }

        }

        public class Practice : Lesson
        {
            private int numberOfTasks { get; set; }

            public override void Read(string[] words) { }
            public override void Write() { }
        }
        public class Lab : Lesson 
        {
            private string equipment { get; set; }

            public override void Read(string[] words) { }
            public override void Write() { }

        }
        public class Lecture : Lesson 
        {
            private int numberOfGroups { get; set; }

            public override void Read(string[] words) 
            {
                //Lecture.date = DateTime.Parse(words[1]);

            }
            public override void Write() { }
        }

        static void Main(string[] args)
        {
            Lesson[] lessons = new Lesson[] { };
            StreamReader f = new StreamReader("test.txt");
            while (!f.EndOfStream)
            {
                string s = f.ReadLine();
                string[] words = s.Split();
                switch (words[0])
                {
                    case "Лекция":

                        Lecture lecture = new Lecture();
                        lecture.Read(words);
                        break;
                    case "Практика":
                        Practice practice = new Practice();
                        practice.Read(words);
                        break;
                    case "Лабораторная":
                        Lab lab = new Lab();
                        lab.Read(words);
                        break;
                    default:
                        Console.WriteLine("Конец файла");
                        break;

                }
            }

        }
    }
}
