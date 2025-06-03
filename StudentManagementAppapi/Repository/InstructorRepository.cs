using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace StudentManagementAppapi.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly StudentManagementDbContext _studentManagementDbContext;

        public InstructorRepository(StudentManagementDbContext studentManagementDbContext) =>
            _studentManagementDbContext = studentManagementDbContext;

        public void Create(Instructor instructor)
        {
            _studentManagementDbContext.Instructors.Add(instructor);
            
        }

        public List<Instructor> GetAllInstructors(Func<Instructor, bool> expression)
        {
            var instructors = _studentManagementDbContext.Instructors.Where(expression);
            var instructor = instructors.Skip(1).Take(15).ToList();
            return instructor;
        }

        public Instructor GetByName(string fullname)
        {
            var instructor = _studentManagementDbContext.Instructors.FirstOrDefault(i => i.FullName == fullname);
            return instructor;
        }

        public bool IsExist(Func<Instructor, bool> expression)
        {
            return _studentManagementDbContext.Instructors.Any(expression);
        }

        public void RemoveInstructor(string instructorname)
        {
            var check = _studentManagementDbContext.Instructors.FirstOrDefault(c => c.FullName == instructorname);
            var delete = _studentManagementDbContext.Remove(check);
           
        }

        public int SaveChanges()
        {
            return _studentManagementDbContext.SaveChanges();
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            var existingInstructor = _studentManagementDbContext.Instructors.Find(instructor.FullName);
            if (existingInstructor == null)
                return null;

            _studentManagementDbContext.Entry(existingInstructor).CurrentValues.SetValues(instructor);
            return existingInstructor;
        }
    }
}
