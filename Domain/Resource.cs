using System.ComponentModel;

namespace Domain
{
    public record Resource : AbstractEntity
    {
    }

    public enum Resources
    {
        [Description("Web Interface")]
        WebInterface = 100,
        [Description("Active Directory")]
        ActiveDirectory = 200,
        [Description("System Services")]
        SystemServices = 300,
        [Description("Ip Telephony Users")]
        IpTelephonyUsers = 400,
        [Description("CUCM Integration")]
        CUCMIntegration = 500,
    }
}
