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
        public static void SendNewUserEmail(string recipient, Team team, string password)
        {
            RestClient client = new RestClient("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api", $"{EnvironmentVariables.MailgunKey}");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", $"{EnvironmentVariables.MailgunDomain}", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"RecTeam <NewUsers@{EnvironmentVariables.MailgunDomain}>");
            request.AddParameter("to", $"{recipient}");
            request.AddParameter("subject", $"Invitation to join team {team.Name}");
            request.AddParameter("text", $"You have been invited to join the team {team.Name} on <a href=\"http://www.RecTeam.net/Account/Login\">RecTeam.net</a>.  Please sign in using the following credentials:\nEmail: {recipient}\nPassword: {password}");
            request.Method = Method.POST;

            RestResponse response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
        }

        public static void SendNewTeammateEmail(string recipient, Team team)
        {
            RestClient client = new RestClient("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api", $"{EnvironmentVariables.MailgunKey}");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", $"{EnvironmentVariables.MailgunDomain}", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"RecTeam <NewUsers@{EnvironmentVariables.MailgunDomain}>");
            request.AddParameter("to", $"{recipient}");
            request.AddParameter("subject", $"Invitation to join team {team.Name}");
            request.AddParameter("text", $"You have been invited to join the team {team.Name} on <a href=\"http://www.RecTeam.net/Teams/Details/{team.Id}\">RecTeam.net</a>.  Please log in with the following email address: {recipient}.");
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
