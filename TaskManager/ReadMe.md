# Task Management API

## Опис

**Task Management API** — це RESTful API для управління користувачами та завданнями. API дозволяє реєструвати користувачів, аутентифікувати їх за допомогою JWT-токенів та виконувати CRUD-операції над завданнями.

## Функціональність

- **Користувачі:**
    - Реєстрація користувача.
    - Вхід користувача (автентифікація) з отриманням JWT токена.
- **Завдання:**
    - Отримання списку завдань для автентифікованого користувача.
    - Перегляд окремого завдання за його ID.
    - Додавання нового завдання.
    - Оновлення існуючого завдання.
    - Видалення завдання.

---

## Технології

- **.NET 8**
- **Entity Framework Core (EF Core)**
- **Microsoft SQL Server**
- **JWT (JSON Web Tokens)** для аутентифікації
- **BCrypt** для хешування паролів
- **Swagger** для автоматичної документації API

---

## Запуск проекту

### Локально

1. **Клонування репозиторію:**
   ```bash
   git clone <url>
   cd <project-folder>
   
2. **Налаштування бази даних: У файлі appsettings.json додайте підключення до вашої бази даних:**
   ```json
    "ConnectionStrings": {
    "DefaultConnection": "Server=<your_server>;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
    }   
   
3. **Міграція бази даних:**
   ```bash
    dotnet ef database update
4. **Запуск проекту:**
   ```bash
    dotnet run
5. **Відкриття Swagger UI: Відкрийте в браузері http://localhost:5000/swagger (порт може змінюватися залежно від конфігурації).**
 

## API Ендпоінти
### Користувачі

1. **POST /api/user/register**
- Реєстрація нового користувача.
- Request Body:
  ```json
    {
     "username": "string",
     "email": "string",
     "password": "string"
    }
2. **POST /api/user/login**
- Вхід користувача.
- Request Body:
    ```json
    {
      "username": "string",
      "password": "string"
    }
- Response:
    ```json
    {
      "token": "string"
    }
### Завдання
1. **GET /api/task**
- Отримати список завдань для авторизованого користувача (з фільтрацією та сортуванням).
2. **GET /api/task/{id}**
- Отримати конкретне завдання за його ID.
3. **POST /api/task**
- Створити нове завдання.
- Request Body:
    ```json
    {
      "title": "string",
      "description": "string",
      "dueDate": "2024-12-31T23:59:59",
      "priority": 1
    }
4. **PUT /api/task/{id}**
- Оновити існуюче завдання.
- Request Body:
    ```json
    {
      "title": "string",
      "description": "string",
      "dueDate": "2024-12-31T23:59:59",
      "status": 1,
      "priority": 2
    }
5. **DELETE /api/task/{id}**
- Видалити завдання за його ID.
### Особливості
- Аутентифікація: Всі ендпоінти завдань захищені JWT токенами.
- Валідація: Використовуються DTO із валідацією для запитів.
- Логування помилок: Реалізований глобальний обробник винятків для надання відповідних повідомлень клієнту.
### Автор
- Яременюк Андрій - https://github.com/ZeiteRoN