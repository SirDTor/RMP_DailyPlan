using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Lab1
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            BackgroundColorPicker.SelectedIndex = Preferences.Get("BackgroundColorIndex", 0);
            ButtonBackgroundColorPicker.SelectedIndex = Preferences.Get("ButtonBackgroundColorIndex", 0);
            TextColorPicker.SelectedIndex = Preferences.Get("TextColorIndex", 0);
            FontSizeSlider.Value = Preferences.Get("FontSize", 16f);

            var savedDate = Preferences.Get("CurrentDate", DateTime.Now.ToString("yyyy-MM-dd"));
            CurrentDatePicker.Date = DateTime.Parse(savedDate);
        }

        private void OnApplySettingsClicked(object sender, EventArgs e)
        {
            Preferences.Set("BackgroundColorIndex", BackgroundColorPicker.SelectedIndex);
            Preferences.Set("ButtonBackgroundColorIndex", ButtonBackgroundColorPicker.SelectedIndex);
            Preferences.Set("TextColorIndex", TextColorPicker.SelectedIndex);
            Preferences.Set("FontSize", (float)FontSizeSlider.Value);
            Preferences.Set("CurrentDate", CurrentDatePicker.Date.ToString("yyyy-MM-dd"));

            ApplySavedThemeSettings();

            Navigation.PopAsync();
        }

        private void OnResetSettingsClicked(object sender, EventArgs e)
        {
            Preferences.Remove("BackgroundColorIndex");
            Preferences.Remove("ButtonBackgroundColorIndex");
            Preferences.Remove("TextColorIndex");
            Preferences.Remove("FontSize");
            Preferences.Remove("CurrentDate");

            BackgroundColorPicker.SelectedIndex = 0; 
            ButtonBackgroundColorPicker.SelectedIndex = 0;
            TextColorPicker.SelectedIndex = 0;
            FontSizeSlider.Value = 16; 
            CurrentDatePicker.Date = DateTime.Now;

            ApplySavedThemeSettings();
        }

        private void ApplySavedThemeSettings()
        {
            var backgroundColorIndex = Preferences.Get("BackgroundColorIndex", 0);
            var buttonBackgroundColorIndex = Preferences.Get("ButtonBackgroundColorIndex", 0);
            var textColorIndex = Preferences.Get("TextColorIndex", 0);
            var fontSize = Preferences.Get("FontSize", 16f);

            Application.Current.Resources["BackgroundColor"] = GetBackgroundColor(backgroundColorIndex);
            Application.Current.Resources["ButtonBackgroundColor"] = GetButtonBackgroundColor(buttonBackgroundColorIndex);
            Application.Current.Resources["PrimaryTextColor"] = GetTextColor(textColorIndex);
            Application.Current.Resources["FontSize"] = fontSize;
        }

        private Color GetButtonBackgroundColor(int index)
        {
            return index switch
            {
                0 => Colors.Red,
                1 => Colors.Yellow,
                2 => Color.FromArgb("#F5F5DC"), // Бежевый
                _ => Colors.White
            };
        }

        private Color GetBackgroundColor(int index)
        {
            return index switch
            {
                0 => Colors.White,
                1 => Colors.LightGray,
                2 => Color.FromArgb("#F5F5DC"), // Бежевый
                _ => Colors.White
            };
        }

        private Color GetTextColor(int index)
        {
            return index switch
            {
                0 => Colors.Black,
                1 => Colors.Blue,
                2 => Colors.DarkGray,
                _ => Colors.Black
            };
        }
    }
}
