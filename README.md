# WPF-XML\_SzerzokCikkek



\# Szerzők–Cikkek nyilvántartó program



A \*\*WPF-XML\_SzerzokCikkek\*\* egy asztali alkalmazás, amely szerzők és az általuk írt cikkek nyilvántartására szolgál. A program három adattáblát kezel egy több-a-többhöz kapcsolattal: \*\*Szerzők\*\* (SzerzőID, Név), \*\*Cikkek\*\* (CikkID, Cím, URL) és egy kapcsolótábla (\*\*SzerzőkCikkek\*\*), amely tárolja, hogy melyik szerző melyik cikket írta. A felhasználó a \*\*Fájl\*\* menüből betöltheti az adatokat egy `adatok.xml` fájlból, illetve elmentheti oda (ha a fájl nem létezik, a program mintaadatokkal tölti fel magát). A \*\*Böngészés\*\* menüből három nézet között lehet váltani: külön megtekinthető a szerzők listája, a cikkek listája (kattintható linkkel), valamint egy \*\*Kombinált\*\* nézet, ahol egy legördülő listából kiválasztott szerzőhöz kilistázódnak a hozzá tartozó cikkek a társszerzőkkel és URL-ekkel együtt.



\## Technológiai megoldások



A program \*\*WPF\*\* (Windows Presentation Foundation) technológiára épül \*\*C#\*\* nyelven, a felhasználói felület \*\*XAML\*\* jelölőnyelvvel van leírva. Az adatkezelés típusos \*\*DataSet\*\* (`dsAdatok.xsd`) segítségével történik, amely három  táblát (`DataTable`: `dtSzerzők`, `dtCikkek`, `dtSzerzőkCikkek`) és a közöttük lévő \*\*idegen kulcs kapcsolatokat\*\* (`Constraint1`, `Constraint2`, `FK\_` keyref-ek) foglalja magába. Az adatok perzisztens tárolása \*\*XML szerializációval\*\* valósul meg a `ReadXml` / `WriteXml` metódusok (`XmlReadMode.Auto`, `XmlWriteMode.WriteSchema`) segítségével. A felületen \*\*DockPanel\*\*, \*\*Menu\*\*, \*\*DataGrid\*\* (benne `DataGridTextColumn` és `DataGridHyperlinkColumn`) és egy saját \*\*UserControl\*\* (`ucKombinalt`) szerepel, amely `ComboBox` és `TextBox` vezérlőket tartalmaz Grid elrendezésben. A kombinált nézet lekérdezései \*\*LINQ to DataSet\*\* szintaxissal készülnek, a több szerző nevének összefűzését pedig az `Aggregate` metódus végzi. A `dsAdatok` példányt az `Application.Current.Properties` szótáron keresztül osztja meg a főablak és a UserControl egy egyszerű, az egész alkalmazásra kiterjedő adatmegosztási megoldással.

