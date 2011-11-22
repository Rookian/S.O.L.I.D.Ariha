namespace System.IO
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Text;
    using System.Threading;

    /// <summary>Represents a writer that can write a sequential series of characters. This class is abstract.</summary>
    /// <filterpriority>2</filterpriority>
    [Serializable, ComVisible(true)]
    public abstract class TextWriter : MarshalByRefObject, IDisposable
    {
        private const string InitialNewLine = "\r\n";

        /// <summary>Provides a TextWriter with no backing store that can be written to, but not read from.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly TextWriter Null = new NullTextWriter();

        /// <summary>Stores the new line characters used for this TextWriter.</summary>
        protected char[] CoreNewLine;

        private IFormatProvider InternalFormatProvider;

        /// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter"></see> class.</summary>
        protected TextWriter()
        {
            this.CoreNewLine = new char[] {'\r', '\n'};
            this.InternalFormatProvider = null;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.IO.TextWriter"></see> class with the specified format provider.</summary>
        /// <param name="formatProvider">An <see cref="T:System.IFormatProvider"></see> object that controls formatting. </param>
        protected TextWriter(IFormatProvider formatProvider)
        {
            this.CoreNewLine = new char[] {'\r', '\n'};
            this.InternalFormatProvider = formatProvider;
        }

        /// <summary>When overridden in a derived class, returns the <see cref="T:System.Text.Encoding"></see> in which the output is written.</summary>
        /// <returns>The Encoding in which the output is written.</returns>
        /// <filterpriority>1</filterpriority>
        public abstract System.Text.Encoding Encoding { get; }

        /// <summary>Gets an object that controls formatting.</summary>
        /// <returns>An <see cref="T:System.IFormatProvider"></see> object for a specific culture, or the formatting of the current culture if no other culture is specified.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual IFormatProvider FormatProvider
        {
            get
            {
                if (this.InternalFormatProvider == null)
                {
                    return Thread.CurrentThread.CurrentCulture;
                }
                return this.InternalFormatProvider;
            }
        }

        /// <summary>Gets or sets the line terminator string used by the current TextWriter.</summary>
        /// <returns>The line terminator string for the current TextWriter.</returns>
        /// <filterpriority>2</filterpriority>
        public virtual string NewLine
        {
            get { return new string(this.CoreNewLine); }
            set
            {
                if (value == null)
                {
                    value = "\r\n";
                }
                this.CoreNewLine = value.ToCharArray();
            }
        }

        #region IDisposable Members

        /// <summary>Releases all resources used by the <see cref="T:System.IO.TextWriter"></see> object.</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>Closes the current writer and releases any system resources associated with the writer.</summary>
        /// <filterpriority>1</filterpriority>
        public virtual void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextWriter"></see> and optionally releases the managed resources.</summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
        /// <filterpriority>1</filterpriority>
        public virtual void Flush()
        {
        }

        /// <summary>Creates a thread-safe wrapper around the specified TextWriter.</summary>
        /// <returns>A thread-safe wrapper.</returns>
        /// <param name="writer">The TextWriter to synchronize. </param>
        /// <exception cref="T:System.ArgumentNullException">writer is null. </exception>
        /// <filterpriority>2</filterpriority>
        [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
        public static TextWriter Synchronized(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (writer is SyncTextWriter)
            {
                return writer;
            }
            return new SyncTextWriter(writer);
        }

        /// <summary>Writes the text representation of a Boolean value to the text stream.</summary>
        /// <param name="value">The Boolean to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(bool value)
        {
            this.Write(value ? "True" : "False");
        }

        /// <summary>Writes a character array to the text stream.</summary>
        /// <param name="buffer">The character array to write to the text stream. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(char[] buffer)
        {
            if (buffer != null)
            {
                this.Write(buffer, 0, buffer.Length);
            }
        }

        /// <summary>Writes a character to the text stream.</summary>
        /// <param name="value">The character to write to the text stream. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(char value)
        {
        }

        /// <summary>Writes the text representation of a decimal value followed by a line terminator to the text stream.</summary>
        /// <param name="value">The decimal value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(decimal value)
        {
            this.Write(value.ToString(this.FormatProvider));
        }

        /// <summary>Writes the text representation of an 8-byte floating-point value to the text stream.</summary>
        /// <param name="value">The 8-byte floating-point value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(double value)
        {
            this.Write(value.ToString(this.FormatProvider));
        }

        /// <summary>Writes the text representation of a 4-byte signed integer to the text stream.</summary>
        /// <param name="value">The 4-byte signed integer to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(int value)
        {
            this.Write(value.ToString(this.FormatProvider));
        }

        /// <summary>Writes the text representation of an 8-byte signed integer to the text stream.</summary>
        /// <param name="value">The 8-byte signed integer to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(long value)
        {
            this.Write(value.ToString(this.FormatProvider));
        }

        /// <summary>Writes the text representation of an object to the text stream by calling ToString on that object.</summary>
        /// <param name="value">The object to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(object value)
        {
            if (value != null)
            {
                IFormattable formattable = value as IFormattable;
                if (formattable != null)
                {
                    this.Write(formattable.ToString(null, this.FormatProvider));
                }
                else
                {
                    this.Write(value.ToString());
                }
            }
        }

        /// <summary>Writes the text representation of a 4-byte floating-point value to the text stream.</summary>
        /// <param name="value">The 4-byte floating-point value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(float value)
        {
            this.Write(value.ToString(this.FormatProvider));
        }

        /// <summary>Writes a string to the text stream.</summary>
        /// <param name="value">The string to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(string value)
        {
            if (value != null)
            {
                this.Write(value.ToCharArray());
            }
        }

        /// <summary>Writes the text representation of a 4-byte unsigned integer to the text stream.</summary>
        /// <param name="value">The 4-byte unsigned integer to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        [CLSCompliant(false)]
        public virtual void Write(uint value)
        {
            this.Write(value.ToString(this.FormatProvider));
        }

        /// <summary>Writes the text representation of an 8-byte unsigned integer to the text stream.</summary>
        /// <param name="value">The 8-byte unsigned integer to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        [CLSCompliant(false)]
        public virtual void Write(ulong value)
        {
            this.Write(value.ToString(this.FormatProvider));
        }

        /// <summary>Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"></see>.</summary>
        /// <param name="arg0">An object to write into the formatted string. </param>
        /// <param name="format">The formatting string. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(string format, object arg0)
        {
            this.Write(string.Format(this.FormatProvider, format, new object[] {arg0}));
        }

        /// <summary>Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"></see>.</summary>
        /// <param name="arg">The object array to write into the formatted string. </param>
        /// <param name="format">The formatting string. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to arg. Length. </exception>
        /// <exception cref="T:System.ArgumentNullException">format or arg is null. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(string format, params object[] arg)
        {
            this.Write(string.Format(this.FormatProvider, format, arg));
        }

        /// <summary>Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"></see>.</summary>
        /// <param name="arg0">An object to write into the formatted string. </param>
        /// <param name="arg1">An object to write into the formatted string. </param>
        /// <param name="format">The formatting string. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(string format, object arg0, object arg1)
        {
            this.Write(string.Format(this.FormatProvider, format, new object[] {arg0, arg1}));
        }

        /// <summary>Writes a subarray of characters to the text stream.</summary>
        /// <param name="count">The number of characters to write. </param>
        /// <param name="buffer">The character array to write data from. </param>
        /// <param name="index">Starting index in the buffer. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index or count is negative. </exception>
        /// <exception cref="T:System.ArgumentException">The buffer length minus index is less than count. </exception>
        /// <exception cref="T:System.ArgumentNullException">The buffer parameter is null. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(char[] buffer, int index, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index",
                                                      Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count",
                                                      Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
            }
            if ((buffer.Length - index) < count)
            {
                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
            }
            for (int i = 0; i < count; i++)
            {
                this.Write(buffer[index + i]);
            }
        }

        /// <summary>Writes out a formatted string, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"></see>.</summary>
        /// <param name="arg2">An object to write into the formatted string. </param>
        /// <param name="arg0">An object to write into the formatted string. </param>
        /// <param name="arg1">An object to write into the formatted string. </param>
        /// <param name="format">The formatting string. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void Write(string format, object arg0, object arg1, object arg2)
        {
            this.Write(string.Format(this.FormatProvider, format, new object[] {arg0, arg1, arg2}));
        }

        /// <summary>Writes a line terminator to the text stream.</summary>
        /// <returns>The default line terminator is a carriage return followed by a line feed ("\r\n"), but this value can be changed using the <see cref="P:System.IO.TextWriter.NewLine"></see> property.</returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine()
        {
            this.Write(this.CoreNewLine);
        }

        /// <summary>Writes the text representation of a Boolean followed by a line terminator to the text stream.</summary>
        /// <param name="value">The Boolean to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(bool value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes an array of characters followed by a line terminator to the text stream.</summary>
        /// <param name="buffer">The character array from which data is read. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(char[] buffer)
        {
            this.Write(buffer);
            this.WriteLine();
        }

        /// <summary>Writes a character followed by a line terminator to the text stream.</summary>
        /// <param name="value">The character to write to the text stream. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(char value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes the text representation of a decimal value followed by a line terminator to the text stream.</summary>
        /// <param name="value">The decimal value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(decimal value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes the text representation of a 8-byte floating-point value followed by a line terminator to the text stream.</summary>
        /// <param name="value">The 8-byte floating-point value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(double value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes the text representation of a 4-byte signed integer followed by a line terminator to the text stream.</summary>
        /// <param name="value">The 4-byte signed integer to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(int value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes the text representation of an 8-byte signed integer followed by a line terminator to the text stream.</summary>
        /// <param name="value">The 8-byte signed integer to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(long value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes the text representation of an object by calling ToString on this object, followed by a line terminator to the text stream.</summary>
        /// <param name="value">The object to write. If value is null, only the line termination characters are written. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(object value)
        {
            if (value == null)
            {
                this.WriteLine();
            }
            else
            {
                IFormattable formattable = value as IFormattable;
                if (formattable != null)
                {
                    this.WriteLine(formattable.ToString(null, this.FormatProvider));
                }
                else
                {
                    this.WriteLine(value.ToString());
                }
            }
        }

        /// <summary>Writes the text representation of a 4-byte floating-point value followed by a line terminator to the text stream.</summary>
        /// <param name="value">The 4-byte floating-point value to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(float value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes a string followed by a line terminator to the text stream.</summary>
        /// <param name="value">The string to write. If value is null, only the line termination characters are written. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(string value)
        {
            if (value == null)
            {
                this.WriteLine();
            }
            else
            {
                int length = value.Length;
                int num2 = this.CoreNewLine.Length;
                char[] destination = new char[length + num2];
                value.CopyTo(0, destination, 0, length);
                switch (num2)
                {
                    case 2:
                        destination[length] = this.CoreNewLine[0];
                        destination[length + 1] = this.CoreNewLine[1];
                        break;

                    case 1:
                        destination[length] = this.CoreNewLine[0];
                        break;

                    default:
                        Buffer.InternalBlockCopy(this.CoreNewLine, 0, destination, length*2, num2*2);
                        break;
                }
                this.Write(destination, 0, length + num2);
            }
        }

        /// <summary>Writes the text representation of a 4-byte unsigned integer followed by a line terminator to the text stream.</summary>
        /// <param name="value">The 4-byte unsigned integer to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        [CLSCompliant(false)]
        public virtual void WriteLine(uint value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes the text representation of an 8-byte unsigned integer followed by a line terminator to the text stream.</summary>
        /// <param name="value">The 8-byte unsigned integer to write. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        [CLSCompliant(false)]
        public virtual void WriteLine(ulong value)
        {
            this.Write(value);
            this.WriteLine();
        }

        /// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"></see>.</summary>
        /// <param name="arg0">The object to write into the formatted string. </param>
        /// <param name="format">The formatted string. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(string format, object arg0)
        {
            this.WriteLine(string.Format(this.FormatProvider, format, new object[] {arg0}));
        }

        /// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"></see>.</summary>
        /// <param name="arg">The object array to write into format string. </param>
        /// <param name="format">The formatting string. </param>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to arg.Length. </exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <exception cref="T:System.ArgumentNullException">A string or object is passed in as null. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(string format, params object[] arg)
        {
            this.WriteLine(string.Format(this.FormatProvider, format, arg));
        }

        /// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"></see>.</summary>
        /// <param name="arg0">The object to write into the format string. </param>
        /// <param name="arg1">The object to write into the format string. </param>
        /// <param name="format">The formatting string. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(string format, object arg0, object arg1)
        {
            this.WriteLine(string.Format(this.FormatProvider, format, new object[] {arg0, arg1}));
        }

        /// <summary>Writes a subarray of characters followed by a line terminator to the text stream.</summary>
        /// <returns>Characters are read from buffer beginning at index and ending at index + count.</returns>
        /// <param name="count">The maximum number of characters to write. </param>
        /// <param name="buffer">The character array from which data is read. </param>
        /// <param name="index">The index into buffer at which to begin reading. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">index or count is negative. </exception>
        /// <exception cref="T:System.ArgumentException">The buffer length minus index is less than count. </exception>
        /// <exception cref="T:System.ArgumentNullException">The buffer parameter is null. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(char[] buffer, int index, int count)
        {
            this.Write(buffer, index, count);
            this.WriteLine();
        }

        /// <summary>Writes out a formatted string and a new line, using the same semantics as <see cref="M:System.String.Format(System.String,System.Object)"></see>.</summary>
        /// <param name="arg2">The object to write into the format string. </param>
        /// <param name="arg0">The object to write into the format string. </param>
        /// <param name="arg1">The object to write into the format string. </param>
        /// <param name="format">The formatting string. </param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
        /// <exception cref="T:System.ArgumentNullException">format is null. </exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextWriter"></see> is closed. </exception>
        /// <exception cref="T:System.FormatException">The format specification in format is invalid.-or- The number indicating an argument to be formatted is less than zero, or larger than or equal to the number of provided objects to be formatted. </exception>
        /// <filterpriority>1</filterpriority>
        public virtual void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            this.WriteLine(string.Format(this.FormatProvider, format, new object[] {arg0, arg1, arg2}));
        }

        #region Nested type: NullTextWriter

        [Serializable]
        private sealed class NullTextWriter : TextWriter
        {
            internal NullTextWriter() : base(CultureInfo.InvariantCulture)
            {
            }

            public override System.Text.Encoding Encoding
            {
                get { return System.Text.Encoding.Default; }
            }

            public override void Write(string value)
            {
            }

            public override void Write(char[] buffer, int index, int count)
            {
            }

            public override void WriteLine()
            {
            }

            public override void WriteLine(object value)
            {
            }

            public override void WriteLine(string value)
            {
            }
        }

        #endregion

        #region Nested type: SyncTextWriter

        [Serializable]
        internal sealed class SyncTextWriter : TextWriter, IDisposable
        {
            private TextWriter _out;

            internal SyncTextWriter(TextWriter t) : base(t.FormatProvider)
            {
                this._out = t;
            }

            public override System.Text.Encoding Encoding
            {
                get { return this._out.Encoding; }
            }

            public override IFormatProvider FormatProvider
            {
                get { return this._out.FormatProvider; }
            }

            public override string NewLine
            {
                [MethodImpl(MethodImplOptions.Synchronized)]
                get { return this._out.NewLine; }
                [MethodImpl(MethodImplOptions.Synchronized)]
                set { this._out.NewLine = value; }
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Close()
            {
                this._out.Close();
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this._out.Dispose();
                }
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Flush()
            {
                this._out.Flush();
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(bool value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(char[] buffer)
            {
                this._out.Write(buffer);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(char value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(decimal value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(double value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(int value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(long value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(object value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(float value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(string value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(uint value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(ulong value)
            {
                this._out.Write(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(string format, object[] arg)
            {
                this._out.Write(format, arg);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(string format, object arg0)
            {
                this._out.Write(format, arg0);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(string format, object arg0, object arg1)
            {
                this._out.Write(format, arg0, arg1);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(char[] buffer, int index, int count)
            {
                this._out.Write(buffer, index, count);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void Write(string format, object arg0, object arg1, object arg2)
            {
                this._out.Write(format, arg0, arg1, arg2);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine()
            {
                this._out.WriteLine();
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(char value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(decimal value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(char[] buffer)
            {
                this._out.WriteLine(buffer);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(bool value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(double value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(int value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(long value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(object value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(float value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(string value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(uint value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(ulong value)
            {
                this._out.WriteLine(value);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(string format, object arg0)
            {
                this._out.WriteLine(format, arg0);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(string format, object[] arg)
            {
                this._out.WriteLine(format, arg);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(string format, object arg0, object arg1)
            {
                this._out.WriteLine(format, arg0, arg1);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(char[] buffer, int index, int count)
            {
                this._out.WriteLine(buffer, index, count);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public override void WriteLine(string format, object arg0, object arg1, object arg2)
            {
                this._out.WriteLine(format, arg0, arg1, arg2);
            }
        }

        #endregion
    }
}