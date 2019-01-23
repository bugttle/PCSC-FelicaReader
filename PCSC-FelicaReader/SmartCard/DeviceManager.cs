using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static SmartCard.WinSCard;
using static SmartCard.WinSCard.Constants;

namespace SmartCard
{
    public class DeviceManager : IDisposable
    {
        IntPtr context = IntPtr.Zero;
        FelicaReader[] readers = null;
        readonly object lockObject = new object();
        Thread thread = null;

        public bool IsRunning { get; private set; }

        public IEnumerable<FelicaReader> Readers
        {
            get { return readers; }
        }

        public Action<bool> OnStatusChange = null;
        public Action<Exception> OnError = null;
        public Action<FelicaReader, FelicaCard> OnCardPresented = null;
        public Action<FelicaReader> OnCardRemoved = null;

        bool IsEstablished
        {
            get { return context != IntPtr.Zero; }
        }

        public DeviceManager()
        {
        }

        public void Start()
        {
            lock (lockObject)
            {
                thread = new Thread(new ThreadStart(StartPollingOnThread));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }

        void StartPollingOnThread()
        {
            IsRunning = true;
            OnStatusChange?.Invoke(IsRunning);

            try
            {
                context = EstablishContext(Scope.SCARD_SCOPE_USER);
                readers = ListReaders();
            }
            catch (Exception e)
            {
                OnError?.Invoke(e);
            }

            if (readers.Length != 0)
            {
                InitializeReaderStates();

                while (true)
                {
                    if (GetStatusChange())
                    {
                        foreach (var reader in readers)
                        {
                            if (reader.IsPresent && !reader.IsInUsed)
                            {
                                try
                                {
                                    var card = reader.Connect(context);
                                    OnCardPresented?.Invoke(reader, card);
                                }
                                catch (Exception e)
                                {
                                    OnError?.Invoke(e);
                                }
                            }
                            if (reader.IsEmpty)
                            {
                                OnCardRemoved?.Invoke(reader);
                            }
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            lock (lockObject)
            {
                if (thread != null)
                {
                    try
                    {
                        thread.Abort();
                        thread.Join();
                    }
                    catch (Exception) { }
                    thread = null;
                }

                if (readers != null)
                {
                    foreach (var reader in readers)
                    {
                        reader.Disconnect();
                    }
                }
                Cancel();
                ReleaseContext();

                IsRunning = false;
                OnStatusChange?.Invoke(IsRunning);
            }
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        void InitializeReaderStates()
        {
            GetStatusChange(0, ref readers);
        }

        bool GetStatusChange(uint timeout = 1000)
        {
            foreach (var reader in readers)
            {
                reader.UpdateCurrentState();
            }

            return GetStatusChange(timeout, ref readers);
        }

        #region IDisposable Support

        bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                }

                readers = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support

        #region WinSCard API

        IntPtr EstablishContext(Scope scope)
        {
            var context = IntPtr.Zero;
            if (!IsEstablished)
            {
                var ret = (ReturnCode)NativeMethods.SCardEstablishContext(
                    (uint)scope,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    out context);
                if (ret != ReturnCode.SCARD_S_SUCCESS)
                {
                    throw new ApplicationException("Failed to execute the SCardEstablishContext: Returned value = " + ret);
                }
            }
            return context;
        }

        void ReleaseContext()
        {
            if (IsEstablished)
            {
                var ret = NativeMethods.SCardReleaseContext(context);
                context = IntPtr.Zero;
            }
        }

        unsafe FelicaReader[] ListReaders()
        {
            var readersCount = (uint)SCARD_AUTOALLOCATE; // auto alocate
            var ret = (ReturnCode)NativeMethods.SCardListReaders(
                context,
                null,
                out byte* cReaders,
                ref readersCount);
            if (ret != ReturnCode.SCARD_S_SUCCESS)
            {
                switch (ret)
                {
                    case ReturnCode.SCARD_E_NO_READERS_AVAILABLE:
                        break;

                    default:
                        break;
                }
                return new FelicaReader[0];
            }

            var readers = Utils.SplitStringByNull(cReaders);
            NativeMethods.SCardFreeMemory(context, cReaders); // free

            return readers.Select(reader => new FelicaReader(reader)).ToArray();
        }

        bool GetStatusChange(uint timeout, ref FelicaReader[] readers)
        {
            var states = readers.Select(reader => reader.ReaderState).ToArray();

            var ret = (ReturnCode)NativeMethods.SCardGetStatusChange(
                context,
                timeout,
                states,
                (uint)states.Length);
            if (ret != ReturnCode.SCARD_S_SUCCESS)
            {
                return false;
            }

            for (int i = 0, len = readers.Length; i < len; ++i)
            {
                readers[i].ReaderState = states[i];
            }

            return true;
        }

        void Cancel()
        {
            if (IsEstablished)
            {
                NativeMethods.SCardCancel(context);
            }
        }

        #endregion WinSCard API
    }
}
