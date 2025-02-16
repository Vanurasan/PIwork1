using System;
using System.Collections.Generic;
using System.IO;

namespace PIwork1
{
    internal class Program
    {
        public class Lesson
        {
            protected DateTime date { get; set; }
            protected string room { get; set; }
            protected string teacher { get; set; }

            public virtual void Read(string[] words) { }
            public virtual void Write() { }
        }

        public class Practice : Lesson
        {
            protected int numberOfTasks { get; set; }

            public override void Read(string[] words)
            {
                date = DateTime.Parse(words[1]);
                room = words[2];
                teacher = words[3] + " " + words[4];
                //numberOfTasks = Convert.ToInt32(words[5]);
            }

            public override void Write()
            {
                Console.WriteLine($"Практическое занятие {date} Кабинет: {room} Педагог: {teacher} Количество задач: {numberOfTasks}");
            }
        }

        public class Lab : Lesson
        {
            protected string equipment { get; set; }

            public override void Read(string[] words)
            {
                date = DateTime.Parse(words[1]);
                room = words[2];
                teacher = words[3] + " " + words[4];
                //equipment = words[5];
            }

            public override void Write()
            {
                Console.WriteLine($"Лабораторная работа {date} Кабинет: {room} Педагог: {teacher} Экипировка: {equipment}");
            }
        }

        public class Lecture : Lesson
        {
            protected int numberOfGroups { get; set; }

            public override void Read(string[] words)
            {
                date = DateTime.Parse(words[1]);
                room = words[2];
                teacher = words[3] + " " + words[4];
                //numberOfGroups = Convert.ToInt32(words[5]);
            }

            public override void Write()
            {
                Console.WriteLine($"Лекция {date} Кабинет: {room} Педагог: {teacher} Количество групп: {numberOfGroups}");
            }
        }

        static void Main(string[] args)
        {
            List<Lesson> lessons = new List<Lesson>();
            StreamReader f = new StreamReader("C:\\Users\\Vanur\\OneDrive\\Рабочий стол\\test.txt");

            while (!f.EndOfStream)
            {
                string s = f.ReadLine();
                string[] words = s.Split();

                switch (words[0])
                {
                    case "Лекция":
                        var lecture = new Lecture();
                        lecture.Read(words);
                        lessons.Add(lecture);
                        break;
                    case "Практика":
                        var practice = new Practice();
                        practice.Read(words);
                        lessons.Add(practice);
                        break;
                    case "Лабораторная":
                        var lab = new Lab();
                        lab.Read(words);
                        lessons.Add(lab);
                        break;
                }
            }

            f.Close(); // Закрываем файл после чтения

            Console.WriteLine("Файл прочитан\n");

            foreach (var lesson in lessons)
            {
                lesson.Write(); // Вызываем Write() у всех объектов
            }

            Console.ReadLine();
        }
    }
}
