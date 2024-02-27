using CodeReview.CodeExample.Factory.Factories;
using CodeReview.CodeExample.Factory.Utils;

namespace CodeReview.CodeExample;

public class ProcessorEx : IProcessor
{
    //Props to store values while creating instance of processor class.
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string To { get; private set; }

    private readonly IMessagingFactory _messagingFactory;
    private readonly IConnectionChecker _connectionChecker;

    #region Constructor

    // Declaring constructor instead of Set method.
    public ProcessorEx(
        string name,
        string pwd,
        string to,
        IMessagingFactory messagingFactory,
        IConnectionChecker connectionChecker
        )
    {
        Username = name;
        Password = pwd;
        To = to;
        _messagingFactory = messagingFactory;
        _connectionChecker = connectionChecker;
    }
    #endregion

    /// <summary>
    /// Sends message
    /// </summary>
    /// <param name="protocol">protocol type</param>
    /// <param name="message"></param>
    public void SendMessage(ProtocolTypes protocol, string message)
    {
        try
        {
            var isConnected = _connectionChecker.VerifyInternetConnection();
            if (isConnected)
            {
                // Create a messaging utility based on the specified protocol type
                IMessagingUtils messagingUtils = _messagingFactory.CreateMessagingUtils(protocol);

                messagingUtils.SendMessage(Username, Password, To, message);

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Recieves message
    /// </summary>
    /// <param name="protocol">Protocol Type</param>
    /// <param name="message"></param>
    /// <exception cref="Exception"></exception>
    public void Receive(ProtocolTypes protocol, out string receivedMessage)
    {
        try
        {
            var isConnected = _connectionChecker.VerifyInternetConnection();
            if (isConnected)
            {
                IMessagingUtils messagingUtils = _messagingFactory.CreateMessagingUtils(protocol);

                receivedMessage = messagingUtils.Receive(Username, Password);

            }
            receivedMessage = "internet connection not estableshed";

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error receiving message: {ex.Message}");
            throw;
        }

    }
}
