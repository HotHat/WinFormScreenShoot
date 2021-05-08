using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WinFormScreenShoot
{
    class BaiduTranslate
    {
        private String m_appId = "20151201000007148";
        private String m_appSecret = "Sa6PSyxCbdefr7A6bMjS";

        private String m_url = "https://fanyi-api.baidu.com/api/trans/vip/translate?";

        public  Models.TranslateResult translate(String q)
        {
            using (HttpClient client = new HttpClient())
            {
                // 设置超时限制，有效防止浪费资源
                client.Timeout = TimeSpan.FromSeconds(30);
                //使用 GetAsync 进行异步 HTTPGET 请求
                var uriBuild = new UriBuilder(m_url);
                var query = HttpUtility.ParseQueryString("");
                query["q"] = q;
                query["from"] = "en";
                query["to"] = "zh";
                query["appid"] = m_appId;
                query["salt"] = "123456789";
                query["sign"] = CaptureHelpers.Md5(m_appId + query["q"] + query["salt"] + m_appSecret);

                uriBuild.Query = query.ToString();
                var uri = uriBuild.Uri;

                Console.WriteLine(uri);
                HttpResponseMessage response = client.GetAsync(uri).Result;
                // 判断服务器响应代码是否为 2XX
                response.EnsureSuccessStatusCode();
                //使用 await 语法读取响应内容
                string responseBody = response.Content.ReadAsStringAsync().Result;

                var json = JObject.Parse(responseBody);
                var res = json["trans_result"];

                String src = "";
                string dst = "";
                foreach (var it in res)
                {
                    var obj = it.Value<JObject>();
                    src += obj.GetValue("src").ToString();
                    dst += obj.GetValue("dst").ToString();
                  
                }
                // Console.WriteLine(trans);

                return new Models.TranslateResult(src, dst) ;
            }
        }
    }
}
