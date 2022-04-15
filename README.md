* należy napisać moduł (biblioteka dll) do obsługi wypożyczalni książek
* kod należy napisać w C#, w dowolnej wersji .NET

* moduł udostępnia funkcje:
	- zarządzanie czytelnikami
	- zarządzanie księgozbiorem
	- wypożyczenie i zwrot książki
	- obliczenie kary za przekroczenie terminu zwrotu
	
* kara za opóźnienie jest naliczana następująco:
	- dla wykładowców
		* do 3 dni jest gratis
		* do 14 dni 2zł/dzień
		* do 28 dni 5zł/dzień
		* powyżej 10zł/dzień
	- dla studentów
		* do 7 dni 1zł/dzień
		* do 14 dni 2zł/dzień
		* do 28 dni 5zł/dzień
		* powyżej 10zł/dzień
	- dla pracowników biblioteki
		* do 28 dni gratis
		* powyżej 5zł/dzień

* ustalenia dotyczące książek:
	- książka może występować w kilku egzemplarzach
	- wypożyczalnia nie rozróżnia książek o tym samym tytule
	
* ustalenia wypożyczeń:
	- nie można wypożyczyć książki, która nie została oddana
	- system zewnętrzny wskazuje datę wypożyczenia/zwrotu (bibliotekarka lubi zbierać pracę z całego tygodnia i wpisywać ją do systemu dopiero w weekend)
	- termin zwrotu książki jest zarządzany i przechowywany przez system zewnętrzny
	- decyzja o naliczeniu kary będzie podejmowana w systemie wykorzystującym bibliotekę

* ustalenia dotyczące czytelników:
	- nie może być dwóch czytelników o tym samym numerze PESEL
	- student może się stać wykładowcą lub pracownikiem
	- wykładowca może się stać pracownikiem, ale nie sudentem
	- pracownik nie może się stać ani studentem ani wykładowcą
	
* reguły biznesowe należy zaszyć w kodzie biblioteki

* repozytorium danych może być (do wyboru przez kandydata):
	- dowolna baza relacyjna
	- pliki w dowolnym formacie


 





 
 




