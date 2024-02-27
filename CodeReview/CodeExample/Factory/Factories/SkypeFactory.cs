using CodeReview.CodeExample.Factory.Utils;

namespace CodeReview.CodeExample.Factory.Factories;

public class SkypeFactory : IMessagingFactory
{
    public IMessagingUtils CreateMessagingUtils(ProtocolTypes protocol)
    {
        if (protocol == ProtocolTypes.Skype)
        {
            return new SkypeUtilsEx();
        }
        else
        {
            throw new ArgumentException("Unsupported protocol type");
        }
    }
}
