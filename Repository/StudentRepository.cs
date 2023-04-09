using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVCMock.Data;
using MVCMock.Models;
using System.Linq;

namespace MVCMock.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DatabaseContext context;

        public StudentRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void AddStudent(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }

        public Student? GetStudentById(int StudentId)
        {
            return context.Students.Find(StudentId);
        }   

        public void DeleteStudent(int StudentId)
        {
            var student = context.Students.Find(StudentId);
            if(student!=null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
        }

        public List<Student> GetStudents()
        {
            return context.Students.Include(s=> s.StudentHobbies).ToList();
        }

        public void UpdateStudent(int studentId, Student updateStudent, int[] selectedhobbyIds)
        {
            var student = context.Students
                .Include(s => s.StudentHobbies)
                .SingleOrDefault(s => s.Id == studentId);

            if (student != null)
            {
                student.FirstName = updateStudent.FirstName;
                student.LastName = updateStudent.LastName;
                student.DateOfBirth = updateStudent.DateOfBirth;

                if (student.CourseId != updateStudent.CourseId)
                {
                    var newCourse = context.Courses.Find(updateStudent.CourseId);
                    student.Course = newCourse;
                }

                // Update hobbies only if there is a change in selectedhobbyIds
                if (selectedhobbyIds != null)
                {
                    // Remove existing hobbies
                    student.StudentHobbies.Clear();

                }
                    

                    // Add new hobbies
                    foreach (var hobbyId in selectedhobbyIds)
                    {
                        var hobby = context.Hobbies.Find(hobbyId);
                        if (hobby != null)
                        {
                            student.StudentHobbies.Add(new StudentHobby { Student = student, Hobbies = hobby });
                        }
                    }
                

                context.SaveChanges();
            }
        }




        public IEnumerable<Hobbies> GetHobbiesByIds(IEnumerable<int> ids)
        {
            return context.Hobbies.Where(h => ids.Contains(h.Id));
        }
        public IEnumerable<Hobbies> GetAllHobbies()
        {
            return context.Hobbies.ToList();
        }

    }
}
