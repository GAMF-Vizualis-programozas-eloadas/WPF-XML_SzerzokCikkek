using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using WPF_Szerzok_Cikkek;

namespace WPF_XML_SzerzokCikkek
{
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
				dsAdatok.ReadXml("..\\..\\..\\adatok.xml", XmlReadMode.IgnoreSchema);
				MessageBox.Show("Adatok betöltve az adatok.xml fájlból.", "Sikeres betöltés", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception)
			{
				var sz1 = dsAdatok.dtSzerzők.AdddtSzerzőkRow(1, "Kocsis Zoltán Tamás");
				var sz2 = dsAdatok.dtSzerzők.AdddtSzerzőkRow(2, "Kovács János");
				var sz3 = dsAdatok.dtSzerzők.AdddtSzerzőkRow(3, "Tóth László");
				var c1 = dsAdatok.dtCikkek.AdddtCikkekRow(1, "Informatikai kutatás a gerincsebészetben",
					"http://gradus.kefo.hu/archive/2020-2/2020_2_CSC_002_Kocsis.pdf");
				var c2 = dsAdatok.dtCikkek.AdddtCikkekRow(2, "Kéztörésből felépülő páciens erőnlétének vizsgálata mems szenzor segítségével",
					"http://gradus.kefo.hu/archive/2020-1/2020_1_ENG_001_Toth.pdf");
				var c3 = dsAdatok.dtCikkek.AdddtCikkekRow(3, "A teniszben használt mozdulatok mozgáselemzése mems szenzorok segítségével neuro-motorikus betegségben szenvedő pácienseknél",
					"http://gradus.kefo.hu/archive/2019-1/2019_1_CSC_004_Toth.pdf");
				dsAdatok.dtSzerzőkCikkek.AdddtSzerzőkCikkekRow(sz1, c1);
				dsAdatok.dtSzerzőkCikkek.AdddtSzerzőkCikkekRow(sz2, c1);
				dsAdatok.dtSzerzőkCikkek.AdddtSzerzőkCikkekRow(sz3, c2);
				dsAdatok.dtSzerzőkCikkek.AdddtSzerzőkCikkekRow(sz3, c3);
				MessageBox.Show("Sikertelen betöltés! Adatbázis alapértelmezett adatokkal létrehozva.", "Hiba", 
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void miMentés_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				dsAdatok.WriteXml("..\\..\\..\\adatok.xml", XmlWriteMode.IgnoreSchema);
				MessageBox.Show("Adatok mentve az adatok.xml fájlba.", "Sikeres mentés", 
					MessageBoxButton.OK, MessageBoxImage.Information);	
			}
			catch (Exception exc)
			{
				MessageBox.Show("Sikertelen mentés!"+exc.Message, "OK",MessageBoxButton.OK, 
					MessageBoxImage.Error);
			}
		}

		private void miKilépés_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void miSzerzők_Click(object sender, RoutedEventArgs e)
		{
			dgCikkek.Visibility = Visibility.Collapsed;
			ucKombinált.Visibility = Visibility.Collapsed;
			dgSzerzők.Visibility = Visibility.Visible;
			dgSzerzők.ItemsSource = dsAdatok.dtSzerzők;
		}

		private void miCikkek_Click(object sender, RoutedEventArgs e)
		{
			dgSzerzők.Visibility = Visibility.Collapsed;
			ucKombinált.Visibility = Visibility.Collapsed;
			dgCikkek.Visibility = Visibility.Visible;
			dgCikkek.ItemsSource = dsAdatok.dtCikkek;
		}

		private void miKombinált_Click(object sender, RoutedEventArgs e)
		{
			dgSzerzők.Visibility = Visibility.Collapsed;
			dgCikkek.Visibility = Visibility.Collapsed;
			ucKombinált.Visibility = Visibility.Visible;
		}

		private void dgCikkek_Hyperlink_RequestNavigate(object sender, 
				RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) 
				{ UseShellExecute = true });
			e.Handled = true;
		}
	}
}