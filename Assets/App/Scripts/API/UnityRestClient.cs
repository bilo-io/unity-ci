using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using Newtonsoft.Json;

public class UnityRestClient : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string[] APIData = new string[4];
    [SerializeField]
    // private GameObject apiFeedbackLabel;
    // private TextMeshPro tmp;
    void Start()
    {
        string id = "5f70e753d3aa607f41fa6523";
        string apiUrl = "http://localhost:4000/api/todos";


        string sampleData = "{ \"name\": \"UnityWebRequest_Test\", \"description\": \"Testing UnityWebRequest_Test\", \"status\": \"todo\" }";
        StartCoroutine(GetRequest(apiUrl));
        StartCoroutine(PostRequest(apiUrl, sampleData));
        StartCoroutine(PutRequest($"{apiUrl}/{id}", sampleData));
        // StartCoroutine(DeleteRequest($"{apiUrl}/{id}"));
    }

    private void Update()
    {
        // var textData = "";
        // for (int i = 0; i < APIData.Length; i++)
        // {
        //     textData += $"\n{APIData[i]}";
        // }
        // tmp.SetText(textData);
    }

    void OnGUI()
    {
        var textData = "";
        var style = new GUIStyle();
        style.fontSize = 32;
        for (int i = 0; i < APIData.Length; i++)
        {
            textData += $"\n{APIData[i]}";
        }
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), textData, style);
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
                APIData[0] = $"GET => error ({request.responseCode})";
            }
            else
            {
                var json = request.downloadHandler.text;
                Debug.Log($"<color=#44FF90>({request.responseCode}) => API.SUCCESS</color>\n{json}");
                APIData[0] = $"GET => success ({request.responseCode}) => {json}";
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
                APIData[1] = $"POST => error ({request.responseCode})";
            }
            else
            {
                var json = request.downloadHandler.text;
                Debug.Log($"<color=#44FF90>({request.responseCode}) => API.SUCCESS</color>\n{json}");
                APIData[1] = $"POST => success ({request.responseCode})";
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
                APIData[2] = $"PUT => error ({request.responseCode}) => {request.error}";
            }
            else
            {
                var json = request.downloadHandler.text;

                if (request.responseCode < 400)
                {
                    Debug.Log($"<color=#44FF90>({request.responseCode}) => API.SUCCESS</color>\n{json}");
                    APIData[2] = $"PUT => success ({request.responseCode}) => {json}";
                }
                else
                {
                    Debug.Log($"<color=red>({request.responseCode}) => API.ERROR</color> {request.error}");
                    APIData[2] = $"PUT => error ({request.responseCode}) => {request.error}";
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
                APIData[3] = $"DELETE => error ({request.responseCode}) => {request.error}";
            }
            else
            {
                // var json = request.downloadHandler.text;

                if (request.responseCode < 400)
                {
                    Debug.Log($"<color=#44FF90>({request.responseCode}) => API.SUCCESS</color>");
                    APIData[3] = $"DELETE => success ({request.responseCode})";
                }
                else
                {
                    Debug.Log($"<color=red>({request.responseCode}) => API.ERROR</color> {request.error}");
                    APIData[3] = $"DELETE => error ({request.responseCode}) => {request.error}";
                }
            }
        }
    }
}
