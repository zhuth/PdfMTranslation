using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using JBig2Decoder;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Net;
using FreeImageAPI;

namespace PdfMTranslation
{
    public partial class Form1 : Form
    {
        private int pages = 0;
        private int currentPage = 1;
        private string text, cacheFilename;
        private PdfReader reader;

        private GTBackend gt = new GTBackend();

        private class Config
        {
            private const string configFile = "config.json";

            public int page = 0;
            public string filename = "";
            public string style = "light";
            public string targetLanguage = "en";

            private Config() { }
            private static Config _inst = new Config();

            public static int Page { get { return _inst.page; } set { _inst.page = value; } }
            public static string Filename { get { return _inst.filename; } set { _inst.filename = value; } }
            public static string Style { get { return _inst.style; } set { _inst.style = value; } }
            public static string TargetLanguage { get { return _inst.targetLanguage; } set { _inst.targetLanguage = value; } }
            
            static public void Load()
            {
                if (File.Exists(configFile))
                    _inst = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile));
            }

            static public void Save()
            {
                File.WriteAllText(configFile, Newtonsoft.Json.JsonConvert.SerializeObject(_inst));
            }
        }

        private Dictionary<int, string> translatedCache = new Dictionary<int, string>();

        public Form1()
        {
            InitializeComponent();
            Config.Load();
            gt.TargetLanguageISOCode = Config.TargetLanguage;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (reader != null)
                Form1_FormClosing(sender, null);

            var ofd = new OpenFileDialog { Filter = "PDF|*.pdf" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            loadFile(ofd.FileName);
        }

        private void loadFile(string filename)
        {
            reader = new PdfReader(filename);
            pages = reader.NumberOfPages;
            currentPage = 1;

            Config.Filename = filename;
            loadTranslationCache();

            loadPage();
        }

        private void loadTranslationCache()
        {
            cacheFilename = Config.Filename.GetHashCode().ToString("X8") + "_" + gt.TargetLanguageISOCode + ".cache";

            translatedCache.Clear();
            if (File.Exists(cacheFilename))
            {
                translatedCache = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, string>>(File.ReadAllText(cacheFilename));
            }
        }

        private PdfObject FindImageInPDFDictionary(PdfDictionary pg)
        {
            PdfDictionary res =
                (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));


            PdfDictionary xobj =
              (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
            if (xobj != null)
            {
                foreach (PdfName name in xobj.Keys)
                {

                    PdfObject obj = xobj.Get(name);
                    if (obj.IsIndirect())
                    {
                        PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);

                        PdfName type =
                          (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));

                        //image at the root of the pdf
                        if (PdfName.IMAGE.Equals(type))
                        {
                            return obj;
                        }// image inside a form
                        else if (PdfName.FORM.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        } //image inside a group
                        else if (PdfName.GROUP.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        }

                    }
                }
            }

            return null;

        }
        
        private void loadPage()
        {
            txtTranslated.Text = "";
            if (reader == null) return;
            text = Helper.Reparagrah(PdfTextExtractor.GetTextFromPage(reader, currentPage, new LocationTextExtractionStrategy()));
            if (text == null) text = "";
            PdfDictionary pg = reader.GetPageN(currentPage);
            PdfObject obj = FindImageInPDFDictionary(pg);
            if (obj != null)
            {
                try
                {
                    int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    PdfObject pdfObj = reader.GetPdfObject(XrefIndex);
                    PdfStream pdfStream = (PdfStream)pdfObj;
                    var pdfImage = new PdfImageObject((PRStream)pdfStream);
                    var pictype = pdfImage.GetFileType();
                    MemoryStream stream = new MemoryStream();
                    if (pictype == "jbig2")
                    {
                        var decoder = new JBIG2StreamDecoder();
                        JBIG2StreamDecoder.debug = true;
                        var b = decoder.decodeJBIG2(pdfImage.GetImageAsBytes());

                        stream = new MemoryStream(b);
                    }
                    else
                    {
                        var fimg = FreeImage.LoadFromStream(new MemoryStream(pdfImage.GetImageAsBytes()));
                        if (fimg.IsNull) throw new ArgumentException();
                        FreeImage.SaveToStream(fimg, stream, FREE_IMAGE_FORMAT.FIF_BMP);
                    }

                    var img = Image.FromStream(stream);
                    if (Config.Style == "dark")
                    {
                        var bmp = (Bitmap)img;
                        int w = bmp.Width, h = bmp.Height;
                        BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                        unsafe
                        {
                            int* bytes = (int*)data.Scan0;
                            for (int i = w * h - 1; i >= 0; i--)
                                bytes[i] = ~bytes[i];
                        }
                        bmp.UnlockBits(data);
                    }
                    pic.Image = img;
                    txtOriginal.Visible = false;
                }
                catch
                {
                    txtOriginal.Text = text;
                    txtOriginal.Visible = true;
                    splitContainer1.Panel1.VerticalScroll.Value = 0;
                }
            }

            txtPageNum.Text = "" + currentPage;

            if (translatedCache.ContainsKey(currentPage))
            {
                txtTranslated.Text = translatedCache[currentPage];
            }
            else
            {
                clickTranslate();
            }
            Config.Page = currentPage;
        }

        private async void clickTranslate()
        {
            txtTranslated.Text = await gt.TranslateAsync(text);
            if (!translatedCache.ContainsKey(currentPage)) translatedCache.Add(currentPage, "");
            translatedCache[currentPage] = txtTranslated.Text;
        }

        private string getPreviousLine()
        {
            if (currentPage == 1) return "";
            var s = PdfTextExtractor.GetTextFromPage(reader, currentPage - 1, new LocationTextExtractionStrategy());
            return s.Split('\n').Last((x) => x.Length > 10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtTranslated.Font = new Font("Times New Roman", 14);

            applyStyle();
            
            if (!string.IsNullOrEmpty(Config.Filename) && File.Exists(Config.Filename))
            {
                int page = Config.Page;
                loadFile(Config.Filename);
                currentPage = page;
                loadPage();
            }
        }

        private void prevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPage == 1) return;
            --currentPage;
            loadPage();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPage == pages) return;
            ++currentPage;
            loadPage();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    prevToolStripMenuItem_Click(sender, null);
                    break;
                case Keys.PageDown:
                    nextToolStripMenuItem_Click(sender, null);
                    break;
                case Keys.F5:
                    reloadToolStripMenuItem_Click(sender, null);
                    break;
                case Keys.C:
                    if (e.Control) copyOriginalToolStripMenuItem_Click(sender, null);
                    break;
                case Keys.S:
                    if (e.Control) Form1_FormClosing(sender, null);
                    break;
            }
        }

        private void txtPageNum_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int t = 0;
                if (int.TryParse(txtPageNum.Text, out t) && t >= 1 && t <= pages)
                {
                    currentPage = t;
                    loadPage();
                } else
                {
                    txtPageNum.Text = "" + currentPage;
                }
            }
        }
        
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickTranslate();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            splitContainer1.Panel1.AutoScroll = true;
            pic.Width = splitContainer1.Panel1.Width;
            if (pic.Image != null)
            {
                double ratio = (double)pic.Image.Height / pic.Image.Width;
                pic.Height = (int)(ratio * pic.Width);
            }
        }

        private void txtTranslated_MouseUp(object sender, MouseEventArgs e)
        {
            if (txtTranslated.Text.Length > 0)
            {
                double r = (double)txtTranslated.SelectionStart / txtTranslated.Text.Length;
                picCursor.Top = (int)(pic.Height * r) - splitContainer1.Panel1.VerticalScroll.Value;
                picCursor.Visible = true;
                picCursor.Select();
            }
            else
            {
                picCursor.Visible = false;
            }
        }

        private void copyOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(text);
        }

        private void copyTranslatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtTranslated.Text);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string s = Helper.Normalize(txtSearch.Text);

                for (int i =currentPage+1;i<=pages+currentPage;++i)
                {
                    var txt = PdfTextExtractor.GetTextFromPage(reader, ((i-1)%pages)+1, new LocationTextExtractionStrategy());
                    if (Helper.Normalize(txt).Contains(s))
                    {
                        currentPage = ((i - 1) % pages) + 1;
                        loadPage();
                        break;
                    }
                }
            }
        }
        
        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (darkToolStripMenuItem.Text == "&Dark")
            {
                Config.Style = "dark";
            } else
            {
                Config.Style = "light";
            }
            applyStyle();
            loadPage();
        }

        private void applyStyle()
        {
            if (Config.Style == "dark")
            {
                darkToolStripMenuItem.Text = "&Light";
                txtTranslated.BackColor = Color.FromArgb(30, 30, 30);
                txtTranslated.ForeColor = Color.LightGray;
            }
            else
            {
                darkToolStripMenuItem.Text = "&Dark";
                txtTranslated.BackColor = Color.White;
                txtTranslated.ForeColor = Color.Black;
            }
        }

        private void toEnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.TargetLanguage = "en";
            gt.TargetLanguageISOCode = Config.TargetLanguage;
            loadTranslationCache();
        }

        private void toChineseSimpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.TargetLanguage = "zh-CN";
            gt.TargetLanguageISOCode = Config.TargetLanguage;
            loadTranslationCache();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(cacheFilename))
                File.WriteAllText(cacheFilename, Newtonsoft.Json.JsonConvert.SerializeObject(translatedCache));
            Config.Save();
        }
    }
}
