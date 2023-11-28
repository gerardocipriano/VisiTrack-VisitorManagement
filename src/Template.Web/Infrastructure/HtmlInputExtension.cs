using System;
using System.Diagnostics;
using System.Globalization;

namespace Template.Infrastructure.AspNetCore
{
    public static class HtmlInputExtension
    {
        #region DateTime -> string

        [DebuggerStepThrough]
        public static string ToDateHtmlInput(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }

        [DebuggerStepThrough]
        public static string ToDateHtmlInput(this DateTime? value)
        {
            return value.HasValue ? value.Value.ToDateHtmlInput() : null;
        }

        [DebuggerStepThrough]
        public static string ToTimeHtmlInput(this DateTime value)
        {
            return value.ToString("HH:mm");
        }

        [DebuggerStepThrough]
        public static string ToTimeHtmlInput(this DateTime? value)
        {
            return value.HasValue ? value.Value.ToTimeHtmlInput() : null;
        }

        [DebuggerStepThrough]
        public static string ToDateTimeHtmlInput(this DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm");
        }

        [DebuggerStepThrough]
        public static string ToDateTimeHtmlInput(this DateTime? value)
        {
            return value.HasValue ? value.Value.ToDateTimeHtmlInput() : null;
        }

        #endregion

        #region string -> DateTime

        [DebuggerStepThrough]
        public static DateTime ToDateFromHtmlInput(this string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        [DebuggerStepThrough]
        public static DateTime? ToDateNullableFromHtmlInput(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.ToDateFromHtmlInput();
        }

        [DebuggerStepThrough]
        public static DateTime ToTimeFromHtmlInput(this string value)
        {
            return DateTime.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture);
        }

        [DebuggerStepThrough]
        public static DateTime? ToTimeNullableFromHtmlInput(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.ToTimeFromHtmlInput();
        }

        [DebuggerStepThrough]
        public static DateTime ToDateTimeFromHtmlInput(this string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
        }

        [DebuggerStepThrough]
        public static DateTime? ToDateTimeNullableFromHtmlInput(this string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.ToDateTimeFromHtmlInput();
        }

        #endregion
    }
}
