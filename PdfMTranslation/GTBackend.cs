using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;

namespace PdfMTranslation
{
    public class GTBackend
    {
        private Jint.Engine eng = new Jint.Engine();
        private string tkk = "0.0";
        private CookieContainer cc = new CookieContainer();

        public string TargetLanguageISOCode { get; set; } = "en";

        private const string GTTOKEN_JS="gt-token.js";

        public GTBackend()
        {
            if (!File.Exists(GTTOKEN_JS))
            {
                var w = createWebRequest("https://translate.google.cn/");
                string html = "";
                using (StreamReader sr = new StreamReader(w.GetResponse().GetResponseStream()))
                    html = sr.ReadToEnd();
                var h = new HtmlDocument();
                h.LoadHtml(html);
                foreach (var script in h.DocumentNode.QuerySelectorAll("script"))
                    if (script.Attributes.Contains("src") && script.Attributes["src"].Value.Contains("/releases"))
                    {
                        var beautifier = new Jint.Engine();
                        beautifier.Execute(File.ReadAllText("codebeautify.js"));
                        using (var srscript = new StreamReader(
                            createWebRequest("https://translate.google.cn" + script.Attributes["src"].Value).GetResponse().GetResponseStream()))
                        {
                            var js = beautifier.Invoke("js_beautify", srscript.ReadToEnd()).AsString().Split('\n');
                            for (int jln = 0;jln<js.Length;++jln)
                            {
                                if (js[jln].Contains("String.fromCharCode(116)"))
                                {
                                    int jstart, jend;
                                    for (jstart = jln - 1; jstart > 0; --jstart) if (js[jstart].StartsWith("var ")) break;
                                    for (jend = jln + 1; jend < js.Length; ++jend) if (js[jend].StartsWith("var ")) break;
                                    File.WriteAllText(GTTOKEN_JS, string.Join(Environment.NewLine, js, jstart, jend - jstart));
                                    break;
                                }
                            }
                        }
                        break;
                    }
            }
            eng.Execute(File.ReadAllText(GTTOKEN_JS));

            string funcName = "";
            foreach (Match m in Regex.Matches(File.ReadAllText(GTTOKEN_JS), @"(.*?) = function\(a\)"))
                funcName = m.Groups[1].Value.Trim();

            eng.Execute("function gettk(a, tkk) { window={TKK: tkk}; return %%(a); }".Replace("%%", funcName));
        }

        private long timestamp()
        {
            return (long)((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        private void updateTKK()
        {
            var now = timestamp() / 3600000;
            if (tkk.StartsWith("" + now + ".")) return;
            var wc = createWebRequest("https://translate.google.cn/");
            
            using (StreamReader sr = new StreamReader(wc.GetResponse().GetResponseStream()))
            {
                foreach(Match m in Regex.Matches(sr.ReadToEnd(), @"(TKK.*?)</script>"))
                {
                    eng.Execute(m.Groups[1].Value);
                }
            }

            tkk = eng.GetValue("TKK").AsString();
        }

        private string getToken(string text)
        {
            updateTKK();
            return eng.Invoke("gettk", text, tkk).AsString();
        }

        private WebRequest createWebRequest(string url)
        {
            var wc = WebRequest.CreateHttp(url);
            wc.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            wc.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            wc.Referer = "https://translate.google.cn/";
            wc.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            wc.CookieContainer = cc;
            return wc;
        }
        
        public async Task<string> TranslateAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || !Regex.IsMatch(text, @"[a-zA-Z]")) return text;

            var wc = createWebRequest(string.Format(
                "https://translate.google.cn/translate_a/single?client=t&sl=auto&tl={1}&hl={1}&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&ie=UTF-8&oe=UTF-8&otf=1&ssel=0&tsel=0&kc=3{0}",
                getToken(text),
                TargetLanguageISOCode
                ));
            wc.Method = "POST";

            using (StreamWriter sw = new StreamWriter(wc.GetRequestStream()))
            {
                sw.Write("q=" + WebUtility.UrlEncode(text));
            }
            string r = "";
            try
            {
                var resp = await wc.GetResponseAsync();
                var stream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(stream))
                {
                    var json = sr.ReadToEnd();
                    var j = Newtonsoft.Json.Linq.JArray.Parse(json);
                    foreach (var jobj in j[0])
                        r += jobj[0].ToString().Replace("\n", Environment.NewLine);
                }
            }
            catch (WebException w)
            {
                Console.WriteLine(w.Response);
            }
            return r;
        }
    }
}
