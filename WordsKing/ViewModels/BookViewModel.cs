using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using WordsKing.Pages;

namespace WordsKing.ViewModels
{
    public class BookViewModel : ObservableObject
    {
        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        private List<WordModel> wordModels = new List<WordModel>();
        public AsyncRelayCommand LoadCommandAsync { get; set; }
        public BookListItemViewModel BookListItemViewModel { get; set; }
        public BookViewModel()
        {
            LoadCommandAsync = new AsyncRelayCommand(LoadAsync);

            WeakReferenceMessenger.Default.Register<BookViewModel, string, string>(this, Const.CONTINUE_STYDU, (x, y) =>
            {
                CurrentPage = App.ServiceProvider.GetRequiredService<StudyWordPage>();
                CurrentPage.DataContext = new CurrentWordViewModel(wordModels.First());
            });
        }

        public async Task LoadAsync()
        {
            IsRunning = true;
            try
            {
                string resourceName = $"WordsKing.WordsBook.{BookListItemViewModel.Id}.json";
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                using (JsonTextReader jReader = new JsonTextReader(reader))
                {
                    jReader.SupportMultipleContent = true;

                    while (jReader.Read())
                    {
                        if (jReader.TokenType == JsonToken.StartObject)
                        {
                            wordModels.Add(JObject.Load(jReader).ToObject<WordModel>());
                        }
                    }
                }

                CurrentPage = App.ServiceProvider.GetRequiredService<BookDetailPage>();
                CurrentPage.DataContext = new BookDetailPageViewModel(BookListItemViewModel);
            }
            finally 
            {
                IsRunning = false;
            }
        }
    }
}
