using Domains.ViewModels;
using Servicies.PasswordGenerator.Validators;

namespace Servicies.PasswordGenerator {
    public class PasswordGenerator {
        private readonly PasswordGeneratorModel _model;
        private readonly Random _random;

        public PasswordGenerator(PasswordGeneratorModel model) {
            _model = model;
            _model.Password = "";
            _random = new Random();
        }

        public void Generate() {
            var validator = new Validator();
            for(int i = 0; i < _model.Length; i++) {
                var chars = validator.Validate(_model);
                if(chars.Length == 0) {
                    throw new Exception("Password is impossible");
                }
                _model.Password += chars[_random.Next(chars.Length)];
            }
        }
    }
}
