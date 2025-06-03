namespace StudentManagementAppapi.DTO.Wrapper
{
    public class ResponseWrapperl<T>
    {
        public bool IsSuccessful { get; set; }
        public List<string> Messages { get; set; }
        public T Data { get; set; }

        public ResponseWrapperl<T> Success(T data, string message = null)
        {
            IsSuccessful = true;
            Messages = [message];
            Data = data;

            return this;
        }

        public ResponseWrapperl<T> Failed(string message)
        {
            IsSuccessful = false;
            Messages = [message];

            return this;
        }
    }

}
