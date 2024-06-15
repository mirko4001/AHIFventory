using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using Wpf.Ui;
using Wpf.Ui.Extensions;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace AHIFventory
{
    public class GlobalFunction
    {
        public static async void ShowCustomMessageBox(string title, string content)
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = title,
                Content = content,
            };

            _ = await uiMessageBox.ShowDialogAsync();
        }

        public static void ShowToastNotification(string title, string content)
        {
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
            {
                new AdaptiveText()
                {
                    Text = title
                },
                new AdaptiveText()
                {
                    Text = content
                }
            }
                }
            };

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual
            };

            string xmlContent = toastContent.GetContent();

            var toastXml = new XmlDocument();
            toastXml.LoadXml(xmlContent);

            var toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier("AHIFusion").Show(toast);
        }
    }
}
