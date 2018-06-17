using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechPush.Bussiness.Helper;
using TechPush.Core.Dto;
using TechPush.Core.RemoteServices;

namespace TechPush.Bussiness.RemoteServices
{
    public class RestClientService : IRestClientService
    {

        #region ctor

        public RestClientService()
        {
        }

        #endregion

        #region Delete

        public async Task<HttpResponse<TResponse>> DeleteAsync<TResponse>(string url)
        {
            return await DeleteAsync(url, HttpConverterUtils.GetResponseJsonConverter<TResponse>());
        }

        public async Task<HttpResponse<TResponse>> DeleteAsync<TResponse>(string url, Func<HttpResponseMessage, Task<TResponse>> responseConverter)
        {
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.DeleteAsync(url);
                return await HttpConverterUtils.CreateResponse(httpResponse, responseConverter);
            }
        }

        public async Task<HttpResponse> DeleteAsync(string url)
        {
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.DeleteAsync(url);
                return HttpConverterUtils.CreateResponse(httpResponse);
            }
        }

        #endregion

        #region Get

        public async Task<HttpResponse> GetAsync(string url)
        {
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                return HttpConverterUtils.CreateResponse(httpResponse);
            }
        }

        public async Task<HttpResponse<TResponse>> GetAsync<TResponse>(string url)
        {
            return await GetAsync(url, HttpConverterUtils.GetResponseJsonConverter<TResponse>());
        }

        public async Task<HttpResponse<TResponse>> GetAsync<TResponse>(string url, Func<HttpResponseMessage, Task<TResponse>> responseConverter)
        {
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                
                return await HttpConverterUtils.CreateResponse(httpResponse, responseConverter);
            }
        }

        #endregion

        #region Post

        public async Task<HttpResponse> PostAsync<TRequest>(string url, TRequest entity)
        {
            return await PostAsync(url, entity, HttpConverterUtils.GetRequestJsonConverter<TRequest>());
        }

        public async Task<HttpResponse> PostAsync<TRequest>(string url, TRequest entity, Func<TRequest, Task<HttpContent>> requestConverter)
        {
            HttpContent content = await requestConverter(entity);
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.PostAsync(url, content);
                return HttpConverterUtils.CreateResponse(httpResponse);
            }
        }

        public async Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest entity)
        {
            return await PostAsync(url, entity, HttpConverterUtils.GetRequestJsonConverter<TRequest>(), HttpConverterUtils.GetResponseJsonConverter<TResponse>());
        }

        public async Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest entity, Func<HttpResponseMessage, Task<TResponse>> responseConverter)
        {
            return await PostAsync(url, entity, HttpConverterUtils.GetRequestJsonConverter<TRequest>(), responseConverter);
        }

        public async Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string url,
                                                                                TRequest entity,
                                                                                Func<TRequest, Task<HttpContent>> requestConverter,
                                                                                Func<HttpResponseMessage, Task<TResponse>> responseConverter)
        {
            HttpContent content = await requestConverter(entity);
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.PostAsync(url, content);
                return await HttpConverterUtils.CreateResponse(httpResponse, responseConverter);
            }
        }


        public async Task<HttpResponse> PostAsync(string url)
        {
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.PostAsync(url, null);
                return HttpConverterUtils.CreateResponse(httpResponse);
            }
        }

        public async Task<HttpResponse<TResponse>> PostAsync<TResponse>(string url)
        {
            return await PostAsync(url, HttpConverterUtils.GetResponseJsonConverter<TResponse>());
        }


        public async Task<HttpResponse<TResponse>> PostAsync<TResponse>(string url, Func<HttpResponseMessage, Task<TResponse>> responseConverter)
        {
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.PostAsync(url, null);
                return await HttpConverterUtils.CreateResponse(httpResponse, responseConverter);
            }
        }

        #endregion

        #region Put

        public async Task<HttpResponse> PutAsync<TRequest>(string url, TRequest entity)
        {
            return await PutAsync(url, entity, HttpConverterUtils.GetRequestJsonConverter<TRequest>());
        }

        public async Task<HttpResponse> PutAsync<TRequest>(string url, TRequest entity, Func<TRequest, Task<HttpContent>> requestConverter)
        {
            HttpContent content = await requestConverter(entity);
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.PutAsync(url, content);
                return HttpConverterUtils.CreateResponse(httpResponse);
            }
        }

        public async Task<HttpResponse<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest entity)
        {
            return await PutAsync(url, entity, HttpConverterUtils.GetRequestJsonConverter<TRequest>(), HttpConverterUtils.GetResponseJsonConverter<TResponse>());
        }

        public async Task<HttpResponse<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest entity, Func<HttpResponseMessage, Task<TResponse>> responseConverter)
        {
            return await PutAsync(url, entity, HttpConverterUtils.GetRequestJsonConverter<TRequest>(), responseConverter);
        }

        public async Task<HttpResponse<TResponse>> PutAsync<TRequest, TResponse>(string url,
                                                                                TRequest entity,
                                                                                Func<TRequest, Task<HttpContent>> requestConverter,
                                                                                Func<HttpResponseMessage, Task<TResponse>> responseConverter)
        {
            HttpContent content = await requestConverter(entity);
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.PutAsync(url, content);
                return await HttpConverterUtils.CreateResponse(httpResponse, responseConverter);
            }
        }

        public async Task<HttpResponse> PutAsync(string url)
        {
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.PutAsync(url, null);
                return HttpConverterUtils.CreateResponse(httpResponse);
            }
        }

        public async Task<HttpResponse<TResponse>> PutAsync<TResponse>(string url)
        {
            return await PutAsync(url, HttpConverterUtils.GetResponseJsonConverter<TResponse>());
        }


        public async Task<HttpResponse<TResponse>> PutAsync<TResponse>(string url, Func<HttpResponseMessage, Task<TResponse>> responseConverter)
        {
            using (HttpClient client = Create())
            {
                HttpResponseMessage httpResponse = await client.PutAsync(url, null);
                return await HttpConverterUtils.CreateResponse(httpResponse, responseConverter);
            }
        }

        #endregion
        public HttpClient Create()
        {
            var client =  new System.Net.Http.HttpClient();
            client.BaseAddress = new System.Uri(Common.BaseUrl);
            return client;
        }
    }
}