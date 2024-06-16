using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using Wpf.Ui;
using Wpf.Ui.Extensions;
using System.Windows;

namespace AHIFventory
{
    public class GlobalFunction
    {
        public static SnackbarService SnackbarService = new SnackbarService();
        
        public static async void ShowCustomMessageBox(string title, string content)
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = title,
                Content = content,
            };

            _ = await uiMessageBox.ShowDialogAsync();
        }

        public static void ShowSnackbar(
            string title,
            string content,
            TimeSpan? duration = null,
            ControlAppearance? appearance = null,
            SymbolIcon icon = null)
        {
            appearance ??= ControlAppearance.Info;
            icon ??= new SymbolIcon(SymbolRegular.Info28);
            duration ??= TimeSpan.FromSeconds(2);

            SnackbarService.Show(
                title,
                content,
                appearance.Value,
                icon,
                duration.Value
            );
        }

    }
}
