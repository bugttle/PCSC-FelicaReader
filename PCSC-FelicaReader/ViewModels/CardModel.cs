using Reactive.Bindings;
using System;
using System.ComponentModel;

namespace PCSC_FelicaReader.ViewModels
{
    public class CardModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // IDm of the card
        public ReactiveProperty<string> IDm { get; } = new ReactiveProperty<string>("");

        public CardModel()
        {
        }

        public void Dispose()
        {
        }
    }
}
