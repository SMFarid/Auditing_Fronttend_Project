
using System.Collections.Generic;
using Frontend_Project.ViewModel;

namespace Frontend_Project.Common
{
    public class CommonResponse<T>
    {

        public CommonResponse()
        {
            Errors = new List<Error>();

        }
        public bool IsSuccess { get => Errors.Count == 0; }
        public List<Error> Errors { get; set; }
        public T Data { get; set; }
    }
}
