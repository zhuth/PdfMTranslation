using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PdfMTranslation
{
    class Helper
    {
        public static IEnumerable<char> RemoveDiacriticsEnum(string src, bool compatNorm, Func<char, char> customFolding)
        {
            foreach (char c in src.Normalize(compatNorm ? NormalizationForm.FormKD : NormalizationForm.FormD))
                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    case UnicodeCategory.NonSpacingMark:
                    case UnicodeCategory.SpacingCombiningMark:
                    case UnicodeCategory.EnclosingMark:
                        //do nothing
                        break;
                    default:
                        yield return customFolding(c);
                        break;
                }
        }
        public static IEnumerable<char> RemoveDiacriticsEnum(string src, bool compatNorm)
        {
            return RemoveDiacritics(src, compatNorm, c => c);
        }
        public static string RemoveDiacritics(string src, bool compatNorm, Func<char, char> customFolding)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in RemoveDiacriticsEnum(src, compatNorm, customFolding))
                sb.Append(c);
            return sb.ToString();
        }
        public static string RemoveDiacritics(string src, bool compatNorm)
        {
            return RemoveDiacritics(src, compatNorm, c => c);
        }
        public static string Normalize(string src)
        {
            Regex r = new Regex(@"\s");
            return RemoveDiacritics(r.Replace(src, "").ToLower(), true);
        }

        public static string Reparagrah(string text)
        {
            var lines = text.Split('\n');
            var r = "";
            int max = lines.Max((s) => s.Length);
            int thr = (int)lines.Average((s) => s.Length);
            bool first = true;
            string last = ".";

            foreach(string l in lines)
            {
                if (l.Length == 0) continue;
                if (first) { first = false;  continue; }
                if (l[0] == l[0].ToString().ToUpper()[0] && (last.Length < thr || ".,?!;/'\"".Contains(last.Last()))) r += "\n";
                r += l;
                last = l;
            }

            return r.Trim();
        }
    }
}
