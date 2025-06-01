using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonLibrary
{
    public static class CommandProcessor
    {
        public static void Execute(string lessonsfilePath, string filePath, ObservableCollection<Lesson> lessons)
        {
            var commands = File.ReadAllLines(filePath);
            int errorCount = 0;

            foreach (var command in commands)
            {
                if (string.IsNullOrWhiteSpace(command))
                    continue;

                try
                {
                    var trimmed = command.Trim();

                    if (trimmed.StartsWith("ADD ", StringComparison.OrdinalIgnoreCase))
                    {
                        string data = trimmed.Substring(4);
                        var parts = data.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        var lesson = FileWorks.CreateLessonFromCsvParts(parts);
                        lessons.Add(lesson);
                        Logger.LogInfo($"Добавлен урок: {lesson.Teacher} на {lesson.Date:yyyy-MM-dd}");
                    }
                    else if (trimmed.StartsWith("REM ", StringComparison.OrdinalIgnoreCase))
                    {
                        string condition = trimmed.Substring(4);
                        RemoveByCondition(lessons, condition);
                        Logger.LogInfo($"Удаление по условию: '{condition}' выполнено");
                    }
                    else if (trimmed.StartsWith("SAVE ", StringComparison.OrdinalIgnoreCase))
                    {
                        FileWorks.Save(lessonsfilePath, lessons);
                        Logger.LogInfo($"Сохранение в файл '{lessonsfilePath}' выполнено");
                    }
                    else
                    {
                        throw new FormatException("Неизвестная команда: " + trimmed);
                    }
                }
                catch (Exception ex)
                {
                    errorCount++;
                    Logger.LogError($"Ошибка в строке команды: \"{command}\" — {ex.GetType().Name}: {ex.Message}");
                    // Продолжаем выполнение следующих команд
                }
            }

            if (errorCount > 0)
                Logger.LogWarning($"Выполнение команд завершено с {errorCount} ошибками.");
            else
                Logger.LogInfo("Выполнение команд завершено без ошибок.");
        }



        public static void RemoveByCondition(ObservableCollection<Lesson> lessons, string condition)
        {
            try
            {
                var parts = condition.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3)
                    throw new FormatException("Неверный формат условия.");

                string field = parts[0].ToLower();
                string op = parts[1];
                string value = parts[2];

                var toRemove = lessons.Where(lesson => EvaluateCondition(lesson, field, op, value)).ToList();
                foreach (var lesson in toRemove)
                    lessons.Remove(lesson);

                Logger.LogInfo($"Удалено {toRemove.Count} объектов по условию '{condition}'");
            }
            catch (Exception ex)
            {
                Logger.LogError("Ошибка при разборе команды REM: " + ex.Message);
            }
        }

        private static bool EvaluateCondition(Lesson lesson, string field, string op, string value)
        {
            switch (field)
            {
                case "date":
                    if (DateTime.TryParse(value, out var targetDate))
                    {
                        int cmp = DateTime.Compare(lesson.Date.Date, targetDate.Date);
                        return Compare(cmp, op);
                    }
                    break;

                case "teacher":
                    return CompareString(lesson.Teacher, op, value);

                case "room":
                    return CompareString(lesson.Room, op, value);

                case "sum": // Числовое поле: зависит от типа урока
                    int actual = lesson switch
                    {
                        Lecture l => l.NumberOfGroups,
                        Practice p => p.NumberOfTasks,
                        Lab lab => int.TryParse(lab.Equipment, out var e) ? e : -1,
                        _ => -1
                    };

                    if (int.TryParse(value, out int target))
                        return Compare(actual.CompareTo(target), op);
                    break;
            }

            return false;
        }

        private static bool Compare(int cmp, string op) => op switch
        {
            "==" => cmp == 0,
            "!=" => cmp != 0,
            ">" => cmp > 0,
            "<" => cmp < 0,
            ">=" => cmp >= 0,
            "<=" => cmp <= 0,
            _ => false
        };

        private static bool CompareString(string actual, string op, string expected)
        {
            string normActual = Normalize(actual);
            string normExpected = Normalize(expected);

            return op switch
            {
                "==" => normActual == normExpected,
                "!=" => normActual != normExpected,
                _ => false
            };
        }

        private static string Normalize(string input)
        {
            return input
                .Trim()
                .ToLowerInvariant()
                .Replace(".", "")
                .Replace("  ", " "); // убрать двойные пробелы, если есть
        }



        //private static void SaveLessonsToFile(ObservableCollection<Lesson> lessons, string path)
        //{
        //    var lines = new List<string>();

        //    foreach (var lesson in lessons)
        //    {
        //        string type = lesson switch
        //        {
        //            Lecture => "Лекция",
        //            Practice => "Практика",
        //            Lab => "Лабораторная",
        //            _ => "Неизвестно"
        //        };

        //        string line = type + ";" + lesson.Date.ToString("yyyy.MM.dd") + ";" +
        //                      lesson.Room + ";" + lesson.Teacher + ";";

        //        if (lesson is Lecture l)
        //            line += l.NumberOfGroups;
        //        else if (lesson is Practice p)
        //            line += p.NumberOfTasks;
        //        else if (lesson is Lab lab)
        //            line += lab.Equipment;

        //        lines.Add(line);
        //    }

        //    File.WriteAllLines(path, lines);
        //}
    }

}
