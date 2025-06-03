using Microsoft.EntityFrameworkCore;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Data;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Repository
{
    public class SchoolClassRepository : ISchoolClassRepository
    {
        private readonly StudentManagementDbContext _studentManagementDbContext;

        public SchoolClassRepository(StudentManagementDbContext studentManagementDbContext)
        {
            _studentManagementDbContext = studentManagementDbContext;
        }

        public void Add(SchoolClass schoolClass)
        {
            _studentManagementDbContext.SchoolClasses.Add(schoolClass);
             
        }

        public void Delete(string classname)
        {
            var check = _studentManagementDbContext.SchoolClasses.FirstOrDefault(c => c.Name == classname);
            var delete = _studentManagementDbContext.SchoolClasses.Remove(check);
            
        }

        public List<SchoolClass> GetAll(Func<SchoolClass, bool> expression)
        {
            var get = _studentManagementDbContext.SchoolClasses.Where(expression);
            var check = get.Skip(0).Take(6).ToList();
            return check;
        }

        public SchoolClass GetById(Guid id)
        {
            var get = _studentManagementDbContext.SchoolClasses.FirstOrDefault(c => c.Id == id);
            return get;
        }

        public SchoolClass GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            return _studentManagementDbContext.SchoolClasses
                           .FirstOrDefault(sc => sc.Name.ToLower() == name.ToLower());
        }

        public bool IsExist(Func<SchoolClass, bool> expression)
        {
            return _studentManagementDbContext.SchoolClasses.Any(expression);
        }

        public int SaveChanges()
        {
            return _studentManagementDbContext.SaveChanges();
        }


        public SchoolClass Update(SchoolClass schoolClass)
        {
            var existingclass = _studentManagementDbContext.SchoolClasses.Find(schoolClass.Name);
            if (existingclass == null)
                return null;

            _studentManagementDbContext.Entry(existingclass).CurrentValues.SetValues(schoolClass);
            return existingclass;
        }
    }
}
