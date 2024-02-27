using System.Net.NetworkInformation;

namespace CodeReview.CodeExample;

public class ConnectionChecker : IConnectionChecker

{
    private readonly string _domain = "news.com"; 
    public bool VerifyInternetConnection()
    {
        if (new Ping().Send(_domain).Status != IPStatus.Success)
        {
           return false;
        } 
        return true;
    }
}
