namespace CollegeManager.Models
{
    public class Course
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }
    }
}