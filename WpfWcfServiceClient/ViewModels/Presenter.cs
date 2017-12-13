using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfWcfServiceClient.Models;

namespace WpfWcfServiceClient.ViewModels
{
    public class Presenter : ObservableObject
    {
        //private readonly TextConverter _textConverter = new TextConverter(s => Presenter.GetUserName()+ ": " + s);
        private string _someText;
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();

        public static string GetMessages()
        {
            SimpleService.SimpleServiceClient client = new SimpleService.SimpleServiceClient();
            var message = (client.GetMessage());
            return message.Result;
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

        public ICommand DisplayMessageCommand
        {
            get { return new DelegateCommand(DisplayMessage); }
        }       

        private void DisplayMessage()
        {
            if (string.IsNullOrWhiteSpace(SomeText)) return;
            AddToHistory(GetMessages()+ ": " +SomeText);
            SomeText = string.Empty;
        }

        private void AddToHistory(string item)
        {
            if (!_history.Contains(item))
                _history.Add(item);
        }
    }
}
