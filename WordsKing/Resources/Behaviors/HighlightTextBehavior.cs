using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WordsKing.Resources.Behaviors
{
    public class HighlightTextBehavior : Behavior<TextBlock>
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HighlightTextBehavior),
            new PropertyMetadata(null, OnTextChanged));

        public static readonly DependencyProperty HighlightWordProperty =
            DependencyProperty.Register("HighlightWord", typeof(string), typeof(HighlightTextBehavior),
            new PropertyMetadata(null, OnTextChanged));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string HighlightWord
        {
            get => (string)GetValue(HighlightWordProperty);
            set => SetValue(HighlightWordProperty, value);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (HighlightTextBehavior)d;
            behavior.UpdateText();
        }

        private void UpdateText()
        {
            if (AssociatedObject == null || string.IsNullOrEmpty(Text))
                return;

            AssociatedObject.Inlines.Clear();

            if (string.IsNullOrEmpty(HighlightWord))
            {
                AssociatedObject.Inlines.Add(new Run(Text));
                return;
            }

            int index = Text.IndexOf(HighlightWord, StringComparison.OrdinalIgnoreCase);
            if (index < 0)
            {
                AssociatedObject.Inlines.Add(new Run(Text));
                return;
            }

            AssociatedObject.Inlines.Add(new Run(Text.Substring(0, index)));
            AssociatedObject.Inlines.Add(new Run(Text.Substring(index, HighlightWord.Length))
            {
                Foreground = Brushes.Red,
                FontWeight = FontWeights.Bold
            });
            AssociatedObject.Inlines.Add(new Run(Text.Substring(index + HighlightWord.Length)));
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            UpdateText();
        }
    }
}
