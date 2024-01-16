using Domains.ViewModels;

namespace Servicies.PasswordGenerator.Validators {
    public class Validator {
        private const string LOWERCASE = "abcdefghijklmnopqrstuvwxyz";
        private const string UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DIGITS = "0123456789";
        private const string SYMBOLS = "!@#$%^&*()_+-=[]{};':\"\\/?><,.";
        private const string EXCLUDE_SIMILAR = "iI1loO0";

        public string Validate(PasswordGeneratorModel model) {
            string result = "";
            if (model.UseLowercase) result += LOWERCASE;
            if (model.UseUppercase) result += UPPERCASE;
            if (model.UseDigits) result += DIGITS;
            if (model.UseSymbols) result += SYMBOLS;
            if (model.DontUseExcludeSimilar) {
                foreach (var symbol in EXCLUDE_SIMILAR){
                    result = result.Replace(symbol.ToString(), "");
                }
            }
            if (model.UseUnique) {
                foreach (var symbol in model.Password) {
                    result = result.Replace(symbol.ToString(), "");
                }
            }
            return result;
        }
    }
}
