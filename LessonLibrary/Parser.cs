using System;
using System.Collections.Generic;
using System.IO;

namespace LessonLibrary
{
    public static class Parser
    {
        public static List<Lesson> ParseFromFile(string filePath)
        {
            List<Lesson> lessons = new List<Lesson>();
            //ПОМЕНЯТЬ АДРЕС ЧИТАЕМОГО ФАЙЛА
            StreamReader f = new StreamReader(filePath);

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

            return lessons;

        }
    }
}
