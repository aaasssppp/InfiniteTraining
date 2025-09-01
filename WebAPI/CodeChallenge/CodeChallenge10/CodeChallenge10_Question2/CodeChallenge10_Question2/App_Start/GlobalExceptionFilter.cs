using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http;

namespace CodeChallenge10_Question2.App_Start
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // TODO: log context.Exception (file/db/trace)
            var error = new { Message = "An unexpected error occurred. Please contact administrator." };
            context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, error);
        }
    }
}