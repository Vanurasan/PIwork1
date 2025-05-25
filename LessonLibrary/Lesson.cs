using System;
using System.Collections.Generic;
using System.IO;

namespace LessonLibrary
{
    public abstract class Lesson
    {
        public DateTime Date { get; protected set; }
        public string Room { get; protected set; }
        public string Teacher { get; protected set; }

        //Заменим Read на конструктор. Не совсем соответсвует первому заданию но,
        //такое исполнение кажется лучше чем такой метод

        public Lesson(DateTime date, string room, string teacher) 
        {
            Date = date;
            Room = room;
            Teacher = teacher;
        }
        public abstract string ToText(); // для отображения в WPF
        public string DisplayText => ToText();


    }


    
}
