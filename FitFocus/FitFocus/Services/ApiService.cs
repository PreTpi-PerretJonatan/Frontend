using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FitFocus.Models;
using System.Diagnostics;
using System.Linq;

namespace FitFocus.Services
{
	public static class ApiService
	{
        public static string GetUrl()
        {
            return "https://fitfocus.cld.education/api/";
        }

        /// <summary>
        /// Send an authentification request
        /// </summary>
        /// <param name="Url">url to connect</param>
        /// <param name="securityString">secure string of the user</param>
        /// <returns>response from the server</returns>
        public async static Task<User> PostToAPIEndpoint_Authentify(string securityString)
        {
            try
            {
                string Url = GetUrl() + "login";
                string html = string.Empty;
                List<KeyValuePair<string, string>> body = new List<KeyValuePair<string, string>>();
                body.Add(new KeyValuePair<string, string>("secure_string", securityString));
                Response response = JsonConvert.DeserializeObject<Response>(await PostToAPIEndpoint(body, Url));
                if (response.success)
                {
                    User user = new User();
                    LoginData loginData = response.data;
                    user.SecureString = securityString;
                    user.Token = loginData.token;
                    user.Username = loginData.username;
                    user.Policies = loginData.policies == null ? "" : loginData.policies;
                    user.IsAuthenticated = true;
                    return user;
                }
                return new User();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw new Exception("API error");
            }
        }

        /// <summary>
        /// Send an authentification request
        /// </summary>
        /// <param name="Url">url to connect</param>
        /// <param name="securityString">secure string of the user</param>
        /// <returns>response from the server</returns>
        public async static Task<List<Workout>> PostToAPIEndpoint_GetWorkouts(string token)
        {
            try
            {
                string Url = GetUrl() + "workouts";
                string html = string.Empty;
                List<KeyValuePair<string, string>> body = new List<KeyValuePair<string, string>>();
                body.Add(new KeyValuePair<string, string>("api_token", token));
                string a = await PostToAPIEndpoint(body, Url);
                WorkoutResponse response = JsonConvert.DeserializeObject<WorkoutResponse>(a);
                if (response.success)
                {
                    Workout[] workouts = response.data;
                    return workouts.ToList();
                }
                return new List<Workout>();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                throw new Exception("API error");
            }
        }



        /// <summary>
        /// Work around the ssl certificate in debug mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certification"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        /// <summary>
        /// Make an API request to the url passed with the html passed
        /// </summary>
        /// <param name="body">html to send to server</param>
        /// <param name="Url">url to send the request</param>
        /// <returns>response from the server</returns>
        public async static Task<string> PostToAPIEndpoint(List<KeyValuePair<string, string>> body, string Url)
        {
            if (false)
            {
                using (var client = new HttpClient())
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                    httpWebRequest.ContentType = "application/form-data";
                    httpWebRequest.Method = "POST";
                    //skip the ssl certificates
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                    string json = JsonConvert.SerializeObject(body);
                    try
                    {
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                        {
                            streamWriter.Write(json);
                        }
                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var responseText = streamReader.ReadToEnd();
                            Console.WriteLine(responseText);
                            return (responseText);
                        }
                    }
                    catch (Exception ex)
                    {
                        string text = ex.ToString();
                        Console.WriteLine(text);
                        throw ex;
                    }
                };
            }
            else
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Url);
                var content = new FormUrlEncodedContent(body);
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
                return responseString;
            }
        }
    }
}

