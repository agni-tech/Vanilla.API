using System.Globalization;

namespace Vanilla.Shared.Interfaces;

public interface ILocalizationHelper
{
    public string GetString(string key, CultureInfo culture);
}
