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
                string date = DateBox.Text.Trim();

                ResultLesson = LessonFactory.CreateLesson(type, date, room, teacher, extra);


                this.DialogResult = true;  // закрыть окно или сбросить поля
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла непредвиденная ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
