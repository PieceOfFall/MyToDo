

namespace MyToDo.Common.Models
{
    public class ApiResponse<T>
    {
        public string msg { get; set; }
        public int status { get; set; }

        public T? data { get; set; }

        public ApiResponse() 
        {

        }

        public ApiResponse(string msg,int status) 
        {
            this.msg = msg;
            this.status = status;
        }

        public ApiResponse(string msg, int status, T data)
        {
            this.msg = msg;
            this.status = status;
            this.data = data;
        }
    }
}
