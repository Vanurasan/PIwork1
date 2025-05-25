using System;

namespace LessonLibrary
{
    public class Lab : Lesson
    {
        public string Equipment { get; protected set; }

        public override void Read(string[] words)
        {
            Date = DateTime.Parse(words[1]);
            Room = words[2];
            Teacher = words[3] + " " + words[4];
            Equipment = words[5];
        }

        public override string ToText()
        {
            return $"Лабораторная {Date:dd.MM.yyyy} Кабинет: {Room} Педагог: {Teacher} Оборудование: {Equipment}";
        }

        //public Lab(DateTime date, string room, string teacher, string equipment)
        //{
        //    Date = date;
        //    Room = room;
        //    Teacher = teacher;
        //    Equipment = equipment;
        //}
    }
}