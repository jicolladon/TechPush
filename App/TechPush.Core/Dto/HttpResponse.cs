using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TechPush.Core.Dto
{
    public class HttpResponse
    {
        public HttpResponseMessage ResponseMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
    }

    /// <summary>
    /// Full response data with deserialized entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type in response to deserialize</typeparam>
    public class HttpResponse<TEntity> : HttpResponse
    {
        public TEntity Entity { get; set; }
    }
}
