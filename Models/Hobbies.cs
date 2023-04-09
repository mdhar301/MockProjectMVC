using MVCMock.Models;

public class Hobbies
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<StudentHobby> StudentHobbies { get; set; }
}