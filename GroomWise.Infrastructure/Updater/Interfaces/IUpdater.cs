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

namespace GroomWise.Infrastructure.Updater.Interfaces;

public interface IUpdater
{
    string GetVersion();
    Task<bool?> CheckForUpdates(bool silent = false);
    Task PerformUpdate(IProgress<double> progress);
}
