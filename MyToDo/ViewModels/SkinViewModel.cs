using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyToDo.ViewModels
{
    public class SkinViewModel : NavigationViewModel
    {
        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public DelegateCommand<object> ChangeHueCommand { get; private set; }

        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private bool _isDarkTheme;

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if(SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }


        public SkinViewModel(IContainerProvider provider) : base(provider)
        {
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
            _isDarkTheme = paletteHelper.GetTheme().GetBaseTheme() == BaseTheme.Dark;  
        }

        private void ChangeHue(object obj)
        {
            var hue = (Color)obj;

            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(hue);
            theme.PrimaryMid = new ColorPair(hue);
            theme.PrimaryDark = new ColorPair(hue);

            paletteHelper.SetTheme(theme);
        }
    }
}
