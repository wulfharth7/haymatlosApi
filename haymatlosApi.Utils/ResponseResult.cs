namespace haymatlosApi.haymatlosApi.Utils
{
    public class ResponseResult<T>
    {
        public ResponseResult()
        {
        }
        public ResponseResult(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
