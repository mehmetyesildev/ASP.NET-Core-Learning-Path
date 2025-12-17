namespace basics.Models
{
    public class Repository
    {
        private static readonly List<Course> _courses = new();

        static Repository()
        {
            _courses = new List<Course>()
            {
                new Course() {
                    Id=1,
                    Title="Aspnet Core kursu",
                    Description="Güzel bir kurs",
                    image="1.jpg",
                    Tags= new string[]{"aspnet","web geliştirme"},
                    isActive=true,
                    ishome= true
                },
                new Course() {
                    Id=2,
                    Title="Php",
                    Description="Güzel bir kurs",
                    image= "2.jpg",
                    Tags= new string[]{"php","web geliştirme"},
                    isActive=true,
                    ishome= true
                    },
                    
                new Course() {
                    Id=3,
                    Title="Dijango",
                    Description="Güzel bir kurs",
                    image="3.jpg",
                    isActive=true,
                    ishome= false},
                new Course() {
                    Id=4,
                    Title="jaVA",
                    Description="Güzel bir kurs",
                    image= "1.jpg",
                    isActive=true,
                    ishome= true
                    },

            };
        }
        public static List<Course> Courses
        {
            get
            {
                return _courses;
            }
        }
        public static Course? GetById(int? id)
        {
            return _courses.FirstOrDefault(c => c.Id == id);
        }
    }
}