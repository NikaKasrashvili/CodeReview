namespace CodeReview.CodeExample.Factory.Utils;

/// <summary>
/// Interface for the concrete "products".
/// Declares the operations all concrete products must implement.
/// </summary>
public interface IMessagingUtils
{
    /// <summary>
    /// Sends message to the concrete user.
    /// </summary>
    /// <param name="username">username of sender</param>
    /// <param name="password">password of sender</param>
    /// <param name="to">user that recievs message</param>
    /// <param name="message">concrete message that is being sent</param>
    void SendMessage(string username, string password, string to, string message);

    /// <summary>
    /// Recieves a message
    /// </summary>
    /// <param name="username">Senders username</param>
    /// <param name="password">Senders password</param>
    /// <returns></returns>
    string Receive(string username, string password);
}
