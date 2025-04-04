﻿using _CampusFinder.Errors;
using System.Net;
using System.Text.Json;

namespace CampusFinder.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        //1.next =>Reference(Pointer) to Next Invoke of MiddleWare 
        //2.logger =>   when  happen Exception => make log to Exception at Console
        //3.environment =>Check on environment, if Development will appeare the details Excption and  if Production will not appeare the details Excption

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        //context => 
        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                //If there no Execption will move to next Middleware,but if there problems(Execptions) move to catch
                await next.Invoke(context);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, ex.Message);
                //Log Exception in Database[Production]

                //Header of Response Message => 1.status Code 2.Content type 

                context.Response.ContentType = "application/json"; //ContentType(json")
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //StatusCode(500)

                //Body of Response Message contain Object => 1.Message  2.status Code  3.Details of Exception

                var response = env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);

                //var response => from class API Exception respose  must be convert to Json

                //naming that inside response object of Execption is pascal case,and Javascript notUnderstand Json if was pascal case Convert to CamelCase 
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);


                await context.Response.WriteAsync(json);




            }
        }
    }
}
