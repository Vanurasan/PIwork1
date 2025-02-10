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
            protected DateTime date { get; set; }
            protected string room { get; set; }
            protected string teacher { get; set; }

            public virtual void Read(string[] words) { }
            public virtual void Write() { }

        }

        public class Practice : Lesson
        {
            private int numberOfTasks { get; set; }

            public override void Read(string[] words) 
            {
                date = DateTime.Parse(words[1]);
                room = words[2];
                teacher = words[3];
                numberOfTasks = Convert.ToInt32(words[4]);
            }
            public override void Write() 
            {
                Console.WriteLine("Практическое занятие "+date+" Кабинет:"+room+" Педагог:"+teacher+" Количество запланированных задач:"+numberOfTasks);
            }
        }
        public class Lab : Lesson 
        {
            private string equipment { get; set; }

            public override void Read(string[] words) 
            {
                date = DateTime.Parse(words[1]);
                room = words[2];
                teacher = words[3];
                equipment = words[4];
            }
            public override void Write() 
            {
                Console.WriteLine("Практическое занятие " + date + " Кабинет:" + room + " Педагог:" + teacher + " Необходимая экиперовка:" + equipment);
            }

        }
        public class Lecture : Lesson 
        {
            private int numberOfGroups { get; set; }

            public override void Read(string[] words) 
            {
                date = DateTime.Parse(words[1]);
                room = words[2];
                teacher = words[3];
                numberOfGroups = Convert.ToInt32(words[4]);
            }
            public override void Write() 
            {
                Console.WriteLine("Практическое занятие " + date + " Кабинет:" + room + " Педагог:" + teacher + " Количество групп:" + numberOfGroups);
            }
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
