using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;

using Newtonsoft.Json;

public class ApiClient : IRestClient
{
    #region HELPERS
    internal static string requestError(UnityWebRequest request)
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

    internal static T requestResponse<T>(UnityWebRequest request)
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

    internal static IEnumerator Request(string url, string method, string data = null, Action<UnityWebRequest> done = null)
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
                request.SetRequestHeader("Content-Type", API.CONTENT_TYPE_JSON);
                request.SetRequestHeader("Accept", API.CONTENT_TYPE_JSON);

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
                request.SetRequestHeader("Content-Type", API.CONTENT_TYPE_JSON);
                request.SetRequestHeader("Accept", API.CONTENT_TYPE_JSON);

                yield return request.SendWebRequest();
                done?.Invoke(request);
                break;
        }
    }

    internal static IEnumerator Post(string url, object o, Action<UnityWebRequest> done = null) =>
        Request(url, UnityWebRequest.kHttpVerbPOST, JsonConvert.SerializeObject(o), done);

    internal static IEnumerator Get(string url, Action<UnityWebRequest> done = null) =>
        Request(url, UnityWebRequest.kHttpVerbGET, null, done);

    internal static Action<T1, T2> wrapCallback<T1, T2>(Action<T1, T2> doneCallback)
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
