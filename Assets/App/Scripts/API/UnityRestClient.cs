using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;

using Newtonsoft.Json;

public class UnityRestClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string id = "5f70bdd8b5ca1152aaa366d4";
        string apiUrl = "http://localhost:4000/api/todos";

        string sampleData = "{ \"name\": \"UnityWebRequest_Test2\", \"description\": \"Testing UnityWebRequest_Test\", \"status\": \"todo\" }";
        // StartCoroutine(GetRequest(apiUrl));
        // StartCoroutine(PostRequest(apiUrl, sampleData));
        // StartCoroutine(PutRequest($"{apiUrl}/{id}", sampleData));
        // StartCoroutine(DeleteRequest($"{apiUrl}/{id}"));
    }

    IEnumerator GetRequest(string uri)
    {
        Debug.Log($"<color=cyan>GET => </color>{uri}");
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log($"<color=red>({request.responseCode}) => API.ERROR</color=cyan>\n{request.error}");
            }
            else
            {
                var json = request.downloadHandler.text;
                Debug.Log($"<color=#44FF90>({request.responseCode}) => API.SUCCESS</color>\n{json}");
            }
        }
    }

    IEnumerator PostRequest(string uri, string data)
    {
        Debug.Log($"<color=cyan>POST => </color>{uri}");
        using (UnityWebRequest request = UnityWebRequest.Post(uri, data))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "text/csv");
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log($"<color=red>({request.responseCode}) => API.ERROR</color>");
            }
            else
            {
              var json = request.downloadHandler.text;
                Debug.Log($"<color=#44FF90>({request.responseCode}) => API.SUCCESS</color>\n{json}");
            }
        }
    }

    IEnumerator PutRequest(string uri, string data)
    {
        Debug.Log($"<color=cyan>PUT => </color>{uri}");
        using (UnityWebRequest request = UnityWebRequest.Put(uri, data))
        {
            request.method = UnityWebRequest.kHttpVerbPUT;
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "text/csv");
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log($"<color=red>({request.responseCode}) => API.ERROR</color> {request.error}");
            }
            else
            {
                var json = request.downloadHandler.text;

                if(request.responseCode < 400)
                {
                    Debug.Log($"<color=#44FF90>({request.responseCode}) => API.SUCCESS</color>\n{json}");
                }
                else
                {
                    Debug.Log($"<color=red>({request.responseCode}) => API.ERROR</color> {request.error}");
                }
            }
        }
    }

    IEnumerator DeleteRequest(string uri)
    {
        Debug.Log($"<color=cyan>DELETE => </color>{uri}");
        using (UnityWebRequest request = UnityWebRequest.Delete(uri))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log($"<color=red>({request.responseCode}) => API.ERROR</color>\n{request.error}");
            }
            else
            {
                // var json = request.downloadHandler.text;

                if(request.responseCode < 400)
                {
                    Debug.Log($"<color=#44FF90>({request.responseCode}) => API.SUCCESS</color>");
                }
                else
                {
                    Debug.Log($"<color=red>({request.responseCode}) => API.ERROR</color> {request.error}");
                }
            }
        }
    }
}
