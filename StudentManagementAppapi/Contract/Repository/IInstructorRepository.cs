using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface IInstructorRepository
    {
        Instructor GetByName(string fullname);
        void Create(Instructor instructor);
        List<Instructor> GetAllInstructors(Func<Instructor, bool> expression);
        bool IsExist(Func<Instructor, bool> expression);
        void RemoveInstructor(string instructorname);
        Instructor UpdateInstructor(Instructor instructor);
        int SaveChanges();
    }

}
