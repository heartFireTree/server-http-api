  
  //最古老的传统的请求方式,通过字节流解析
  public class HttpHelp:ApiController
  {
    var resultMsg = new ApiResultMsg { Code = 0 };
    string str = "";
    
    try
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("api url地址");
        req.Method = "POST";//GET
        req.ContentType = "application/json";
        byte[] data = Encoding.UTF8.GetBytes(str);
        req.ContentLength = data.Length; //请求长度
        using (Stream s = req.GetRequestStream())
        {
            s.Write(data, 0, data.Length);//向当前流中写入字节
            s.Close(); //关闭当前流
        }

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //响应结果
        Stream stream = resp.GetResponseStream();
        string result = "";
        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        {
            result = reader.ReadToEnd();
        }
        resultMsg.Data = result;
    }
    catch (Exception ex)
    {
        resultMsg.Code = -1;
        resultMsg.Msg = ex.Message;
    }
    
     return Json<ApiResultMsg>(resultMsg);
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
    
    
