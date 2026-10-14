<div align="center">

# ASP.NET Core Web API Starter

**Template Web API sạch, chuẩn RESTful, thuần Async và Thread-safe trên nền .NET 10 & C# 13.**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13.0-239120?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![OpenAPI](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=flat-square&logo=swagger&logoColor=black)](https://swagger.io/)
[![License](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](LICENSE)

**[Tính năng](#-tính-năng) • [Khởi động nhanh](#-khởi-động-nhanh) • [Cấu trúc](#-cấu-trúc-dự-án) • [API Endpoints](#-danh-sách-api) • [FAQ](#-faq)**

</div>

---

## ✨ Tính năng

| Nhóm | Mô tả chi tiết |
| ---- | -------------- |
| **Kiến trúc 3 lớp** | Phân tầng rõ ràng: `Controllers` ➔ `Services` ➔ `Infrastructures`. |
| **Zero Setup DB** | Tích hợp sẵn In-Memory Mock Store, không cần cài đặt SQL Server hay Docker. |
| **Thread-Safe** | Dữ liệu lưu trữ an toàn bằng `ConcurrentDictionary` và cấp phát ID nguyên tử qua `Interlocked.Increment`. |
| **100% Async I/O** | Toàn bộ tầng Controller và Service xử lý bất đồng bộ qua `Task<T>`. |
| **Xử lý lỗi tập trung** | `GlobalExceptionMiddleware` tự động bắt và trả lỗi chuẩn JSON `{ isSuccess, statusCode, errorCode, message }`. |
| **Data Validation** | Tự động chặn dữ liệu sai qua DataAnnotations và `InvalidModelStateResponseFactory`. |
| **Công cụ test sẵn** | Đi kèm Swagger UI trực quan và file `WebAPIDemo.http` gồm 12 kịch bản test có sẵn. |

---

## 🚀 Khởi động nhanh

### Yêu cầu môi trường
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) trở lên.

### 1. Clone và khởi chạy

```bash
git clone https://github.com/Tuannll/dotnet-webapi-starter.git
cd dotnet-webapi-starter
dotnet run
```

### 2. Truy cập tài liệu API (Swagger UI)

Mở trình duyệt tại:
- **Swagger UI:** `http://localhost:5065/swagger`
- **OpenAPI Json:** `http://localhost:5065/openapi/v1.json`

> [!TIP]
> Bạn có thể mở file `WebAPIDemo.http` ngay trong VS Code (kèm extension REST Client) hoặc Visual Studio để gửi request test trực tiếp không cần mở Postman.

---

## 📁 Cấu trúc dự án

```text
dotnet-webapi-starter/
├── Controllers/              # Tiếp nhận HTTP Request, routing, trả HTTP Status Code
│   ├── ProductsController.cs
│   └── StudentsController.cs
├── Dtos/                     # Đối tượng truyền dữ liệu & Validation Rules
│   ├── FilterDto.cs          # Phân trang và tìm kiếm cơ sở
│   ├── PagingDto.cs          # Kết quả phân trang bọc ngoài
│   ├── Products/
│   └── Students/
├── Exceptions/               # Định nghĩa lỗi nghiệp vụ tùy chỉnh
│   ├── ErrorCodes.cs
│   └── UserFriendlyException.cs
├── Infrastructures/          # Kho lưu trữ dữ liệu (Thread-safe In-Memory Store)
│   ├── ApplicationDbContext.cs
│   └── IApplicationDbContext.cs
├── Middleware/               # Bắt ngoại lệ toàn cục & chuẩn hóa Response lỗi
│   └── GlobalExceptionMiddleware.cs
├── Models/                   # Domain Entities
│   ├── Product.cs
│   └── Student.cs
├── Services/                 # Tầng xử lý nghiệp vụ (Business Logic)
│   ├── Abstracts/
│   └── Implements/
├── Program.cs                # Cấu hình DI, Middleware, Swagger và Pipeline
├── WebAPIDemo.http           # 12 kịch bản test API tự động
└── WebAPIDemo.csproj
```

---

## 📡 Danh sách API

### 1. Products (`/api/products`)

| Method | Endpoint | Mô tả |
| :--- | :--- | :--- |
| `GET` | `/api/products` | Lấy danh sách sản phẩm (hỗ trợ phân trang, lọc theo giá, tìm theo tên) |
| `GET` | `/api/products/{id}` | Lấy chi tiết sản phẩm theo ID |
| `POST` | `/api/products` | Thêm sản phẩm mới (`201 Created` + `Location` header) |
| `PUT` | `/api/products/{id}` | Cập nhật thông tin sản phẩm |
| `DELETE` | `/api/products/{id}` | Xóa sản phẩm theo ID (`204 NoContent`) |

### 2. Students (`/api/students`)

| Method | Endpoint | Mô tả |
| :--- | :--- | :--- |
| `GET` | `/api/students` | Lấy danh sách sinh viên (hỗ trợ phân trang, lọc theo tuổi, tìm theo tên) |
| `GET` | `/api/students/{id}` | Lấy chi tiết sinh viên theo ID |
| `POST` | `/api/students` | Thêm sinh viên mới |
| `PUT` | `/api/students/{id}` | Cập nhật sinh viên |
| `DELETE` | `/api/students/{id}` | Xóa sinh viên |

---

## 🛡️ Định dạng phản hồi lỗi (Error Response Format)

Mọi lỗi trả về từ hệ thống đều tuân thủ cấu trúc đồng nhất:

```json
{
  "isSuccess": false,
  "statusCode": 404,
  "errorCode": 1001,
  "message": "Product with id 999 not found."
}
```

---

## ❓ FAQ

**Q: Làm sao để chuyển sang dùng SQL Server / PostgreSQL thật?**  
A: Tạo DbContext kế thừa từ `Microsoft.EntityFrameworkCore.DbContext`, cài gói NuGet tương ứng (`Microsoft.EntityFrameworkCore.SqlServer` hoặc `Npgsql.EntityFrameworkCore.PostgreSQL`), sau đó cập nhật đăng ký `IApplicationDbContext` trong `Program.cs`.

**Q: Repo này có phù hợp để làm mock server cho Frontend không?**  
A: Có. Dữ liệu mock có sẵn, thao tác CRUD hoạt động ngay trong bộ nhớ, không cần cấu hình cơ sở dữ liệu.

---

## 📄 License

Phát hành dưới giấy phép [MIT](LICENSE). Tự do sử dụng và tùy biến cho mục đích cá nhân lẫn thương mại.
