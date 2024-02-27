namespace CodeReview.CodeExample.Factory.Utils;

public class ViberUtilsEx : IMessagingUtils
{
    //Here we should create Dependency injection for SkypeApi.
    // Lets imagine we have it.
    public ViberUtilsEx()
    {

    }
    public void SendMessage(string username, string password, string to, string message)
    {
        ViberApi.Send(to, message, username, password);
    }
    public string Receive(string username, string password)
    {
        return ViberApi.RecieveMessage(username, password);
    }

}
