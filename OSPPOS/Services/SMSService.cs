using DMX.Data;
using System.Net;

namespace DMX.Services
{
    public class SMSService(XContext context, ILogger<SMSService> log) : BackgroundService
    {
        public readonly ILogger<SMSService> logger = log;


        public readonly XContext ctx = context;

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogDebug("SMS Service is starting");
            stoppingToken.Register(() => logger.LogDebug("SMS Service is stopping."));
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogDebug("SMS Service is running in background");
                var pendingSMSTasks = ctx.SMSTasks.Where(x => !x.IsSent).AsEnumerable();
                foreach (var Tel in pendingSMSTasks)
                {
                    string URL = "https://frog.wigal.com.gh/ismsweb/sendmsg?";
                    string from = "JHC";
                    string username = "KofiPoku";
                    string password = "Az36400@osp";
                    string to = Tel.Telephone;
                    string messageText = "Thank you for joining Joy House Chapel. Your church ID is" + Tel.MemberId + "You are Welcome";

                    // Creating URL to send sms
                    string message = URL
                        + "username="
                        + username
                        + "&password="
                        + password
                        + "&from="
                        + from
                        + "&to="
                        + to
                        + "&service="
                        + "SMS"
                        + "&message="
                        + messageText;



                    HttpClient httpclient = new();

                    var response2 = await httpclient.SendAsync(new HttpRequestMessage(HttpMethod.Post, message));
                    if (response2.StatusCode == HttpStatusCode.OK)
                    {
                        // Do something with response. Example get content:
                        // var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);
                        Tel.IsSent = true;
                    }

                }
                await SendEmailsAsync(pendingSMSTasks);
                await Task.Delay(1000 * 60 * 5, stoppingToken);
            }
            logger.LogDebug("Demo service is stopping");


        }

        private async Task SendEmailsAsync(object pendingEmailTasks)
        {



            //Client client = new();

            //we creating the necessary URL string:
            //string GeneratedID = (from c in ctx.Clients where c.Id == client.Id select c.ClientNumber).FirstOrDefault().ToString()
                       ;
            string URL = "https://frog.wigal.com.gh/ismsweb/sendmsg?";
            string from = "JHC";
            string username = "KofiPoku";
            string password = "Az36400@osp";
            //string to = client.Telephone;
            string messageText = "Thank you for joining Joy House Chapel. Your church ID is"  /*GeneratedID*/ ;

            // Creating URL to send sms
            string message = URL
                + "username="
                + username
                + "&password="
                + password
                + "&from="
                + from
                + "&to="
                //+ to
                + "&service="
                + "SMS"
                + "&message="
                + messageText;



            HttpClient httpclient = new();

            var response2 = await httpclient.SendAsync(new HttpRequestMessage(HttpMethod.Post, message));
            if (response2.StatusCode == HttpStatusCode.OK)
            {
                // Do something with response. Example get content:
                // var responseContent = await response.Content.ReadAsStringAsync ().ConfigureAwait (false);

            }

            await Task.CompletedTask;

        }
    }
}







