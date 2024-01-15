using Domains.Domains;

namespace Domains.ViewModels {
    public class PasswordListModel {
        public SearchModel SearchModel { get; set; }
        public IEnumerable<PasswordInfo> PasswordInfos { get; set; }
        public string ConfirmCode { get; set; }

    }
}
