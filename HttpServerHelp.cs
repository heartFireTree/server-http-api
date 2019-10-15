using System.Net.Http;
using Newtonsoft.Json;

public static class HttpServerHelp
{
    public static ApiResultMsg Get<T>(string token,string url,out T returnObj)
    {
       returnObj = default(T);
       var resultMsg = new ApiResultMsg { Code = -1 };
        using (HttpClient httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url).Result;
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                resultMsg.Msg = "登录状态已丢失，请重新登录";
                resultMsg.Code = 407;
                return resultMsg;
            }
            resultMsg = JsonConvert.DeserializeObject<ApiResultMsg>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            if (resultMsg.Code == 0)
            {
                returnObj = JsonConvert.DeserializeObject<T>(resultMsg.Data.ToString());
            }    
        }
         return resultMsg;
    }
    
      public static ApiResultMsg Post<T>(string token, string url, object param, out T returnObj)
      {
         returnObj = default(T);
         var resultMsg = new ApiResultMsg { Code = -1 };
         using(HttpClient httpClient = new HttpClient())
         {
             httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
             HttpResponseMessage httpResponseMessage = httpClient.PostAsJsonAsync(url, param).Result;
             if (!httpResponseMessage.IsSuccessStatusCode)
              {
                  resultMsg.Msg = "登录状态已丢失，请重新登录";
                  resultMsg.Code = 407;
                  return resultMsg;
              }
              resultMsg = JsonConvert.DeserializeObject<ApiResultMsg>(httpResponseMessage.Content.ReadAsStringAsync().Result);
              if (resultMsg.Code == 0)
              {
                  returnObj = JsonConvert.DeserializeObject<T>(resultMsg.Data.ToString());
              }
         }
         return resultMsg;
      }
}

  public class ApiResultMsg
    {
        /// <summary>
        /// 0：表示成功 -1:错误 
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }
        /// <summary>
        /// 返回信息 返回信息
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }
        /// <summary>
        /// 返回页面
        /// </summary>
        [JsonProperty("back_url")]
        public string BackURL { get; set; }
        /// <summary>
        /// 返回总行数
        /// </summary>
        [JsonProperty("total")]
        public int total { get; set; }
    }
