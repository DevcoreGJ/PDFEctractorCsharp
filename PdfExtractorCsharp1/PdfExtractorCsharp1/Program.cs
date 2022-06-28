using System;
using System.Net;
using System.Web.Script.Serialization; // requires the reference 'System.Web.Extensions'
using System.IO;


class PdfTextExtractor
{
    // When you have your own cliend ID and secret, specify them here:
    private static string CLIENT_ID = "FREE_TRIAL_ACCOUNT";
    private static string CLIENT_SECRET = "PUBLIC_SECRET";

    private static string API_URL = "https://api.whatsmate.net/v1/pdf/extract?url=";

    static void Main(string[] args)
    {
        // TODO: Specify the URL of your small PDF document (less than 1MB and 10 pages)
        // To extract text from bigger PDf document, you need to use the async method.
        string pdfUrl = "https://www.harvesthousepublishers.com/data/files/excerpts/9780736948487_exc.pdf";

        PdfTextExtractor pdfTextExtractor = new PdfTextExtractor();
        string text = pdfTextExtractor.extractText(pdfUrl);

        Console.WriteLine("===============================");
        Console.WriteLine("PDF TEXT IS AS FOLLOWS:");
        Console.WriteLine(text);

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }

    public string extractText(string pdfUrl)
    {
        string pdfText;

        try
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
                client.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

                string endpointUrl = API_URL + pdfUrl;
                pdfText = client.DownloadString(endpointUrl);
            }
        }
        catch (WebException webEx)
        {
            Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
            Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String body = reader.ReadToEnd();
            Console.WriteLine(body);
            pdfText = "ERROR";
        }

        return pdfText;
    }

}