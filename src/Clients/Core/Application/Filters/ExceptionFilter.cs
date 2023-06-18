﻿using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exc = context.Exception;

            var notifications = JsonConvert.SerializeObject(new
            {
                error = exc.Message
            },
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.Result = new ContentResult
            {
                Content = notifications,
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}