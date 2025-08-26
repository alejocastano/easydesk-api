# 🛠️ EasyDesk

**EasyDesk** is a lightweight, educational Help Desk API built using ASP.NET Core.  

---

## 🚀 Features

- Ticket management (create, update, assign, resolve)
- User roles (Admin, Agent, Submitter)
- Status tracking (Open, In Progress, Resolved, Closed)
---

## 📦 Tech Stack

| Layer             | Technology                 |
|------------------|----------------------------|
| Language          | C#                         |
| Framework         | ASP.NET Core (.NET 8)      |
| ORM               | Entity Framework Core      |
| Database          | PostgreSQL (via Supabase)  |
| Documentation     | Swagger (Swashbuckle)      |
| Testing           | xUnit, Moq                 |
| Authentication    | (Coming Soon)              |

---

## 🧱 Project Structure

```bash
/src
  /EasyDesk.API             # Entry point
  /EasyDesk.Application     # Use cases, interfaces
  /EasyDesk.Domain          # Core entities and logic
  /EasyDesk.Infrastructure  # EFCore, PostgreSQL, external services
/tests
  /EasyDesk.Tests           # Unit tests
