using System;


namespace LessonLibrary
{
    public class Practice : Lesson
    {
        public int NumberOfTasks { get; protected set; }

        public Practice(DateTime date, string room, string teacher, int number) : base(date, room, teacher)
        {
            NumberOfTasks = number;
        }

        public override string ToText()
        {
            return $"Практика {Date:dd.MM.yyyy} Кабинет: {Room} Педагог: {Teacher} Кол-во задач: {NumberOfTasks}";
        }
    }
}
