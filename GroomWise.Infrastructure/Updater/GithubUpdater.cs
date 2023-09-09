﻿// GroomWise
// Copyright (C) 2023  John Russell C. Camo (@russkyc)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY

using GroomWise.Infrastructure.Configuration.Interfaces;
using GroomWise.Infrastructure.Navigation.Interfaces;
using GroomWise.Infrastructure.Updater.Interfaces;
using Injectio.Attributes;
using Onova;
using Onova.Services;

namespace GroomWise.Infrastructure.Updater;

[RegisterSingleton<IUpdater, GithubUpdater>]
public class GithubUpdater : IUpdater
{
    private Version? _latestVersion;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private readonly IConfigurationService _configuration;
    private readonly IUpdateManager _updateManager = new UpdateManager(
        new GithubPackageResolver(
            repositoryOwner: "russkyc",
            repositoryName: "groomwise",
            assetNamePattern: "groomwise-standalone-*.zip"
        ),
        new ZipPackageExtractor()
    );

    public GithubUpdater(
        IConfigurationService configuration,
        IDialogService dialogService,
        INavigationService navigationService
    )
    {
        _configuration = configuration;
        _dialogService = dialogService;
        _navigationService = navigationService;
        var semanticVersion = _configuration.Version.Split(".").Select(Int32.Parse).ToArray();
        _latestVersion = new Version(semanticVersion[0], semanticVersion[1], semanticVersion[2]);
    }

    public string GetVersion()
    {
        return $"{_latestVersion.Major}.{_latestVersion.Minor}.{_latestVersion.Build}";
    }

    public async Task<bool> CheckForUpdates()
    {
        var result = await _updateManager.CheckForUpdatesAsync();

        if (result.CanUpdate is false)
        {
            return false;
        }

        var semanticVersion = _configuration.Version.Split(".").Select(Int32.Parse).ToArray();

        if (semanticVersion is not { })
        {
            return false;
        }

        if (result.LastVersion!.Major > semanticVersion[0])
        {
            _latestVersion = result.LastVersion;
            return true;
        }

        if (result.LastVersion!.Minor > semanticVersion[1])
        {
            _latestVersion = result.LastVersion;
            return true;
        }

        if (result.LastVersion!.Build > semanticVersion[2])
        {
            _latestVersion = result.LastVersion;
            return true;
        }

        _latestVersion = new Version(semanticVersion[0], semanticVersion[1], semanticVersion[2]);
        return false;
    }

    public async Task PerformUpdate(IProgress<double> progress)
    {
        if (_latestVersion is null)
        {
            return;
        }
        await _updateManager.PrepareUpdateAsync(_latestVersion, progress);
        var dialogResult = await Task.Run(
            () =>
                _dialogService.Create(
                    $"Update {_latestVersion} Downloaded",
                    $"Restart now?",
                    _navigationService
                )
        );

        if (dialogResult is false)
        {
            return;
        }
        await Task.Run(() => _updateManager.LaunchUpdater(_latestVersion));
        Environment.Exit(0);
    }
}