namespace CodeReview.CodeExample;

public interface IProcessor
{
    void SendMessage(ProtocolTypes protocol, string message);
    void Receive(ProtocolTypes protocol, out string receivedMessage);
}
