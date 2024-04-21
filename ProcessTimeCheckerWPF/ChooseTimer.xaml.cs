using PTC.Domain.GlobalClasses;
using PTC.Services.Services;
using System.Diagnostics;
using System.Windows;

namespace ProcessTimeCheckerWPF
{
	/// <summary>
	/// Interaction logic for ChooseTimer.xaml
	/// </summary>
	public partial class ChooseTimer : Window
	{
		private readonly SettingsService _SS = new();
		private static int sliderValue;
		public ChooseTimer()
		{
			InitializeComponent();
			slider.Minimum = 2;
		}

		private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			label.Content = (int)slider.Value;
			sliderValue = (int)slider.Value;
		}

		private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			int currentRefreshValue = await _SS.GetRefreshTime();
			if (currentRefreshValue == sliderValue)
			{
				this.Close();
			}
			else
			{
				string msg = $"Yenileme Hızını {sliderValue}'ya Değiştirmek İstiyor Musunuz? Bu Uygulamayı Yeniden Başlatacaktır. \n Would You Like to Update the Refresh Timer to {sliderValue}? This Will Restart the App.";
				string title = "Yenilenme Hızını Güncelle \n Update Refresh Timer";
				MessageBoxResult dialog = MessageBox.Show(msg,
										  title,
										  MessageBoxButton.YesNo,
										  MessageBoxImage.Question);
				if (dialog == MessageBoxResult.Yes)
				{

					await _SS.SetRefreshTime(sliderValue);
					Process.Start(GlobalVariables.filePath);
					Application.Current.Shutdown();
				}
				else
				{
					this.Close();
				}
			}
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			int currentRrate = await _SS.GetRefreshTime();
			sliderValue = currentRrate;
			slider.Value = currentRrate;
		}
	}
}
