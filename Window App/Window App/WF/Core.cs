using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Window_App.Common;

namespace Window_App.WF
{
    public class Core 
    {
        public async Task<List<string>> PostSelectedOriginalJsonAsync(JArray originalJson, List<int> selectedIndexes, string bearer, string apiUrl)
        {
            var results = new List<string>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-authorization", "Bearer " + bearer);
                client.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");

                foreach (int idx in selectedIndexes)
                {
                    JObject rowObj = (JObject)originalJson[idx];

                    if (rowObj["accessMode"] == null || string.IsNullOrWhiteSpace(rowObj["accessMode"].ToString()))
                        continue;

                    string json = rowObj.ToString(Newtonsoft.Json.Formatting.Indented);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, content);
                    string result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        results.Add(result);
                    else
                        results.Add($"Error {response.StatusCode}: {result}");
                }
            }

            return results;
        }


        public async Task<string> PostSoapRequestAsync(string url, string action, string bearer, string saleOrderId)
        {
            using (var client = new HttpClient())
            {
                // Authorization if required
                if (!string.IsNullOrWhiteSpace(bearer))
                    client.DefaultRequestHeaders.Add("x-authorization", "Bearer " + bearer);

                // SOAP Action
                client.DefaultRequestHeaders.Add("SOAPAction", action);

                // SOAP Envelope
                string soapEnvelope = $@"soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:web=""http://webservice.sss.sbn.com/"">
                   <soapenv:Header/>
                    <soapenv:Body>
                        <web:listOrderDetailByOrderNo>
                            <!--Optional:-->
                            <ORDER_NO>{saleOrderId}</ORDER_NO>
                        </web:listOrderDetailByOrderNo>
                    </soapenv:Body>
                </soapenv:Envelope>";

                var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");

                var response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }
    }
}
