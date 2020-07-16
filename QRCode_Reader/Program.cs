using System;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QRCode_Reader
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            
            string scannfilepth;
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine("\n");
            Console.Write("Please entar a scan file Path - ");
            scannfilepth = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", scannfilepth);
            Console.WriteLine("------------------------------------------------------------------------------------");
            string responseQRDataDetails = Upload(scannfilepth).GetAwaiter().GetResult();
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("---------------QR CODE DETAILS -----------------------------------------------------");
            Console.WriteLine(responseQRDataDetails);
            Console.WriteLine("------------------------------------------------------------------------------------");

            Console.ReadLine();
        }


        private static async Task<string> Upload(string filepath)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://api.qrserver.com/v1/read-qr-code/");
                var content = new MultipartFormDataContent();

                byte[] byteArray = System.IO.File.ReadAllBytes(filepath);
                content.Add(new ByteArrayContent(byteArray), "file", "file.GIF");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                return "Error Occured";
            }

        }

    }
}
