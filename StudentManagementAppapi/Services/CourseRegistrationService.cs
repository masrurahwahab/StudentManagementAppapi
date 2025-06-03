using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
   
    public class CourseRegistrationService : ICourseRegistrationService
    {
        private readonly ICourseRegistrationRepository _courseRegistrationRepository;

        public CourseRegistrationService(ICourseRegistrationRepository courseRegistrationRepository)
        {
            _courseRegistrationRepository = courseRegistrationRepository;
        }

        public ResponseWrapperl<Guid> EnrollForACourse(Enrollforacourse enrollforacourse)
        {
            try
            {
                var alreadyExists = _courseRegistrationRepository.IsExist(
                    c => c.Student.FullName == enrollforacourse.FullName &&
                         c.Course.CourseName == enrollforacourse.CourseName
                );

                if (alreadyExists)
                {
                    return new ResponseWrapperl<Guid>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Student already enrolled in this course." },
                        
                    };
                }

                
                var course = new Course
                {
                    CourseName = enrollforacourse.CourseName
                };

                var student = new Student
                {
                    FirstName = enrollforacourse.FirstName
                };

                var courseRegistration = new CourseRegistration
                {
                    Id = Guid.NewGuid(),
                    Course = course,
                    Student = student,
                    RegisteredAt = DateTime.UtcNow
                };

                _courseRegistrationRepository.EnrolForCourse(courseRegistration);
                _courseRegistrationRepository.SaveChanges();

                return new ResponseWrapperl<Guid>
                {
                    IsSuccessful = true,
                    Messages = new List<string> {"Enrollment successful" },                    
                    Data = courseRegistration.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<Guid>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" },
                };
            }
        }

        public ResponseWrapperl<IEnumerable<ViewAllEnrolledCourses>> GetAllEnrollCourses()
        {
            try
            {
                var all = _courseRegistrationRepository
                    .GetAllCourseRegistration(_ => true)
                    .Select(c => new ViewAllEnrolledCourses
                    {
                        CourseName = c.Course.CourseName,
                        StudentName = c.Student.FullName,
                        DateRegistered = c.RegisteredAt
                    });

                return new ResponseWrapperl<IEnumerable<ViewAllEnrolledCourses>>
                {
                    IsSuccessful = true,
                    Data = all
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<IEnumerable<ViewAllEnrolledCourses>>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" },
                };
            }
        }

        public ResponseWrapperl<ViewAllEnrolledCourses> GetEnrollCourse(string fullname)
        {
            try
            {
                var course = _courseRegistrationRepository.Get(c => c.Student.FullName == fullname);

                if (course == null)
                {
                    return new ResponseWrapperl<ViewAllEnrolledCourses>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Course not found." },
                       
                    };
                }

                var view = new ViewAllEnrolledCourses
                {
                    CourseName = course.Course.CourseName,
                    StudentName = course.Student.FullName,
                    DateRegistered = course.RegisteredAt
                };

                return new ResponseWrapperl<ViewAllEnrolledCourses>
                {
                    IsSuccessful = true,
                    Data = view
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<ViewAllEnrolledCourses>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" },
                };
            }
        }
    }

}
