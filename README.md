<img src="https://raw.githubusercontent.com/russkyc/groomwise-releases/main/groomwise-banner.svg" style="width: 100%;" />

<h2 align="center">GroomWise - A Pet Grooming CRM</h2>

### 1.0 Project Structure and Conventions
#### 1.1 MVVM-S (Model-View-ViewModel-Services)
> MVVM-S is based on the MVVM structure, which stands for Model-View-ViewModel. All business logic, data manipulation and data provision are contained within the Model. All controls, and user interaction are handled within the View. The ViewModel connects the Model to the View and handles interaction logic. While S stands for Services, in MVVM-S Services provide additional modules to manipulate and display data among other things, for a cleaner codebase.

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
- Divis.DarkHelpers.WPF

#### 2.3 Database, Entity Mapping, and Datastore
> Object-Relational-Mapping (ORM)
- FreeSql
- JsonFlatFileDataStore

#### 2.4 Security
> Encryption and Secure Storage
- CredentialManagement(Windows Credential Manager)
- NetCoreEncrypt(Encryption)

#### 2.5 Testing
- Xunit