using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseTray
{
    class Zmena
    {
        public const UInt32 SPI_SETMOUSESPEED = 0x0071;
        public const UInt32 SPI_SETWHEELSCROLLLINES = 0x0069;
        [DllImport("User32.dll")]
        static extern Boolean SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, UInt32 pvParam, UInt32 fWinIni);
        [DllImport("User32.dll")]
        static extern Boolean SetDoubleClickTime(uint Arg1);
        public static void ZmenitSens(uint i)
        {
            SystemParametersInfo(
                SPI_SETMOUSESPEED,
                0,
                i,
                0);
        }
        public static void ZmenitScroll(uint i)
        {
            SystemParametersInfo(
                SPI_SETWHEELSCROLLLINES,
                i,
                0,
                0);
        }
        public static void ZmenitDoubleClick(uint i)
        {
            SetDoubleClickTime(i);
        }
    }
}
