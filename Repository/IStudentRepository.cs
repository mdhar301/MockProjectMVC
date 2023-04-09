using MVCMock.Models;

namespace MVCMock.Repository
{
    public interface IStudentRepository
    {
        public List<Student> GetStudents();

        public void AddStudent(Student student);

        public Student GetStudentById(int StudentId);

        public void UpdateStudent(int StudentId, Student student, int[] selectedhobbyIds);

        public void DeleteStudent(int StudentId);

        IEnumerable<Hobbies> GetAllHobbies();
        IEnumerable<Hobbies> GetHobbiesByIds(IEnumerable<int> ids);
    }
}
