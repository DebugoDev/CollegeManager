namespace CollegeManager.Models
{
    public class Student
    {
        public string? Name { get; set; }
        public string? Registration { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public override string ToString()
        {
            return $"{Registration} - {Name}";
        }
    }
}