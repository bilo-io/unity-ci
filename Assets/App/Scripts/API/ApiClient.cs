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

public class ApiClient //: IRestClient
{
    #region TYPES
    public class Header
    {
        public string name { get; set; }
        public string value { get; set; }

        public override string ToString()
            => $"{name}: {value}";
    }

    public enum ContentType
    {
        // [Decription("application/json")]
        JSON
    }
    #endregion

    #region PROPERTIES
    public static bool logOutput { get; set; }
    // public static TimeSpan timeout { get => ApiClient.timeout; set => client.Timeout = value; }
    #endregion

      #region VARIABLES
		private string responseTypeColor = "#5ee5e5";
		private string responseSuccessColor = "#39e564";
		private string responseErrorColor = "#e0370d";
		#endregion

    #region HELPERS
    public static string requestError(UnityWebRequest request)
    {
        string responseBody = string.Empty;
        if (request.downloadHandler != null)
        {
            responseBody = request.downloadHandler.text;
        }
        string errorMessage = $"API ERROR ({request.responseCode}): {responseBody}, error: {request.error}";
        Debug.Log(errorMessage);
        return errorMessage;
    }

    public static T requestResponse<T>(UnityWebRequest request)
    {
        try
        {
            var responseData = request.downloadHandler.text;
            Debug.Log($"API SUCCESS ({request.responseCode}): {responseData}");
            return JsonConvert.DeserializeObject<T>(responseData);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return default(T);
        }
    }
    #endregion

    public static IEnumerator Request(string url, string method, string data = null, Action<UnityWebRequest> done = null)
    {
        Debug.Log($"<color=yellow>{method}</color> =>: <color=blue>{url}</color>");
        UnityWebRequest request;

        switch (method)
        {
            // -----------------------------------
            // GET
            case UnityWebRequest.kHttpVerbGET:
                request = UnityWebRequest.Get(url);

                yield return request.SendWebRequest();
                done?.Invoke(request);
                break;

            // -----------------------------------
            // DELETE
            case UnityWebRequest.kHttpVerbDELETE:
                request = UnityWebRequest.Delete(url);

                yield return request.SendWebRequest();
                done?.Invoke(request);
                break;

            // -----------------------------------
            // POST
            case UnityWebRequest.kHttpVerbPOST:
                request = UnityWebRequest.Post(url, data);

                request.method = UnityWebRequest.kHttpVerbPOST;
                request.downloadHandler = new DownloadHandlerBuffer();
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                // request.SetRequestHeader("Content-Type", Enumerations.GetDescription((ContentType)ContentType.JSON));
                // request.SetRequestHeader("Accept", Enumerations.GetDescription((ContentType)ContentType.JSON));
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");

                yield return request.SendWebRequest();
                done?.Invoke(request);
                break;

            // -----------------------------------
            // PUT
            case UnityWebRequest.kHttpVerbPUT:
                request = UnityWebRequest.Post(url, data);

                request.method = UnityWebRequest.kHttpVerbPOST;
                request.downloadHandler = new DownloadHandlerBuffer();
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                // request.SetRequestHeader("Content-Type", Enumerations.GetDescription((ContentType)ContentType.JSON));
                // request.SetRequestHeader("Accept", Enumerations.GetDescription((ContentType)ContentType.JSON));
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Accept", "application/json");

                yield return request.SendWebRequest();
                done?.Invoke(request);
                break;
        }
    }

    public static IEnumerator Post(string url, object o, Action<UnityWebRequest> done = null) =>
        Request(url, UnityWebRequest.kHttpVerbPOST, JsonConvert.SerializeObject(o), done);

    public static IEnumerator Get(string url, Action<UnityWebRequest> done = null) =>
        Request(url, UnityWebRequest.kHttpVerbGET, null, done);

    public static Action<T1, T2> wrapCallback<T1, T2>(Action<T1, T2> doneCallback)
    {
        // NOTE:
        // in case of having missing done callback use empty function to skip checks
        // on null or not callback instance.
        return doneCallback ?? ((_arg1, _arg2) => { });
    }

    #region PUBLIC API
    // TODO: Interface implementation "IRestClient"
    // Issues:
    // - "Task" return type ...issue with WebGL
    // - HttpMethod vs string ("GET", "POST" etc.)
    //
    #endregion
}
