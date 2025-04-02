using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace WordsKing.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private ObservableCollection<BookListItemViewModel> _bookItems;
        public ObservableCollection<BookListItemViewModel> BookItems
        {
            get => _bookItems;
            set => SetProperty(ref _bookItems, value);
        }

        public AsyncRelayCommand LoadCommandAsync { get; set; }
        public MainWindowViewModel()
        {
            BookItems = new ObservableCollection<BookListItemViewModel>();
            LoadCommandAsync = new AsyncRelayCommand(LoadAsync);
        }

        private async Task LoadAsync()
        {
            BookItems.Clear();
            await LoadBookItemsAsync();
        }

        public async Task LoadBookItemsAsync() 
        {
            var assembly = Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames().Where(x=>x.StartsWith("WordsKing.WordsBook"));

            string resourceName = "WordsKing.WordsBook.bookLists.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var obj = JObject.Parse(await reader.ReadToEndAsync());
                var array = obj["data"]["normalBooksInfo"].ToArray();

                foreach (var name in names) 
                {
                    if (name != resourceName) 
                    {
                        var item = array.FirstOrDefault(x => x["id"].ToString() == 
                            Path.GetFileNameWithoutExtension(name.Replace("WordsKing.WordsBook.",string.Empty)));
                        BookItems.Add(new BookListItemViewModel()
                        {
                            Id = item["id"].ToString(),
                            Title = item["title"].ToString(),
                            Introduce = item["introduce"].ToString(),
                            WordNum = item["wordNum"].Value<int>(),
                            Cover = item["cover"].ToString(),
                            Tags = item["tags"].ToArray().Select(x=>new Tag(x["tagName"].ToString())),
                        });
                    }
                }
            }
        }
    }
}
