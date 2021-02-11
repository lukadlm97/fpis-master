using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRC.Data.Service
{
    public interface IUser
    {
        Task<string> LogIn(string username, string password);

        Task<bool> Register(string username, string password, string firstName, string lastName);
    }
}