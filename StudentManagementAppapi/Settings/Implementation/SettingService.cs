using StudentManagementAppapi.DTO.RequestModel;
using StudentManagementAppapi.DTO.Wrapper;
using StudentManagementAppapi.Settings.Interfaces;

namespace StudentManagementAppapi.Settings.Implementation
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _repository;

        public SettingService(ISettingRepository repository)
        {
            _repository = repository;
        }

        public ResponseWrapper<bool> ResetPassword(Guid userId, ForgetPassword model)
        {
            try
            {
                var result = _repository.ResetPassword(model.FormerPassword, model.UpdatedPassword, userId);

                if (result == null)
                {
                    return new ResponseWrapper<bool>
                    {
                        Successs = false,
                        Message = "Invalid former password or user not found." ,
                        Data = false
                    };
                }

                return new ResponseWrapper<bool>
                {
                    Successs = true,
                    Message = "Password updated successfully.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<bool>
                {
                    Successs = false,
                    Message =  $"An error occurred: {ex.Message}",
                    Data = false
                };
            }
        }
    }

}
