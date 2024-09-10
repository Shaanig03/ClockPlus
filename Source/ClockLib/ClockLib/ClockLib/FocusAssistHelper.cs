using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClockLib
{
    public static class FocusAssistHelper
    {

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int ZwUpdateWnfStateData(
            ref ulong StateName,
            IntPtr Buffer,
            int Length,
            Guid TypeId,
            IntPtr ExplicitScope,
            uint MatchingChangeStamp,
            bool CheckStamp
        );

        [DllImport("ntdll.dll", SetLastError = true)]
        static extern int ZwQueryWnfStateData(
            ref ulong StateName,
            Guid TypeId,
            IntPtr ExplicitScope,
            out uint ChangeStamp,
            IntPtr Buffer,
            ref int BufferSize
        );

        const ulong WNF_SHEL_QUIET_MOMENT_SHELL_MODE_CHANGED = 0xd83063ea3bf5075UL;

        public static int GetFocusAssistMode(out FocusAssistMode mode)
        {

            var stateName = WNF_SHEL_QUIET_MOMENT_SHELL_MODE_CHANGED;

            int bufferSize = 4;
            var bufferPtr = Marshal.AllocHGlobal(bufferSize);

            var result = ZwQueryWnfStateData(ref stateName, Guid.Empty, IntPtr.Zero, out _, bufferPtr, ref bufferSize);

            if (result == 0)
            {

                var modeAsByteArray = new byte[bufferSize];

                Marshal.Copy(bufferPtr, modeAsByteArray, 0, modeAsByteArray.Length);

                mode = (FocusAssistMode)BitConverter.ToUInt32(modeAsByteArray);

            }
            else
            {
                mode = FocusAssistMode.Off;
            }

            return result;

        }

        public static int SetFocusAssistMode(FocusAssistMode mode)
        {

            var stateName = WNF_SHEL_QUIET_MOMENT_SHELL_MODE_CHANGED;

            var modeAsByteArray = BitConverter.GetBytes((uint)mode);

            var bufferPtr = Marshal.AllocHGlobal(modeAsByteArray.Length);

            try
            {

                Marshal.Copy(modeAsByteArray, 0, bufferPtr, modeAsByteArray.Length);

                return ZwUpdateWnfStateData(ref stateName, bufferPtr, modeAsByteArray.Length, Guid.Empty, IntPtr.Zero, 0, false);

            }
            finally
            {
                Marshal.FreeHGlobal(bufferPtr);
            }

        }

    }

    public enum FocusAssistMode : uint
    {
        Off = 0,
        Game = 1,
        Fullscreen = 2,
    }
}
