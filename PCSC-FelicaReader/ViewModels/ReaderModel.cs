using Reactive.Bindings;
using System;
using System.ComponentModel;

namespace PCSC_FelicaReader.ViewModels
{
    public class ReaderModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Serial Number of the reader
        public ReactiveProperty<string> SerialNumber { get; } = new ReactiveProperty<string>("");

        public ReaderModel()
        {
        }

        public void Dispose()
        {
        }
    }
}
