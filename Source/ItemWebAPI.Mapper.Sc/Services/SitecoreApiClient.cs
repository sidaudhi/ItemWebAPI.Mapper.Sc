using ItemWebAPI.Mapper.Sc.Configuration;
using ItemWebAPI.Mapper.Sc.Contracts;
using ItemWebAPI.Mapper.Sc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace ItemWebAPI.Mapper.Sc.Services
{
    /// <summary>
    /// Client class for Sitecore Item WebAPI integration
    /// </summary>
    public class SitecoreApiClient: ISitecoreClient, IDisposable
    {
        HttpClient _client;
        SitecoreConfiguration _configuration;
        string _database;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="exceptionManager">ExceptionManager</param>
        public SitecoreApiClient(string database = null)
        {
            _configuration = (SitecoreConfiguration)System.Configuration.ConfigurationManager.GetSection("sitecore");
            if (_configuration != null)
            {
                _database = !String.IsNullOrEmpty(database) ? database : _configuration.Host.Database;
                _client = new HttpClient();
                _client.BaseAddress = new Uri(_configuration.Host.Endpoint);
                _client.DefaultRequestHeaders.Add("X-Scitemwebapi-Username", _configuration.Host.Credentials.UserName);
                _client.DefaultRequestHeaders.Add("X-Scitemwebapi-Password", _configuration.Host.Credentials.Password);
                _client.DefaultRequestHeaders.Add("X-Scitemwebapi-Encrypted", _configuration.Host.Credentials.Encrypt ? "1" : "0");
            }
        }

        /// <summary>
        /// Gets Sitecore Items by ID
        /// </summary>
        /// <param name="id">ID of the item</param>
        /// <param name="language">Optional Language of the Item</param>
        /// <returns>Sitecore Item</returns>
        public Item GetById(string id, string language = null)
        {
            string queryFormat = "/-/item/v1/?sc_itemid={0}&sc_database={1}&payload=full";
            if (!String.IsNullOrEmpty(language))
                queryFormat += "&language={2}";

            string url = String.IsNullOrEmpty(language) ? String.Format(queryFormat, id, _database) : String.Format(queryFormat, id, _database, language);

            var response = Get(url);

            var result = ProcessResponseMessage<ApiResult>(response);

            if (result != null && result.Result != null && result.Result.ResultCount > 0)
                return result.Result.Items.FirstOrDefault();

            return null;
        }

        /// <summary>
        /// Gets Sitecore Items by a Query
        /// </summary>
        /// <param name="query">Sitecore Fast Query</param>
        /// <param name="language">Optional Language Parameter</param>
        /// <returns>Collection of Sitecore Items</returns>
        public IEnumerable<Item> GetByQuery(string query, string language = null)
        {
            try
            {
                query = WebUtility.UrlEncode(query);
                string queryFormat = "/-/item/v1/?query={0}&sc_database={1}&payload=full";
                if (!String.IsNullOrEmpty(language))
                    queryFormat += "&language={2}";

                string url = String.IsNullOrEmpty(language) ? String.Format(queryFormat, query, _database) : String.Format(queryFormat, query, _database, language);

                var response = Get(url);

                var result = ProcessResponseMessage<ApiResult>(response);

                if (result != null && result.Result != null && result.Result.ResultCount > 0)
                    return result.Result.Items;

                return null;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Wrapper around the _client GetAsync method with additional logging
        /// </summary>
        /// <param name="url">URL for the request</param>
        /// <returns>Response from the GET method</returns>
        private HttpResponseMessage Get(string url)
        {
            var response = _client.GetAsync(url).Result;
            return response;
        }

        /// <summary>
        /// Helper Response Processing & Deserlization of HttpResponseMessage
        /// </summary>
        /// <typeparam name="T">Type of Expected Response</typeparam>
        /// <param name="response">HttpResponseMessage received from HttpClient</param>
        /// <returns>Deserialized object of Expected Response Type T</returns>
        private T ProcessResponseMessage<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                T content = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                return content;
            }
            return default(T);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _client.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}