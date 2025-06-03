using Microsoft.AspNetCore.Http.HttpResults;
using Org.BouncyCastle.Crypto.Generators;
using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;
using StudentManagementAppapi.Model.Enum;

namespace StudentManagementAppapi.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorService(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public ResponseWrapperl<Guid> AddNewInstructor(SiginRequestModel sigin)
        {
            try
            {
                if (_instructorRepository.IsExist(c => c.User.Email == sigin.Email))
                    return new ResponseWrapperl<Guid>().Failed("Email already exists.");

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = sigin.Email,
                    PasswordHash = sigin.Password,
                    Username = sigin.UserName,
                    Role = Role.Instructor
                };

                var instructor = new Instructor
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    FirstName = sigin.FirstName,
                    LastName = sigin.LastName
                };

                _instructorRepository.Create(instructor);
                _instructorRepository.SaveChanges();

                return new ResponseWrapperl<Guid>().Success(instructor.Id, "Instructor created successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<Guid>().Failed($"An error occurred: {ex.Message}");
            }
        }


        public ResponseWrapperl<IEnumerable<ViewAllInstructors>> GetAllInstructor()
        {
            try
            {
                var instructors = _instructorRepository.GetAllInstructors(i => true);

                var dto = new ViewAllInstructors
                {
                    AllInstructors = instructors.Select(i => new InstructorDto
                    {
                        Id = i.Id,
                        FullName = i.FullName,
                        Email = i.User.Email
                    }).ToList()
                };
                return null;
                //return new ResponseWrapperl<ViewAllInstructors>().Success(instructors., "Fetched successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<IEnumerable<ViewAllInstructors>>().Failed($"An error occurred: {ex.Message}");
            }
        }


        public ResponseWrapperl<InstructorResponsemodel> GetInstructorByName(string fullname)
        {
            try
            {
                var instructor = _instructorRepository.GetByName(fullname);
                if (instructor == null)
                    return new ResponseWrapperl<InstructorResponsemodel>().Failed("Instructor not found.");

                var result = new InstructorResponsemodel
                {
                    Id = instructor.Id,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Email = instructor.User.Email,
                    Username = instructor.User.Username
                };
                return null;

               // return new ResponseWrapperl<Guid>().Success(result., "Found instructor.");              
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<InstructorResponsemodel>().Failed($"An error occurred: {ex.Message}");
            }
        }


        public ResponseWrapperl<Guid> RemoveInstructor(string fullname)
        {
            try
            {
                var instructor = _instructorRepository.GetByName(fullname);
                if (instructor == null)
                    return new ResponseWrapperl<Guid>().Failed("Instructor not found.");

                _instructorRepository.RemoveInstructor(fullname);
                _instructorRepository.SaveChanges();

                return new ResponseWrapperl<Guid>().Success(instructor.Id,"Instructor removed successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<Guid>().Failed($"An error occurred: {ex.Message}");
            }
        }



        public ResponseWrapperl<InstructorResponsemodel> UpdateInstructorProfile(UpdateInstructorProfile update)
        {
            try
            {
                var instructor = _instructorRepository.GetByName(update.Username);
                if (instructor == null)
                    return new ResponseWrapperl<InstructorResponsemodel>().Failed("Instructor not found.");

                instructor.FirstName = update.FirstName;
                instructor.LastName = update.LastName;

                _instructorRepository.UpdateInstructor(instructor);
                _instructorRepository.SaveChanges();

                var result = new InstructorResponsemodel
                {
                    Id = instructor.Id,
                    Username = instructor.User.Username,
                    Email = instructor.User.Email
                };

                return new ResponseWrapperl<InstructorResponsemodel>().Success(result,"Profile updated successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<InstructorResponsemodel>().Failed($"An error occurred: {ex.Message}");
            }
        }

    }
}
