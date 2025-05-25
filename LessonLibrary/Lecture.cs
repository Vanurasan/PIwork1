using System;

namespace LessonLibrary
{
    public class Lecture : Lesson
    {
        public int NumberOfGroups { get; protected set; }

        public override string ToText()
        {
            return $"Лекция {Date:dd.MM.yyyy} Кабинет: {Room} Педагог: {Teacher} Групп: {NumberOfGroups}";
        }

        public Lecture(DateTime date, string room, string teacher, int numberofgroups) : base(date, room, teacher)
        {
            NumberOfGroups = numberofgroups;
        }
    }
}
