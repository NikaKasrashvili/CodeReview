using CodeReview.CodeExample.Factory.Utils;

namespace CodeReview.CodeExample.Factory.Factories;

public class ViberFactory : IMessagingFactory
{
    public IMessagingUtils CreateMessagingUtils(ProtocolTypes protocol)
    {
        if (protocol == ProtocolTypes.Viber)
        {
            return new ViberUtilsEx();
        }
        else
        {
            throw new ArgumentException("Unsupported protocol type");
        }
    }
}
