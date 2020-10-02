using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;

using Newtonsoft.Json;
// using Fuzzy.Extensions;
// NOTE: get description extension method is in Fuzzy.Extensions

namespace UnityCI.API
{
    public class RestClientWebGL
    {
        public class Header
        {
            public string name { get; set; }
            public string value { get; set; }

            public override string ToString()
              => $"{name}: {value}";
        }

        #region PROPERTIES
        // public static string endPoint { get { return ConnectionStrings.url; } }
        public static string endPoint { get { return ConnectionStrings.instance.url; } }
        #endregion

        #region VARIABLES
        private string responseTypeColor = "#5ee5e5";
        private string responseSuccessColor = "#39e564";
        private string responseErrorColor = "#e0370d";
        private static Header[] defaultJsonHeaders = new Header[]{
          new Header{ name = "Content-Type", value = "application/json" },
          new Header{ name = "Accept", value = "application/json" }
        };
        #endregion

        #region PUBLIC API
        public static string GetResourceUrl(string resource)
          => $"{endPoint}/{resource}";

        public static IEnumerator GetAsync<T>(string resource, string authToken, Action<UnityWebRequest> callback = null, Header[] headers = null)
            => Request(resource, UnityWebRequest.kHttpVerbGET, authToken, null, callback, headers);

        public static IEnumerator PostAsync<T>(string resource, object content, string authToken, Action<UnityWebRequest> callback = null, Header[] headers = null)
            => Request(resource, UnityWebRequest.kHttpVerbPOST, authToken, JsonConvert.SerializeObject(content), callback, headers);

        public static IEnumerator PutAsync<T>(string resource, object content, string authToken, Action<UnityWebRequest> callback = null, Header[] headers = null)
            => Request(resource, UnityWebRequest.kHttpVerbPUT, authToken, JsonConvert.SerializeObject(content), callback, headers);

        public static IEnumerator DeleteAsync<T>(string resource, string authToken, Action<UnityWebRequest> callback = null, Header[] headers = null)
            => Request(resource, UnityWebRequest.kHttpVerbDELETE, authToken, null, callback, headers);

        public static IEnumerator Request(string resource, string method, string authToken, string data = null, Action<UnityWebRequest> callback = null, Header[] headers = null)
        {
            var url = GetResourceUrl(resource);
            Debug.Log($"<color=yellow>{method}</color> =>: <color=blue>{url}</color>");
            UnityWebRequest request;

            switch (method)
            {
                // -----------------------------------
                // GET
                case UnityWebRequest.kHttpVerbGET:
                    request = UnityWebRequest.Get(url);
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
                    }

                    yield return request.SendWebRequest();
                    callback?.Invoke(request);
                    break;

                // -----------------------------------
                // DELETE
                case UnityWebRequest.kHttpVerbDELETE:
                    request = UnityWebRequest.Delete(url);
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
                    }

                    yield return request.SendWebRequest();
                    callback?.Invoke(request);
                    break;

                // -----------------------------------
                // POST
                case UnityWebRequest.kHttpVerbPOST:
                    request = UnityWebRequest.Post(url, data);

                    request.method = UnityWebRequest.kHttpVerbPOST;
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                    // if (headers != null && headers.Length > 0)
                    // {
                    //     foreach (Header header in headers)
                    //     {
                    //         request.SetRequestHeader(header.name, header.value);
                    //     }
                    // }
                    request.SetRequestHeader("Content-Type", "application/json");
                    request.SetRequestHeader("Accept", "application/json");

                    if (!string.IsNullOrEmpty(authToken))
                    {
                        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
                    }

                    yield return request.SendWebRequest();
                    callback?.Invoke(request);
                    break;

                // -----------------------------------
                // PUT
                case UnityWebRequest.kHttpVerbPUT:
                    request = UnityWebRequest.Put(url, data);

                    request.method = UnityWebRequest.kHttpVerbPUT;
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                    // if (headers != null && headers.Length > 0)
                    // {
                    //     foreach (Header header in headers)
                    //     {
                    //         request.SetRequestHeader(header.name, header.value);
                    //     }
                    // }
                    request.SetRequestHeader("Content-Type", "application/json");
                    request.SetRequestHeader("Accept", "application/json");
                    if (!string.IsNullOrEmpty(authToken))
                    {
                        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
                    }

                    yield return request.SendWebRequest();
                    callback?.Invoke(request);
                    break;
            }
        }
        #endregion
    }
}
