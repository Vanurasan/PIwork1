using System;


namespace LessonLibrary
{
    public class Practice : Lesson
    {
        public int NumberOfTasks { get; protected set; }

        public override void Read(string[] words)
        {
            Date = DateTime.Parse(words[1]);
            Room = words[2];
            Teacher = words[3] + " " + words[4];
            NumberOfTasks = Convert.ToInt32(words[5]);
        }

        public override string ToText()
        {
            return $"Практика {Date:dd.MM.yyyy} Кабинет: {Room} Педагог: {Teacher} Кол-во задач: {NumberOfTasks}";
        }
    }
}
