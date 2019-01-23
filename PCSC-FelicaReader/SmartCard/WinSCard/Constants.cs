﻿using System;
using System.ComponentModel;

namespace SmartCard
{
    internal partial class WinSCard
    {
        internal static class Constants
        {
            /* Special value */
            internal static readonly uint SCARD_AUTOALLOCATE = 0xFFFFFFFF; // -1

            internal enum Disposition : uint
            {
                SCARD_LEAVE_CARD = 0,
                SCARD_RESET_CARD = 1,
                SCARD_UNPOWER_CARD = 2,
                SCARD_EJECT_CARD = 3,
            }

            /// <summary>
            /// https://msdn.microsoft.com/en-us/library/cc242804.aspx
            /// </summary>
            internal enum Scope : uint
            {
                SCARD_SCOPE_USER = 0x00000000,
                SCARD_SCOPE_TERMINAL = 0x00000001,
                SCARD_SCOPE_SYSTEM = 0x00000002,
            }

            /// <summary>
            /// https://msdn.microsoft.com/en-us/library/cc242849.aspx
            /// </summary>
            internal enum Share : uint
            {
                SCARD_SHARE_EXCLUSIVE = 0x00000001,
                SCARD_SHARE_SHARED = 0x00000002,
                SCARD_SHARE_DIRECT = 0x00000003,
            }

            /// <summary>
            /// https://msdn.microsoft.com/en-us/library/cc242850.aspx
            /// </summary>
            [Flags]
            internal enum State : uint
            {
                SCARD_STATE_UNAWARE = 0x0000,
                SCARD_STATE_IGNORE = 0x0001,
                SCARD_STATE_CHANGED = 0x0002,
                SCARD_STATE_UNKNOWN = 0x0004,
                SCARD_STATE_UNAVAILABLE = 0x0008,
                SCARD_STATE_EMPTY = 0x0010,
                SCARD_STATE_PRESENT = 0x0020,
                SCARD_STATE_ATRMATCH = 0x0040,
                SCARD_STATE_EXCLUSIVE = 0x0080,
                SCARD_STATE_INUSE = 0x0100,
                SCARD_STATE_MUTE = 0x0200,
                SCARD_STATE_UNPOWERED = 0x0400,
            }

            /// <summary>
            /// https://msdn.microsoft.com/en-us/library/cc242848.aspx
            /// </summary>
            internal enum Protocol : uint
            {
                SCARD_PROTOCOL_UNDEFINED = 0x00000000,
                SCARD_PROTOCOL_T0 = 0x00000001,
                SCARD_PROTOCOL_T1 = 0x00000002,
                SCARD_PROTOCOL_Tx = 0x00000003,
                SCARD_PROTOCOL_RAW = 0x00010000,
            }

            /// <summary>
            /// https://msdn.microsoft.com/en-us/library/cc242851.aspx
            /// </summary>
            internal enum ReturnCode : uint
            {
                [Description("No error was encountered.")]
                SCARD_S_SUCCESS = 0,

                [Description("The client attempted a smart card operation in a remote session, such as a client session running on a terminal server, and the operating system in use does not support smart card redirection.")]
                ERROR_BROKEN_PIPE = 0x00000109,

                [Description("An error occurred in setting the smart card file object pointer.")]
                SCARD_E_BAD_SEEK = 0x80100029,

                [Description("The action was canceled by an SCardCancel request.")]
                SCARD_E_CANCELLED = 0x80100002,

                [Description("The system could not dispose of the media in the requested manner.")]
                SCARD_E_CANT_DISPOSE = 0x8010000E,

                [Description("The smart card does not meet minimal requirements for support.")]
                SCARD_E_CARD_UNSUPPORTED = 0x8010001C,

                [Description("The requested certificate could not be obtained.")]
                SCARD_E_CERTIFICATE_UNAVAILABLE = 0x8010002D,

                [Description("A communications error with the smart card has been detected.")]
                SCARD_E_COMM_DATA_LOST = 0x8010002F,

                [Description("The specified directory does not exist in the smart card.")]
                SCARD_E_DIR_NOT_FOUND = 0x80100023,

                [Description("The reader driver did not produce a unique reader name.")]
                SCARD_E_DUPLICATE_READER = 0x8010001B,

                [Description("The specified file does not exist in the smart card.")]
                SCARD_E_FILE_NOT_FOUND = 0x80100024,

                [Description("The requested order of object creation is not supported.")]
                SCARD_E_ICC_CREATEORDER = 0x80100021,

                [Description("No primary provider can be found for the smart card.")]
                SCARD_E_ICC_INSTALLATION = 0x80100020,

                [Description("The data buffer for returned data is too small for the returned data.")]
                SCARD_E_INSUFFICIENT_BUFFER = 0x80100008,

                [Description("An ATR string obtained from the registry is not a valid ATR string.")]
                SCARD_E_INVALID_ATR = 0x80100015,

                [Description("The supplied PIN is incorrect.")]
                SCARD_E_INVALID_CHV = 0x8010002A,

                [Description("The supplied handle was not valid.")]
                SCARD_E_INVALID_HANDLE = 0x80100003,

                [Description("One or more of the supplied parameters could not be properly interpreted.")]
                SCARD_E_INVALID_PARAMETER = 0x80100004,

                [Description("Registry startup information is missing or not valid.")]
                SCARD_E_INVALID_TARGET = 0x80100005,

                [Description("One or more of the supplied parameter values could not be properly interpreted.")]
                SCARD_E_INVALID_VALUE = 0x80100011,

                [Description("Access is denied to the file.")]
                SCARD_E_NO_ACCESS = 0x80100027,

                [Description("The supplied path does not represent a smart card directory.")]
                SCARD_E_NO_DIR = 0x80100025,

                [Description("The supplied path does not represent a smart card file.")]
                SCARD_E_NO_FILE = 0x80100026,

                [Description("The requested key container does not exist on the smart card.")]
                SCARD_E_NO_KEY_CONTAINER = 0x80100030,

                [Description("Not enough memory available to complete this command.")]
                SCARD_E_NO_MEMORY = 0x80100006,

                [Description("The smart card PIN cannot be cached.Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This error code is not available.")]
                SCARD_E_NO_PIN_CACHE = 0x80100033,

                [Description("No smart card reader is available.")]
                SCARD_E_NO_READERS_AVAILABLE = 0x8010002E,

                [Description("The smart card resource manager is not running.")]
                SCARD_E_NO_SERVICE = 0x8010001D,

                [Description("The operation requires a smart card, but no smart card is currently in the device.")]
                SCARD_E_NO_SMARTCARD = 0x8010000C,

                [Description("The requested certificate does not exist.")]
                SCARD_E_NO_SUCH_CERTIFICATE = 0x8010002C,

                [Description("The reader or card is not ready to accept commands.")]
                SCARD_E_NOT_READY = 0x80100010,

                [Description("An attempt was made to end a nonexistent transaction.")]
                SCARD_E_NOT_TRANSACTED = 0x80100016,

                [Description("The PCI receive buffer was too small.")]
                SCARD_E_PCI_TOO_SMALL = 0x80100019,

                [Description("The smart card PIN cache has expired.Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This error code is not available.")]
                SCARD_E_PIN_CACHE_EXPIRED = 0x80100032,

                [Description("The requested protocols are incompatible with the protocol currently in use with the card.")]
                SCARD_E_PROTO_MISMATCH = 0x8010000F,

                [Description("The smart card is read-only and cannot be written to.Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP: This error code is not available.")]
                SCARD_E_READ_ONLY_CARD = 0x80100034,

                [Description("The specified reader is not currently available for use.")]
                SCARD_E_READER_UNAVAILABLE = 0x80100017,

                [Description("The reader driver does not meet minimal requirements for support.")]
                SCARD_E_READER_UNSUPPORTED = 0x8010001A,

                [Description("The smart card resource manager is too busy to complete this operation.")]
                SCARD_E_SERVER_TOO_BUSY = 0x80100031,

                [Description("The smart card resource manager has shut down.")]
                SCARD_E_SERVICE_STOPPED = 0x8010001E,

                [Description("The smart card cannot be accessed because of other outstanding connections.")]
                SCARD_E_SHARING_VIOLATION = 0x8010000B,

                [Description("The action was canceled by the system, presumably to log off or shut down.")]
                SCARD_E_SYSTEM_CANCELLED = 0x80100012,

                [Description("The user-specified time-out value has expired.")]
                SCARD_E_TIMEOUT = 0x8010000A,

                [Description("An unexpected card error has occurred.")]
                SCARD_E_UNEXPECTED = 0x8010001F,

                [Description("The specified smart card name is not recognized.")]
                SCARD_E_UNKNOWN_CARD = 0x8010000D,

                [Description("The specified reader name is not recognized.")]
                SCARD_E_UNKNOWN_READER = 0x80100009,

                [Description("An unrecognized error code was returned.")]
                SCARD_E_UNKNOWN_RES_MNG = 0x8010002B,

                [Description("This smart card does not support the requested feature.")]
                SCARD_E_UNSUPPORTED_FEATURE = 0x80100022,

                [Description("An attempt was made to write more data than would fit in the target object.")]
                SCARD_E_WRITE_TOO_MANY = 0x80100028,

                [Description("An internal communications error has been detected.")]
                SCARD_F_COMM_ERROR = 0x80100013,

                [Description("An internal consistency check failed.")]
                SCARD_F_INTERNAL_ERROR = 0x80100001,

                [Description("An internal error has been detected, but the source is unknown.")]
                SCARD_F_UNKNOWN_ERROR = 0x80100014,

                [Description("An internal consistency timer has expired.")]
                SCARD_F_WAITED_TOO_LONG = 0x80100007,

                [Description("The operation has been aborted to allow the server application to exit.")]
                SCARD_P_SHUTDOWN = 0x80100018,

                [Description("The action was canceled by the user.")]
                SCARD_W_CANCELLED_BY_USER = 0x8010006E,

                [Description("The requested item could not be found in the cache.")]
                SCARD_W_CACHE_ITEM_NOT_FOUND = 0x80100070,

                [Description("The requested cache item is too old and was deleted from the cache.")]
                SCARD_W_CACHE_ITEM_STALE = 0x80100071,

                [Description("The new cache item exceeds the maximum per-item size defined for the cache.")]
                SCARD_W_CACHE_ITEM_TOO_BIG = 0x80100072,

                [Description("No PIN was presented to the smart card.")]
                SCARD_W_CARD_NOT_AUTHENTICATED = 0x8010006F,

                [Description("The card cannot be accessed because the maximum number of PIN entry attempts has been reached.")]
                SCARD_W_CHV_BLOCKED = 0x8010006C,

                [Description("The end of the smart card file has been reached.")]
                SCARD_W_EOF = 0x8010006D,

                [Description("The smart card has been removed, so further communication is not possible.")]
                SCARD_W_REMOVED_CARD = 0x80100069,

                [Description("The smart card was reset.")]
                SCARD_W_RESET_CARD = 0x80100068,

                [Description("Access was denied because of a security violation.")]
                SCARD_W_SECURITY_VIOLATION = 0x8010006A,

                [Description("Power has been removed from the smart card, so that further communication is not possible.")]
                SCARD_W_UNPOWERED_CARD = 0x80100067,

                [Description("The smart card is not responding to a reset.")]
                SCARD_W_UNRESPONSIVE_CARD = 0x80100066,

                [Description("The reader cannot communicate with the card, due to ATR string configuration conflicts.")]
                SCARD_W_UNSUPPORTED_CARD = 0x80100065,

                [Description("The card cannot be accessed because the wrong PIN was presented.")]
                SCARD_W_WRONG_CHV = 0x8010006B,
            }

            /// <summary>
            /// SDK for NFC ユーザーズマニュアル PC/SC編（Lite）
            /// https://www.sony.co.jp/Products/felica/business/products/ICS-D004.html
            /// </summary>
            internal static class PaSoRi
            {
                internal enum Contrl : uint
                {
                    SCARD_CTL_CODE = 0x003136b0,
                }

                internal enum Command : byte
                {
                    ESC_CMD_GET_INFO = 0xC0,
                    ESC_CMD_SET_OPTION = 0xC1,
                    ESC_CMD_TARGET_COMM = 0xC4,
                    ESC_CMD_SNEP = 0xC6,
                    ESC_CMD_APDU_WRAP = 0xFF,
                }
            }
        }
    }
}
