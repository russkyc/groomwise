// GroomWise
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
        return $"{_latestVersion!.Major}.{_latestVersion.Minor}.{_latestVersion.Build}";
    }

    public async Task<bool?> CheckForUpdates(bool silent = false)
    {
        if (_configuration.CheckForUpdates is false)
        {
            return false;
        }
        var result = await _updateManager.CheckForUpdatesAsync();
        var semanticVersion = _configuration.Version.Split(".").Select(Int32.Parse).ToArray();

        if (
            result.LastVersion is { }
            && result.LastVersion
                > new Version(semanticVersion[0], semanticVersion[1], semanticVersion[2])
        )
        {
            _latestVersion = result.LastVersion;
            return await Task.Run(
                () =>
                    _dialogService.CreateYesNo(
                        "New Update Available",
                        $"Update to Version {_latestVersion.Major}.{_latestVersion.Minor}.{_latestVersion.Build}?",
                        _navigationService
                    )
            );
        }
        if (silent)
        {
            return false;
        }
        await Task.Run(
            () =>
                _dialogService.CreateOk(
                    "No Updates Available",
                    "You are using the latest version",
                    _navigationService
                )
        );
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
                _dialogService.CreateYesNo(
                    $"Update {_latestVersion} Downloaded",
                    "Install update and restart application?",
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
