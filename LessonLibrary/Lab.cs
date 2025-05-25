using System;

namespace LessonLibrary
{
    public class Lab : Lesson
    {
        public string Equipment { get; protected set; }

        public override string ToText()
        {
            return $"Лабораторная {Date:dd.MM.yyyy} Кабинет: {Room} Педагог: {Teacher} Оборудование: {Equipment}";
        }

        public Lab(DateTime date, string room, string teacher, string equipment) : base(date, room, teacher)
        {
            Equipment = equipment;
        }
    }
}