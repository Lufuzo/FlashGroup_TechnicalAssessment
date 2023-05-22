using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using SQL_SanitizeWordsMVC_Client.Models;
using System.Net;
using System.Text;

namespace SQL_SanitizeWordsMVC_Client
{
    public class APIGateway
    {
        private string url = "https://localhost:7117/api/Words";
        private HttpClient httpClient = new HttpClient();

        public List<Word> ListWords()
        {
            List<Word> words = new List<Word>();
            if(url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                HttpResponseMessage responce = httpClient.GetAsync(url).Result;
                if (responce.IsSuccessStatusCode)
                {
                    string result = responce.Content.ReadAsStringAsync().Result;
                    var datacol = JsonConvert.DeserializeObject<List<Word>>(result);
                    if (datacol != null)
                    {
                        words = datacol;
                    }
                }
                else
                {
                    string result = responce.Content.ReadAsStringAsync().Result;
                    throw new Exception("Errour occcured at the API Endpoint, Error Info. "  +result);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Errour occcured at the API Endpoint, Error Info. " + ex.Message);
            }
            finally { }

            return words;
        
        }

        public Word CreateWord(Word word)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string json = JsonConvert.SerializeObject(word);
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Word>(result);
                    if (data != null)
                    {
                        word = data;
                    }

                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occcured at the API Endpoint, Error Info. " + result);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occcured at the API Endpoint, Error Info. " + ex.Message);
            }
            finally
            { }

            return word;
        }

        public Word GetWord(int id)
        {
            Word word = new Word();
            url = url + "/" + id;

            if(url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try 
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
               

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Word>(result);
                    if (data != null)
                    {
                        word = data;
                    }
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occcured at the API Endpoint, Error Info. " + result);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Errour occcured at the API Endpoint, Error Info. " + ex.Message);
            }

            return word;
        
        }


        public void UpdateWord(Word word)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            int id = word.id;

            url = url + "/" + id;

            string json = JsonConvert.SerializeObject(word);
           

            try 
            {
                HttpResponseMessage response = httpClient.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;

                if (!response.IsSuccessStatusCode)
                { 
                  string result = response.Content.ReadAsStringAsync().Result;
                   throw new Exception("Error occcured at the API Endpoint, Error Info. " + result);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occcured at the API Endpoint, Error Info. " + ex.Message);
            }
            return;
        }

        public void DeleteWord(int id)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            url = url + "/" + id;
            

            try
            {
                HttpResponseMessage response = httpClient.DeleteAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error occcured at the API Endpoint, Error Info. " + result);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error occcured at the API Endpoint, Error Info. " + ex.Message);
            }
        }

    }
}
