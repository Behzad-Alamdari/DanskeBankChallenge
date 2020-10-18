using System.Diagnostics.CodeAnalysis;

namespace DBC.WebApi.ResponseWrappers
{
    public class Response<T> where T : class
    {
        public Response()
        {

        }

        public Response([DisallowNull] T data)
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
