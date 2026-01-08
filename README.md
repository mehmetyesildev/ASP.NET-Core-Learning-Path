# 🚀 ASP.NET Core 8.0 - Backend Learning Path
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Web API](https://img.shields.io/badge/Architecture-Web%20API-blue?style=for-the-badge)
![Security](https://img.shields.io/badge/Security-JWT%20%26%20Identity-green?style=for-the-badge&logo=json-web-tokens)
![Entity Framework](https://img.shields.io/badge/ORM-Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=nuget&logoColor=white)
![Database](https://img.shields.io/badge/Database-SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![VS Code](https://img.shields.io/badge/Visual%20Studio%20Code-007ACC?style=for-the-badge&logo=visual-studio-code&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=json-web-tokens&logoColor=white)

Bu repository, **ASP.NET Core 8** mimarisini, modern web geliştirme tekniklerini, güvenli API altyapılarını ve veritabanı yönetim süreçlerini adım adım öğrenmek amacıyla geliştirdiğim projelerin kaynak kodlarını içerir.

Projeler, temel **MVC** yapısından başlayarak; form yönetimi, validasyonlar, **RESTful API** tasarımı ve **JWT (JSON Web Token)** tabanlı güvenlik mimarisine kadar uzanan teknik bir serüveni kapsar.

---

## 📂 İçerikteki Projeler ve Teknik Kazanımlar

Bu repo altında, her biri farklı bir yetkinliği hedefleyen 4 ana modül bulunmaktadır:

### 1️⃣ Basics & MeetingApp (MVC Temelleri)
ASP.NET Core dünyasına giriş ve Model-View-Controller (MVC) deseninin kavranması.
* **Routing:** URL yapılandırması ve Controller-Action mekanizması.
* **Razor Syntax:** HTML içerisinde dinamik C# kodlarının işlenmesi.
* **Layouts & Sections:** Tekrar kullanılabilir arayüz şablonlarının (Master Page) oluşturulması.
* **ViewBag & ViewData:** Controller'dan View'a veri taşıma yöntemleri.

### 2️⃣ FormsApp (İleri Seviye Form Yönetimi)
Kullanıcı etkileşimi, veri girişi ve güvenli veri işleme süreçleri.
* **Model Binding:** Form verilerinin (QueryString, FormCollection) nesnelerle otomatik eşleştirilmesi.
* **Tag Helpers:** `asp-for`, `asp-controller` gibi helper yapıları ile temiz HTML yazımı.
* **Server-Side Validation:** Data Annotations (`[Required]`, `[StringLength]`) ile sunucu taraflı veri doğrulama.
* **File Upload:** Ürün görsellerinin sunucuya güvenli bir şekilde yüklenmesi ve yönetimi.

### 3️⃣ EfCoreApp (Veritabanı & ORM)
Veri kalıcılığı için **Entity Framework Core** kullanımı ve SQL süreçlerinin yönetimi.
* **Code-First Approach:** Veritabanı tablolarının C# sınıfları (Entity) üzerinden tasarlanması.
* **Migrations:** Veritabanı şemasındaki değişikliklerin (`add-migration`) kod üzerinden yönetilmesi.
* **Relational Database:**
  * **One-to-Many:** (Örn: Bir Öğretmenin birden fazla Kursu olabilir.)
  * **Many-to-Many:** (Örn: Bir Öğrenci çok kursa, bir Kurs çok öğrenciye sahip olabilir.)
* **LINQ:** Veri sorgulama, filtreleme ve Join işlemleri.

### 4️⃣ ProductAPI (RESTful Servisler & API Güvenliği)
Modern SPA (Single Page Application) ve Mobil uygulamalar için veri sağlayan güvenli Backend servislerinin inşası.
* **Rest Architecture:** `[ApiController]` mimarisi ve HTTP Verbs (GET, POST, PUT, DELETE) standartları.
* **Security (Identity & JWT):**
  * **ASP.NET Core Identity** ile Kullanıcı Kayıt (Register) ve Giriş (Login) işlemleri.
  * **JWT (JSON Web Token)** üretimi ve `[Authorize]` attribute ile uç noktaların (Endpoints) korunması.
* **Data Integrity (DTO):** Entity nesnelerini dış dünyadan gizlemek için **Data Transfer Object** deseninin uygulanması.
* **Integration & Testing:**
  * **CORS Policies:** Farklı domainlerden (Client) gelen isteklere izin verilmesi.
  * **Client Consumption:** JavaScript `Fetch API` kullanılarak yazılmış **Test İstemcisi** ile Token tabanlı veri alışverişi simülasyonu.

---

## 🛠️ Teknoloji Yığını (Tech Stack)

| Kategori | Teknoloji / Araç |
|----------|------------------|
| **Backend** | .NET 8.0, C# 12 |
| **Architectures** | MVC, RESTful API |
| **Security** | ASP.NET Core Identity, JWT (Bearer Token) |
| **ORM** | Entity Framework Core |
| **Database** | SQLite / MS SQL Server LocalDB |
| **Frontend** | Bootstrap 5, HTML5, Javascript (Fetch API) |
| **IDE & Tools** | Visual Studio Code, Git, Postman/Swagger |

---

### 👨‍💻 Geliştirici

**Mehmet Yeşil** - Bilgisayar Mühendisi

🔗 **LinkedIn:** [https://www.linkedin.com/in/mehmet-yesil/]
