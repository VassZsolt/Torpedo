# Torpedó játék

> .NET-es grafikus (WPF keretrendszerben írt) játék elkészítése.

## Játék leírása

[Magyar](https://hu.wikipedia.org/wiki/Torped%C3%B3_(j%C3%A1t%C3%A9k))

## Alapkövetelmények
> Ezek hiányában a feladat értékelhetetlennek minősül

- Git repo használata
    - Rendszeres commit minden csapattagtól. A csapattagok a commit-jaik alapján lesznek értékelve.
- Egy solution használata
- Konvenciók alkalmazása: [NI C# Style Guide](https://github.com/ni/csharp-styleguide)
    - A package is bereferálható, de a warning-okat ki kell javítani ezesetben

## Követelmények

### Egymás ellen lehessen játszani ugyanazon a gépen, ugyanabban az alkalmazásban

- Kezdőképernyő: Két játékos nevet kér be
- A játék során adott játékos nem láthatja az ellenfele lépéseit, hajóit. Ennek kiküszöbölésére egy egyedi megoldás implementálása (életszerű helyzet szimulálása)

### AI ellen lehessen játszani

- Kezdőképernyő: Egy játékos nevet kér be
- Kezdő játékos véletlenszerűen választva
- AI elemezési sorrend
    1. Random találgat
    2. Ha van találat akkor már a mellette lévő mezőket lövi
         - UNIT teszteket írni a logikához
- A játék során végig a játékos tábláját látjuk, de az AI lépései, választási stratégiája is látható legyen (ne legyen villámgyors, követhetetlen)
- Egy játék teljes menetének a mentése és visszajátszhatósága. A játékfelületen végignézhető adott játék minden lépése.

### Játékmenet

- Belépéskor kérjen neve(ke)t, majd a játék végén ahhoz mentse az eredményeket
    - Név nem lehet: üres, whitespace, különleges karakterek szűrése pl !?_-:;#
- A kezdőképernyőn egy gomb segítségével betölthető legyen a mindenkori eredménylista a tárolt adatokból
    - Listázza az egyes korábbi menetek adatai táblázatban (játékosok, körök száma, játékosok találatai, nyertes)
- A játék közben látható eredményjelző adatai:
    - Körök száma
    - Saját találatok
    - Ellenfél találatai
    - Milyen hajók vannak még és melyek lettek elsüllyesztve
    - Billentyűkombinációra mutassa meg az AI hajóit (Csak AI ellen működjön)

### Játék vége

- Tárolja le az eredményeket 
    - Lehetőségek:
        - Adatbázisban (Entity Framework), a DB engine szabadon választható (MSSQL, Postgre, stb) 
        - Adat fájlban JSON, XML, stb
