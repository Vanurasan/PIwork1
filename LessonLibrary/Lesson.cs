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

        public abstract void Read(string[] words);
        public abstract string ToText(); // для отображения в WPF
        public string DisplayText => ToText();


    }


    
}
