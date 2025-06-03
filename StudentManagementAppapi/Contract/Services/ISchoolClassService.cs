using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.ResponseModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Model.Entities;

namespace StudentManagementAppapi.Contract.Services
{
    public interface ISchoolClassService
    {
        ResponseWrapperl<bool> Add(SchoolClassRequestModel model);
        ResponseWrapperl<bool> Delete(string className);
        ResponseWrapperl<List<SchoolClassResponseModel>> GetAll();
        ResponseWrapperl<SchoolClassResponseModel> GetById(Guid id);
        ResponseWrapperl<SchoolClassResponseModel> GetByName(string name);
        ResponseWrapperl<SchoolClassResponseModel> Update(Guid id, SchoolClassRequestModel model);
    }
}
