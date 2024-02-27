namespace CodeReview.CodeExample.Factory.Utils;

public class SkypeUtilsEx : IMessagingUtils
{
    //Here we should create Dependency injection for SkypeApi.
    // Lets imagine we have it.
    public SkypeUtilsEx()
    {

    }
    public void SendMessage(string username, string password, string to, string message)
    {
        var newMessage = message + " from " + username.ToUpper();
        SkypeApi.Send(to, newMessage, username, password);
    }
    public string Receive(string username, string password)
    {
        return SkypeApi.GetNextMessage(username, password);
    }

}
