namespace LessonLibrary.Tests
{
    [TestClass]
    public sealed class AddLessonTests
    {
        [TestMethod]
        public void AddLesson_Lecture()
        {
            //arrange
            string type = "Лекция";
            string dateStr = "2025.02.02";
            string room = "123";
            string teacher = "Ефимов В.В.";
            string extra = "8";
            Lesson expected = new Lecture(Convert.ToDateTime(dateStr), room, teacher, Convert.ToInt32(extra));

            //act
            Lesson actual = LessonFactory.CreateLesson(type, dateStr, room, teacher, extra);

            //assert
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.Room, actual.Room);
            Assert.AreEqual(expected.Teacher, actual.Teacher);

            var expectedLecture = expected as Lecture;
            var actualLecture = actual as Lecture;
            Assert.IsNotNull(expectedLecture);
            Assert.IsNotNull(actualLecture);
            Assert.AreEqual(expectedLecture.NumberOfGroups, actualLecture.NumberOfGroups);

        }
    }
}
