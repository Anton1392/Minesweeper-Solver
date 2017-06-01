using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Enables control of processes on the computer.
using System.Runtime.InteropServices;
using System.Diagnostics;

// Enables screenshot and graphics functions.
using System.Drawing;

namespace MSSolver
{
    /// <summary>
    /// Static class handling Input/Output for Minesweeper.
    /// Handles moving mouse/clicking/taking screenshots/finding processes/moving windows etc.
    /// </summary>
    public static class MSIO
    {
        // The Minesweeper process handle.
        // This is used for sending input to the process, for example when moving the window.
        private static IntPtr MSProcessHandle;

        #region DLLImports

        // NOTE: Method and variable names are set in stone, as they are imported from an external source.

        // Imports the SetWindowPos method from user32.dll.
        [DllImport("user32.dll", SetLastError = true)]
        //                                      hWnd is the window handle that the function "hooks up" to.
        //                                      |                   The handle of a window to insert the window "after" on the z-axis. This can be IntPtr.Zero for no effect.
        //                                      |                   |           The new position of the left side of the window.        
        //                                      |                   |           |       The new position of the top of the window.
        //                                      |                   |           |       |       The new width of the window, in pixels.
        //                                      |                   |           |       |       |       The new height of the window, in pixels.
        //                                      |                   |           |       |       |       |           The window sizing and positioning flags.
        //                                      |                   |           |       |       |       |           |
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        // This constant is used for the window sizing and positioning flag in the method above.
        // SWP_NOSIZE ignores the cx and cy parameters in the function above.
        const uint SWP_NOSIZE = 0x0001;

        // Imports the SetCursorPos function from user32.dll. This is used to move the mouse.
        [DllImport("user32.dll", SetLastError = true)]
        static extern void SetCursorPos(int X, int Y);

        // Imports the mouse_event function. This function is useful to simulate mouse buttons and wheel events.
        [DllImport("user32.dll", SetLastError = true)]
        //                                    dwFlags is the specified mouse action. Check the enums below.
        //                                    |         The screen position x-coordinate to do the action on.
        //                                    |         |       The screen position y-coordinate to do the action on.
        //                                    |         |       |          Addional data about mouse events. We don't need this, it should be 0.
        //                                    |         |       |          |            An extra value associated with the mouse event. We don't need this either.
        //                                    |         |       |          |            |
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        // Useful flags for the mouse_event function. These can be mixed and matched to click
        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
        }

        #endregion

        /// <summary>
        /// Sets the position of an opened Winmine__XP window to 0,0 on the screen.
        /// </summary>
        public static void ResetMSWindowPos()
        {
            // Finds the process handle.
            FindMSProcessHandle();

            // Sets the Minesweeper window to position 0,0.
            SetWindowPos(MSProcessHandle, IntPtr.Zero, 0, 0, 0, 0, SWP_NOSIZE);
        }

        /// <summary>
        /// Takes an image from screen in a square between two points.
        /// </summary>
        /// <param name="x1">X-Coordinate of the first point.</param>
        /// <param name="y1">Y-Coordinate of the first point.</param>
        /// <param name="x2">X-Coordinate of the second point.</param>
        /// <param name="y2">Y-Coordinate of the second point.</param>
        public static Bitmap Screenshot(int x1, int y1, int x2, int y2)
        {
            // Creates a Bitmap with the correct width and height. Difference in coordinates gives the dimensions.
            Bitmap scr = new Bitmap(x2 - x1, y2 - y1);

            using (Graphics g = Graphics.FromImage(scr as Image))
            {
                // x1 = source corner x-coord.
                // y1 = source corner y-coord.
                // 0, 0 is where in the image the copy should be placed.
                // "new Size(x2 - x1, y2 - y1)" is the size of the screenshot.
                g.CopyFromScreen(x1, y1, 0, 0, new Size(x2 - x1, y2 - y1));
            }

            // Returns the Bitmap screenshot.
            return scr;
        }

        /// <summary>
        /// Moves the cursor to the specified screen coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate to move the mouse to.</param>
        /// <param name="y">The y-coordinate to move the mouse to.</param>
        public static void MoveMouse(int x, int y)
        {
            SetCursorPos(x, y);
        }

        /// <summary>
        /// Clicks the mouse button at the current coordinates.
        /// </summary>
        public static void Click()
        {
            // Grabs the current cursor position. This method does not want to alter it.
            Point p = Cursor.Position;

            // Pushes and releases the left mouse button at the current mouse coordinates.
            mouse_event((int)MouseEventFlags.LEFTDOWN, p.X, p.Y, 0, 0);
            mouse_event((int)MouseEventFlags.LEFTUP, p.X, p.Y, 0, 0);
        }

        /// <summary>
        /// Right-clicks the mouse button at the current coordinates.
        /// </summary>
        public static void RightClick()
        {
            // Grabs the current cursor position. This method does not want to alter it.
            Point p = Cursor.Position;

            // Pushes and releases the right mouse button at the current mouse coordinates.
            mouse_event((int)MouseEventFlags.RIGHTDOWN, p.X, p.Y, 0, 0);
            mouse_event((int)MouseEventFlags.RIGHTUP, p.X, p.Y, 0, 0);
        }

        /// <summary>
        /// Clicks both of the mouse buttons at the same time.
        /// </summary>
        public static void ClickBothMouseButtons()
        {
            // Grabs the current cursor position. This method does not want to alter it.
            Point p = Cursor.Position;

            // Pushes and releases both the left and right mouse button at the current mouse coordinates.
            mouse_event((int)MouseEventFlags.LEFTDOWN, p.X, p.Y, 0, 0);
            mouse_event((int)MouseEventFlags.RIGHTDOWN, p.X, p.Y, 0, 0);
            mouse_event((int)MouseEventFlags.RIGHTUP, p.X, p.Y, 0, 0);
            mouse_event((int)MouseEventFlags.LEFTUP, p.X, p.Y, 0, 0);
        }

        /// <summary>
        /// Finds and stores the window handle of the Winmine__XP window.
        /// </summary>
        private static void FindMSProcessHandle()
        {
            try
            {
                // Finds a list of all processes with the name, and picks the first one on the list.
                // Grabs the handle from the process and stores it as an IntPtr.
                Process MSProcess = Process.GetProcessesByName("Winmine__XP")[0];
                MSProcessHandle = MSProcess.MainWindowHandle;
            }
            catch
            {
                // If the process couldn't be found, show an error box.
                MessageBox.Show("Could not find Minesweeper process. Make sure the window is open and try again.");          
            }
        }
    }
}
