using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRestClient
{
    // Start is called before the first frame update
    #region PUBLIC API
    T DeserializeContent<T>(Stream stream);
    string GetResourceUrl(string resource);
    Task<T> GetAsync<T>(string resource, string authToken = null, Header[] headers = null);
    Task<HttpResponseMessage> GetAsync(string resource, string authToken = null, Header[] headers = null);
    Task<T> PostAsync<T>(string resource, object content, string authToken = null, Header[] headers = null);
    Task<HttpResponseMessage> PostAsync(string resource, object content, string authToken = null, Header[] headers = null);
    Task<T> PutAsync<T>(string resource, object content, string authToken = null, Header[] headers = null);
    Task<HttpResponseMessage> PutAsync(string resource, object content, string authToken = null, Header[] headers = null);
    Task<HttpResponseMessage> DeleteAsync(string resource, string authToken = null, Header[] headers = null);
    #endregion


    #region PUBLIC API - SENDING
    Task<HttpResponseMessage> SendAsync(string resource, HttpMethod httpMethod, string authToken, Header[] headers);
    Task<HttpResponseMessage> SendAsync(string resource, HttpMethod httpMethod, string authToken, object content, Header[] headers);
    #endregion
}
