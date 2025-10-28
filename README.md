PROJECT SỬ DỤNG .NET8.0
1. Khi clone project hãy mở cửa sổ "Package Manage Console" trong Visual Studio
Và chạy 2 dòng lệnh sau để migrate database do dùng code first

LƯU Ý: Vào trong appsetting để đổi connection string cho phù hợp với máy local đang chạy

Add-Migration Initial -p DataAccessLayer -s Presentation
Update-Database -p DataAccessLayer -s Presentation

2. Sau khi chajy migration thành công config lại start up project là Presentation và khởi động