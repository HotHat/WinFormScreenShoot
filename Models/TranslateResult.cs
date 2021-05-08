using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormScreenShoot.Models
{
    class TranslateResult
    {
        public String src;
        public String dst;

        public TranslateResult(String src, string dst)
        {
            this.src = src;
            this.dst = dst;
        }

        public override string ToString()
        {
            return "{ \"src\": \"" + src + "\", \"dst\": \"" + dst + "\"}";
        }
    }
}
