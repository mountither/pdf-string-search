using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Globalization;
using System.IO;
using System.Linq;

namespace pdf_text_find
{
    class ReadFile
    {
        public List<FileData> ReadPdfFile(string locName, string searchText)
        {
            List<FileData> fd = new List<FileData>();

            string dirLoc = null;

            switch (locName)
            {
                case "lc":
                    dirLoc = DotNetEnv.Env.GetString("LOCATION_1");
                    break;
                case "tt":
                    dirLoc = DotNetEnv.Env.GetString("LOCATION_2");
                    break;
                case "bk":
                    dirLoc = DotNetEnv.Env.GetString("LOCATION_3");
                    break;
                default:
                    break;
            }

            string[] fileEntries = Directory.GetFiles(String.Format(@"{0}",dirLoc));

            foreach (string fileName in fileEntries)
            {

                if (File.Exists(fileName)) ;
                {

                    PdfReader pdfReader = new PdfReader(fileName);
                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {

                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                        string currentPageText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                        if (currentPageText.Contains(searchText, (StringComparison)CompareOptions.IgnoreCase))
                        {

                            fd.Add(new FileData { FileName = fileName, PageNumber = page });
                        }
                    }
                    pdfReader.Close();
                }
            }
            return fd;
        }
    }
}
