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

        public ResponseWrapperl<bool> ResetPassword(Guid userId, ForgetPassword model)
        {
            try
            {
                var result = _repository.ResetPassword(model.FormerPassword, model.UpdatedPassword, userId);

                if (result == null)
                {
                    return new ResponseWrapperl<bool>
                    {
                        IsSuccessful = false,
                        Messages = new List<string> { "Invalid former password or user not found." },
                        Data = false
                    };
                }

                return new ResponseWrapperl<bool>
                {
                    IsSuccessful = true,
                    Messages = new List<string> { "Password updated successfully." },
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseWrapperl<bool>
                {
                    IsSuccessful = false,
                    Messages = new List<string> { $"An error occurred: {ex.Message}" },
                    Data = false
                };
            }
        }
    }

}
