using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace pdf_text_find
{
    class Program
    {
        static void Main(string[] args)
        {
            DotNetEnv.Env.Load();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            Console.WriteLine("Enter location (folder1 or folder2 or folder3)");
            string locname = Console.ReadLine();
            Console.WriteLine("Enter the Search text: ");
            string search = Console.ReadLine();

            Read read = new Read();
            List<FileData> pages = read.ReadPdfFile(locname, search);

            if (pages == null)
            {
                return;
            }

            foreach (FileData page in pages)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Filename: " + page.FileName + " | Page Number: " + page.PageNumber);
                Console.ResetColor();

            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Amount of Search Results: " + pages.Count);
            Console.ResetColor();
            

        }

    }
    
}

class Read
{
    public List<FileData> ReadPdfFile(string locName, string searchText)
    {
        List<FileData> fd = new List<FileData>();

        string dirLoc = null;
        
        if (locName == "folder1")
        {
            dirLoc = DotNetEnv.Env.GetString("LOCATION_1");
        }
        else if (locName == "folder2")
        {
            dirLoc = DotNetEnv.Env.GetString("LOCATION_2"); ;
        }
        else if (locName == "folder3")
        {
            dirLoc = DotNetEnv.Env.GetString("LOCATION_3"); ;
        }
        else
        {
            Console.WriteLine("File does not exist");
            return null;
        }



        string[] fileEntries = Directory.GetFiles(dirLoc);

        foreach (string fileName in fileEntries)
        {
            
            if (File.Exists(fileName));
            {

                PdfReader pdfReader = new PdfReader(fileName);
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {

                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                    string currentPageText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    if (currentPageText.Contains(searchText, (StringComparison)CompareOptions.IgnoreCase))
                    {

                        fd.Add(new FileData { FileName = fileName, PageNumber = page});
                    }
                }
                pdfReader.Close();
            }
        }
        return fd;
    }
}


class FileData
{
    public string FileName { get; set; }
    public int PageNumber { get; set; }

}
