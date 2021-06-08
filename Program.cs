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

            Console.WriteLine("Enter location (tt or lc or bk)");
            string locname = Console.ReadLine();
            Console.WriteLine("Enter the Search text: ");
            string search = Console.ReadLine();

            ReadFile read = new ReadFile();
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


