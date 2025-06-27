# 🐣 Poultry Farm Management System

A desktop-based poultry farm management application built with **C# (WinForms)** and **MySQL**, designed to automate critical operations of a poultry farm including chick mortality tracking, feed consumption monitoring, batch lifecycle management, and real-time inventory updates.

---

## 📋 Table of Contents

- [🚀 Features](#-features)
- [🛠️ Tech Stack](#️-tech-stack)
- [📸 Screenshots](#-screenshots)
- [📂 Folder Structure](#-folder-structure)
- [🧠 Concepts Implemented](#-concepts-implemented)
- [💾 Setup Instructions](#-setup-instructions)
- [📌 Future Improvements](#-future-improvements)
- [👨‍💻 Author](#-author)
- [📜 License](#-license)

---

## 🚀 Features

- ✅ **Batch Registration**  
  Create and manage poultry batches with breed type, initial count, and start date.

- ✅ **Mortality Recording**  
  Log daily chick mortality with reasons and auto-adjust the remaining count.

- ✅ **Feed Usage Tracking**  
  Record daily feed consumption by batch to monitor efficiency.

- ✅ **Inventory Monitoring**  
  Automatically update feed inventory and alert on low levels.

- ✅ **Live Dashboard**  
  See current batch status, mortality summaries, and feed consumption in one view.

- ✅ **3-Tier Architecture**  
  Separated concerns with clear layers: UI, Business Logic (BL), and Data Access (DL).

- ✅ **Validation & Error Handling**  
  Catch and handle errors gracefully with form validations.

---

## 🛠️ Tech Stack

| Layer         | Technology           |
|---------------|----------------------|
| **Frontend**  | C# WinForms (.NET Framework) |
| **Backend**   | C# Business Logic Layer (BL) |
| **Database**  | MySQL                |
| **Architecture** | 3-Tier (UI → BL → DL) |
| **Programming** | OOP Principles      |

---

## 📸 Screenshots

> Add screenshots in the `screenshots/` folder and reference them here:

- **Dashboard View**  
  ![Dashboard](screenshots/dashboard.png)

- **Batch Management Form**  
  ![Batch Form](screenshots/batch.png)

- **Mortality Entry Form**  
  ![Mortality Form](screenshots/mortality.png)

---

## 📂 Folder Structure

