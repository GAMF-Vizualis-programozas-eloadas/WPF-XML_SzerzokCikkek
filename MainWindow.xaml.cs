using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Szerzok_Cikkek;

namespace WPF_XML_SzerzokCikkek
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private dsAdatok dsAdatok;
		public MainWindow()
		{
			InitializeComponent();
			dsAdatok = new dsAdatok();
			Application.Current.Properties["dsAdatok"] = dsAdatok;
		}

		private void miBetöltés_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				dsAdatok.ReadXml("adatok.xml", XmlReadMode.Auto);
			}
			catch (Exception)
			{
				var sz1 = dsAdatok.dtSzerzők.AdddtSzerzőkRow(1, "Kocsis Zoltán Tamás");
				var sz2 = dsAdatok.dtSzerzők.AdddtSzerzőkRow(2, "Kovács János");
				var sz3 = dsAdatok.dtSzerzők.AdddtSzerzőkRow(3, "Tóth László");
				var c1 = dsAdatok.dtCikkek.AdddtCikkekRow(1, "Informatikai kutatás a gerincsebészetben",
					"http://gradus.kefo.hu/current/2020-2/2020_2_CSC_002_Kocsis.pdf");
				var c2 = dsAdatok.dtCikkek.AdddtCikkekRow(2, "Kéztörésből felépülő páciens erőnlétének vizsgálata mems szenzor segítségével",
					"http://gradus.kefo.hu/archive/2020-1/2020_1_ENG_001_Toth.pdf");
				var c3 = dsAdatok.dtCikkek.AdddtCikkekRow(3, "A teniszben használt mozdulatok mozgáselemzése mems szenzorok segítségével neuro-motorikus betegségben szenvedő pácienseknél",
					"http://gradus.kefo.hu/archive/2019-1/2019_1_CSC_004_Toth.pdf");
				dsAdatok.dtSzerzőkCikkek.AdddtSzerzőkCikkekRow(sz1, c1);
				dsAdatok.dtSzerzőkCikkek.AdddtSzerzőkCikkekRow(sz2, c1);
				dsAdatok.dtSzerzőkCikkek.AdddtSzerzőkCikkekRow(sz3, c2);
				dsAdatok.dtSzerzőkCikkek.AdddtSzerzőkCikkekRow(sz3, c3);
			}
		}

		private void miMentés_Click(object sender, RoutedEventArgs e)
		{
			dsAdatok.WriteXml("adatok.xml", XmlWriteMode.WriteSchema);
		}

		private void miKilépés_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void miSzerzők_Click(object sender, RoutedEventArgs e)
		{
			dgCikkek.Visibility = Visibility.Collapsed;
			ucKombinalt.Visibility = Visibility.Collapsed;
			dgSzerzők.Visibility = Visibility.Visible;
			dgSzerzők.ItemsSource = dsAdatok.dtSzerzők;
		}

		private void miCikkek_Click(object sender, RoutedEventArgs e)
		{
			dgSzerzők.Visibility = Visibility.Collapsed;
			ucKombinalt.Visibility = Visibility.Collapsed;
			dgCikkek.Visibility = Visibility.Visible;
			dgCikkek.ItemsSource = dsAdatok.dtCikkek;
		}

		private void miKombinált_Click(object sender, RoutedEventArgs e)
		{
			dgSzerzők.Visibility = Visibility.Collapsed;
			dgCikkek.Visibility = Visibility.Collapsed;
			ucKombinalt.Visibility = Visibility.Visible;
		}
	}
}