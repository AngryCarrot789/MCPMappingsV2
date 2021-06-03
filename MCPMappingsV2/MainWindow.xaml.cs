using System.Windows;
using System.Windows.Input;
using MCPMappingsV2.Mappings.Controls;
using MCPMappingsV2.Windows;
using MCPMappingsV2.Windows.Search;
using REghZyFramework.Utilities;

namespace MCPMappingsV2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainViewScroller, BaseView<MainViewModel> {
        public MainViewModel Model {
            get => this.DataContext as MainViewModel;
            set => this.DataContext = value;
        }

        public GlobalSearchWindow SearchWindow { get; }

        public MainWindow() {
            InitializeComponent();
            this.Model = new MainViewModel(this);
            this.SearchWindow = new GlobalSearchWindow(new GlobalSearchViewModel(this.Model));
        }

        public void ScrollClassToBottom() {

        }

        public void ScrollClassToSelected() {
            this.ClassListView.ScrollIntoView(this.ClassListView.SelectedItem);
        }

        public void ScrollFieldToBottom() {

        }

        public void ScrollFieldToSelected() {
            this.FieldListView.ScrollIntoView(this.FieldListView.SelectedItem);
        }

        public void ScrollMethodToBottom() {

        }

        public void ScrollMethodToSelected() {
            this.MethodListView.ScrollIntoView(this.MethodListView.SelectedItem);
        }

        // cant be bothered to do binding for this :(
        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            this.SearchWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Application.Current.Shutdown(0);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.F) {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control) && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift)) {
                    this.SearchWindow.Show();
                }
            }
        }
    }
}
