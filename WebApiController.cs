using System.Net.Http;
using Newtonsoft.Json;

public ApiResult GetMerber(string qr)
  {
      ApiResult apiresult = new ApiResult { Code = 0 };

      string token = GetMemberToken();

      using (HttpClient httpClient = new HttpClient())
      {


          string url = "localhost:8080/crm/VIPCus/GetVIPInfoByApi";
          httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

          //直接传递对象的方式, 接受方 需要在函数前加上[FormBody]
          //var dic = new Dictionary<string, string>();
          //dic.Add("GetData", qr);
          //HttpContent httpContent = new FormUrlEncodedContent(dic);
          
          //参数对象
          HttpContent content = new StringContent(qr);
          
          //设置参数类型,可以传递数组对象
          content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
          
          HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url, content).Result;
          apiresult = JsonConvert.DeserializeObject<MemberResult>(httpResponseMessage.Content.ReadAsStringAsync().Result);

          if (ApiResul.Data != null)
          {
              ApiResul.Data = JsonConvert.DeserializeObject(ApiResul.Data.ToString());
          }
      }
      return apiresult;
  }
  
  public class ApiResult
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
    }
