# ProgramowanieWiR-Projekt

Projekt programowania rozproszonego – Master–Worker
Cel projektu

Celem projektu jest rozwiązanie problemu obliczeniowego z wykorzystaniem programowania rozproszonego i równoległego. Zadaniem aplikacji jest odnalezienie hasła na podstawie skrótu MD5 metodą brute-force poprzez podział pracy pomiędzy wiele procesów.

Opis rozwiązania

Projekt składa się z dwóch aplikacji konsolowych:

1. MasterServer – przydziela zakresy danych Workerom i odbiera wynik.

2. WorkerNode – wykonuje obliczenia i przesyła znalezione hasło do Mastera.

Komunikacja odbywa się przez TCP, a obliczenia w Workerze są realizowane równolegle z użyciem Parallel.ForEach.

Technologie:

1. C#, .NET

2. TCP/IP

Programowanie rozproszone i równoległe

Uruchomienie (terminal):

1. Uruchom MasterServer:

cd MasterServer
dotnet run


2. W osobnych terminalach uruchom Workerów:

cd WorkerNode
dotnet run


Każdy Worker otrzymuje inny zakres danych i rozpoczyna obliczenia.

Rezultat

Hasło zostaje odnalezione szybciej dzięki rozproszeniu pracy pomiędzy wiele procesów oraz wykorzystaniu wielu rdzeni CPU.