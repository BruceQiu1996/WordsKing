using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NAudio.Wave;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.Xml;

namespace WordsKing.ViewModels
{
    public class StudyWordPageViewModel : ObservableObject
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

        public AsyncRelayCommand PlayUKVoiceCommandAsync { get; set; }
        public AsyncRelayCommand PlayUSVoiceCommandAsync { get; set; }
        public RelayCommand NextCommand { get; set; }
        private List<WordModel> _words;
        private int _index = 0;
        public StudyWordPageViewModel()
        {

            PlayUKVoiceCommandAsync = new AsyncRelayCommand(async () =>
            {
                await PlayVoiceAsync(1);
            });

            PlayUSVoiceCommandAsync = new AsyncRelayCommand(async () =>
            {
                await PlayVoiceAsync(2);
            });

            NextCommand = new RelayCommand(Next);
        }

        public void Load(List<WordModel> words)
        {
            _words = words;
            _index = -1;
            Next();
        }

        private void Next()
        {
            _index++;
            if (_index < _words.Count)
            {
                var model = _words[_index];
                Word = model.HeadWord;
                if (model.Content?.Word?.Content?.Sentence?.Sentences != null)
                    Sentences = new ObservableCollection<Sentence>(model.Content?.Word?.Content?.Sentence?.Sentences);
                else
                    Sentences = null;

                if (model?.Content?.Word?.Content?.Phrase?.Phrases != null)
                    Phrases = new ObservableCollection<Phrase>(model.Content.Word.Content.Phrase.Phrases);
                else
                    Phrases = null;

                if (model?.Content?.Word?.Content?.Trans != null)
                    Trans = new ObservableCollection<Translation>(model.Content.Word.Content.Trans);
                else
                    Trans = null;
                Usphone = model.Content?.Word?.Content?.Usphone;
                Ukphone = model.Content?.Word?.Content?.Ukphone;
                WordDesc = $"{model.BookId}/{model.Content?.Word?.WordId}";
            }
        }

        private bool isPlay = false;

        private async Task PlayVoiceAsync(int type)
        {
            try
            {
                if (isPlay)
                    return;

                isPlay = true;
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(string.Format(Const.VoiceInterface, Word, type));
                    result.EnsureSuccessStatusCode();

                    using (Stream audioStream = await result.Content.ReadAsStreamAsync())
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await audioStream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        // 使用NAudio播放
                        using (var waveOut = new WaveOutEvent())
                        using (var reader = new Mp3FileReader(memoryStream))
                        {
                            waveOut.Init(reader);
                            waveOut.Play();

                            while (waveOut.PlaybackState == PlaybackState.Playing)
                            {
                                await Task.Delay(100);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                isPlay = false;
            }
        }
    }
}
