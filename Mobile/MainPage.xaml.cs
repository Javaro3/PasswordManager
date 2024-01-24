using Repository.Repositories;

namespace Mobile {
    public partial class MainPage : ContentPage {
        int count = 0;
        private readonly UserRepository _userRepository;

        public MainPage(UserRepository userRepository) {
            InitializeComponent();
            _userRepository = userRepository;
        }

        private void OnCounterClicked(object sender, EventArgs e) {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} time";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
