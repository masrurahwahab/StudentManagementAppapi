using StudentManagementAppapi.Contract.Repository;
using StudentManagementAppapi.Contract.Services;
using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Services
{
    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository _repository;

        public SchoolClassService(ISchoolClassRepository repository)
        {
            _repository = repository;
        }

        public ResponseWrapperl<bool> Add(SchoolClassRequestModel model)
        {
            try
            {
                if (_repository.IsExist(c => c.Name == model.Name))
                    return new ResponseWrapperl<bool>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Class already exists." }
                    };

                var entity = new SchoolClass
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Section = model.Section
                };

                _repository.Add(entity);
                _repository.SaveChanges();

                return new ResponseWrapperl<bool>
                {
                    IsSuccessful = true,
                    Messages = new List<string> { "Class added successfully." },
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<bool>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" }
                };
            }
        }

        public ResponseWrapperl<bool> Delete(string className)
        {
            try
            {
                if (!_repository.IsExist(c => c.Name == className))
                    return new ResponseWrapperl<bool>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Class not found." }
                    };

                _repository.Delete(className);
                _repository.SaveChanges();

                return new ResponseWrapperl<bool>
                {
                    IsSuccessful = true,
                    Messages = new List<string> { "Deleted successfully." },
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<bool>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" }
                };
            }
        }

        public ResponseWrapperl<List<SchoolClassResponseModel>> GetAll()
        {
            try
            {
                var classes = _repository.GetAll(c => true);

                var result = classes.Select(c => new SchoolClassResponseModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Section = c.Section,
                    StudentCount = c.Students?.Count ?? 0,
                    CourseCount = c.Courses?.Count ?? 0
                }).ToList();

                return new ResponseWrapperl<List<SchoolClassResponseModel>>
                {
                    IsSuccessful = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<List<SchoolClassResponseModel>>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" }
                };
            }
        }

        public ResponseWrapperl<SchoolClassResponseModel> GetById(Guid id)
        {
            try
            {
                var result = _repository.GetById(id);

                if (result == null)
                {
                    return new ResponseWrapperl<SchoolClassResponseModel>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Class not found." }
                    };
                }

                var responseModel = new SchoolClassResponseModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    Section = result.Section,
                    StudentCount = result.Students?.Count ?? 0,
                    CourseCount = result.Courses?.Count ?? 0
                };

                return new ResponseWrapperl<SchoolClassResponseModel>
                {
                    IsSuccessful = true,
                    Data = responseModel
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<SchoolClassResponseModel>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" }
                };
            }
        }

        public ResponseWrapperl<SchoolClassResponseModel> GetByName(string name)
        {
            try
            {
                var result = _repository.GetByName(name);

                if (result == null)
                {
                    return new ResponseWrapperl<SchoolClassResponseModel>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Class not found." }
                    };
                }

                var responseModel = new SchoolClassResponseModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    Section = result.Section,
                    StudentCount = result.Students?.Count ?? 0,
                    CourseCount = result.Courses?.Count ?? 0
                };

                return new ResponseWrapperl<SchoolClassResponseModel>
                {
                    IsSuccessful = true,
                    Data = responseModel
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<SchoolClassResponseModel>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" }
                };
            }
        }

        public ResponseWrapperl<SchoolClassResponseModel> Update(Guid id, SchoolClassRequestModel model)
        {
            try
            {
                var existingClass = _repository.GetById(id);
                if (existingClass == null)
                {
                    return new ResponseWrapperl<SchoolClassResponseModel>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Class not found for update." }
                    };
                }

                existingClass.Name = model.Name;
                existingClass.Section = model.Section;

                var updated = _repository.Update(existingClass);
                if (updated == null)
                {
                    return new ResponseWrapperl<SchoolClassResponseModel>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Failed to update class." }
                    };
                }

                _repository.SaveChanges();

                var responseModel = new SchoolClassResponseModel
                {
                    Id = updated.Id,
                    Name = updated.Name,
                    Section = updated.Section,
                    StudentCount = updated.Students?.Count ?? 0,
                    CourseCount = updated.Courses?.Count ?? 0
                };

                return new ResponseWrapperl<SchoolClassResponseModel>
                {
                    IsSuccessful = true,
                    Messages = new List<string> { "Class updated successfully." },
                    Data = responseModel
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<SchoolClassResponseModel>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" }
                };
            }
        }
    }


}
