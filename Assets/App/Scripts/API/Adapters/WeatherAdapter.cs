using UnityEngine;
using UnityCI.API;

namespace UnityCI.Adapters
{
    public class WeatherAdapter : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(Weather.Forecast((success, error) =>
            {
                Debug.Log($"Success: {success}");
                Debug.Log($"Error: {error}");

                if(error != null)
                {
                    Debug.Log($"<color=#44FF90>API.SUCCESS</color>\n{error}");
                }
                else
                {
                    Debug.Log("<color=red>API.ERROR</color>");
                }
            }));
        }
    }
}
