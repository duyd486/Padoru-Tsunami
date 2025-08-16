# 🎄 Padoru Tsunami
 Zombie Tsunami Clone
> Một tựa game vui nhộn phong cách **endless runner**, lấy cảm hứng từ Zombie Tsunami, nơi bạn điều khiển một bầy Padoru chạy qua thành phố và lôi kéo thêm nhiều đồng đội vào "tsunami" của mình!

![Unity](https://img.shields.io/badge/engine-Unity-000?logo=unity)

---

## 📸 Demo

![image](https://github.com/user-attachments/assets/3ba76c1e-dd10-4dd2-98d1-82c173268060)
![image](https://github.com/user-attachments/assets/a512fb2f-b6c7-4a50-8c57-632b4ecb8846)
![Demo Screenshot](./Screenshots/demo.png)

---

## 🎮 Gameplay

- Điều khiển một Padoru chạy không ngừng nghỉ, tránh chướng ngại vật và thu thập thêm đồng đội.
- Càng nhiều Padoru → càng mạnh → càng vui!
- Gameplay đơn giản, nhưng đòi hỏi phản xạ và chiến thuật.

---

## 🧩 Tính năng nổi bật

- ✅ Cơ chế **chạy liên tục** (endless runner) tương tự Zombie Tsunami
- ✅ Hệ thống **Padoru nối đuôi** có thể tăng/giảm linh hoạt
- ✅ Áp dụng các **design pattern cơ bản**:
  - Singleton – Quản lý các hệ thống toàn cục
  - Object Pool – Tối ưu hiệu năng sinh / hủy đối tượng, đặc biệt dùng nhiều trong việc tái sử dụng của thể loại endless runner
  - Observer – Giao tiếp giữa các hệ thống UI / gameplay

---

## 🛠️ Công nghệ & Công cụ

| Công nghệ / Công cụ | Mục đích |
|---------------------|----------|
| Unity (C#)          | Game engine chính |
| Blender / Photoshop  | Thiết kế đồ họa nhân vật & môi trường |
| Design Patterns     | Tổ chức code gọn gàng & có thể mở rộng |

---

## 📦 Cài đặt & chạy thử

### Yêu cầu
- Unity Editor phiên bản **2021.x** trở lên, được tối ưu nhất là phiên bản **2022.3.61f1**
- Máy tính chạy Windows / macOS

### Clone repo

```bash
git clone https://github.com/duyd486/Padoru-Tsunami.git
```
### Mở trong Unity
- Mở Unity Hub → "Open Project" → Chọn thư mục Padoru-Tsunami
- Nhấn Play để chạy game thử
