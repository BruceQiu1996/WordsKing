using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace WordsKing.ViewModels
{
    public class BookDetailPageViewModel : ObservableObject
    {
        private BookListItemViewModel _bookListItemViewModel;
        public BookListItemViewModel BookListItemViewModel
        {
            get { return _bookListItemViewModel; }
            set => SetProperty(ref _bookListItemViewModel, value);
        }

        public RelayCommand ContinueStudyCommand { get; set; }

        public BookDetailPageViewModel(BookListItemViewModel viewModel)
        {
            BookListItemViewModel = viewModel;
            ContinueStudyCommand = new RelayCommand(() =>
                {
                    WeakReferenceMessenger.Default.Send("Study", Const.CONTINUE_STYDU);
                });
        }
    }
}
