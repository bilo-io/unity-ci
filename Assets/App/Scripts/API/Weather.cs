using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Newtonsoft.Json;

namespace UnityCI.API
{
    public class Weather : API
    {
        public static string url = "https://api.coingecko.com/api/v3/exchange_rates";
        public static IEnumerator Forecast(Action<string, string> doneCallback = null)
        {
            var done = wrapCallback(doneCallback);

            try
            {
                return Get(url, (request) =>
                    {
                        if (request.isNetworkError || request.responseCode != 201)
                            done(null, requestError(request));
                        else
                            done(requestResponse<string>(request), null);
                    });
            }
            catch (Exception ex)
            {
                // catch here all the exceptions ensure never die
                Debug.Log(ex.Message);
                done(null, ex.Message);
            }

            return null;
        }
    }
}
