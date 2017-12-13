using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfWcfServiceClient.Models;

namespace WpfWcfServiceClient.ViewModels
{
    public class Presenter : ObservableObject
    {
        private readonly TextConverter _textConverter = new TextConverter(s => Presenter.GetUserName()+ ": " + s);
        private string _someText;
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();

        public static string GetUserName()
        {
            SimpleService.SimpleServiceClient client = new SimpleService.SimpleServiceClient();
            var user = (client.GetUserName());
            return user;
        }

        public string SomeText
        {
            get { return _someText; }
            set
            {
                _someText = value;
                RaisePropertyChangedEvent("SomeText");
            }
        }

        public IEnumerable<string> History
        {
            get { return _history; }
        }

        public ICommand ConvertTextCommand
        {
            get { return new DelegateCommand(ConvertText); }
        }

        private void ConvertText()
        {
            if (string.IsNullOrWhiteSpace(SomeText)) return;
            AddToHistory(_textConverter.ConvertText(SomeText));
            SomeText = string.Empty;
        }

        private void AddToHistory(string item)
        {
            if (!_history.Contains(item))
                _history.Add(item);
        }
    }
}
