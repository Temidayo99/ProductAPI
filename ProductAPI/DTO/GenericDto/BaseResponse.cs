namespace ProductAPI.DTO.GenericDto
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }
        public BaseResponse()
        {
            
        }

        public BaseResponse(T data, string code, string message, bool status)
        {
            Data = data;
            ResponseCode = code;
            Message = message;
            IsSuccessful = status;
        }
    }
}
