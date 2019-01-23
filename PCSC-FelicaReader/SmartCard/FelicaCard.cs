using System;
using System.Linq;
using static SmartCard.WinSCard;
using static SmartCard.WinSCard.Constants;

namespace SmartCard
{
    public class FelicaCard
    {
        const int SW_LENGTH = 2;

        // reference: "SDK for NFC Starter Kit" sample
        const int PCSC_RECV_BUFF_LEN = 262;

        internal IntPtr Card { set; get; }
        internal Protocol Protocol { set; get; }

        internal FelicaCard(IntPtr card, Protocol protocol)
        {
            Card = card;
            Protocol = protocol;
        }

        public string GetIDm()
        {
            var data = GetData(0x00);
            return BitConverter.ToString(data);
        }

        byte[] GetData(byte p1)
        {
            // 3.2.6. 独自定義(拡張)コマンド 独自コマンド定義により、APDU で FeliCa
            byte CLA = 0xFF;
            byte INS = 0xCA;
            byte P2 = 0x00;
            byte LE = 0x00;
            return SendCommand(new byte[] { CLA, INS, p1, P2, LE });
        }

        internal byte[] SendCommand(byte[] command)
        {
            return Transmit(Card, Protocol, command);
        }

        #region WinSCard API

        byte[] Transmit(IntPtr card, Protocol protocol, byte[] sendBuffer)
        {
            var recvBuffer = new byte[PCSC_RECV_BUFF_LEN];
            var recvLength = (uint)recvBuffer.Length;

            var ret = (ReturnCode)NativeMethods.SCardTransmit(
                card,
                NativeMethods.CardProtocol2PCI(protocol),
                sendBuffer,
                (uint)sendBuffer.Length,
                IntPtr.Zero,
                ref recvBuffer[0],
                out recvLength);
            if (ret != ReturnCode.SCARD_S_SUCCESS)
            {
                throw new ApplicationException("Failed to execute the SCardTransmit: Returned value = " + ret);
            }

            var sw = new byte[SW_LENGTH];
            Array.Copy(recvBuffer, recvLength - SW_LENGTH, sw, 0, SW_LENGTH);

            if (sw[0] == 0x90 && sw[1] == 0x00)
            {
                // Success
                return recvBuffer.Take((int)recvLength - SW_LENGTH).ToArray();
            }

            return null;
        }

        #endregion WinSCard API
    }
}
