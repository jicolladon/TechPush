using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechPush.Core.Dto;

namespace TechPush.Core.RemoteServices
{
    public interface IRestClientService
    {
        /// <summary>
        /// Delete de recurso con respuesta raw
        /// </summary>
        /// <param name="url">Recurso</param>
        /// <returns></returns>
        Task<HttpResponse> DeleteAsync(string url);
        /// <summary>
        /// Delete de recurso con objeto de respuesta en formato json
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<HttpResponse<TResponse>> DeleteAsync<TResponse>(string url);
        /// <summary>
        /// Delete de recurso con convertidor de respuesta
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="responseConverter"></param>
        /// <returns></returns>
        Task<HttpResponse<TResponse>> DeleteAsync<TResponse>(string url, Func<HttpResponseMessage, Task<TResponse>> responseConverter);
        /// <summary>
        /// Get de recurso con respuesta raw
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<HttpResponse> GetAsync(string url);
        /// <summary>
        /// Get de recurso con objeto de respuesta en formato json
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<HttpResponse<TResponse>> GetAsync<TResponse>(string url);
        /// <summary>
        /// Get de recurso con convertidor de objeto de respuesta
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="responseConverter"></param>
        /// <returns></returns>
        Task<HttpResponse<TResponse>> GetAsync<TResponse>(string url, Func<HttpResponseMessage, Task<TResponse>> responseConverter);

        Task<HttpResponse> PostAsync(string url);
        Task<HttpResponse> PostAsync<TRequest>(string url, TRequest entity);
        Task<HttpResponse> PostAsync<TRequest>(string url, TRequest entity, Func<TRequest, Task<HttpContent>> requestConverter);
        Task<HttpResponse<TResponse>> PostAsync<TResponse>(string url);
        Task<HttpResponse<TResponse>> PostAsync<TResponse>(string url, Func<HttpResponseMessage, Task<TResponse>> responseConverter);

        Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest entity);
        Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest entity, Func<HttpResponseMessage, Task<TResponse>> responseConverter);
        Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest entity, Func<TRequest, Task<HttpContent>> requestConverter, Func<HttpResponseMessage, Task<TResponse>> responseConverter);

        Task<HttpResponse> PutAsync(string url);
        Task<HttpResponse> PutAsync<TRequest>(string url, TRequest entity);
        Task<HttpResponse> PutAsync<TRequest>(string url, TRequest entity, Func<TRequest, Task<HttpContent>> requestConverter);

        Task<HttpResponse<TResponse>> PutAsync<TResponse>(string url);
        Task<HttpResponse<TResponse>> PutAsync<TResponse>(string url, Func<HttpResponseMessage, Task<TResponse>> responseConverter);
        Task<HttpResponse<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest entity);
        Task<HttpResponse<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest entity, Func<HttpResponseMessage, Task<TResponse>> responseConverter);
        Task<HttpResponse<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest entity, Func<TRequest, Task<HttpContent>> requestConverter, Func<HttpResponseMessage, Task<TResponse>> responseConverter);
    }
}
