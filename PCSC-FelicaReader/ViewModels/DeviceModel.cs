using Reactive.Bindings;
using System;
using System.ComponentModel;

namespace PCSC_FelicaReader.ViewModels
{
    public class DeviceModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // status
        public ReactiveProperty<bool> IsRunning { get; } = new ReactiveProperty<bool>(false);

        // message
        public ReactiveProperty<string> Message { get; } = new ReactiveProperty<string>("");

        public ReaderModel Reader { get; } = new ReaderModel();
        public CardModel Card { get; } = new CardModel();

        public void Dispose()
        {
        }
    }
}
