namespace StudentManagementAppapi.DTO.ResponseModel
{
    public class ViewProfile
    {
        public DateOnly DOB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
