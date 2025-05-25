using LessonLibrary;

namespace LessonLibrary
{
    //Класс для добавление 
    public static class LessonFactory
    {
        public static Lesson CreateLesson(
            string type,
            string dateStr,
            string room,
            string teacher,
            string extra)
        {
            if (!DateTime.TryParse(dateStr, out DateTime date))
                throw new ArgumentException("Дата введена некорректно");

            if (string.IsNullOrWhiteSpace(room))
                throw new ArgumentException("Аудитория не может быть пустой");

            if (string.IsNullOrWhiteSpace(teacher))
                throw new ArgumentException("Преподаватель не может быть пустым");

            switch (type.ToLower())
            {
                case "лекция":
                    if (!int.TryParse(extra, out int numberOfGroups))
                        throw new ArgumentException("Количество групп введено некорректно");
                    return new Lecture(date, room, teacher, numberOfGroups);

                case "практика":
                    if (!int.TryParse(extra, out int numberOfTasks))
                        throw new ArgumentException("Количество заданий введено некорректно");
                    return new Practice(date, room, teacher, numberOfTasks);

                case "лабораторная":
                    if (string.IsNullOrWhiteSpace(extra))
                        throw new ArgumentException("Оборудование не может быть пустым");
                    return new Lab(date, room, teacher, extra);

                default:
                    throw new ArgumentException("Неизвестный тип занятия");
            }
        }
    }
}
