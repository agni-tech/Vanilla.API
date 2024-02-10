using Vanilla.Shared.Interfaces;
using Vanilla.Shared.Resources.Localization;
using System.Globalization;
using System.Resources;

namespace Vanilla.Shared.Helpers;

public class LocalizationHelper : ILocalizationHelper
{

    readonly ResourceManager _resource;
    public LocalizationHelper()
        => _resource = new ResourceManager(typeof(Catalog));

    public string GetString(string key, CultureInfo culture)
        => _resource.GetString(key, culture) ?? key;
}
