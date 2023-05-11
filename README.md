<img src="https://raw.githubusercontent.com/russkyc/groomwise-releases/main/groomwise-banner.svg" style="width: 100%;" />

<h2 align="center">GroomWise - A Pet Grooming CRM</h2>

### 1.0 Project Structure and Conventions
#### 1.1 MVVM-S (Model-View-ViewModel-Services)
> MVVM-S is based on the MVVM structure, which stands for Model-View-ViewModel. All business logic, data manipulation and data provision are contained within the Model. All controls, and user interaction are handled within the View. The ViewModel connects the Model to the View and handles interaction logic. While S stands for Services, in MVVM-S Services provide additional modules to manipulate and display data among other things, for a cleaner codebase.

#### 1.2 SOLID Principles (OOP)
> The SOLID principles are a set of five principles that provide guidelines for writing clean, maintainable, and scalable object-oriented code. Each letter corresponds to a principle

1. Single Responsibility Principle (SRP): Each class should have only one responsibility, meaning that it should have only one reason to change.
2. Open/Closed Principle (OCP): Software entities should be open for extension but closed for modification. This means that you should be able to add new functionality to a system without having to modify existing code.
3. Liskov Substitution Principle (LSP): Objects of a superclass should be able to be replaced with objects of a subclass without affecting the correctness of the program.
4. Interface Segregation Principle (ISP): Clients should not be forced to depend on interfaces they do not use. This principle promotes the idea of creating smaller and more focused interfaces.
5. Dependency Inversion Principle (DIP): High-level modules should not depend on low-level modules. Instead, both should depend on abstractions. Abstractions should not depend on details. Details should depend on abstractions.

### 2.0 Technologies and Frameworks
#### 2.1 Platform and Design
> Platform, UX design and user interaction behaviors
- WPF (Windows Presentation Framework)
- Russkyc.ModernControls.WPF
- Material.Icons.Wpf

#### 2.2 Core Application Services
> Packages used for MVVM-S, Dependency Injection, App Services
- Russkyc.AttachedUtilities.FileStreamExtensions
- Russkyc.Services.HotkeyListener
- Russkyc.DependencyInjection
- Russkyc.Abstractions
- CommunityToolkit.MVVM
- Microsoft.Xaml.Behaviors

#### 2.3 Database and Entity Mapping
> Object-Relational-Mapping (ORM)
- FreeSql

#### 2.4 Security
> Encryption and Secure Storage
- CredentialManagement(Windows Credential Manager)
- NetCoreEncrypt(Encryption)