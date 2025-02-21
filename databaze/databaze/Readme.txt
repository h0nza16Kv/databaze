Databaze
 - Databázova aplikace určená pro  knihovnu. Konkrétně pro její zákazníky.

Systémové požadavky
 - Visual Studio a SQL Server Management Studio -> pokud nemáte budete je muset nainstalovat.

Nastavení a spuštění aplikace 
 1) Vytvoření databáze:
  - V SSMS stiskneme tlačítko New Query (Rozhraní se nachazí na horní liště), kam poté zkopírujeme script pro vytvoření - generateScript.txt. 

 2) Vytvoření projektu:
    Vytvoření:
  a) Stáhneme projekt z GitHubu, rozzipujeme a po rozzipování ho pomocí solutionu spustíme.
  b) zkopírujeme URL repozitáře a ve VS zvolíme možnost clone repository, kam následně vložíme URL a vytvoříme projekt.
   - Pro funkčnost budou potřeba následující package: System.Configuration.ConfigurationManager, System.Data.SqlClient.
   - Můžeme je nainstalovat pomocí NuGet Package Manager ve VS.

Konfigurace:
  - Připojení je nakonfigurováno v souboru App.config. Na výběr máme ze dvou variant Windows Authentication a SQL Server Authentication. Příklad přípojení:
	a) Windows Authentication --> <add name="WinAu" connectionString="Database=nezev_serveru;Initial Catalog=pv;Integrated Security=True;" providerName="System.Data.SqlClient" />
	b) SQL Server Authentication --> <add name="SerAu" connectionString="Data Source=nezev_serveru;Initial Catalog=pv;User Id=uzivatel;Password=Vase_heslo;" providerName="System.Data.SqlClient" />

 Singleton:
  - Ve třídě Singleton nastavíme connection buď na WinAu = Windows Authentication nebo SerAu = SQL Server Authentication
    např. static Singleton()
	 {
      connection = ConfigurationManager.ConnectionStrings["WinAu"].ConnectionString;
	 }

  3) Spuštění aplikace ve Visual Studiu:
   - Nyní, když máme připojenou dattabázi, můžeme spustit aplikaci. Aplikace se spouští klasicky pomocí Run na horní liště ve VS. 
   - Pokud chceme importovat pomocí csv je potřeba nejdříve odkomentovat objekt csvImport a cesty k souborům. Poté jěště přesunot soubory Zakaznik.csv a Autor.csv 
   do bin/Debug/net8.0. Pro větší přehlednost zakomentujte objekt menu.
   - Všechny soubory se nachází ve složce Files
  
	


