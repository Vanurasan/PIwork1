using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using LessonLibrary;

namespace LessonViewer
{
    public partial class MainWindow : Window
    {

        private string? currentFilePath = null;
        private ObservableCollection<Lesson> lessons = new ObservableCollection<Lesson>();


        public MainWindow()
        {
            InitializeComponent();
            LessonListBox.ItemsSource = lessons;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    currentFilePath = dialog.FileName;
                    var loadedLessons = Parser.ParseFromFile(currentFilePath);
                    lessons.Clear();
                    foreach (var lesson in loadedLessons)
                        lessons.Add(lesson);
                }
                catch
                {
                    MessageBox.Show("Ошибка при чтении файла.");
                }
            }
        }

        private void AddLesson_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddLessonWindow();
            if (window.ShowDialog() == true && window.ResultLesson != null)
            {
                lessons.Add(window.ResultLesson);
            }
        }

        private void DeleteLesson_Click(object sender, RoutedEventArgs e)
        {
            if (LessonListBox.SelectedItem is Lesson selectedLesson)
                lessons.Remove(selectedLesson);
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                MessageBox.Show("Файл не открыт.");
                return;
            }

            try
            {
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

                File.WriteAllLines(currentFilePath, lines);
                MessageBox.Show("Файл успешно сохранён.");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении файла.");
            }
        }

    }
}
