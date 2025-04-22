using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace WordsKing.ViewModels
{
    public class CurrentWordViewModel : ObservableObject
    {
        private string _word;
        public string Word
        {
            get => _word;
            set => SetProperty(ref _word, value);
        }

        private ObservableCollection<Sentence> _sentences;
        public ObservableCollection<Sentence> Sentences
        {
            get => _sentences;
            set => SetProperty(ref _sentences, value);
        }

        private ObservableCollection<Phrase> _phrases;
        public ObservableCollection<Phrase> Phrases
        {
            get => _phrases;
            set => SetProperty(ref _phrases, value);
        }

        private ObservableCollection<Translation> _trans;
        public ObservableCollection<Translation> Trans
        {
            get => _trans;
            set => SetProperty(ref _trans, value);
        }


        /// <summary>
        /// 美式发音
        /// </summary>
        private string _usphone;
        public string Usphone
        {
            get => _usphone;
            set => SetProperty(ref _usphone, value);
        }

        /// <summary>
        /// 英式发音
        /// </summary>
        private string _ukphone;
        public string Ukphone
        {
            get => _ukphone;
            set => SetProperty(ref _ukphone, value);
        }

        private string _wordDesc;
        public string WordDesc
        {
            get => _wordDesc;
            set => SetProperty(ref _wordDesc, value);
        }

        private bool _passed;
        public bool Passed
        {
            get => _passed;
            set => SetProperty(ref _passed, value);
        }

        private bool _like;
        public bool Like
        {
            get => _like;
            set => SetProperty(ref _like, value);
        }
        public CurrentWordViewModel(WordModel model)
        {
            Word = model.HeadWord;
            Sentences = new ObservableCollection<Sentence>(model.Content?.Word?.Content?.Sentence?.Sentences);
            Phrases = new ObservableCollection<Phrase>(model.Content.Word.Content.Phrase.Phrases);
            Trans = new ObservableCollection<Translation>(model.Content.Word.Content.Trans);
            Usphone = model.Content.Word.Content.Usphone;
            Ukphone = model.Content.Word.Content.Ukphone;
            WordDesc = $"{model.BookId}/{model.Content.Word.WordId}";
        }
    }
}
