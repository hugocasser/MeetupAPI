using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Meetup.Configuration;

public static class AuthConfiguration
{
    public static string Issuer { get; } = "modsen-meetup";
    public static string Audience { get; } = "meetup-user";
    private static string StaticKey { get; } = "TopSecretTokenTopSecretToken1234567890";
    public static DateTime Expires { get; set; } = DateTime.Now.AddDays(1);
    
    public static SymmetricSecurityKey GetSymmetricSecurityKeyStatic()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticKey));
    }
}