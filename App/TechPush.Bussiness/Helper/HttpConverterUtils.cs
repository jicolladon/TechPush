using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechPush.Core.Dto;

namespace TechPush.Bussiness.Helper
{
    public static class HttpConverterUtils
    {
        /// <summary>
        /// Generates a HttpContent for JSON with the TEntity object serialized
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="entity">Entity instance to convert to json</param>
        /// <returns></returns>
        public static StringContent LoadJsonContent<TEntity>(TEntity entity)
        {
            string jsonData = JsonConvert.SerializeObject(entity);
            StringContent res = new StringContent(jsonData, Encoding.UTF8, "application/json");
            return res;
        }

        /// <summary>
        /// Returns a converter that converts the TRequest entity in json data format
        /// </summary>
        /// <typeparam name="TRequest">Request entity type</typeparam>
        /// <returns></returns>
        public static Func<TRequest, Task<HttpContent>> GetRequestJsonConverter<TRequest>()
        {
            return new Func<TRequest, Task<HttpContent>>((TRequest entry) => Task.FromResult((HttpContent)LoadJsonContent(entry)));
        }

        /// <summary>
        /// Converts the json result in HttpResponseMessage to a TResponse object
        /// </summary>
        /// <typeparam name="TResponse">Entity type in the response</typeparam>
        /// <returns></returns>
        public static Func<HttpResponseMessage, Task<TResponse>> GetResponseJsonConverter<TResponse>()
        {
            return new Func<HttpResponseMessage, Task<TResponse>>(async (response) =>
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                TResponse entity = (string.IsNullOrEmpty(responseContent)) ?
                                                            default(TResponse) :
                                                            DeserializeJSon<TResponse>(responseContent);
                return await Task.FromResult(entity);
            });
        }

        public static Func<IDictionary<string, string>, Task<HttpContent>> GetRequestFormUrlEncodedConverter()
        {
            return new Func<IDictionary<string, string>, Task<HttpContent>>((IDictionary<string, string> entry) =>
            {
                HttpContent content = new FormUrlEncodedContent(entry);
                return Task.FromResult(content);
            });
        }

        /// <summary>
        /// Deserialize json data into an entity type
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="data">json data</param>
        /// <returns></returns>
        public static T DeserializeJSon<T>(string data)
        {
            return (!string.IsNullOrEmpty(data))
                ? JsonConvert.DeserializeObject<T>(data)
                : default(T);
        }

        /// <summary>
        /// Creates a full response with raw data only
        /// </summary>
        /// <param name="httpResponse">Client call response</param>
        /// <returns></returns>
        public static HttpResponse CreateResponse(HttpResponseMessage httpResponse)
        {
            HttpResponse response = new HttpResponse();
            response.StatusCode = httpResponse.StatusCode;
            response.ResponseMessage = httpResponse;
            response.IsSuccessful = httpResponse.IsSuccessStatusCode;
            return response;
        }
        /// <summary>
        /// Create a full response using a converter to obtain the response entity using a json converter
        /// </summary>
        /// <typeparam name="TResponse">Entity type</typeparam>
        /// <param name="httpResponse">Full response data with entity</param>
        /// <returns></returns>
        public static async Task<HttpResponse<TResponse>> CreateResponse<TResponse>(HttpResponseMessage httpResponse)
        {
            return await CreateResponse(httpResponse, GetResponseJsonConverter<TResponse>());
        }

        /// <summary>
        /// Create a full response using a converter to obtain the response entity
        /// </summary>
        /// <typeparam name="TResponse">Entity type</typeparam>
        /// <param name="httpResponse">Full response data with entity</param>
        /// <param name="converter">Converter to convert from response to entity instance</param>
        /// <returns></returns>
        public static async Task<HttpResponse<TResponse>> CreateResponse<TResponse>(HttpResponseMessage httpResponse, Func<HttpResponseMessage, Task<TResponse>> converter)
        {
            HttpResponse<TResponse> response = new HttpResponse<TResponse>();
            response.StatusCode = httpResponse.StatusCode;
            response.ResponseMessage = httpResponse;
            response.IsSuccessful = httpResponse.IsSuccessStatusCode;

            if (httpResponse.IsSuccessStatusCode)
            {
                response.Entity = await converter(httpResponse);
            }

            return response;
        }
    }
}
