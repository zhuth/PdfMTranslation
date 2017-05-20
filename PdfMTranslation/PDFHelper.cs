using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using JBig2Decoder;
using FreeImageAPI;

using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace PdfMTranslation
{
    public class PDFHelper
    {
        private string filename;
        private PdfReader reader;

        public PDFHelper(string filename)
        {
            this.filename = filename;
            reader = new PdfReader(filename);
        }

        public int NumberOfPages
        {
            get
            {
                return reader.NumberOfPages;
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

        public string GetRawText(int currentPage)
        {
            return PdfTextExtractor.GetTextFromPage(reader, currentPage, new LocationTextExtractionStrategy());
        }

        public string GetText(int currentPage)
        {
            var text = Helper.Reparagrah(GetRawText(currentPage));
            if (string.IsNullOrWhiteSpace(text)) return "";
            return text.Trim();
        }

        public Image GetImage(int currentPage, bool reverseColor = false)
        {
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
                    if (reverseColor)
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
                    return img;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
    }
}
