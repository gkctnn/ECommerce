using System.Collections.Generic;

namespace ECommerce.WebApi.Models
{
    public abstract class BaseResponse
    {
        public List<string> Errors { get; set; }
        public bool HasError { get; set; }
        public bool IsSuccess { get; set; }
    }
}
