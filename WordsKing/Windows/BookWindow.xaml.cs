using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WordsKing.ViewModels;

namespace WordsKing.Windows
{
    /// <summary>
    /// BookWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BookWindow : Window
    {
        public BookWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetRequiredService<BookViewModel>();
        }

        public BookViewModel ViewModel => DataContext as BookViewModel;
    }
}
