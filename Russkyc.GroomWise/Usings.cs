// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

// DotNet
global using System;
global using System.Linq;
global using System.Globalization;
global using System.ComponentModel;
global using System.Linq.Expressions;
global using System.Windows;
global using System.Windows.Input;
global using System.Reflection;
global using System.Windows.Data;
global using System.Threading;
global using System.Diagnostics;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.ComponentModel.DataAnnotations;

// Ini.Net
global using Ini.Net;

// Case Extensions
global using CaseExtensions;

// DarkHelpers
global using DarkHelpers.Collections;
global using DarkHelpers.WPF;

// NetCore.Encrypt
global using NETCore.Encrypt;
global using NETCore.Encrypt.Extensions;

// CredentialManagement
global using CredentialManagement;

// CommunityToolkit.Mvvm
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;

// Russkyc.DependencyInjection
global using Russkyc.DependencyInjection.Implementations;
global using Russkyc.DependencyInjection.Interfaces;

// Russkyc.Abstractions
global using Russkyc.Abstractions.Interfaces;
global using Russkyc.Abstractions.Abstractions;

// Russkyc.ModernControls
global using org.russkyc.moderncontrols;
global using org.russkyc.moderncontrols.Helpers;

// Russkyc.FileStreamExtensions
global using Russkyc.AttachedUtilities.FileStreamExtensions;

// Material.Icons
global using Material.Icons;
global using Material.Icons.WPF;

// FreeSql
global using FreeSql;
global using FreeSql.DataAnnotations;

// GroomWise
global using GroomWise.Views;
global using GroomWise.Views.Pages;
global using GroomWise.ViewModels;
global using GroomWise.ViewModels.App;
global using GroomWise.ViewModels.Appointments;
global using GroomWise.ViewModels.Customers;
global using GroomWise.ViewModels.Dashboard;
global using GroomWise.ViewModels.Employees;
global using GroomWise.ViewModels.Pets;
global using GroomWise.ViewModels.Reports;
global using GroomWise.ViewModels.Inventory;
global using GroomWise.ViewModels.Services;
global using GroomWise.Models.Data;
global using GroomWise.Models.Enums;
global using GroomWise.Models.Helper;
global using GroomWise.Models.Converter;
global using GroomWise.Models.Commands;
global using GroomWise.Models.Entities;
global using GroomWise.Models.Interfaces;
global using GroomWise.Models.Collections;
global using GroomWise.Models.Abstractions;
global using GroomWise.Models.Interfaces.View;
global using GroomWise.Models.Interfaces.Service;
global using GroomWise.Models.Interfaces.ViewModel;
global using GroomWise.Views.Dialogs;
global using GroomWise.Services;
global using GroomWise.Services.App;
global using GroomWise.Services.Data;
global using GroomWise.Services.Factory;
global using GroomWise.Services.Repository;
