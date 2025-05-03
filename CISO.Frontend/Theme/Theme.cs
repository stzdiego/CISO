using MudBlazor;

namespace CISO.Frontend.Theme;

public static class Theme
{
    public static MudTheme ResourcesTheme => new MudTheme
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#1976D2",
            Secondary = "#FF4081",
            Background = "#F5F5F5",
            AppbarBackground = "#1976D2",
            AppbarText = "#FFF",
            TextPrimary = "#000000",
            TextSecondary = "#757575",
            ActionDefault = "#000000",
            ActionDisabled = "#BDBDBD"
        },
        PaletteDark = new PaletteDark()
        {
            Primary = "#BB86FC",
            Secondary = "#03DAC6",
            Background = "#121212",
            AppbarBackground = "#1F1F1F",
            AppbarText = "#FFFFFF",
            TextPrimary = "#FFFFFF",
            TextSecondary = "#B0BEC5",
            ActionDefault = "#FFFFFF",
            ActionDisabled = "#BDBDBD"
        },
        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "4px",
            DrawerMiniWidthLeft = "56px",
            DrawerMiniWidthRight = "56px",
            DrawerWidthLeft = "240px",
            AppbarHeight = "64px",
            DrawerWidthRight = "240px"
        }
    };
}