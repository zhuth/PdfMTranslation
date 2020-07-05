using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PdfMTranslation
{
    public class MTBackend
    {
        Py.GILState gil = Py.GIL();
        dynamic ts = Py.Import("translators");
        public string TargetLanguageISOCode { get; internal set; }

        public async Task<string> TranslateAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            return await Task.Run<string>(() => {
                var r = ts.bing(text, "auto", TargetLanguageISOCode);
                return ("" + r) as string;
                });
        }
    }
}
