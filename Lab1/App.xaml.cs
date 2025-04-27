namespace Lab1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ApplySavedThemeSettings();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
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
                2 => Color.FromArgb("#F5F5DC"),
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