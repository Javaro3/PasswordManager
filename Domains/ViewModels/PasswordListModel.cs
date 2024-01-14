using Domains.Domains;

namespace Domains.ViewModels {
    public class PasswordListModel {
        public IEnumerable<PasswordInfo> PasswordInfos { get; set; }
        public string ConfirmCode { get; set; }
    }
}
