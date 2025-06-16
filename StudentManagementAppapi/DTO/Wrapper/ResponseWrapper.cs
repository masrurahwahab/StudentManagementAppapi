namespace StudentManagementAppapi.DTO.Wrapper
{
   
    public class ResponseWrapper<T>
    {
        public bool Successs { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ResponseWrapper<T> Success(T data, string message = "")
        {
            return new ResponseWrapper<T>
            {
                Successs = true,
                Message = message,
                Data = data
            };
        }

        public static ResponseWrapper<T> Failure(string message, T? data = default)
        {
            return new ResponseWrapper<T>
            {
                Successs = false,
                Message = message,
                Data = data
            };
        }
    }
}
