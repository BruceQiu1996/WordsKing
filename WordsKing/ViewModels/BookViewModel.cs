using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace WordsKing.ViewModels
{
    public class BookViewModel : ObservableObject
    {
        private BookListItemViewModel _bookListItemViewModel;
        public BookListItemViewModel BookListItemViewModel
        {
            get { return _bookListItemViewModel; }
            set => SetProperty(ref _bookListItemViewModel, value);
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public AsyncRelayCommand LoadCommandAsync { get; set; }
        public BookViewModel()
        {
            LoadCommandAsync = new AsyncRelayCommand(LoadAsync);
        }

        public async Task LoadAsync()
        {
            IsRunning = true;
            try
            {
                var array = new List<WordModel>();
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
                            array.Add(JObject.Load(jReader).ToObject<WordModel>());
                        }
                    }
                }
            }
            finally 
            {
                IsRunning = false;
            }
        }
    }
}
