using System.Windows;
using System.Windows.Input;
using REghZyFramework.Utilities;

namespace MCPMappingsV2.Windows.Search {
    /// <summary>
    /// Interaction logic for GlobalSearchWindow.xaml
    /// </summary>
    public partial class GlobalSearchWindow : Window, BaseView<GlobalSearchViewModel> {
        public GlobalSearchViewModel Model { 
            get => this.DataContext as GlobalSearchViewModel; 
            set => this.DataContext = value;
        }


        public GlobalSearchWindow(GlobalSearchViewModel search) {
            InitializeComponent();
            this.Model = search;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                this.Hide();
            }
        }
    }
}
