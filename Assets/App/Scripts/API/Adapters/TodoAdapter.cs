using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

using UnityCI.API;
using UnityCI.Models;

public class TodoAdapter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // string sampleData = "{ \"name\": \"RestClientWebGL_Test\", \"description\": \"Testing UnityWebRequest_Test\", \"status\": \"todo\" }";
        Todo todo = new Todo()
        {
            name = "RestClientWebGL_Test",
            description = "Testing UnityWebRequest_Test",
            status = "todo"
        };
        string jsonData = JsonConvert.SerializeObject(todo, Formatting.Indented);

        StartCoroutine(RestClientWebGL.GetAsync<Todo>("todos", null, (request =>
        {
            HandleResponse(request);
        }), null));

        StartCoroutine(RestClientWebGL.PostAsync<Todo>("todos", jsonData, null, (request =>
        {
            HandleResponse(request);
        }), null));

        StartCoroutine(RestClientWebGL.PutAsync<Todo>("todos/5f5a851a680a3f4c81f44258", jsonData, null, (request =>
        {
            HandleResponse(request);
        }), null));

        StartCoroutine(RestClientWebGL.DeleteAsync<object>("todos/5f5a851a680a3f4c81f44258", null, (request =>
        {
            HandleResponse(request);
        }), null));
    }

    void HandleResponse(UnityWebRequest request)
    {
        string requestSummary = $"{request.method} ({request.responseCode})";
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log($"<color=red>{requestSummary} => API.ERROR</color>");
        }
        else
        {
            if (request.responseCode < 400)
            {
                var json = request?.downloadHandler?.text;
                Debug.Log($"<color=#44FF90>{requestSummary} => API.SUCCESS</color>\n{json}");
            }
            else
            {
                Debug.Log($"<color=red>{requestSummary} => API.ERROR</color> {request.error}");
            }
        }
    }
}
