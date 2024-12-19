using System.Linq;

namespace VentSystems
{
    public static class PasswordChecker
    {

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            var hasUpperCase = password.Any(char.IsUpper);
            if (!hasUpperCase)
            {
                return false;
            }

            var hasLowerCase = password.Any(char.IsLower);
            if (!hasLowerCase)
            {
                return false;
            }

            var hasDigit = password.Any(char.IsDigit);
            if (!hasDigit)
            {
                return false;
            }
            
            return password.Any(c => !char.IsLetterOrDigit(c));
        }
        
    }
}