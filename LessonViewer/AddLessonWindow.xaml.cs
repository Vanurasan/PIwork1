using LessonLibrary;
using System;
using System.Windows;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LessonViewer
{
    public partial class AddLessonWindow : Window
    {
        public Lesson? ResultLesson { get; private set; }

        public AddLessonWindow()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string type = (LessonTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()?.Trim() ?? "";
                string room = RoomBox.Text.Trim();
                string teacher = TeacherBox.Text.Trim();
                string extra = ExtraBox.Text.Trim();

                if (!DateTime.TryParse(DateBox.Text, out DateTime date))
                {
                    MessageBox.Show("Введите корректную дату в формате ГГГГ.ММ.ДД (например, 2025.02.05)");
                    return;
                }

                if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(room) ||
                    string.IsNullOrWhiteSpace(teacher) || string.IsNullOrWhiteSpace(extra))
                {
                    MessageBox.Show("Заполните все поля.");
                    return;
                }

                string outputstring = $"заглушка {date:yyyy.MM.dd} {room} {teacher} {extra}";
                string[] words = outputstring.Split();

                switch (type.ToLower())
                {
                    case "лекция":
                        if (!int.TryParse(extra, out int groups))
                        {
                            MessageBox.Show("Введите число групп.");
                            return;
                        }

                        ResultLesson = new Lecture();
                        break;

                    case "практика":
                        if (!int.TryParse(extra, out int tasks))
                        {
                            MessageBox.Show("Введите число заданий.");
                            return;
                        }

                        ResultLesson = new Practice();
                        break;

                    case "лабораторная":
                        
                        ResultLesson = new Lab();
                        break;

                    default:
                        MessageBox.Show("Неизвестный тип занятия.");
                        return;
                }

                ResultLesson.Read(words);
                 
                DialogResult = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при создании урока.");
            }
        }
    }
}
