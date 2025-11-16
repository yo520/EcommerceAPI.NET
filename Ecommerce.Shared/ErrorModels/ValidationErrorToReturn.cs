using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.ErrorModels
{
    public class ValidationErrorToReturn
    {
        public int statusCode { get; set; }=(int)HttpStatusCode.BadRequest;
        public string message { get; set; }="Validation Error";
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}
