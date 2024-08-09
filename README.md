# Testing-End-points
## Popis
Tento projekt obsahuje jednoduchou konzolovou aplikaci pro práci s úkoly (`Todo`), testy pro ověření její funkčnosti a API server, který poskytuje úkoly prostřednictvím různých endpointů.

## Instalace

1. **Stažení souborů**  
   Stáhněte všechny soubory a složky do stejného adresáře. 

2. **Přístup do složky**  
   Otevřete příkazový řádek a přesuňte se do složky `MyFirstTestingProject`:
   ```bash
   cd MyFirstTestingProject
3. **Spuštění aplikace**
   Spusťte projekt pomocí příkazu:
   ```bash
   dotnet run

   ## Soubory

### Program.cs
Tento soubor obsahuje konzolovou aplikaci, která využívá klienta `TodoClient` pro komunikaci s API.

#### TodoClient třída:
- `GetAll()`: Vrátí všechny úkoly.
- `GetById(int id)`: Vrátí úkol podle ID.
- `GetByDay(string day)`: Vrátí úkoly podle dne.
- `GetByLocation(string location)`: Vrátí úkoly podle umístění.
- `GetTodosCount()`: Vrátí počet úkolů.
- `Ende()`: Ukončí klienta.

#### Program třída:
- Inicializuje `TodoClient` a vykonává několik operací, jako je získání úkolů a jejich zobrazení v konzoli.

### Tests.cs
Tento soubor obsahuje testy pro `TodoClient` pomocí NUnit frameworku.

Testy zahrnují:
- `Get_All_Todos()`: Ověřuje, že všechny úkoly jsou vráceny správně.
- `Get_By_Id_Todos()`: Ověřuje, že úkoly vrácené podle ID jsou správné.
- `Get_By_Day_Todos()`: Ověřuje, že úkoly vrácené podle dne jsou správné.
- `Get_By_Location_NonExistent()`: Ověřuje chování klienta, když není nalezen žádný úkol podle umístění.
- `Get_By_Day_NonExistent()`: Ověřuje chování klienta, když není nalezen žádný úkol podle dne.
- `Get_By_Location_Todos()`: Ověřuje, že úkoly vrácené podle umístění jsou správné.
- `Get_Count()`: Ověřuje, že počet úkolů je správný.

### Program.cs (API)
Tento soubor obsahuje API server implementovaný pomocí ASP.NET Core, který poskytuje úkoly prostřednictvím různých endpointů.

Endpointy zahrnují:
- `/todos`: Vrátí všechny úkoly.
- `/todos/id/{id}`: Vrátí úkol podle ID.
- `/todos/day/{day}`: Vrátí úkoly podle dne.
- `/todos/location/{location}`: Vrátí úkoly podle umístění.
- `/todos/count`: Vrátí počet úkolů.

## Použití

### Otevření řešení
Otevřete `.sln` soubor v integrovaném vývojovém prostředí (IDE), jako je Visual Studio.

### Spuštění testů nebo aplikace
V IDE můžete spustit testy kliknutím na `Run Tests` nebo obdobnou volbu v rozhraní IDE, nebo spustit konzolovou aplikaci.
