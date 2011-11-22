namespace System
{
    using Microsoft.Win32;
    using Microsoft.Win32.SafeHandles;
    using System.IO;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Text;
    using System.Threading;

    /// <summary>Represents the standard input, output, and error streams for console applications. This class cannot be inherited.</summary>
    /// <filterpriority>1</filterpriority>
    public static class Console
    {
        private const int _DefaultConsoleBufferSize = 0x100;
        private const int CapsLockVKCode = 20;
        private const int MaxBeepFrequency = 0x7fff;
        private const int MaxConsoleTitleLength = 0x5fb4;
        private const int MinBeepFrequency = 0x25;
        private const int NumberLockVKCode = 0x90;
        private static Win32Native.InputRecord _cachedInputRecord;
        private static ConsoleCancelEventHandler _cancelCallbacks;
        private static IntPtr _consoleInputHandle;
        private static IntPtr _consoleOutputHandle;
        private static byte _defaultColors;
        private static TextWriter _error;
        private static bool _haveReadDefaultColors;
        private static ControlCHooker _hooker;
        private static TextReader _in;
        private static TextWriter _out;
        private static bool _wasErrorRedirected;
        private static bool _wasOutRedirected;
        private static object s_InternalSyncObject;

        /// <summary>Gets or sets the background color of the console.</summary>
        /// <returns>A <see cref="T:System.ConsoleColor"></see> that specifies the background color of the console; that is, the color that appears behind each character. The default is black.</returns>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor"></see>. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static ConsoleColor BackgroundColor
        {
            get
            {
                bool flag;
                Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo(false, out flag);
                if (!flag)
                {
                    return ConsoleColor.Black;
                }
                Win32Native.Color c = (Win32Native.Color) ((short) (bufferInfo.wAttributes & 240));
                return ColorAttributeToConsoleColor(c);
            }
            set
            {
                bool flag;
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                Win32Native.Color color = ConsoleColorToColorAttribute(value, true);
                Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo(false, out flag);
                if (flag)
                {
                    short attributes = (short) (bufferInfo.wAttributes & -241);
                    attributes = (short) (((ushort) attributes) | ((ushort) color));
                    Win32Native.SetConsoleTextAttribute(ConsoleOutputHandle, attributes);
                }
            }
        }

        /// <summary>Gets or sets the height of the buffer area.</summary>
        /// <returns>The current height, in rows, of the buffer area.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than or equal to zero.-or- The value in a set operation is greater than or equal to <see cref="F:System.Int16.MaxValue"></see>.-or- The value in a set operation is less than <see cref="P:System.Console.WindowTop"></see> + <see cref="P:System.Console.WindowHeight"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        public static int BufferHeight
        {
            get { return GetBufferInfo().dwSize.Y; }
            set { SetBufferSize(BufferWidth, value); }
        }

        /// <summary>Gets or sets the width of the buffer area.</summary>
        /// <returns>The current width, in columns, of the buffer area.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than or equal to zero.-or- The value in a set operation is greater than or equal to <see cref="F:System.Int16.MaxValue"></see>.-or- The value in a set operation is less than <see cref="P:System.Console.WindowLeft"></see> + <see cref="P:System.Console.WindowWidth"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        public static int BufferWidth
        {
            get { return GetBufferInfo().dwSize.X; }
            set { SetBufferSize(value, BufferHeight); }
        }

        /// <summary>Gets a value indicating whether the CAPS LOCK keyboard toggle is turned on or turned off.</summary>
        /// <returns>true if CAPS LOCK is turned on; false if CAPS LOCK is turned off.</returns>
        /// <filterpriority>1</filterpriority>
        public static bool CapsLock
        {
            get { return ((Win32Native.GetKeyState(20) & 1) == 1); }
        }

        private static IntPtr ConsoleInputHandle
        {
            get
            {
                if (_consoleInputHandle == IntPtr.Zero)
                {
                    _consoleInputHandle = Win32Native.GetStdHandle(-10);
                }
                return _consoleInputHandle;
            }
        }

        private static IntPtr ConsoleOutputHandle
        {
            get
            {
                if (_consoleOutputHandle == IntPtr.Zero)
                {
                    _consoleOutputHandle = Win32Native.GetStdHandle(-11);
                }
                return _consoleOutputHandle;
            }
        }

        /// <summary>Gets or sets the column position of the cursor within the buffer area.</summary>
        /// <returns>The current position, in columns, of the cursor.</returns>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.-or- The value in a set operation is greater than or equal to <see cref="P:System.Console.BufferWidth"></see>. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred.-or-Debugging with the Quick Console is enabled. </exception>
        /// <filterpriority>1</filterpriority>
        public static int CursorLeft
        {
            get { return GetBufferInfo().dwCursorPosition.X; }
            set { SetCursorPosition(value, CursorTop); }
        }

        /// <summary>Gets or sets the height of the cursor within a character cell.</summary>
        /// <returns>The size of the cursor expressed as a percentage of the height of a character cell. The property value ranges from 1 to 100.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 1 or greater than 100. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static int CursorSize
        {
            get
            {
                Win32Native.CONSOLE_CURSOR_INFO console_cursor_info;
                if (!Win32Native.GetConsoleCursorInfo(ConsoleOutputHandle, out console_cursor_info))
                {
                    __Error.WinIOError();
                }
                return console_cursor_info.dwSize;
            }
            set
            {
                Win32Native.CONSOLE_CURSOR_INFO console_cursor_info;
                if ((value < 1) || (value > 100))
                {
                    throw new ArgumentOutOfRangeException("value", value,
                                                          Environment.GetResourceString("ArgumentOutOfRange_CursorSize"));
                }
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                if ((value == 100) && ((Environment.OSInfo & Environment.OSName.Win9x) != Environment.OSName.Invalid))
                {
                    value = 0x63;
                }
                IntPtr consoleOutputHandle = ConsoleOutputHandle;
                if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_cursor_info))
                {
                    __Error.WinIOError();
                }
                console_cursor_info.dwSize = value;
                if (!Win32Native.SetConsoleCursorInfo(consoleOutputHandle, ref console_cursor_info))
                {
                    __Error.WinIOError();
                }
            }
        }

        /// <summary>Gets or sets the row position of the cursor within the buffer area.</summary>
        /// <returns>The current position, in rows, of the cursor.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.-or- The value in a set operation is greater than or equal to <see cref="P:System.Console.BufferHeight"></see>. </exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <filterpriority>1</filterpriority>
        public static int CursorTop
        {
            get { return GetBufferInfo().dwCursorPosition.Y; }
            set { SetCursorPosition(CursorLeft, value); }
        }

        /// <summary>Gets or sets a value indicating whether the cursor is visible.</summary>
        /// <returns>true if the cursor is visible; otherwise, false.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static bool CursorVisible
        {
            get
            {
                Win32Native.CONSOLE_CURSOR_INFO console_cursor_info;
                if (!Win32Native.GetConsoleCursorInfo(ConsoleOutputHandle, out console_cursor_info))
                {
                    __Error.WinIOError();
                }
                return console_cursor_info.bVisible;
            }
            set
            {
                Win32Native.CONSOLE_CURSOR_INFO console_cursor_info;
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                IntPtr consoleOutputHandle = ConsoleOutputHandle;
                if (!Win32Native.GetConsoleCursorInfo(consoleOutputHandle, out console_cursor_info))
                {
                    __Error.WinIOError();
                }
                console_cursor_info.bVisible = value;
                if (!Win32Native.SetConsoleCursorInfo(consoleOutputHandle, ref console_cursor_info))
                {
                    __Error.WinIOError();
                }
            }
        }

        /// <summary>Gets the standard error output stream.</summary>
        /// <returns>A <see cref="T:System.IO.TextWriter"></see> that represents the standard error output stream.</returns>
        /// <filterpriority>1</filterpriority>
        public static TextWriter Error
        {
            [HostProtection(SecurityAction.LinkDemand, UI = true)]
            get
            {
                if (_error == null)
                {
                    InitializeStdOutError(false);
                }
                return _error;
            }
        }

        /// <summary>Gets or sets the foreground color of the console.</summary>
        /// <returns>A <see cref="T:System.ConsoleColor"></see> that specifies the foreground color of the console; that is, the color of each character that is displayed. The default is gray.</returns>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor"></see>. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static ConsoleColor ForegroundColor
        {
            get
            {
                bool flag;
                Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo(false, out flag);
                if (!flag)
                {
                    return ConsoleColor.Gray;
                }
                Win32Native.Color c = (Win32Native.Color) ((short) (bufferInfo.wAttributes & 15));
                return ColorAttributeToConsoleColor(c);
            }
            set
            {
                bool flag;
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                Win32Native.Color color = ConsoleColorToColorAttribute(value, false);
                Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo(false, out flag);
                if (flag)
                {
                    short attributes = (short) (bufferInfo.wAttributes & -16);
                    attributes = (short) (((ushort) attributes) | ((ushort) color));
                    Win32Native.SetConsoleTextAttribute(ConsoleOutputHandle, attributes);
                }
            }
        }

        /// <summary>Gets the standard input stream.</summary>
        /// <returns>A <see cref="T:System.IO.TextReader"></see> that represents the standard input stream.</returns>
        /// <filterpriority>1</filterpriority>
        public static TextReader In
        {
            [HostProtection(SecurityAction.LinkDemand, UI = true)]
            get
            {
                if (_in == null)
                {
                    lock (InternalSyncObject)
                    {
                        if (_in == null)
                        {
                            TextReader @null;
                            Stream stream = OpenStandardInput(0x100);
                            if (stream == Stream.Null)
                            {
                                @null = StreamReader.Null;
                            }
                            else
                            {
                                Encoding encoding = Encoding.GetEncoding((int) Win32Native.GetConsoleCP());
                                @null = TextReader.Synchronized(new StreamReader(stream, encoding, false, 0x100, false));
                            }
                            Thread.MemoryBarrier();
                            _in = @null;
                        }
                    }
                }
                return _in;
            }
        }

        /// <summary>Gets or sets the encoding the console uses to read input. </summary>
        /// <returns>The encoding used to read console input.</returns>
        /// <exception cref="T:System.ArgumentNullException">The property value in a set operation is null.</exception>
        /// <exception cref="T:System.PlatformNotSupportedException">This property's set operation is not supported on Windows 98, Windows 98 Second Edition, or Windows Millennium Edition.</exception>
        /// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
        /// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
        /// <filterpriority>1</filterpriority>
        public static Encoding InputEncoding
        {
            get { return Encoding.GetEncoding((int) Win32Native.GetConsoleCP()); }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (Environment.IsWin9X())
                {
                    throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_Win9x"));
                }
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                uint codePage = (uint) value.CodePage;
                lock (InternalSyncObject)
                {
                    if (!Win32Native.SetConsoleCP(codePage))
                    {
                        __Error.WinIOError();
                    }
                    _in = null;
                }
            }
        }

        private static object InternalSyncObject
        {
            get
            {
                if (s_InternalSyncObject == null)
                {
                    object obj2 = new object();
                    Interlocked.CompareExchange(ref s_InternalSyncObject, obj2, null);
                }
                return s_InternalSyncObject;
            }
        }

        /// <summary>Gets a value indicating whether a key press is available in the input stream.</summary>
        /// <returns>true if a key press is available; otherwise, false.</returns>
        /// <exception cref="T:System.InvalidOperationException">Standard input is redirected to a file instead of the keyboard. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        public static bool KeyAvailable
        {
            [HostProtection(SecurityAction.LinkDemand, UI = true)]
            get
            {
                if (_cachedInputRecord.eventType == 1)
                {
                    return true;
                }
                Win32Native.InputRecord buffer = new Win32Native.InputRecord();
                int numEventsRead = 0;
                while (true)
                {
                    if (!Win32Native.PeekConsoleInput(ConsoleInputHandle, out buffer, 1, out numEventsRead))
                    {
                        int errorCode = Marshal.GetLastWin32Error();
                        if (errorCode == 6)
                        {
                            throw new InvalidOperationException(
                                Environment.GetResourceString("InvalidOperation_ConsoleKeyAvailableOnFile"));
                        }
                        __Error.WinIOError(errorCode, "stdin");
                    }
                    if (numEventsRead == 0)
                    {
                        return false;
                    }
                    short virtualKeyCode = buffer.keyEvent.virtualKeyCode;
                    if (IsKeyDownEvent(buffer) && !IsModKey(virtualKeyCode))
                    {
                        return true;
                    }
                    if (!Win32Native.ReadConsoleInput(ConsoleInputHandle, out buffer, 1, out numEventsRead))
                    {
                        __Error.WinIOError();
                    }
                }
            }
        }

        /// <summary>Gets the largest possible number of console window rows, based on the current font and screen resolution.</summary>
        /// <returns>The height of the largest possible console window measured in rows.</returns>
        /// <filterpriority>1</filterpriority>
        public static int LargestWindowHeight
        {
            get { return Win32Native.GetLargestConsoleWindowSize(ConsoleOutputHandle).Y; }
        }

        /// <summary>Gets the largest possible number of console window columns, based on the current font and screen resolution.</summary>
        /// <returns>The width of the largest possible console window measured in columns.</returns>
        /// <filterpriority>1</filterpriority>
        public static int LargestWindowWidth
        {
            get { return Win32Native.GetLargestConsoleWindowSize(ConsoleOutputHandle).X; }
        }

        /// <summary>Gets a value indicating whether the NUM LOCK keyboard toggle is turned on or turned off.</summary>
        /// <returns>true if NUM LOCK is turned on; false if NUM LOCK is turned off.</returns>
        /// <filterpriority>1</filterpriority>
        public static bool NumberLock
        {
            get { return ((Win32Native.GetKeyState(0x90) & 1) == 1); }
        }

        /// <summary>Gets the standard output stream.</summary>
        /// <returns>A <see cref="T:System.IO.TextWriter"></see> that represents the standard output stream.</returns>
        /// <filterpriority>1</filterpriority>
        public static TextWriter Out
        {
            [HostProtection(SecurityAction.LinkDemand, UI = true)]
            get
            {
                if (_out == null)
                {
                    InitializeStdOutError(true);
                }
                return _out;
            }
        }

        /// <summary>Gets or sets the encoding the console uses to write output. </summary>
        /// <returns>The encoding used to write console output.</returns>
        /// <exception cref="T:System.ArgumentNullException">The property value in a set operation is null.</exception>
        /// <exception cref="T:System.PlatformNotSupportedException">This property's set operation is not supported on Windows 98, Windows 98 Second Edition, or Windows Millennium Edition.</exception>
        /// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
        /// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
        /// <filterpriority>1</filterpriority>
        public static Encoding OutputEncoding
        {
            get { return Encoding.GetEncoding((int) Win32Native.GetConsoleOutputCP()); }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (Environment.IsWin9X())
                {
                    throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_Win9x"));
                }
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                lock (InternalSyncObject)
                {
                    if ((_out != null) && !_wasOutRedirected)
                    {
                        _out.Flush();
                        _out = null;
                    }
                    if ((_error != null) && !_wasErrorRedirected)
                    {
                        _error.Flush();
                        _error = null;
                    }
                    if (!Win32Native.SetConsoleOutputCP((uint) value.CodePage))
                    {
                        __Error.WinIOError();
                    }
                }
            }
        }

        /// <summary>Gets or sets the title to display in the console title bar.</summary>
        /// <returns>The string to be displayed in the title bar of the console. The maximum length of the title string is 24500 characters.</returns>
        /// <exception cref="T:System.ArgumentNullException">In a set operation, the specified title is null. </exception>
        /// <exception cref="T:System.InvalidOperationException">In a get operation, the retrieved title is longer than 24500 characters. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the specified title is longer than 24500 characters. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static string Title
        {
            get
            {
                StringBuilder sb = new StringBuilder(0x5fb5);
                Win32Native.SetLastError(0);
                int consoleTitle = Win32Native.GetConsoleTitle(sb, sb.Capacity);
                if (consoleTitle == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    if (errorCode == 0)
                    {
                        sb.Length = 0;
                    }
                    else
                    {
                        __Error.WinIOError(errorCode, string.Empty);
                    }
                }
                else if (consoleTitle > 0x5fb4)
                {
                    throw new InvalidOperationException(
                        Environment.GetResourceString("ArgumentOutOfRange_ConsoleTitleTooLong"));
                }
                return sb.ToString();
            }
            set
            {
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (value.Length > 0x5fb4)
                {
                    throw new ArgumentOutOfRangeException("value",
                                                          Environment.GetResourceString(
                                                              "ArgumentOutOfRange_ConsoleTitleTooLong"));
                }
                if (!Win32Native.SetConsoleTitle(value))
                {
                    __Error.WinIOError();
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the combination of the <see cref="F:System.ConsoleModifiers.Control"></see> modifier key and <see cref="F:System.ConsoleKey.C"></see> console key (CTRL+C) is treated as ordinary input or as an interruption that is handled by the operating system.</summary>
        /// <returns>true if CTRL+C is treated as ordinary input; otherwise, false.</returns>
        /// <exception cref="T:System.IO.IOException">Unable to get or set the input mode of the console input buffer. -or-Debugging with the Quick Console is enabled.</exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static bool TreatControlCAsInput
        {
            get
            {
                IntPtr consoleInputHandle = ConsoleInputHandle;
                if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
                {
                    throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
                }
                int mode = 0;
                if (!Win32Native.GetConsoleMode(consoleInputHandle, out mode))
                {
                    __Error.WinIOError();
                }
                return ((mode & 1) == 0);
            }
            set
            {
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                IntPtr consoleInputHandle = ConsoleInputHandle;
                if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
                {
                    throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
                }
                int mode = 0;
                bool consoleMode = Win32Native.GetConsoleMode(consoleInputHandle, out mode);
                if (value)
                {
                    mode &= -2;
                }
                else
                {
                    mode |= 1;
                }
                if (!Win32Native.SetConsoleMode(consoleInputHandle, mode))
                {
                    __Error.WinIOError();
                }
            }
        }

        /// <summary>Gets or sets the height of the console window area.</summary>
        /// <returns>The height of the console window measured in rows.</returns>
        /// <exception cref="T:System.IO.IOException">Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Console.WindowWidth"></see> property or the value of the <see cref="P:System.Console.WindowHeight"></see> property is less than or equal to 0.-or-The value of the <see cref="P:System.Console.WindowHeight"></see> property plus the value of the <see cref="P:System.Console.WindowTop"></see> property is greater than or equal to <see cref="F:System.Int16.MaxValue"></see>.-or-The value of the <see cref="P:System.Console.WindowWidth"></see> property or the value of the <see cref="P:System.Console.WindowHeight"></see> property is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
        /// <filterpriority>1</filterpriority>
        public static int WindowHeight
        {
            get
            {
                Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo();
                return ((bufferInfo.srWindow.Bottom - bufferInfo.srWindow.Top) + 1);
            }
            set { SetWindowSize(WindowWidth, value); }
        }

        /// <summary>Gets or sets the leftmost position of the console window area relative to the screen buffer.</summary>
        /// <returns>The leftmost console window position measured in columns.</returns>
        /// <exception cref="T:System.IO.IOException">Debugging with the Quick Console is enabled.</exception>
        /// <filterpriority>1</filterpriority>
        public static int WindowLeft
        {
            get { return GetBufferInfo().srWindow.Left; }
            set { SetWindowPosition(value, WindowTop); }
        }

        /// <summary>Gets or sets the top position of the console window area relative to the screen buffer.</summary>
        /// <returns>The uppermost console window position measured in rows.</returns>
        /// <exception cref="T:System.IO.IOException">Debugging with the Quick Console is enabled.</exception>
        /// <filterpriority>1</filterpriority>
        public static int WindowTop
        {
            get { return GetBufferInfo().srWindow.Top; }
            set { SetWindowPosition(WindowLeft, value); }
        }

        /// <summary>Gets or sets the width of the console window.</summary>
        /// <returns>The width of the console window measured in columns.</returns>
        /// <exception cref="T:System.IO.IOException">Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Console.WindowWidth"></see> property or the value of the <see cref="P:System.Console.WindowHeight"></see> property is less than or equal to 0.-or-The value of the <see cref="P:System.Console.WindowHeight"></see> property plus the value of the <see cref="P:System.Console.WindowTop"></see> property is greater than or equal to <see cref="F:System.Int16.MaxValue"></see>.-or-The value of the <see cref="P:System.Console.WindowWidth"></see> property or the value of the <see cref="P:System.Console.WindowHeight"></see> property is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
        /// <filterpriority>1</filterpriority>
        public static int WindowWidth
        {
            get
            {
                Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo();
                return ((bufferInfo.srWindow.Right - bufferInfo.srWindow.Left) + 1);
            }
            set { SetWindowSize(value, WindowHeight); }
        }

        /// <summary>Occurs when the <see cref="F:System.ConsoleModifiers.Control"></see> modifier key (CTRL) and <see cref="F:System.ConsoleKey.C"></see> console key (C) are pressed simultaneously (CTRL+C).</summary>
        /// <filterpriority>1</filterpriority>
        public static event ConsoleCancelEventHandler CancelKeyPress
        {
            add
            {
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                lock (InternalSyncObject)
                {
                    _cancelCallbacks = (ConsoleCancelEventHandler) Delegate.Combine(_cancelCallbacks, value);
                    if (_hooker == null)
                    {
                        _hooker = new ControlCHooker();
                        _hooker.Hook();
                    }
                }
            }
            remove
            {
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                lock (InternalSyncObject)
                {
                    _cancelCallbacks = (ConsoleCancelEventHandler) Delegate.Remove(_cancelCallbacks, value);
                    if ((_hooker != null) && (_cancelCallbacks == null))
                    {
                        _hooker.Unhook();
                    }
                }
            }
        }

        /// <summary>Plays the sound of a beep through the console speaker.</summary>
        /// <exception cref="T:System.Security.HostProtectionException">This method was executed on a server, such as SQL Server, that does not permit access to a user interface.</exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Beep()
        {
            Beep(800, 200);
        }

        /// <summary>Plays the sound of a beep of a specified frequency and duration through the console speaker.</summary>
        /// <param name="frequency">The frequency of the beep, ranging from 37 to 32767 hertz.</param>
        /// <param name="duration">The duration of the beep measured in milliseconds.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">frequency is less than 37 or more than 32767 hertz.-or-duration is less than or equal to zero.</exception>
        /// <exception cref="T:System.Security.HostProtectionException">This method was executed on a server, such as SQL Server, that does not permit access to the console.</exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Beep(int frequency, int duration)
        {
            if ((frequency < 0x25) || (frequency > 0x7fff))
            {
                throw new ArgumentOutOfRangeException("frequency", frequency,
                                                      Environment.GetResourceString("ArgumentOutOfRange_BeepFrequency",
                                                                                    new object[] {0x25, 0x7fff}));
            }
            if (duration <= 0)
            {
                throw new ArgumentOutOfRangeException("duration", duration,
                                                      Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
            }
            Win32Native.Beep(frequency, duration);
        }

        private static bool BreakEvent(int controlType)
        {
            if ((controlType != 0) && (controlType != 1))
            {
                return false;
            }
            ConsoleCancelEventHandler cancelCallbacks = _cancelCallbacks;
            if (cancelCallbacks == null)
            {
                return false;
            }
            ConsoleSpecialKey controlKey = (controlType == 0)
                                               ? ConsoleSpecialKey.ControlC
                                               : ConsoleSpecialKey.ControlBreak;
            ControlCDelegateData state = new ControlCDelegateData(controlKey, cancelCallbacks);
            WaitCallback callBack = new WaitCallback(Console.ControlCDelegate);
            if (!ThreadPool.QueueUserWorkItem(callBack, state))
            {
                return false;
            }
            TimeSpan timeout = new TimeSpan(0, 0, 30);
            state.CompletionEvent.WaitOne(timeout, false);
            if (!state.DelegateStarted)
            {
                return false;
            }
            state.CompletionEvent.WaitOne();
            state.CompletionEvent.Close();
            return state.Cancel;
        }

        /// <summary>Clears the console buffer and corresponding console window of display information.</summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <filterpriority>1</filterpriority>
        public static void Clear()
        {
            Win32Native.COORD dwWriteCoord = new Win32Native.COORD();
            IntPtr consoleOutputHandle = ConsoleOutputHandle;
            if (consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE)
            {
                throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
            }
            Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo();
            int nLength = bufferInfo.dwSize.X*bufferInfo.dwSize.Y;
            int pNumCharsWritten = 0;
            if (
                !Win32Native.FillConsoleOutputCharacter(consoleOutputHandle, ' ', nLength, dwWriteCoord,
                                                        out pNumCharsWritten))
            {
                __Error.WinIOError();
            }
            pNumCharsWritten = 0;
            if (
                !Win32Native.FillConsoleOutputAttribute(consoleOutputHandle, bufferInfo.wAttributes, nLength,
                                                        dwWriteCoord, out pNumCharsWritten))
            {
                __Error.WinIOError();
            }
            if (!Win32Native.SetConsoleCursorPosition(consoleOutputHandle, dwWriteCoord))
            {
                __Error.WinIOError();
            }
        }

        private static ConsoleColor ColorAttributeToConsoleColor(Win32Native.Color c)
        {
            if (
                ((short)
                 (c &
                  (Win32Native.Color.BackgroundYellow | Win32Native.Color.BackgroundIntensity |
                   Win32Native.Color.BackgroundBlue))) != 0)
            {
                c = (Win32Native.Color) ((short) (((short) c) >> 4));
            }
            return (ConsoleColor) c;
        }

        private static Win32Native.Color ConsoleColorToColorAttribute(ConsoleColor color, bool isBackground)
        {
            if ((color & ~ConsoleColor.White) != ConsoleColor.Black)
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"));
            }
            Win32Native.Color color2 = (Win32Native.Color) ((short) color);
            if (isBackground)
            {
                color2 = (Win32Native.Color) ((short) (((short) color2) << 4));
            }
            return color2;
        }

        private static unsafe bool ConsoleHandleIsValid(SafeFileHandle handle)
        {
            int num;
            if (handle.IsInvalid)
            {
                return false;
            }
            byte bytes = 0x41;
            return (__ConsoleStream.WriteFile(handle, &bytes, 0, out num, IntPtr.Zero) != 0);
        }

        private static void ControlCDelegate(object data)
        {
            ControlCDelegateData data2 = (ControlCDelegateData) data;
            try
            {
                data2.DelegateStarted = true;
                ConsoleCancelEventArgs e = new ConsoleCancelEventArgs(data2.ControlKey);
                data2.CancelCallbacks(null, e);
                data2.Cancel = e.Cancel;
            }
            finally
            {
                data2.CompletionEvent.Set();
            }
        }

        private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo()
        {
            bool flag;
            return GetBufferInfo(true, out flag);
        }

        private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo(bool throwOnNoConsole, out bool succeeded)
        {
            Win32Native.CONSOLE_SCREEN_BUFFER_INFO console_screen_buffer_info;
            succeeded = false;
            IntPtr consoleOutputHandle = ConsoleOutputHandle;
            if (consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE)
            {
                if (throwOnNoConsole)
                {
                    throw new IOException(Environment.GetResourceString("IO.IO_NoConsole"));
                }
                return new Win32Native.CONSOLE_SCREEN_BUFFER_INFO();
            }
            if (!Win32Native.GetConsoleScreenBufferInfo(consoleOutputHandle, out console_screen_buffer_info))
            {
                bool consoleScreenBufferInfo = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-12),
                                                                                      out console_screen_buffer_info);
                if (!consoleScreenBufferInfo)
                {
                    consoleScreenBufferInfo = Win32Native.GetConsoleScreenBufferInfo(Win32Native.GetStdHandle(-10),
                                                                                     out console_screen_buffer_info);
                }
                if (!consoleScreenBufferInfo)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    if ((errorCode == 6) && !throwOnNoConsole)
                    {
                        return new Win32Native.CONSOLE_SCREEN_BUFFER_INFO();
                    }
                    __Error.WinIOError(errorCode, null);
                }
            }
            if (!_haveReadDefaultColors)
            {
                _defaultColors = (byte) (console_screen_buffer_info.wAttributes & 0xff);
                _haveReadDefaultColors = true;
            }
            succeeded = true;
            return console_screen_buffer_info;
        }

        private static Stream GetStandardFile(int stdHandleName, FileAccess access, int bufferSize)
        {
            SafeFileHandle handle = new SafeFileHandle(Win32Native.GetStdHandle(stdHandleName), false);
            if (handle.IsInvalid)
            {
                handle.SetHandleAsInvalid();
                return Stream.Null;
            }
            if ((stdHandleName != -10) && !ConsoleHandleIsValid(handle))
            {
                return Stream.Null;
            }
            return new __ConsoleStream(handle, access);
        }

        private static void InitializeStdOutError(bool stdout)
        {
            lock (InternalSyncObject)
            {
                if ((!stdout || (_out == null)) && (stdout || (_error == null)))
                {
                    Stream stream;
                    TextWriter writer = null;
                    if (stdout)
                    {
                        stream = OpenStandardOutput(0x100);
                    }
                    else
                    {
                        stream = OpenStandardError(0x100);
                    }
                    if (stream == Stream.Null)
                    {
                        writer = TextWriter.Synchronized(StreamWriter.Null);
                    }
                    else
                    {
                        Encoding encoding = Encoding.GetEncoding((int) Win32Native.GetConsoleOutputCP());
                        StreamWriter writer2 = new StreamWriter(stream, encoding, 0x100, false);
                        writer2.HaveWrittenPreamble = true;
                        writer2.AutoFlush = true;
                        writer = TextWriter.Synchronized(writer2);
                    }
                    if (stdout)
                    {
                        _out = writer;
                    }
                    else
                    {
                        _error = writer;
                    }
                }
            }
        }

        private static bool IsKeyDownEvent(Win32Native.InputRecord ir)
        {
            return ((ir.eventType == 1) && ir.keyEvent.keyDown);
        }

        private static bool IsModKey(short keyCode)
        {
            if (((keyCode < 0x10) || (keyCode > 0x12)) && ((keyCode != 20) && (keyCode != 0x90)))
            {
                return (keyCode == 0x91);
            }
            return true;
        }

        /// <summary>Copies a specified source area of the screen buffer to a specified destination area.</summary>
        /// <param name="sourceWidth">The number of columns in the source area. </param>
        /// <param name="targetTop">The topmost row of the destination area. </param>
        /// <param name="sourceHeight">The number of rows in the source area. </param>
        /// <param name="sourceTop">The topmost row of the source area. </param>
        /// <param name="sourceLeft">The leftmost column of the source area. </param>
        /// <param name="targetLeft">The leftmost column of the destination area. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the parameters is less than zero.-or- sourceLeft or targetLeft is greater than or equal to <see cref="P:System.Console.BufferWidth"></see>.-or- sourceTop or targetTop is greater than or equal to <see cref="P:System.Console.BufferHeight"></see>.-or- sourceTop + sourceHeight is greater than or equal to <see cref="P:System.Console.BufferHeight"></see>.-or- sourceLeft + sourceWidth is greater than or equal to <see cref="P:System.Console.BufferWidth"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight,
                                          int targetLeft, int targetTop)
        {
            MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ',
                           ConsoleColor.Black, BackgroundColor);
        }

        /// <summary>Copies a specified source area of the screen buffer to a specified destination area.</summary>
        /// <param name="sourceWidth">The number of columns in the source area. </param>
        /// <param name="targetTop">The topmost row of the destination area. </param>
        /// <param name="sourceBackColor">The background color used to fill the source area. </param>
        /// <param name="sourceHeight">The number of rows in the source area. </param>
        /// <param name="sourceTop">The topmost row of the source area. </param>
        /// <param name="sourceLeft">The leftmost column of the source area. </param>
        /// <param name="sourceForeColor">The foreground color used to fill the source area. </param>
        /// <param name="sourceChar">The character used to fill the source area. </param>
        /// <param name="targetLeft">The leftmost column of the destination area. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentException">One or both of the color parameters is not a member of the <see cref="T:System.ConsoleColor"></see> enumeration. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the parameters is less than zero.-or- sourceLeft or targetLeft is greater than or equal to <see cref="P:System.Console.BufferWidth"></see>.-or- sourceTop or targetTop is greater than or equal to <see cref="P:System.Console.BufferHeight"></see>.-or- sourceTop + sourceHeight is greater than or equal to <see cref="P:System.Console.BufferHeight"></see>.-or- sourceLeft + sourceWidth is greater than or equal to <see cref="P:System.Console.BufferWidth"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static unsafe void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight,
                                                 int targetLeft, int targetTop, char sourceChar,
                                                 ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
        {
            if ((sourceForeColor < ConsoleColor.Black) || (sourceForeColor > ConsoleColor.White))
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), "sourceForeColor");
            }
            if ((sourceBackColor < ConsoleColor.Black) || (sourceBackColor > ConsoleColor.White))
            {
                throw new ArgumentException(Environment.GetResourceString("Arg_InvalidConsoleColor"), "sourceBackColor");
            }
            Win32Native.COORD dwSize = GetBufferInfo().dwSize;
            if ((sourceLeft < 0) || (sourceLeft > dwSize.X))
            {
                throw new ArgumentOutOfRangeException("sourceLeft", sourceLeft,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferBoundaries"));
            }
            if ((sourceTop < 0) || (sourceTop > dwSize.Y))
            {
                throw new ArgumentOutOfRangeException("sourceTop", sourceTop,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferBoundaries"));
            }
            if ((sourceWidth < 0) || (sourceWidth > (dwSize.X - sourceLeft)))
            {
                throw new ArgumentOutOfRangeException("sourceWidth", sourceWidth,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferBoundaries"));
            }
            if ((sourceHeight < 0) || (sourceTop > (dwSize.Y - sourceHeight)))
            {
                throw new ArgumentOutOfRangeException("sourceHeight", sourceHeight,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferBoundaries"));
            }
            if ((targetLeft < 0) || (targetLeft > dwSize.X))
            {
                throw new ArgumentOutOfRangeException("targetLeft", targetLeft,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferBoundaries"));
            }
            if ((targetTop < 0) || (targetTop > dwSize.Y))
            {
                throw new ArgumentOutOfRangeException("targetTop", targetTop,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferBoundaries"));
            }
            if ((sourceWidth != 0) && (sourceHeight != 0))
            {
                bool flag;
                Win32Native.CHAR_INFO[] char_infoArray3;
                new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
                Win32Native.CHAR_INFO[] char_infoArray = new Win32Native.CHAR_INFO[sourceWidth*sourceHeight];
                dwSize.X = (short) sourceWidth;
                dwSize.Y = (short) sourceHeight;
                Win32Native.COORD bufferCoord = new Win32Native.COORD();
                Win32Native.SMALL_RECT readRegion = new Win32Native.SMALL_RECT();
                readRegion.Left = (short) sourceLeft;
                readRegion.Right = (short) ((sourceLeft + sourceWidth) - 1);
                readRegion.Top = (short) sourceTop;
                readRegion.Bottom = (short) ((sourceTop + sourceHeight) - 1);
                fixed (Win32Native.CHAR_INFO* char_infoRef = char_infoArray)
                {
                    flag = Win32Native.ReadConsoleOutput(ConsoleOutputHandle, char_infoRef, dwSize, bufferCoord,
                                                         ref readRegion);
                }
                if (!flag)
                {
                    __Error.WinIOError();
                }
                Win32Native.COORD dwWriteCoord = new Win32Native.COORD();
                dwWriteCoord.X = (short) sourceLeft;
                Win32Native.Color color =
                    (Win32Native.Color)
                    ((short)
                     (ConsoleColorToColorAttribute(sourceBackColor, true) |
                      ConsoleColorToColorAttribute(sourceForeColor, false)));
                short wColorAttribute = (short) color;
                for (int i = sourceTop; i < (sourceTop + sourceHeight); i++)
                {
                    int num2;
                    dwWriteCoord.Y = (short) i;
                    if (
                        !Win32Native.FillConsoleOutputCharacter(ConsoleOutputHandle, sourceChar, sourceWidth,
                                                                dwWriteCoord, out num2))
                    {
                        __Error.WinIOError();
                    }
                    if (
                        !Win32Native.FillConsoleOutputAttribute(ConsoleOutputHandle, wColorAttribute, sourceWidth,
                                                                dwWriteCoord, out num2))
                    {
                        __Error.WinIOError();
                    }
                }
                Win32Native.SMALL_RECT writeRegion = new Win32Native.SMALL_RECT();
                writeRegion.Left = (short) targetLeft;
                writeRegion.Right = (short) (targetLeft + sourceWidth);
                writeRegion.Top = (short) targetTop;
                writeRegion.Bottom = (short) (targetTop + sourceHeight);
                if (((char_infoArray3 = char_infoArray) == null) || (char_infoArray3.Length == 0))
                {
                    char_infoRef2 = null;
                    goto Label_02C4;
                }
                fixed (Win32Native.CHAR_INFO* char_infoRef2 = char_infoArray3)
                {
                    Label_02C4:
                    flag = Win32Native.WriteConsoleOutput(ConsoleOutputHandle, char_infoRef2, dwSize, bufferCoord,
                                                          ref writeRegion);
                }
            }
        }

        /// <summary>Acquires the standard error stream.</summary>
        /// <returns>The standard error stream.</returns>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardError()
        {
            return OpenStandardError(0x100);
        }

        /// <summary>Acquires the standard error stream, which is set to a specified buffer size.</summary>
        /// <returns>The standard error stream.</returns>
        /// <param name="bufferSize">The internal stream buffer size. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">bufferSize is less than or equal to zero. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardError(int bufferSize)
        {
            if (bufferSize < 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize",
                                                      Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            return GetStandardFile(-12, FileAccess.Write, bufferSize);
        }

        /// <summary>Acquires the standard input stream.</summary>
        /// <returns>The standard input stream.</returns>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardInput()
        {
            return OpenStandardInput(0x100);
        }

        /// <summary>Acquires the standard input stream, which is set to a specified buffer size.</summary>
        /// <returns>The standard input stream.</returns>
        /// <param name="bufferSize">The internal stream buffer size. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">bufferSize is less than or equal to zero. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardInput(int bufferSize)
        {
            if (bufferSize < 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize",
                                                      Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            return GetStandardFile(-10, FileAccess.Read, bufferSize);
        }

        /// <summary>Acquires the standard output stream.</summary>
        /// <returns>The standard output stream.</returns>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardOutput()
        {
            return OpenStandardOutput(0x100);
        }

        /// <summary>Acquires the standard output stream, which is set to a specified buffer size.</summary>
        /// <returns>The standard output stream.</returns>
        /// <param name="bufferSize">The internal stream buffer size. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">bufferSize is less than or equal to zero. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static Stream OpenStandardOutput(int bufferSize)
        {
            if (bufferSize < 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize",
                                                      Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            return GetStandardFile(-11, FileAccess.Write, bufferSize);
        }

        /// <summary>Reads the next character from the standard input stream.</summary>
        /// <returns>The next character from the input stream, or negative one (-1) if there are currently no more characters to be read.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static int Read()
        {
            return In.Read();
        }

        /// <summary>Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.</summary>
        /// <returns>A <see cref="T:System.ConsoleKeyInfo"></see> object that describes the <see cref="T:System.ConsoleKey"></see> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo"></see> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers"></see> values, whether one or more SHIFT, ALT, or CTRL modifier keys was pressed simultaneously with the console key.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In"></see> property is redirected from some stream other than the console. -or-The Quick Console is enabled.</exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static ConsoleKeyInfo ReadKey()
        {
            return ReadKey(false);
        }

        /// <summary>Obtains the next character or function key pressed by the user. The pressed key is optionally displayed in the console window.</summary>
        /// <returns>A <see cref="T:System.ConsoleKeyInfo"></see> object that describes the <see cref="T:System.ConsoleKey"></see> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo"></see> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers"></see> values, whether one or more SHIFT, ALT, or CTRL modifier keys was pressed simultaneously with the console key.</returns>
        /// <param name="intercept">Determines whether to display the pressed key in the console window. true to not display the pressed key; otherwise, false. </param>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In"></see> property is redirected from some stream other than the console. -or-The Quick Console is enabled.</exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static ConsoleKeyInfo ReadKey(bool intercept)
        {
            Win32Native.InputRecord record;
            int numEventsRead = -1;
            if (_cachedInputRecord.eventType == 1)
            {
                record = _cachedInputRecord;
                if (_cachedInputRecord.keyEvent.repeatCount == 0)
                {
                    _cachedInputRecord.eventType = -1;
                }
                else
                {
                    _cachedInputRecord.keyEvent.repeatCount = (short) (_cachedInputRecord.keyEvent.repeatCount - 1);
                }
            }
            else
            {
                do
                {
                    if (!Win32Native.ReadConsoleInput(ConsoleInputHandle, out record, 1, out numEventsRead) ||
                        (numEventsRead == 0))
                    {
                        throw new InvalidOperationException(
                            Environment.GetResourceString("InvalidOperation_ConsoleReadKeyOnFile"));
                    }
                } while (!IsKeyDownEvent(record) ||
                         ((record.keyEvent.uChar == '\0') && IsModKey(record.keyEvent.virtualKeyCode)));
                if (record.keyEvent.repeatCount > 1)
                {
                    record.keyEvent.repeatCount = (short) (record.keyEvent.repeatCount - 1);
                    _cachedInputRecord = record;
                }
            }
            ControlKeyState controlKeyState = (ControlKeyState) record.keyEvent.controlKeyState;
            bool shift = (controlKeyState & ControlKeyState.ShiftPressed) != 0;
            bool alt = (controlKeyState & (ControlKeyState.LeftAltPressed | ControlKeyState.RightAltPressed)) != 0;
            bool control = (controlKeyState & (ControlKeyState.LeftCtrlPressed | ControlKeyState.RightCtrlPressed)) != 0;
            ConsoleKeyInfo info = new ConsoleKeyInfo(record.keyEvent.uChar, (ConsoleKey) record.keyEvent.virtualKeyCode,
                                                     shift, alt, control);
            if (!intercept)
            {
                Write(record.keyEvent.uChar);
            }
            return info;
        }

        /// <summary>Reads the next line of characters from the standard input stream.</summary>
        /// <returns>The next line of characters from the input stream, or null if no more lines are available.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line of characters is greater than <see cref="F:System.Int32.MaxValue"></see>.</exception>
        /// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static string ReadLine()
        {
            return In.ReadLine();
        }

        /// <summary>Sets the foreground and background console colors to their defaults.</summary>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static void ResetColor()
        {
            bool flag;
            new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
            GetBufferInfo(false, out flag);
            if (flag)
            {
                short attributes = _defaultColors;
                Win32Native.SetConsoleTextAttribute(ConsoleOutputHandle, attributes);
            }
        }

        /// <summary>Sets the height and width of the screen buffer area to the specified values.</summary>
        /// <param name="width">The width of the buffer area measured in columns. </param>
        /// <param name="height">The height of the buffer area measured in rows. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">height or width is less than or equal to zero.-or- height or width is greater than or equal to <see cref="F:System.Int16.MaxValue"></see>.-or- width is less than <see cref="P:System.Console.WindowLeft"></see> + <see cref="P:System.Console.WindowWidth"></see>.-or- height is less than <see cref="P:System.Console.WindowTop"></see> + <see cref="P:System.Console.WindowHeight"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static void SetBufferSize(int width, int height)
        {
            new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
            Win32Native.SMALL_RECT srWindow = GetBufferInfo().srWindow;
            if ((width < (srWindow.Right + 1)) || (width >= 0x7fff))
            {
                throw new ArgumentOutOfRangeException("width", width,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
            }
            if ((height < (srWindow.Bottom + 1)) || (height >= 0x7fff))
            {
                throw new ArgumentOutOfRangeException("height", height,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferLessThanWindowSize"));
            }
            Win32Native.COORD size = new Win32Native.COORD();
            size.X = (short) width;
            size.Y = (short) height;
            if (!Win32Native.SetConsoleScreenBufferSize(ConsoleOutputHandle, size))
            {
                __Error.WinIOError();
            }
        }

        /// <summary>Sets the position of the cursor.</summary>
        /// <param name="left">The column position of the cursor. </param>
        /// <param name="top">The row position of the cursor. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">left or top is less than zero.-or- left is greater than or equal to <see cref="P:System.Console.BufferWidth"></see>.-or- top is greater than or equal to <see cref="P:System.Console.BufferHeight"></see>. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static void SetCursorPosition(int left, int top)
        {
            if ((left < 0) || (left >= 0x7fff))
            {
                throw new ArgumentOutOfRangeException("left", left,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferBoundaries"));
            }
            if ((top < 0) || (top >= 0x7fff))
            {
                throw new ArgumentOutOfRangeException("top", top,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleBufferBoundaries"));
            }
            new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
            IntPtr consoleOutputHandle = ConsoleOutputHandle;
            Win32Native.COORD cursorPosition = new Win32Native.COORD();
            cursorPosition.X = (short) left;
            cursorPosition.Y = (short) top;
            if (!Win32Native.SetConsoleCursorPosition(consoleOutputHandle, cursorPosition))
            {
                int errorCode = Marshal.GetLastWin32Error();
                Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo();
                if ((left < 0) || (left >= bufferInfo.dwSize.X))
                {
                    throw new ArgumentOutOfRangeException("left", left,
                                                          Environment.GetResourceString(
                                                              "ArgumentOutOfRange_ConsoleBufferBoundaries"));
                }
                if ((top < 0) || (top >= bufferInfo.dwSize.Y))
                {
                    throw new ArgumentOutOfRangeException("top", top,
                                                          Environment.GetResourceString(
                                                              "ArgumentOutOfRange_ConsoleBufferBoundaries"));
                }
                __Error.WinIOError(errorCode, string.Empty);
            }
        }

        /// <summary>Sets the <see cref="P:System.Console.Error"></see> property to the specified <see cref="T:System.IO.TextWriter"></see> object.</summary>
        /// <param name="newError">A <see cref="T:System.IO.TextWriter"></see> stream that is the new standard error output. </param>
        /// <exception cref="T:System.ArgumentNullException">newError is null. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void SetError(TextWriter newError)
        {
            if (newError == null)
            {
                throw new ArgumentNullException("newError");
            }
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
            _wasErrorRedirected = true;
            newError = TextWriter.Synchronized(newError);
            lock (InternalSyncObject)
            {
                _error = newError;
            }
        }

        /// <summary>Sets the <see cref="P:System.Console.In"></see> property to the specified <see cref="T:System.IO.TextReader"></see> object.</summary>
        /// <param name="newIn">A <see cref="T:System.IO.TextReader"></see> stream that is the new standard input. </param>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <exception cref="T:System.ArgumentNullException">newIn is null. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void SetIn(TextReader newIn)
        {
            if (newIn == null)
            {
                throw new ArgumentNullException("newIn");
            }
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
            newIn = TextReader.Synchronized(newIn);
            lock (InternalSyncObject)
            {
                _in = newIn;
            }
        }

        /// <summary>Sets the <see cref="P:System.Console.Out"></see> property to the specified <see cref="T:System.IO.TextWriter"></see> object.</summary>
        /// <param name="newOut">A <see cref="T:System.IO.TextWriter"></see> stream that is the new standard output. </param>
        /// <exception cref="T:System.ArgumentNullException">newOut is null. </exception>
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" /></PermissionSet>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void SetOut(TextWriter newOut)
        {
            if (newOut == null)
            {
                throw new ArgumentNullException("newOut");
            }
            new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
            _wasOutRedirected = true;
            newOut = TextWriter.Synchronized(newOut);
            lock (InternalSyncObject)
            {
                _out = newOut;
            }
        }

        /// <summary>Sets the position of the console window relative to the screen buffer.</summary>
        /// <param name="left">The column position of the upper left  corner of the console window. </param>
        /// <param name="top">The row position of the upper left corner of the console window. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">left or top is less than zero.-or- left + <see cref="P:System.Console.WindowWidth"></see> is greater than <see cref="P:System.Console.BufferWidth"></see>.-or- top + <see cref="P:System.Console.WindowHeight"></see> is greater than <see cref="P:System.Console.BufferHeight"></see>. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static unsafe void SetWindowPosition(int left, int top)
        {
            new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
            Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo();
            Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
            int num = ((left + srWindow.Right) - srWindow.Left) + 1;
            if (((left < 0) || (num > bufferInfo.dwSize.X)) || (num < 0))
            {
                throw new ArgumentOutOfRangeException("left", left,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleWindowPos"));
            }
            int num2 = ((top + srWindow.Bottom) - srWindow.Top) + 1;
            if (((top < 0) || (num2 > bufferInfo.dwSize.Y)) || (num2 < 0))
            {
                throw new ArgumentOutOfRangeException("top", top,
                                                      Environment.GetResourceString(
                                                          "ArgumentOutOfRange_ConsoleWindowPos"));
            }
            srWindow.Bottom = (short) (srWindow.Bottom - ((short) (srWindow.Top - top)));
            srWindow.Right = (short) (srWindow.Right - ((short) (srWindow.Left - left)));
            srWindow.Left = (short) left;
            srWindow.Top = (short) top;
            if (!Win32Native.SetConsoleWindowInfo(ConsoleOutputHandle, true, &srWindow))
            {
                __Error.WinIOError();
            }
        }

        /// <summary>Sets the height and width of the console window to the specified values.</summary>
        /// <param name="width">The width of the console window measured in columns. </param>
        /// <param name="height">The height of the console window measured in rows. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">width or height is less than or equal to zero.-or- width plus <see cref="P:System.Console.WindowLeft"></see> or height plus <see cref="P:System.Console.WindowTop"></see> is greater than or equal to <see cref="F:System.Int16.MaxValue"></see>. -or-width or height is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. -or-Debugging with the Quick Console is enabled.</exception>
        /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" /></PermissionSet>
        public static unsafe void SetWindowSize(int width, int height)
        {
            new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
            Win32Native.CONSOLE_SCREEN_BUFFER_INFO bufferInfo = GetBufferInfo();
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException("width", width,
                                                      Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
            }
            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException("height", height,
                                                      Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
            }
            bool flag2 = false;
            Win32Native.COORD size = new Win32Native.COORD();
            size.X = bufferInfo.dwSize.X;
            size.Y = bufferInfo.dwSize.Y;
            if (bufferInfo.dwSize.X < (bufferInfo.srWindow.Left + width))
            {
                if (bufferInfo.srWindow.Left >= (0x7fff - width))
                {
                    throw new ArgumentOutOfRangeException("width",
                                                          Environment.GetResourceString(
                                                              "ArgumentOutOfRange_ConsoleWindowBufferSize"));
                }
                size.X = (short) (bufferInfo.srWindow.Left + width);
                flag2 = true;
            }
            if (bufferInfo.dwSize.Y < (bufferInfo.srWindow.Top + height))
            {
                if (bufferInfo.srWindow.Top >= (0x7fff - height))
                {
                    throw new ArgumentOutOfRangeException("height",
                                                          Environment.GetResourceString(
                                                              "ArgumentOutOfRange_ConsoleWindowBufferSize"));
                }
                size.Y = (short) (bufferInfo.srWindow.Top + height);
                flag2 = true;
            }
            if (flag2 && !Win32Native.SetConsoleScreenBufferSize(ConsoleOutputHandle, size))
            {
                __Error.WinIOError();
            }
            Win32Native.SMALL_RECT srWindow = bufferInfo.srWindow;
            srWindow.Bottom = (short) ((srWindow.Top + height) - 1);
            srWindow.Right = (short) ((srWindow.Left + width) - 1);
            if (!Win32Native.SetConsoleWindowInfo(ConsoleOutputHandle, true, &srWindow))
            {
                int errorCode = Marshal.GetLastWin32Error();
                if (flag2)
                {
                    Win32Native.SetConsoleScreenBufferSize(ConsoleOutputHandle, bufferInfo.dwSize);
                }
                Win32Native.COORD largestConsoleWindowSize = Win32Native.GetLargestConsoleWindowSize(ConsoleOutputHandle);
                if (width > largestConsoleWindowSize.X)
                {
                    throw new ArgumentOutOfRangeException("width", width,
                                                          Environment.GetResourceString(
                                                              "ArgumentOutOfRange_ConsoleWindowSize_Size",
                                                              new object[] {largestConsoleWindowSize.X}));
                }
                if (height > largestConsoleWindowSize.Y)
                {
                    throw new ArgumentOutOfRangeException("height", height,
                                                          Environment.GetResourceString(
                                                              "ArgumentOutOfRange_ConsoleWindowSize_Size",
                                                              new object[] {largestConsoleWindowSize.Y}));
                }
                __Error.WinIOError(errorCode, string.Empty);
            }
        }

        /// <summary>Writes the text representation of the specified Boolean value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(bool value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the specified Unicode character value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(char value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the text representation of the specified <see cref="T:System.Decimal"></see> value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(decimal value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the text representation of the specified double-precision floating-point value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(double value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the text representation of the specified 32-bit signed integer value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(int value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the text representation of the specified 64-bit signed integer value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(long value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the text representation of the specified object to the standard output stream.</summary>
        /// <param name="value">The value to write, or null. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(object value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the text representation of the specified single-precision floating-point value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(float value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the specified string value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the text representation of the specified 32-bit unsigned integer value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(uint value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the text representation of the specified 64-bit unsigned integer value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(ulong value)
        {
            Out.Write(value);
        }

        /// <summary>Writes the specified array of Unicode characters to the standard output stream.</summary>
        /// <param name="buffer">A Unicode character array. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(char[] buffer)
        {
            Out.Write(buffer);
        }

        /// <summary>Writes the text representation of the specified object to the standard output stream using the specified format information.</summary>
        /// <param name="arg0">Object to write using format. </param>
        /// <param name="format">The format string. </param>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, object arg0)
        {
            Out.Write(format, arg0);
        }

        /// <summary>Writes the text representation of the specified array of objects to the standard output stream using the specified format information.</summary>
        /// <param name="arg">An array of objects to write using format. </param>
        /// <param name="format">The format string. </param>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.ArgumentNullException">format or arg is null. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, params object[] arg)
        {
            Out.Write(format, arg);
        }

        /// <summary>Writes the text representation of the specified objects to the standard output stream using the specified format information.</summary>
        /// <param name="arg0">The first object to write using format. </param>
        /// <param name="arg1">The second object to write using format. </param>
        /// <param name="format">The format string. </param>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, object arg0, object arg1)
        {
            Out.Write(format, arg0, arg1);
        }

        /// <summary>Writes the specified subarray of Unicode characters to the standard output stream.</summary>
        /// <param name="count">The number of characters to write. </param>
        /// <param name="buffer">An array of Unicode characters. </param>
        /// <param name="index">The starting position in buffer. </param>
        /// <exception cref="T:System.ArgumentNullException">buffer is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index or count is less than zero. </exception>
        /// <exception cref="T:System.ArgumentException">index plus count specify a position that is not within buffer. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(char[] buffer, int index, int count)
        {
            Out.Write(buffer, index, count);
        }

        /// <summary>Writes the text representation of the specified objects to the standard output stream using the specified format information.</summary>
        /// <param name="arg2">The third object to write using format. </param>
        /// <param name="arg0">The first object to write using format. </param>
        /// <param name="arg1">The second object to write using format. </param>
        /// <param name="format">The format string. </param>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, object arg0, object arg1, object arg2)
        {
            Out.Write(format, arg0, arg1, arg2);
        }

        [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist )
        {
            ArgIterator iterator = new ArgIterator(__arglist);
            int num = iterator.GetRemainingCount() + 4;
            object[] arg = new object[num];
            arg[0] = arg0;
            arg[1] = arg1;
            arg[2] = arg2;
            arg[3] = arg3;
            for (int i = 4; i < num; i++)
            {
                arg[i] = TypedReference.ToObject(iterator.GetNextArg());
            }
            Out.Write(format, arg);
        }

        /// <summary>Writes the current line terminator to the standard output stream.</summary>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine()
        {
            Out.WriteLine();
        }

        /// <summary>Writes the text representation of the specified Boolean value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(bool value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the specified array of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="buffer">A Unicode character array. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(char[] buffer)
        {
            Out.WriteLine(buffer);
        }

        /// <summary>Writes the specified Unicode character, followed by the current line terminator, value to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(char value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified <see cref="T:System.Decimal"></see> value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(decimal value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified double-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(double value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified 32-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(int value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified 64-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(long value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(object value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified single-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(float value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the specified string value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified 32-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(uint value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified 64-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(ulong value)
        {
            Out.WriteLine(value);
        }

        /// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
        /// <param name="arg0">Object to write using format. </param>
        /// <param name="format">The format string. </param>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, object arg0)
        {
            Out.WriteLine(format, arg0);
        }

        /// <summary>Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
        /// <param name="arg">An array of objects to write using format. </param>
        /// <param name="format">The format string. </param>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.ArgumentNullException">format or arg is null. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, params object[] arg)
        {
            Out.WriteLine(format, arg);
        }

        /// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
        /// <param name="arg0">The first object to write using format. </param>
        /// <param name="arg1">The second object to write using format. </param>
        /// <param name="format">The format string. </param>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, object arg0, object arg1)
        {
            Out.WriteLine(format, arg0, arg1);
        }

        /// <summary>Writes the specified subarray of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="count">The number of characters to write. </param>
        /// <param name="buffer">An array of Unicode characters. </param>
        /// <param name="index">The starting position in buffer. </param>
        /// <exception cref="T:System.ArgumentNullException">buffer is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index or count is less than zero. </exception>
        /// <exception cref="T:System.ArgumentException">index plus count specify a position that is not within buffer. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(char[] buffer, int index, int count)
        {
            Out.WriteLine(buffer, index, count);
        }

        /// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
        /// <param name="arg2">The third object to write using format. </param>
        /// <param name="arg0">The first object to write using format. </param>
        /// <param name="arg1">The second object to write using format. </param>
        /// <param name="format">The format string. </param>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid. </exception>
        /// <filterpriority>1</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Out.WriteLine(format, arg0, arg1, arg2);
        }

        [CLSCompliant(false), HostProtection(SecurityAction.LinkDemand, UI = true)]
        public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist )
        {
            ArgIterator iterator = new ArgIterator(__arglist);
            int num = iterator.GetRemainingCount() + 4;
            object[] arg = new object[num];
            arg[0] = arg0;
            arg[1] = arg1;
            arg[2] = arg2;
            arg[3] = arg3;
            for (int i = 4; i < num; i++)
            {
                arg[i] = TypedReference.ToObject(iterator.GetNextArg());
            }
            Out.WriteLine(format, arg);
        }

        #region Nested type: ControlCDelegateData

        private sealed class ControlCDelegateData
        {
            internal bool Cancel;
            internal ConsoleCancelEventHandler CancelCallbacks;
            internal ManualResetEvent CompletionEvent;
            internal ConsoleSpecialKey ControlKey;
            internal bool DelegateStarted;

            internal ControlCDelegateData(ConsoleSpecialKey controlKey, ConsoleCancelEventHandler cancelCallbacks)
            {
                this.ControlKey = controlKey;
                this.CancelCallbacks = cancelCallbacks;
                this.CompletionEvent = new ManualResetEvent(false);
            }
        }

        #endregion

        #region Nested type: ControlCHooker

        internal sealed class ControlCHooker : CriticalFinalizerObject
        {
            private Win32Native.ConsoleCtrlHandlerRoutine _handler =
                new Win32Native.ConsoleCtrlHandlerRoutine(Console.BreakEvent);

            private bool _hooked;

            internal ControlCHooker()
            {
            }

            ~ControlCHooker()
            {
                this.Unhook();
            }

            internal void Hook()
            {
                if (!this._hooked)
                {
                    if (!Win32Native.SetConsoleCtrlHandler(this._handler, true))
                    {
                        __Error.WinIOError();
                    }
                    this._hooked = true;
                }
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            internal void Unhook()
            {
                if (this._hooked)
                {
                    if (!Win32Native.SetConsoleCtrlHandler(this._handler, false))
                    {
                        __Error.WinIOError();
                    }
                    this._hooked = false;
                }
            }
        }

        #endregion

        #region Nested type: ControlKeyState

        [Flags]
        internal enum ControlKeyState
        {
            CapsLockOn = 0x80,
            EnhancedKey = 0x100,
            LeftAltPressed = 2,
            LeftCtrlPressed = 8,
            NumLockOn = 0x20,
            RightAltPressed = 1,
            RightCtrlPressed = 4,
            ScrollLockOn = 0x40,
            ShiftPressed = 0x10
        }

        #endregion
    }
}