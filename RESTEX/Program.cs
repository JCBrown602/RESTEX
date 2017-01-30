using System;
using System.IO;
using System.Net;
using System.Text;

using Newtonsoft.Json;

namespace RESTEX
{
    public class MarketItem
    {
        public int type_id { get; set; }
        public double average_price { get; set; }
        public double adjusted_price { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://esi.tech.ccp.is/latest/markets/prices/?datasource=tranquility";
            string html = string.Empty;
            MarketItem[] jsonArray;

            MarketItem Item = new MarketItem();

            // Create the web request  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            // HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadLine();
                jsonArray = JsonConvert.DeserializeObject<MarketItem[]>(html);
            }

            //html = JsonConvert.DeserializeObject<MarketItem>(html);

            Console.WriteLine("> Type ID: " + jsonArray[0].type_id + " Adj. Price: " + jsonArray[0].adjusted_price);

            printData(jsonArray);
            #region Keep this
            //// Keep this...
            //using (StreamWriter writer = new StreamWriter("NEW FILE 2.txt"))
            //{
            //    writer.WriteLine("> TESTING...");
                
            //    writer.WriteLine(html);
            //}

            //// Get response  
            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    // Get the response stream  
            //    StreamReader reader = new StreamReader(response.GetResponseStream());

            //    Console.WriteLine("> response.ResponseUri is: {0}", response.ResponseUri);
            //    Console.WriteLine("> response.ContentType is: {0}", response.ContentType);

            //    stuff = reader.ReadLine();

            //    MarketItem Item = new MarketItem();
            
            //    Console.WriteLine(stuff);
            //}

            //using (StreamWriter writer = new StreamWriter("NEW FILE.txt"))
            //{
            //    writer.WriteLine("> TESTING...");
            //}
            #endregion
        }

        public static void printData(MarketItem[] Items)
        {
            string fileName = "data.dat";
            using (StreamWriter w = new StreamWriter(fileName))
            {
                w.WriteLine(">*** Number of Items: " + Items.Length.ToString());
                for (int i = 0; i < Items.Length; i++)
                {
                    w.WriteLine("[" + i.ToString() + "] Type ID: " + Items[i].type_id + " Adj. Price: " + Items[i].adjusted_price);
                    //Console.WriteLine("> Type ID: " + Items[i].type_id + " Adj. Price: " + Items[i].adjusted_price);
                }
                w.WriteLine(">*** Number of Items: " + Items.Length.ToString());

                Console.WriteLine("> Writing to file ({0}) operation complete.", fileName);
            }
        }
    }
}


/* https://login.eveonline.com/oauth/authorize/?
 * response_type=code&
 * redirect_uri=https%3A%2F%2F3rdpartysite.com%2Fcallback&
 * client_id=3rdpartyClientId&
 * scope=characterContactsRead%20characterContactsWrite&
 * state=uniquestate123
 * 
 * OAuth URL in VS Projects
 * 
 * response_type: Must be set to “code”.
 * redirect_uri: After authentication the user will be redirected to this URL on your website. 
 It must match the definition on file in the developers site.
 * client_id: A string identifier for the client, provided by CCP.
 * scope: The requested scopes as a space delimited string.
 * state: An opaque value used by the client to maintain state between the request and callback. 
 The SSO includes this value when redirecting back to the 3rd party website. While not required, it 
 is important to use this for security reasons. 
 http://www.thread-safe.com/2014/05/the-correct-use-of-state-parameter-in.html explains why the state 
 parameter is needed.
 * */
