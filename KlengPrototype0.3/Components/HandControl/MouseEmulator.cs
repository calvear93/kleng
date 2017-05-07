using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace Kleng.Components.HandControl
{
    /// <summary>
    ///     Emulator of mouse operations. Uses the User32.dll
    ///     library from Windows system.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.7.4</version>
    internal static class MouseEmulator
    {
        /// <summary>
        ///     Time between two click operations are performed.
        /// </summary>
        /// <default>25</default>
        public static int IntervalBetweenClicks = 25;

        /// <summary>
        ///     Performs a basic mouse operation.
        /// </summary>
        /// <param name="flag">Mouse Event Flag indicating the mouse operation to perform.</param>
        /// <param name="data">Extra data for operation.</param>
        private static void Perform(int flag, int data)
        {
            // Gets current cursor position.
            CursorPosition position;
            GetCursorPos(out position);
            // Performs the operation.
            mouse_event(flag, position.X, position.Y, data, 0);
        }

        /// <summary>
        ///     Internal structure that eases the management of
        ///     the mouse position on the screen.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct CursorPosition
        {
            // Horizontal coordinate from the screen.
            public readonly int X;
            // Vertical coordinate from the screen.
            public readonly int Y;

            public static implicit operator Point(CursorPosition point)
            {
                return new Point(point.X, point.Y);
            }
        }

        #region MOUSE EVENT FLAGS

        /// <summary>Left mouse button press.</summary>
        private static readonly int LEFTDOWN = 0x0002;

        /// <summary>Left mouse button release.</summary>
        private static readonly int LEFTUP = 0x0004;

        /// <summary>Right mouse button press.</summary>
        private static readonly int RIGHTDOWN = 0x0008;

        /// <summary>Right mouse button release.</summary>
        private static readonly int RIGHTUP = 0x0010;

        /// <summary>Wheel mouse button press.</summary>
        private static readonly int MIDDLEDOWN = 0x0020;

        /// <summary>Wheel mouse button release.</summary>
        private static readonly int MIDDLEUP = 0x0040;

        /// <summary>Wheel mouse.</summary>
        private static readonly int WHEEL = 0x0800;

        #endregion

        #region LOW LEVEL API FUNCTIONS

        /// <summary>
        ///     Performs a basic mouse operation by the low level API User32.dll.
        /// </summary>
        /// <param name="dwFlags">Mouse Event Flag indicating the mouse operation to perform.</param>
        /// <param name="dx">Horizontal coordinate where doing the operation.</param>
        /// <param name="dy">Vertical coordinate where doing the operation.</param>
        /// <param name="dwData">Data associated with mouse event.</param>
        /// <param name="dwExtraInfo">Additional information associated with the mouse event.</param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        /// <summary>
        ///     Sets the current cursor position on the screen.
        /// </summary>
        /// <param name="x">Horizontal coordinate from the screen.</param>
        /// <param name="y">Vertical coordinate from the screen.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);

        /// <summary>
        ///     Gets the current cursor position on the screen.
        /// </summary>
        /// <param name="position">Position of the cursor.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out CursorPosition position);

        #endregion

        #region MOUSE OPERATIONS

        /// <summary>
        ///     Sets the cursor position.
        /// </summary>
        /// <param name="x">Horizontal coordinate from the screen.</param>
        /// <param name="y">Vertical coordinate from the screen.</param>
        public static void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        /// <summary>
        ///     Gets the current cursor position.
        /// </summary>
        /// <returns>Array with horizontal and vertical coordinate. {X, Y}</returns>
        public static int[] GetCursorPosition()
        {
            CursorPosition position;
            GetCursorPos(out position);
            return new[] {position.X, position.Y};
        }

        /// <summary>
        ///     Performs a left click.
        /// </summary>
        public static void LeftClick()
        {
            Perform(LEFTDOWN | LEFTUP, 0);
        }

        /// <summary>
        ///     Performs a left click and keep it pressed.
        /// </summary>
        public static void LeftClickKeep()
        {
            Perform(LEFTDOWN, 0);
        }

        /// <summary>
        ///     Releases the pressed left click.
        /// </summary>
        public static void LeftClickRelease()
        {
            Perform(LEFTUP, 0);
        }

        /// <summary>
        ///     Performs a double left click.
        /// </summary>
        public static void DoubleLeftClick()
        {
            LeftClick();
            Thread.Sleep(IntervalBetweenClicks);
            LeftClick();
        }

        /// <summary>
        ///     Performs a right click.
        /// </summary>
        public static void RightClick()
        {
            Perform(RIGHTDOWN | RIGHTUP, 0);
        }

        /// <summary>
        ///     Performs a right click and keep it pressed.
        /// </summary>
        public static void RightClickKeep()
        {
            Perform(RIGHTDOWN, 0);
        }

        /// <summary>
        ///     Releases the pressed right click.
        /// </summary>
        public static void RightClickRelease()
        {
            Perform(RIGHTUP, 0);
        }

        /// <summary>
        ///     Performs a double left click.
        /// </summary>
        public static void DoubleRightClick()
        {
            RightClick();
            Thread.Sleep(IntervalBetweenClicks);
            RightClick();
        }

        /// <summary>
        ///     Performs a middle (wheel button) click.
        /// </summary>
        public static void MiddleClick()
        {
            Perform(MIDDLEDOWN | MIDDLEUP, 0);
        }

        /// <summary>
        ///     Performs a scroll by mouse wheel.
        /// </summary>
        /// <param name="steps">Strength of scroll operation.</param>
        public static void Wheel(int steps)
        {
            Perform(WHEEL, steps);
        }

        #endregion
    }
}