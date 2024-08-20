using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HeroPlan;

/// <summary>
/// Converts a boolean value to a color for use in XAML bindings.
/// </summary>
public class BoolToColorConverter : IValueConverter
{
    /// <summary>
    /// The color to use when the boolean value is true.
    /// </summary>
    public Color TrueColor { get; set; }

    /// <summary>
    /// The color to use when the boolean value is false.
    /// </summary>
    public Color FalseColor { get; set; }

    /// <summary>
    /// Converts a boolean value to a SolidColorBrush.
    /// </summary>
    /// <returns>A SolidColorBrush of TrueColor if the value is true, otherwise FalseColor.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? new SolidColorBrush(TrueColor) : new SolidColorBrush(FalseColor);
    }

    /// <summary>
    /// Converts a color back to a boolean value. Not implemented in this converter.
    /// </summary>
    /// <exception cref="NotImplementedException">This method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}