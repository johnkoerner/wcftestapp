using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WcfHost
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			userLabel.Content = WindowsIdentity.GetCurrent().Name;
		}

		TestService _service;
		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			_service = new TestService();
			await _service.StartAsync();
			this.mainLabel.Content = _service.Endpoint;
        }
	}
}
