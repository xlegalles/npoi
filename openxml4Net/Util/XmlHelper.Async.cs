using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NPOI.OpenXml4Net.Util
{
    public static partial class XmlHelper
    {
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, bool value, CancellationToken cancellationToken = default)
        {
            return WriteAttributeAsync(sw, attributeName, value, true, cancellationToken:cancellationToken);
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, bool value, bool writeIfBlank, bool defaultValue = false, CancellationToken cancellationToken = default)
        {
            if (value == defaultValue && !writeIfBlank)
                return Task.CompletedTask;
            return WriteAttributeAsync(sw, attributeName, value ? "1" : "0", cancellationToken);
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, double value, CancellationToken cancellationToken = default)
        {
            return WriteAttributeAsync(sw, attributeName, value, false, cancellationToken);
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, double value, bool writeIfBlank, CancellationToken cancellationToken = default)
        {
            if (value == 0.0 && !writeIfBlank)
                return Task.CompletedTask;
            return WriteAttributeAsync(sw, attributeName, value == 0.0 ? "0" : value.ToString(CultureInfo.InvariantCulture), cancellationToken);
        }
        public static async Task WriteAttributeAsync(StreamWriter sw, string attributeName, int value, bool writeIfBlank, CancellationToken cancellationToken = default)
        {
            if (value == 0 && !writeIfBlank)
                return;
            cancellationToken.ThrowIfCancellationRequested();
            
            await sw.WriteAsync(" ");
            await sw.WriteAsync(attributeName);
            await sw.WriteAsync("=\"");
            await sw.WriteAsync(value.ToString(sw.FormatProvider));
            await sw.WriteAsync("\"");
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, int value, int defaultValue, CancellationToken cancellationToken = default)
        {
            if(value == defaultValue)
                return Task.CompletedTask;

            return WriteAttributeAsync(sw, attributeName, value.ToString(CultureInfo.InvariantCulture), cancellationToken);
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, int value, CancellationToken cancellationToken = default)
        {
            return WriteAttributeAsync(sw, attributeName, value, false, cancellationToken);
        }
        public static async Task WriteAttributeAsync(StreamWriter sw, string attributeName, uint value, bool writeIfBlank, CancellationToken cancellationToken = default)
        {
            if (value == 0 && !writeIfBlank)
                return ;
            cancellationToken.ThrowIfCancellationRequested();

            await sw.WriteAsync(" ");
            await sw.WriteAsync(attributeName);
            await sw.WriteAsync("=\"");
            await sw.WriteAsync(value.ToString(sw.FormatProvider));
            await sw.WriteAsync("\"");
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, uint value, CancellationToken cancellationToken = default)
        {
            return WriteAttributeAsync(sw, attributeName, value, false, cancellationToken);
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, string value, CancellationToken cancellationToken = default)
        {
            return WriteAttributeAsync(sw, attributeName, value, false, cancellationToken);
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, string value, bool writeIfBlank, CancellationToken cancellationToken = default)
        {
            return WriteAttributeAsync(sw, attributeName, value, writeIfBlank, string.Empty, cancellationToken);
        }
        public static async Task WriteAttributeAsync(StreamWriter sw, string attributeName, string value, bool writeIfBlank, string defaultValue, CancellationToken cancellationToken = default)
        {
            if ((string.IsNullOrEmpty(value) || defaultValue.Equals(value)) && !writeIfBlank)
                return;
            cancellationToken.ThrowIfCancellationRequested();

            await sw.WriteAsync(" ");
            await sw.WriteAsync(attributeName);
            await sw.WriteAsync("=\"");
            await sw.WriteAsync(value == null ? string.Empty : EncodeXml(value));
            await sw.WriteAsync("\"");
        }
        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, byte[] value, CancellationToken cancellationToken = default)
        {
            if (value == null)
                return Task.CompletedTask;

            return WriteAttributeAsync(sw, attributeName, BitConverter.ToString(value).Replace("-", ""), false, cancellationToken);
        }

        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, uint value, uint defaultValue, bool writeIfBlank = false, CancellationToken cancellationToken = default)
        {
            if(value != defaultValue)
                return WriteAttributeAsync(sw, attributeName, value, true, cancellationToken);
            if(writeIfBlank)
                return WriteAttributeAsync(sw, attributeName, value, writeIfBlank, cancellationToken);
            return Task.CompletedTask;
        }

        public static Task WriteAttributeAsync(StreamWriter sw, string attributeName, DateTime? value, CancellationToken cancellationToken = default)
        {
            if (value == null)
                return Task.CompletedTask;
            return WriteAttributeAsync(sw, attributeName, value.Value.ToString("yyyy-MM-ddTHH:mm:ss"), false, cancellationToken);
        }
    }
}
