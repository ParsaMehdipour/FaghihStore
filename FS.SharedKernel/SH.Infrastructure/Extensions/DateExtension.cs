using System.Globalization;

namespace SH.Infrastructure.Extensions;

public static class DateExtension
{
    public static string ToPersian(this DateTime dateTime)
    {
        return ToPersianDate(dateTime) + " - " + ToPersianTime(dateTime);
    }

    public static string ToPersianTime(this DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();

        return persianCalendar.GetHour(dateTime).ToString("00") + ":" + persianCalendar.GetMinute(dateTime).ToString("00") + ":" + persianCalendar.GetSecond(dateTime).ToString("00");
    }

    public static string ToPersianDate(this DateTime dateTime)
    {
        var persianCalendar = new PersianCalendar();

        return persianCalendar.GetYear(dateTime).ToString("0000") + "/" + persianCalendar.GetMonth(dateTime).ToString("00") + "/" + persianCalendar.GetDayOfMonth(dateTime).ToString("00");
    }
}