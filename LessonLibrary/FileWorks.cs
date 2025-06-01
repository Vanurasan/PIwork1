using System;
using System.Collections.Generic;
using System.IO;

namespace LessonLibrary
{
    public static class FileWorks
    {
        public static List<Lesson> ParseFromFile(string filePath)
        {
            List<Lesson> lessons = new List<Lesson>();

            //Я нашел как по-другому описать данный алгоритм, так что заменим его и добавим try catch
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                try
                {
                    var words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length < 5)
                        throw new FormatException("Недостаточно данных в строке");

                    Lesson lesson = CreateLessonFromWords(words);
                    lessons.Add(lesson);
                }
                catch (Exception ex)
                {
                    // Пока логгер не реализован — просто заглушка
                    Logger.LogError($"Ошибка разбора строки: {ex.Message}");
                }
            }

            return lessons;
        }

        private static Lesson CreateLessonFromWords(string[] words)
        {
            return words[0].ToLower() switch
            {
                "лекция" => LessonFactory.CreateLesson("Лекция", words[1], words[2], words[3] + " " + words[4], words[5]),
                "практика" => LessonFactory.CreateLesson("Практика", words[1], words[2], words[3] + " " + words[4], words[5]),
                "лабораторная" => LessonFactory.CreateLesson("Лабораторная", words[1], words[2], words[3] + " " + words[4], words[5]),
                _ => throw new FormatException("Неизвестный тип занятия: " + words[0])
            };

        }

        public static Lesson CreateLessonFromCsvParts(string[] parts)
        {
            if (parts.Length < 5)
                Logger.LogError("Недостаточно данных для создания урока.");

            string type = parts[0];
            string date = parts[1];
            string room = parts[2];
            string teacher = parts[3];
            string extra = parts[4];

            return LessonFactory.CreateLesson(type, date, room, teacher, extra);
        }


        public static void Save(string filePath, IEnumerable<Lesson> lessons)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Путь к файлу не может быть пустым. Выберите существующий или создайте новый файл");

            var lines = new List<string>();
            foreach (var lesson in lessons)
            {
                string type = lesson switch
                {
                    Lecture => "Лекция",
                    Practice => "Практика",
                    Lab => "Лабораторная",
                    _ => "Неизвестно"
                };

                string line = type + " " + lesson.Date.ToString("yyyy.MM.dd") + " " +
                              lesson.Room + " " + lesson.Teacher + " ";

                if (lesson is Lecture l)
                    line += l.NumberOfGroups;
                else if (lesson is Practice p)
                    line += p.NumberOfTasks;
                else if (lesson is Lab lab)
                    line += lab.Equipment;

                lines.Add(line);
            }

            File.WriteAllLines(filePath, lines);
            Logger.LogInfo("Файл успешно сохранён");
        }
    }
}
