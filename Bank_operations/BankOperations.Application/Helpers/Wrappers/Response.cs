using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Application.Helpers.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data, string messages = null)
        {
            Succeeded = true;
            Messages = messages;
            Data = data;

        }

        public Response(string messages)
        {
            Succeeded = false;
            Messages = messages;
        }

        public bool Succeeded { get; set; }
        public string Messages { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
    }
}
