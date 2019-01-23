using System;
using System.Runtime.InteropServices;
using static SmartCard.WinSCard;
using static SmartCard.WinSCard.Constants;
using static SmartCard.WinSCard.NativeMethods;

namespace SmartCard
{
    public class FelicaReader : IDisposable
    {
        // reference: "SDK for NFC Starter Kit" sample
        const int PCSC_RESP_BUFF_LEN = 262;

        internal SCARD_READERSTATE ReaderState { get; set; }

        FelicaCard card = null;

        internal bool IsPresent
        {
            get { return ((State)ReaderState.dwEventState & State.SCARD_STATE_PRESENT) != 0; }
        }

        internal bool IsEmpty
        {
            get { return ((State)ReaderState.dwEventState & State.SCARD_STATE_EMPTY) != 0; }
        }

        internal bool IsInUsed
        {
            get { return ((State)ReaderState.dwEventState & State.SCARD_STATE_INUSE) != 0; }
        }

        internal FelicaReader(string name)
        {
            ReaderState = new SCARD_READERSTATE()
            {
                szReader = name,
                dwCurrentState = (uint)State.SCARD_STATE_UNAWARE,
            };
        }

        internal void UpdateCurrentState()
        {
            var state = ReaderState;
            state.dwCurrentState = state.dwEventState; // change the current state
            ReaderState = state;
        }

        internal FelicaCard Connect(IntPtr context)
        {
            card = Connect(context, ReaderState.szReader, Share.SCARD_SHARE_SHARED, Protocol.SCARD_PROTOCOL_Tx);
            return card;
        }

        internal void Disconnect()
        {
            if (card != null)
            {
                Disconnect(card.Card, Disposition.SCARD_LEAVE_CARD);
                card = null;
            }
        }

        public string ReadSerialNumber()
        {
            var inBuffer = new byte[] {
                (byte)PaSoRi.Command.ESC_CMD_GET_INFO,
                0x08 // Product Serial Number
            };

            var result = Control(card.Card, (int)PaSoRi.Contrl.SCARD_CTL_CODE, inBuffer);
            if (result == null)
            {
                return null;
            }
            return Utils.ByteTextToString(result.OutBuffer, (int)result.BytesReturned, includedNull: true);
        }

        #region IDisposable Support

        bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Disconnect();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support

        #region WinSCard API

        FelicaCard Connect(IntPtr context, string readerName, Share share, Protocol protocol)
        {
            var ret = (ReturnCode)NativeMethods.SCardConnect(
                context,
                readerName,
                (uint)share,
                (uint)protocol,
                out IntPtr card,
                out uint activeProtocol);
            if (ret != ReturnCode.SCARD_S_SUCCESS)
            {
                throw new ApplicationException("Failed to execute the SCardConnect: Returned value = " + ret);
            }
            return new FelicaCard(card, (Protocol)activeProtocol);
        }

        void Disconnect(IntPtr card, Disposition disconection)
        {
            if (card != IntPtr.Zero)
            {
                NativeMethods.SCardDisconnect(card, (uint)disconection);
            }
        }

        dynamic Control(IntPtr card, uint controlCode, byte[] inBuffer)
        {
            var outBuffer = new byte[PCSC_RESP_BUFF_LEN];

            var ret = (ReturnCode)NativeMethods.SCardControl(
                card,
                controlCode,
                inBuffer,
                (uint)inBuffer.Length,
                ref outBuffer[0],
                (uint)outBuffer.Length,
                out uint bytesReturned);
            if (ret != ReturnCode.SCARD_S_SUCCESS)
            {
                throw new ApplicationException("Failed to execute the SCardControl: Returned value = " + ret);
            }
            return new
            {
                OutBuffer = outBuffer,
                BytesReturned = bytesReturned,
            };
        }

        #endregion WinSCard API
    }
}
