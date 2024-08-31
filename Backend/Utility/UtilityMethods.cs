namespace JobApplicationTracker.Utility;

using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class UtilityMethods
{
    static string region = "us-east-1";
    public static string GetDbUrlFromAWS()
    {
        string secretName = "cp-backend-production/dburl";
        IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

        GetSecretValueRequest request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
        };

        GetSecretValueResponse response = null;

        try
        {
            Console.WriteLine("APP | Getting db key from secret manager");
            response = client.GetSecretValueAsync(request).Result;
            Console.WriteLine($"APP | Response : {JsonConvert.SerializeObject(response)}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"APP | Error while fetching db string from secret manager: {e}");
        }

        JObject jsonObj = JObject.Parse(response.SecretString);

        string dbString = jsonObj["DbString"].ToString();

        // Your code goes here
        return dbString;
    }

    public static void SendSnsNotification()
    {
        string topicArn = getSNSTopicArn();
        string message = "Backend application is successfully started.";
        string subject = "AWS EC2 BACKEND | Startup Notification";

        var snsClient = new AmazonSimpleNotificationServiceClient(RegionEndpoint.USEast1);
        var publishRequest = new PublishRequest
        {
            TopicArn = topicArn,
            Message = message,
            Subject = subject
        };

        try
        {
            var response = snsClient.PublishAsync(publishRequest).Result;
            Console.WriteLine($"APP | Message published : {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"APP | Error publishing message: {ex.Message}");
        }

    }

    private static string getSNSTopicArn()
    {

        string secretName = "cp-backend-production/sns-topic-arn";
        IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

        GetSecretValueRequest request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
        };

        GetSecretValueResponse response = null;

        try
        {
            Console.WriteLine("APP | Getting sns arn from secret manager");
            response = client.GetSecretValueAsync(request).Result;
            Console.WriteLine($"APP | Response : {JsonConvert.SerializeObject(response)}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"APP | Error while fetching arn of sns from secret manager: {e}");
        }

        JObject jsonObj = JObject.Parse(response.SecretString);

        string snsArn = jsonObj["SnsTopicArn"].ToString();

        // Your code goes here
        return snsArn;

    }
}

