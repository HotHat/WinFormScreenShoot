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
        private String m_key = "u0c5ubTKkBlrVpAYg43SFg1v";
        private String m_secret = "z2aTOhu9jG0LiFKojOFyy5ydE0tgh08a";

        Baidu.Aip.Ocr.Ocr client;

        private BaiduOcr()
        {
            //m_key = key;
            //m_secret = secret;
        }

        public static BaiduOcr GetInstance()
        {
            return new BaiduOcr();
        }


        public void send(String filePath)
        {
            client = new Baidu.Aip.Ocr.Ocr(m_key, m_secret);
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

        public String send(byte[] image)
        {
            Console.WriteLine("Start OCR!");
            client = new Baidu.Aip.Ocr.Ocr(m_key, m_secret);
            client.Timeout = 60000;  // 修改超时时间

            // var image = File.ReadAllBytes(filePath);
            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            // var result = client.GeneralBasic(image);
            // Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                    {"language_type", "ENG"},
                    {"detect_direction", "true"},
                    {"detect_language", "false"},
                    {"probability", "true"}
                };
            // 带参数调用通用文字识别, 图片参数为本地图片
            var result = client.GeneralBasic(image, options);
            // Console.WriteLine(result);

            if (result == null) { return ""; }

            String trans = "";

            foreach(var words in result.GetValue("words_result"))
            {
                // Console.WriteLine(words);
                var w = words.Value<String>("words");
                trans += w;
            }

            Console.WriteLine(trans);
            return trans;
        }
    }


    

}
