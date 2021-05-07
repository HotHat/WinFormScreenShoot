using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormScreenShoot
{
    public partial class Form3 : Form
    {
        // 设置APPID/AK/SK
        String APP_ID = "24118900";
        String API_KEY = "u0c5ubTKkBlrVpAYg43SFg1v";
        String SECRET_KEY = "z2aTOhu9jG0LiFKojOFyy5ydE0tgh08a";

        Baidu.Aip.Ocr.Ocr client;

        public Form3()
        {
            InitializeComponent();
            client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间

            var image = File.ReadAllBytes("d://222.png");
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
