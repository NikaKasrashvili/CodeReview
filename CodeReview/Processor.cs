using System.Net.NetworkInformation;

namespace CodeReview;

//SUGGESTION: As a good practice, it's better to introduce interface for processor class which will implement declared methods.
public class Processor
{
    //NOTE 1: "Global State" - The "CurrentUserNameSlashPassword" and "To" fields are static,
    //        which means they are shared among all instances of the class and can lead to some issues in multi-threaded environment.

    //Suggestion: Make those fields private 
    //            or just declare properties instead of fields with private setters and assign them into SET method.
    public static string CurrentUserNameSlashPassword;
    public static string To;  //"To" field is declared, but assignment doesn't happen anywhere. 
                              //Probably it should be assigned by Set method, that misses parameter for assigning this field.

    //NOTE 2: "Dependency Injection": instead of creating instances of skype and viber utils, 
    //         better will be using dependency injection to pass these dependencies externally.

    //SUGGESTION: Use factory method - detailed down.
    SkypeUtils skype = new SkypeUtils();
    viber_utils vbr = new viber_utils(); // in C# class shouldn't be created with snake casess.

    //Note: because of the reason mentioned in NOTE 1, it's better Set method not to be static.
    //       Also, insted of set method, fields can be assigned via constructor.
    public static void Set(string name, string pwd)
    {
        //NOTE 3: In utils method, CurrentUserNameSlashPassword is splitted, so instead of saving them in one field,
        //      it will be better them to be stored separatly.
        //      Also, there's a chance that username contains some slashes, so after spliting it may cause problems.
        CurrentUserNameSlashPassword = name + "/" + pwd;
    }

    //SUGGESTION: To avoid code dublication, we can simple utilize the factory method DP to send and receive messages.
    public void SendMessage(string protocol, string message)
    {
        //NOTE 4: Try-catch block is missing.

        //SUGGESTION : use connection verifying here.
        if (protocol.ToUpper() == "VIBER")
        {
            vbr.SendViber(CurrentUserNameSlashPassword, To, message);
        }
        else if (protocol.ToUpper() == "SKYPE")
        {
            skype.SendSkype(CurrentUserNameSlashPassword, To, message);
        }
    }
    public void Receive(string protocol, ref string message) //NOTE 5: as strings are already reference types and immutable ones,
                                                             // there's no need to use ref parameter. 
                                                             //SubNote: but if we want to return message and store them somewhere, we should use Out parameter
                                                             //instead of ref.
    {
        try
        {
            //NOTE 6: Spell check - there should be "skype" instead of skipe.
            //SUGGESTION : To avoid such type of errors, it's better to add enums for protocol types.
            //SUGGESTION : use connection verifying here.
            if (protocol.ToUpper() == "SKIPE")
            {
                message =
               skype.Skype_recv(CurrentUserNameSlashPassword).ToString();
            }
            else
            {
                message =
               viber.getMessage(CurrentUserNameSlashPassword).ToString();
            }
        }
        catch (Exception)
        {
            //NOTE 7: better practice is to handle exception (e.g., log the error, display a message to the user)
            // and re-throw the same exception only by throw to preserve original stack trace.
            throw new Exception("Processor Error!");
        }
    }
}


//SUGGESTION: As both skype and viber are messaging types and have same methods, it will be usefull to use Factory Method DP.
//            Also, if in future there will be need to add new messaging types, Factory method will be helpfull to add new classes 
//                  according to the Interfaces, to avoid missing some implementations.
public class SkypeUtils
{
    //NOTE 8: SRP is brtoken - SkypeUtils class shouldn't be responsible for verifying internet Conection.
    //SUGGESTION - create new class that will be responsible for verifying internet connection, and instead of using them in utils classes,
    // it can be injected into processor class - that will avoid code dublication.
    public void VerifyInternetConnection()
    {
        if (new Ping().Send("news.com").Status != IPStatus.Success) //NOTE 9: news.com should be stored as a variable and be private. 
        {
            throw new ArgumentException();      //Instead of throwing an exception, we can use another return type to check if internet connection 
                                                //is being established or not. Throwing exceptions are quite heavy operations.
        }
    }

    // NOTE 10: Naming Convention - in viberutils sendViber method, same argument is passed with the name of "str_usrNameAndPass",
    // so, for some developers it may be confusing. To avoid this, it will be better to use same 
    // argument name for both of them.
    public void SendSkype(string str_up, string str_to, string str_message)

    {
        VerifyInternetConnection();
        str_message = str_message + " from " + str_up.ToUpper(); // NOTE 11: We are sharing password of the User!!
        var parts = str_up.Split('/');   //Note: go to NOTE 3.
        SkypeApi.Send(str_to, str_message, parts[0], parts[1]); //NOTE 12: SkypeApi, that declares Send method doesn't exist in current context.
                                                                //We should use Dependency on this library.
    }

    //NOTE 13: as return type of SkypeApi.GetNextMessage() is string, Skype_recv() return type can be string too instead of object type.
    public object Skype_recv(string str_usrNameAndPass)
    {
        VerifyInternetConnection();
        var parts = str_usrNameAndPass.Split('/');
        string message = SkypeApi.GetNextMessage(parts[0], parts[1]); //Same as in NOTE 12.
        return message;
    }
}

// NOTE 14: ViberUtils class shouldn't inherit from SkypeUtils class, because Viber is not a derived class of Skybe, it's independent
//         If there's a need, both should be inhereted from some base class. 
// SubNote: Naming Convention - if we create class named SkypeUtils, it will be better if viber_utils class will be without
//                              snake and starting with upper case like "ViberUtils".
internal class viber_utils : SkypeUtils
{
    public void SendViber(string str_usrNameAndPass, string str_to, string
   str_message)
    {
        base.VerifyInternetConnection();
        var parts = str_usrNameAndPass.Split('/');
        ViberApi.Send(str_to, str_message, parts[0], parts[1]); //Note : go to NOTE 12
    }
    //Note: go to NOTE 13
    public object getMessage(string str_usrNameAndPass)
    {
        base.VerifyInternetConnection();
        var parts = str_usrNameAndPass.Split('/');
        string message = ViberApi.RecieveMessage(parts[0], parts[1]); //Same as Note above.
        return message;
    }
}
