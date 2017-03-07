using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using System.Diagnostics;
using RestSharp.Authenticators;

namespace RecTeam.Models
{
    public class MailgunApi
    {
        public static void SendMailgunMessage(string recipient, string teamName, string password)
        {
            RestClient client = new RestClient("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api", $"{EnvironmentVariables.MailgunKey}");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", $"{EnvironmentVariables.MailgunDomain}", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"RecTeam <mailgun@{EnvironmentVariables.MailgunDomain}>");
            request.AddParameter("to", $"{recipient}");
            request.AddParameter("subject", $"Invitation to join team {teamName}");
            request.AddParameter("text", $"You have been invited to join the team {teamName} on [Domain Name Placeholder].  Please sign in using the following credentials:\nEmail: {recipient}\nPassword: {password}");
            request.Method = Method.POST;

            RestResponse response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            TaskCompletionSource<IRestResponse> tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
