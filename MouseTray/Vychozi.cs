using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseTray
{
    class Vychozi
    {
        public const UInt32 SPI_GETMOUSESPEED = 0x0070;
        public const UInt32 SPI_GETWHEELSCROLLLINES = 0x0068;
        public static uint vychoziSens;
        public static uint vychoziScroll;
        public static uint vychoziDoubleClick;
        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, ref UInt32 pvParam, UInt32 fWinIni);
        [DllImport("User32.dll")]
        static extern uint GetDoubleClickTime();
        static public uint ZjistitSens()
        {
            uint hodnota = 0;
            SystemParametersInfo(
                SPI_GETMOUSESPEED,
                0,
                ref hodnota,
                0);
            return hodnota;
        }
        static public uint ZjistitScroll()
        {
            uint hodnota = 0;
            SystemParametersInfo(
                SPI_GETWHEELSCROLLLINES,
                0,
                ref hodnota,
                0);
            return hodnota;
        }
        static public uint ZjistitDoubleClick()
        {
            uint hodnota = 0;
            hodnota = GetDoubleClickTime();
            return hodnota;
        }
    }
}
