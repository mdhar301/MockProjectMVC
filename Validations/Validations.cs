using FluentValidation;
using MVCMock.Models;

public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter a first name.");
        RuleFor(x => x.FirstName).Matches(@"^[a-zA-Z\s]+$").WithMessage("Do not enter special characters.");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter a last name.");
        RuleFor(x => x.LastName).Matches(@"^[a-zA-Z\s]+$").WithMessage("Do not enter special characters.");

        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Please enter a date of birth.")
                                   .Must(BeAtLeast18YearsOld).WithMessage("Student must be at least 18 years old.");
        RuleFor(x => x.CourseId).NotEmpty().WithMessage("Please select a course.");
     /*  RuleFor(x => x.StudentHobbies)
                    .NotEmpty().WithMessage("At least one hobby is required");
                       }*/

    /*private bool HaveAtLeastOneHobby(ICollection<StudentHobby> arg)
    {
        return !string.IsNullOrWhiteSpacear);
    }*/

    }
    private bool BeAtLeast18YearsOld(DateTime date)
    {
        var currentDate = DateTime.Now;
        var age = currentDate.Year - date.Year - ((currentDate.Month < date.Month || (currentDate.Month == date.Month && currentDate.Day < date.Day)) ? 1 : 0);
        return age >= 18;

    }

    private bool HaveAtLeastOneHobby(string hobbies)
    {
        return !string.IsNullOrWhiteSpace(hobbies);
    }
}
