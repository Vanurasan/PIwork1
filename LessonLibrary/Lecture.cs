using System;

namespace LessonLibrary
{
    public class Lecture : Lesson
    {
        public int NumberOfGroups { get; protected set; }

        public override void Read(string[] words)
        {
            Date = DateTime.Parse(words[1]);
            Room = words[2];
            Teacher = words[3] + " " + words[4];
            NumberOfGroups = Convert.ToInt32(words[5]);
        }

        public override string ToText()
        {
            return $"Лекция {Date:dd.MM.yyyy} Кабинет: {Room} Педагог: {Teacher} Групп: {NumberOfGroups}";
        }

        //public Lecture(DateTime date, string room, string teacher, int numberofgroups)
        //{
        //    Date = date;
        //    Room = room;
        //    Teacher = teacher;
        //    NumberOfGroups = numberofgroups;
        //}
    }
}
