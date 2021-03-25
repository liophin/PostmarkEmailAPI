using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

public class Postmarkapp
{

    private static string ApiURL = "https://api.postmarkapp.com/email";
    private static string TransactionalAPITokens = ""; //ur server api token
    private static string SenderEmail = ""; //ur sender email

    public static PostmarkResponse SendEmail(PostmarkBody item)
    {
        using (WebClient client = new WebClient())
        {
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("X-Postmark-Server-Token", TransactionalAPITokens);
            //------------------------------------------
            var JSONData = new
            {
                From = SenderEmail,
                To = item.To,
                Subject = item.Subject,
                Tag = item.Tag,
                HtmlBody = item.HtmlBody,
                TextBody = item.TextBody,
                MessageStream = "outbound",
            };
            var dataString = JsonConvert.SerializeObject(JSONData);
            //------------------------------------------
            string result = client.UploadString(ApiURL, "POST", dataString);
            PostmarkResponse items = JsonConvert.DeserializeObject<PostmarkResponse>(result);
            return items;
        }

    }
    //------------------------------------------- Send a single email

}

public class PostmarkBody
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Tag { get; set; }
    public string HtmlBody { get; set; }
    public string TextBody { get; set; }
}
//------------------------------------------- for email sending data

public class PostmarkResponse
{
    public string To { get; set; }
    public DateTime SubmittedAt { get; set; }
    public string MessageID { get; set; }
    public int ErrorCode { get; set; }
    public string Message { get; set; }
}
//------------------------------------------- for result message
