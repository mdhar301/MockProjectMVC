using Microsoft.AspNetCore.Mvc;
using MVCMock.Models;
using System.Diagnostics;
using MVCMock.Repository;
using MVCMock.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVCMock.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository repo;
        private readonly DatabaseContext context;
        public HomeController(IStudentRepository repo, DatabaseContext context)
        {
            this.repo = repo;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var courses = context.Courses.ToList();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");
            var students = context.Students.Include(s => s.StudentHobbies).ToList();
            ViewBag.Hobbies = context.Hobbies.ToList();



            return View(repo.GetStudents());
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            ViewBag.Courses = context.Courses.ToList();
            ViewBag.Hobbies = context.Hobbies.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult AddStudent(Student student, int[] selectedHobbyIds) 
        {
            ViewBag.Courses = context.Courses.ToList();
            ViewBag.Hobbies = context.Hobbies.ToList();

            var validator = new StudentValidator();
            var result = validator.Validate(student);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                var hobbies = repo.GetAllHobbies();
                return View(student);
            }
            var selectedHobbies = repo.GetHobbiesByIds(selectedHobbyIds);
            student.StudentHobbies = selectedHobbies.Select(h => new StudentHobby { Student = student, Hobbies = h }).ToList();
            var students = context.Students.Include(s => s.StudentHobbies).ToList();

            repo.AddStudent(student);
            return RedirectToAction("Index");
            

        }
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = context.Students
        .Include(x => x.StudentHobbies)
        .FirstOrDefault(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            ViewBag.Courses = context.Courses.ToList();
            ViewBag.Hobbies = context.Hobbies.ToList();
            var selectedHobbies = student.StudentHobbies.Select(x => x.Hobbies).ToArray();
            ViewBag.SelectedHobbyIds = selectedHobbies;
            return View(repo.GetStudentById(id));
        }
    [HttpPost]
    public IActionResult EditStudent(int id, Student student, int[] selectedHobbyIds)
    {
        var validator = new StudentValidator();
        var result = validator.Validate(student);
        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            ViewBag.Courses = context.Courses.ToList();
            ViewBag.Hobbies = context.Hobbies.ToList();
            return View(student);
        }
            repo.UpdateStudent(id, student, selectedHobbyIds);
            return RedirectToAction("Index");     
    
    }


        [HttpGet]

        public IActionResult DeleteStudent(int id)
        {
            repo.DeleteStudent(id);
            return RedirectToAction("Index");
        }

    }
}
