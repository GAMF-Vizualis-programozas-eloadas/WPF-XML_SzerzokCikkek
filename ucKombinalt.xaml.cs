using System.Data;
using System.Windows;
using System.Windows.Controls;
using WPF_Szerzok_Cikkek;

namespace WPF_XML_SzerzokCikkek
{
	public partial class ucKombinalt : UserControl
	{
		private dsAdatok? dsAdatok;
		public ucKombinalt()
		{
			InitializeComponent();
		}

		private void cbSzerzők_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(dsAdatok == null) return;
			var Szerző = (cbSzerzők.SelectedItem as DataRowView)?.Row as
				dsAdatok.dtSzerzőkRow;
			var Cikkek = from x in dsAdatok.dtSzerzőkCikkek
									 where x.dtSzerzőkRow == Szerző
									 select
									 new
									 {
										 Szerzők = (from y in dsAdatok.dtSzerzőkCikkek
																where y.dtCikkekRow == x.dtCikkekRow
																select
																	y.dtSzerzőkRow.Név).Aggregate("",
													 (a, c) => a + c + ", "),
										 x.dtCikkekRow.Cím,
										 x.dtCikkekRow.URL
									 };

			tbCikkek.Text = Cikkek.Aggregate("", (a, c) => a + c.Szerzők + c.Cím + ", " + c.URL + "\r\n\r\n");
		}

		private void ucKombinalt_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!IsVisible) return;
			dsAdatok = Application.Current.Properties["dsAdatok"] as dsAdatok;
			if (dsAdatok == null) return;
			cbSzerzők.ItemsSource = dsAdatok?.dtSzerzők;
			cbSzerzők.DisplayMemberPath = "Név";
			cbSzerzők.SelectedIndex = 0;
		}
	}
}
