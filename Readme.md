# AI Competition

## Aufbau

- Der Order `AICompetition/data` enthält die Datensätze
- Der Order `AICompetition/pca` enthält die berechneten PCA Matrizen für unterschiedliche Werte von n

## Projekt ausführen

### Projekt bauen
````sh
dotnet build -c Release
````

### PCA berechnen für Iris Datensatz
````sh
dotnet run calculate iris -c Release
````

### Nächster Nachber Klassifizierung Iris Datensatz
````sh
dotnet run classify iris -c Release
````

### PCA berechnen für MNIST Datensatz
````sh
dotnet run calculate mnist -c Release
````

### Nächster Nachber Klassifizierung MNIST Datensatz
````sh
dotnet run classify mnist -c Release
````

## Ergebnisse

### Datensatz Iris (Schwertlilien)

- Trainingsdatensatz: 100
- Testdatensatzgröße: 50

|    N |   HIT |  MISS | RATE    |
|------|-------|-------|---------|
|    1 |    44 |     6 | 88.00 % |
|    2 |    48 |     2 | 96.00 % |
|    3 |    49 |     1 | 98.00 % |


### Datensatz MNIST

- Trainingsdatensatz: 20000
- Testdatensatzgröße: 22000

|    N |   HIT |  MISS | RATE    |
|------|-------|-------|---------|
|    1 |  5377 | 16623 | 24.44 % |
|    2 |  8308 | 13692 | 37.76 % |
|    3 |  9569 | 12431 | 43.50 % |
|    4 | 12238 |  9762 | 55.63 % |
|    5 | 14993 |  7007 | 68.15 % |
|    6 | 17204 |  4796 | 78.20 % |
|    7 | 18304 |  3696 | 83.20 % |
|    8 | 19065 |  2935 | 86.66 % |
|    9 | 19392 |  2608 | 88.15 % |
|   10 | 19852 |  2148 | 90.24 % |
|   11 | 20026 |  1974 | 91.03 % |
|   12 | 20320 |  1680 | 92.36 % |
|   13 | 20551 |  1449 | 93.41 % |
|   14 | 20657 |  1343 | 93.90 % |
|   15 | 20707 |  1293 | 94.12 % |
|   16 | 20833 |  1167 | 94.70 % |
|   17 | 20896 |  1104 | 94.98 % |
|   18 | 20957 |  1043 | 95.26 % |
|   19 | 21015 |   985 | 95.52 % |
|   20 | 21039 |   961 | 95.63 % |
|   21 | 21070 |   930 | 95.77 % |
|   22 | 21113 |   887 | 95.97 % |
|   23 | 21126 |   874 | 96.03 % |
|   24 | 21145 |   855 | 96.11 % |
|   25 | 21141 |   859 | 96.10 % |
|   26 | 21174 |   826 | 96.25 % |
|   27 | 21182 |   818 | 96.28 % |
|   28 | 21183 |   817 | 96.29 % |
|   29 | 21200 |   800 | 96.36 % |
|   30 | 21199 |   801 | 96.36 % |
