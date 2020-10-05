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

        public static IEnumerator GetAsync<T>(string resource, string authToken = null, Action<UnityWebRequest> callback = null, Header[] headers = null)
          => Rest("GET", GetResourceUrl(resource), null, authToken, callback, headers);

        public static IEnumerator PostAsync<T>(string resource, object content, string authToken, Action<UnityWebRequest> callback = null, Header[] headers = null)
          => Rest("POST", GetResourceUrl(resource), content, authToken, callback, headers);

        public static IEnumerator PutAsync<T>(string resource, object content, string authToken, Action<UnityWebRequest> callback = null, Header[] headers = null)
          => Rest("PUT", GetResourceUrl(resource), content, authToken, callback, headers);

        public static IEnumerator DeleteAsync<T>(string resource, string authToken, Action<UnityWebRequest> callback = null, Header[] headers = null)
          => Rest("DELETE", GetResourceUrl(resource), null, authToken, callback, headers);
        #endregion

        #region TODO - DRY UP

        static IEnumerator Rest(string method, string uri, object data = null, string authToken = null, Action<UnityWebRequest> callback = null, Header[] headers = null)
        {
            Debug.Log($"<color=yellow>{method}</color> {uri}");
            switch (method.ToUpper())
            {
                case "GET":
                    using (UnityWebRequest request = UnityWebRequest.Get(uri))
                    {
                        yield return request.SendWebRequest();
                        callback?.Invoke(request);
                    }
                    break;

                case "POST":
                    using (UnityWebRequest request = UnityWebRequest.Post(uri, data.ToString()))
                    {
                        request.method = UnityWebRequest.kHttpVerbPOST;
                        request.downloadHandler = new DownloadHandlerBuffer();
                        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data.ToString()));

                        request.SetRequestHeader("Content-Type", "application/json");
                        request.SetRequestHeader("Accept", "application/json");
                        // if (headers != null && headers.Length > 0)
                        // {
                        //     foreach (Header header in headers)
                        //     {
                        //         request.SetRequestHeader(header.name, header.value);
                        //     }
                        // }
                        yield return request.SendWebRequest();
                        callback?.Invoke(request);
                    }
                    break;

                case "PUT":
                    using (UnityWebRequest request = UnityWebRequest.Put(uri, data.ToString()))
                    {
                        request.method = UnityWebRequest.kHttpVerbPUT;
                        request.downloadHandler = new DownloadHandlerBuffer();
                        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data.ToString()));

                        request.SetRequestHeader("Content-Type", "application/json");
                        request.SetRequestHeader("Accept", "application/json");
                        yield return request.SendWebRequest();
                        callback?.Invoke(request);
                    }
                    break;

                case "DELETE":
                    using (UnityWebRequest request = UnityWebRequest.Delete(uri))
                    {
                        yield return request.SendWebRequest();
                        callback?.Invoke(request);
                    }
                    break;
            }
        }
        #endregion
    }
}
