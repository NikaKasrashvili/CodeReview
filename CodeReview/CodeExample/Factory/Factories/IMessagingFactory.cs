using CodeReview.CodeExample.Factory.Utils;

namespace CodeReview.CodeExample.Factory.Factories;

public interface IMessagingFactory
{
    IMessagingUtils CreateMessagingUtils(ProtocolTypes protocol);
}
