using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;



namespace StudentManagementAppapi.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;

        public CourseService(ICoursesRepository coursesRepository, IInstructorRepository instructorRepository,
            ISchoolClassRepository schoolClassRepository)
        {
            _coursesRepository = coursesRepository;
            _instructorRepository = instructorRepository;
            _schoolClassRepository = schoolClassRepository; 
        }


        public ResponseWrapperl<Guid> AddNewCourse(CreateCourseRequestModel createCourseRequestModel)
        {
           
            bool courseExists = _coursesRepository.IsExist(c => c.CourseName == createCourseRequestModel.CourseName);
            if (courseExists)
            {
                return new ResponseWrapperl<Guid>().Failed("Course with the same name already exists.");
            }

            try
            {
                
                Instructor instructor = _instructorRepository.GetByName(createCourseRequestModel.InstructorName);
                if (instructor == null)
                {
                    instructor = new Instructor
                    {
                        Id = Guid.NewGuid(),
                        FirstName = createCourseRequestModel.InstructorName
                        
                    };
                    _instructorRepository.Create(instructor);
                }

                
                SchoolClass schoolClass = _schoolClassRepository.GetByName(createCourseRequestModel.Class);
                if (schoolClass == null)
                {
                    return new ResponseWrapperl<Guid>().Failed("Specified class does not exist.");
                }

               
                Course course = new Course
                {
                    Id = Guid.NewGuid(),
                    CourseName = createCourseRequestModel.CourseName,
                    CourseCode = string.IsNullOrWhiteSpace(createCourseRequestModel.CourseCode)
                                 ? Course.GenerateCode()
                                 : createCourseRequestModel.CourseCode,
                    InstructorId = instructor.Id,
                    ClassId = schoolClass.Id
                };

                _coursesRepository.CreateCourse(course);
                _coursesRepository.SaveChanges();

                return new ResponseWrapperl<Guid>().Success(course.Id, "Course created successfully.");
            }
            catch (Exception ex)
            {
                
                return new ResponseWrapperl<Guid>().Failed("An error occurred while adding the course. Please try again.");
            }
        }





        public ResponseWrapperl<ViewAllCourses> UpdateCourse(CreateCourseRequestModel createCourseRequestModel)
        {
            try
            {
               
                var existingCourse = _coursesRepository.Get(c => c.CourseName == createCourseRequestModel.CourseName);
                if (existingCourse == null)
                {
                    return new ResponseWrapperl<ViewAllCourses>().Failed("Course not found.");
                }

               
                Instructor instructor = _instructorRepository.GetByName(createCourseRequestModel.InstructorName);
                if (instructor == null)
                {
                    instructor = new Instructor
                    {
                        Id = Guid.NewGuid(),
                        FirstName = createCourseRequestModel.InstructorName
                        
                    };
                    _instructorRepository.Create(instructor);
                    _instructorRepository.SaveChanges();
                }

               
                var schoolClass = _schoolClassRepository.GetByName(createCourseRequestModel.Class);
                if (schoolClass == null)
                {
                    return new ResponseWrapperl<ViewAllCourses>().Failed("Specified class does not exist.");
                }

                
                existingCourse.CourseCode = string.IsNullOrWhiteSpace(createCourseRequestModel.CourseCode)
                                            ? existingCourse.CourseCode
                                            : createCourseRequestModel.CourseCode;

                existingCourse.InstructorId = instructor.Id;
                existingCourse.ClassId = schoolClass.Id;

                
                _coursesRepository.UpdateCourse(existingCourse);
                _coursesRepository.SaveChanges();

               
                var updatedCourseDto = new ViewAllCourses
                {
                    AllavailableCourses = new List<Course> { existingCourse }
                };

                return new ResponseWrapperl<ViewAllCourses>().Success(updatedCourseDto, "Course updated successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<ViewAllCourses>().Failed($"An error occurred while updating the course: {ex.Message}");
            }
        }


        public ResponseWrapperl<Guid> DeleteCourse(string coursename)
        {
            try
            {
                bool exists = _coursesRepository.IsExist(c => c.CourseName == coursename);
                if (!exists)
                {
                    return new ResponseWrapperl<Guid>().Failed("Invalid or non-existent course name.");
                }

                _coursesRepository.DeleteCourse(coursename);
                int result = _coursesRepository.SaveChanges();

                if (result > 0)
                    return new ResponseWrapperl<Guid>().Success(Guid.Empty, "Course deleted successfully.");

                return new ResponseWrapperl<Guid>().Failed("Unable to delete course due to server error.");
            }
            catch (Exception e)
            {
                return new ResponseWrapperl<Guid>().Failed($"An error occurred: {e.Message}");
            }
        }

        public ResponseWrapperl<IEnumerable<Course>> GetAllCourse()
        {
            try
            {
                var courses = _coursesRepository.GetAllCourse(c => true);

                if (!courses.Any())
                    return new ResponseWrapperl<IEnumerable<Course>>().Failed("No courses found.");

                return new ResponseWrapperl<IEnumerable<Course>>().Success(courses, "Courses retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<IEnumerable<Course>>().Failed($"An error occurred: {ex.Message}");
            }
        }

        public ResponseWrapperl<Course> GetCourseByName(string coursename)
        {
            try
            {
                var course = _coursesRepository.Get(c => c.CourseName == coursename);
                if (course == null)
                    return new ResponseWrapperl<Course>().Failed("Course not found.");

                return new ResponseWrapperl<Course>().Success(course, "Course retrieved successfully.");
            }
            catch (Exception e)
            {
                return new ResponseWrapperl<Course>().Failed($"An error occurred: {e.Message}");
            }
        }

    }
}
