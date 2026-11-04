# Sprint1Event

## Описание

Каркас приложения с минимальным набором функциональности при работе с событиями Event (CPUD операции)

## Что нужно знать перед началом

Для запуска проекта потребуется:
* .NET 9 SDK или более новая версия (https://dotnet.microsoft.com/download)
* Visual Studio 2022 или Visual Studio Code

## Как запустить

1. Создайте папку для проекта и перейдите в неё.
2. Выполните команду (компиляция и запуск):
dotnet build EventService.csproj

## Как использовать

Запустите swagger (http://localhost:port/swagger/index.html) и выполните команды CPUD.
-port можно взять из командной строки при запуске приложения:
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5202
	  
## Примеры эндпойнтов API

Метод	Эндпоинт			Описание
GET		/api/events			Получить список событий
GET		/api/events/{id}	Получить событие по ID
POST	/api/events			Создать новое событие
PUT		/api/events/{id}	Изменить событие по ID
DELETE	/api/events/{id}	Удалить событие по ID
