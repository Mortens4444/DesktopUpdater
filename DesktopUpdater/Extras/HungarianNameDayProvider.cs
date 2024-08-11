using DesktopUpdater.Interfaces;
using System.Collections.ObjectModel;
using System.Globalization;

namespace DesktopUpdater.Extras;

public class HungarianNameDayProvider : IHungarianNameDayProvider
{
    public delegate IList<string> NamedayProviderFunction();

    private readonly ReadOnlyDictionary<int, NamedayProviderFunction> monthNamedays =
        new(
            new Dictionary<int, NamedayProviderFunction>
            {
                { 1, GetJanuaryNameDays },
                { 3, GetMarchNameDays },
                { 4, GetAprilNameDays },
                { 5, GetMayNameDays },
                { 6, GetJuneNameDays },
                { 7, GetJulyNameDays},
                { 8, GetAugustNameDays},
                { 9, GetSeptemberNameDays },
                { 10, GetOctoberNameDays },
                { 11, GetNovemberNameDays },
                { 12, GetDecemberNameDays }
            });

    public string GetNameDayText(DateTime date)
    {
        var yesterday = date.AddDays(-1);
        var yesterdaysNameDays = GetHungarianNameDay(yesterday);
        var todaysNameDays = GetHungarianNameDay(date);
        var tommorow = date.AddDays(1);
        var tommorowsNameDays = GetHungarianNameDay(tommorow);

        var mfi = new DateTimeFormatInfo();
        return String.Format("Tegnap ({4}) {0} névnapja volt.{3}Ma ({5}) {1} névnapja van.{3}Holnap ({6}) {2} névnapja lesz.",
            yesterdaysNameDays, todaysNameDays, tommorowsNameDays, Environment.NewLine,
            mfi.GetDayName(yesterday.DayOfWeek), mfi.GetDayName(date.DayOfWeek), mfi.GetDayName(tommorow.DayOfWeek));
    }

    private string GetHungarianNameDay(DateTime date)
    {
        var namedays = GetNamedaysOfMonth(date.Year, date.Month);
        return namedays[date.Day - 1];
    }

    private IList<string> GetNamedaysOfMonth(int year, int month)
    {
        if (month != 2)
        {
            return monthNamedays[month]();
        }
        return GetFebruaryNameDays(year);
    }

    private static IList<string> GetJanuaryNameDays()
    {
        return new List<string> { "ÚJÉV, Fruzsina", "Ábel", "Genovéva, Benjámin", "Titusz, Leona", "Simon", "Boldizsár", "Attila, Ramóna", "Gyöngyvér", "Marcell", "Melánia", "Ágota", "Ernő", "Veronika", "Bódog", "Lóránt, Loránd", "Gusztáv", "Antal, Antónia", "Piroska", "Sára, Márió", "Fábián, Sebestyén", "Ágnes", "Vince, Artúr", "Zelma, Rajmund", "Timót", "Pál", "Vanda, Paula", "Angelika", "Károly, Karola", "Adél", "Martina, Gerda", "Marcella" };
    }

    private static IList<string> GetFebruaryNameDays(int year)
    {
        if (DateTime.IsLeapYear(year))
        {
            return new List<string> { "Ignác", "Karolina, Aida", "Balázs", "Ráhel, Csenge", "Ágota, Ingrid", "Dorottya, Dóra", "Tódor, Rómeó", "Aranka", "Abigél, Alex", "Elvira", "Bertold, Marietta", "Lívia, Lídia", "Ella, Linda", "Bálint, Valentin", "Kolos, Georgina", "Julianna, Lilla", "Donát", "Bernadett", "Zsuzsanna", "Aladár, Álmos", "Eleonóra", "Gerzson", "Alfréd", "Szökőnap", "Mátyás", "Géza", "Edina", "Ákos, Bátor", "Elemér" };
        }

        return new List<string> { "Ignác", "Karolina, Aida", "Balázs", "Ráhel, Csenge", "Ágota, Ingrid", "Dorottya, Dóra", "Tódor, Rómeó", "Aranka", "Abigél, Alex", "Elvira", "Bertold, Marietta", "Lívia, Lídia", "Ella, Linda", "Bálint, Valentin", "Kolos, Georgina", "Julianna, Lilla", "Donát", "Bernadett", "Zsuzsanna", "Aladár, Álmos", "Eleonóra", "Gerzson", "Alfréd", "Mátyás", "Géza", "Edina", "Ákos, Bátor", "Elemér" };
    }

    private static IList<string> GetMarchNameDays()
    {
        return new List<string> { "Albin", "Lujza", "Kornélia", "Kázmér", "Adorján, Adrián", "Leonóra, Inez", "Tamás", "NEMZETKÖZI NŐNAP, Zoltán", "Franciska, Fanni", "Ildikó", "Szilárd", "Gergely", "Krisztián, Ajtony", "Matild", "NEMZETI ÜNNEP, Kristóf", "Henrietta", "Gertrúd, Patrik", "Sándor, Ede", "József, Bánk", "Klaudia", "Benedek", "Beáta, Izolda", "Emőke", "Gábor, Karina", "Irén, Irisz", "Emánuel", "Hajnalka", "Gedeon, Johanna", "Auguszta", "Zalán", "Árpád" };
    }

    private static IList<string> GetAprilNameDays()
    {
        return new List<string> { "Hugó", "Áron", "Buda, Richárd", "Izidor", "Vince", "Vilmos, Bíborka", "Herman", "Dénes", "Erhard", "Zsolt", "Leó, Szaniszló", "Gyula", "Ida", "Tibor", "Anasztázia, Tas", "Csongor", "Rudolf", "Andrea, Ilma", "Emma", "Tivadar", "Konrád", "Csilla, Noémi", "Béla", "György", "Márk", "Ervin", "Zita, Mariann", "Valéria", "Péter", "Katalin, Kitti" };
    }

    private static IList<string> GetMayNameDays()
    {
        return new List<string> { "MUNKA ÜNNENPE, Fülöp, Jakab", "Zsigmond", "Tímea, Irma", "Mónika, Flórián", "Györgyi", "Ivett, Frida", "Gizella", "Mihály", "Gergely", "Ármin, Pálma", "Ferenc", "Pongrác", "Szervác, Imola", "Bonifác", "Zsófia, Szonja", "Mózes, Botond", "Paszkál", "Erik, Alexandra", "Ivó, Milán", "Bernát, Felícia", "Konstantin", "Júlia, Rita", "Dezső", "Eszter, Eliza", "Orbán", "Fülöp, Evelin", "Hella", "Emil, Csanád", "Magdolna", "Janka, Zsanett", "Angéla, Petronella" };
    }

    private static IList<string> GetJuneNameDays()
    {
        return new List<string> { "Tünde", "Kármen, Anita", "Klotild", "Bulcsú", "Fatime", "Norbert, Cintia", "Róbert", "Medárd", "Félix", "Margit, Gréta", "Barnabás", "Villő", "Antal, Anett", "Vazul", "Jolán, Vid", "Jusztin", "Laura, Alida", "Arnold, Levente", "Gyárfás", "Rafael", "Alajos, Leila", "Paulina", "Zoltán", "Iván", "Vilmos", "János, Pál", "László", "Levente, Irén", "Péter, Pál", "Pál" };
    }

    private static IList<string> GetJulyNameDays()
    {
        return new List<string> { "Tihamér, Annamária", "Ottó", "Kornél, Soma", "Ulrik", "Emese, Sarolta", "Csaba", "Appolónia", "Ellák", "Lukrécia", "Amália", "Nóra, Lili", "Izabella, Dalma", "Jenő", "Őrs, Stella", "Henrik, Roland", "Valter", "Endre, Elek", "Frigyes", "Emília", "Illés", "Dániel, Daniella", "Magdolna", "Lenke", "Kinga, Kincső", "Jakab, Kristóf", "Anna, Anikó", "Olga, Liliána", "Szabolcs", "Márta, Flóra", "Judit, Xénia", "Oszkár" };
    }

    private static IList<string> GetAugustNameDays()
    {
        return new List<string> { "Boglárka", "Lehel", "Hermina", "Domonkos, Dominika", "Krisztina", "Berta, Bettina", "Ibolya", "László", "Emőd", "Lőrinc", "Zsuzsanna, Tiborc", "Klára", "Ipoly", "Marcell", "Mária", "Ábrahám", "Jácint", "Ilona", "Huba", "ALKOTMÁNY ÜNN., István", "Sámuel, Hajna", "Menyhért, Mirjam", "Bence", "Bertalan", "Lajos, Patrícia", "Izsó", "Gáspár", "Ágoston", "Beatrix, Erna", "Rózsa", "Erika, Bella" };
    }

    private static IList<string> GetSeptemberNameDays()
    {
        return new List<string> { "Egyed, Egon", "Rebeka, Dorina", "Hilda", "Rozália", "Viktor, Lőrinc", "Zakariás", "Regina", "Mária, Adrienn", "Ádám", "Nikolett, Hunor", "Teodóra", "Mária", "Kornél", "Szeréna, Roxána", "Enikő, Melitta", "Edit", "Zsófia", "Diána", "Vilhelmina", "Friderika", "Máté, Mirella", "Móric", "Tekla", "Gellért, Mercédesz", "Eufrozina, Kende", "Jusztina", "Adalbert", "Vencel", "Mihály", "Jeromos" };
    }

    private static IList<string> GetOctoberNameDays()
    {
        return new List<string> { "Malvin", "Petra", "Helga", "Ferenc", "Aurél", "Brúnó, Renáta", "Amália", "Koppány", "Dénes", "Gedeon", "Brigitta", "Miksa", "Kálmán, Ede", "Helén", "Teréz", "Gál", "Hedvig", "Lukács", "Nándor", "Vendel", "Orsolya", "Előd", "KÖZTÁRSASÁG KIKIÁLTÁSA, Gyöngyi", "Salamon", "Blanka, Bianka", "Dömötör", "Szabina", "Simon, Szimonetta", "Nárcisz", "Alfonz", "Farkas" };
    }

    private static IList<string> GetNovemberNameDays()
    {
        return new List<string> { "Marianna", "Achilles", "Győző", "Károly", "Imre", "Lénárd", "Rezső", "Zsombor", "Tivadar", "Réka", "Márton", "Jónás, Renátó", "Szilvia", "Aliz", "Albert, Lipót", "Ödön", "Hortenzia, Gergő", "Jenő", "Erzsébet", "Jolán", "Olivér", "Cecília", "Kelemen, Klementina", "Emma", "Katalin", "Virág", "Virgil", "Stefánia", "Taksony", "András, Andor" };
    }

    private static IList<string> GetDecemberNameDays()
    {
        return new List<string> { "Elza", "Melinda, Vivien", "Ferenc, Olívia", "Borbála, Barbara", "Vilma", "Miklós", "Ambrus", "Mária", "Natália", "Judit", "Árpád", "Gabriella", "Luca, Otília", "Szilárda", "Valér", "Etelka, Aletta", "Lázár, Olimpia", "Auguszta", "Viola", "Teofil", "Tamás", "Zénó", "Viktória", "Ádám, Éva", "KARÁCSONY, Eugénia", "KARÁCSONY, István", "János", "Kamilla", "Tamás, Tamara", "Dávid", "Szilveszter" };
    }
}
