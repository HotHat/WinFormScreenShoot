using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormScreenShoot
{
    class BaiduOcr
    {
        // 设置APPID/AK/SK
        private String _key = "";
        private String _secret = "";

        Baidu.Aip.Ocr.Ocr client;

        private BaiduOcr(String key, String secret)
        {
            _key = key;
            _secret = secret;
        }

        public static BaiduOcr GetInstance(String key, String secret)
        {
            return new BaiduOcr(key, secret);
        }


        private void send(String filePath)
        {
            client = new Baidu.Aip.Ocr.Ocr(_key, _secret);
            client.Timeout = 60000;  // 修改超时时间

            var image = File.ReadAllBytes(filePath);
            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.GeneralBasic(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                    {"language_type", "ENG"},
                    {"detect_direction", "true"},
                    {"detect_language", "false"},
                    {"probability", "true"}
                };
            // 带参数调用通用文字识别, 图片参数为本地图片
            result = client.GeneralBasic(image, options);
            Console.WriteLine(result);
        }
    }


    

}
