using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Repository
{
    public interface ISchoolClassRepository
    {
        SchoolClass GetByName(string name);
        void Add(SchoolClass schoolClass);
        SchoolClass Update(SchoolClass schoolClass);
        void Delete(string classname);
        SchoolClass GetById(Guid id);
        List<SchoolClass> GetAll(Func<SchoolClass, bool> expression);
        int SaveChanges();
        bool IsExist(Func<SchoolClass, bool> expression);
    }
}
