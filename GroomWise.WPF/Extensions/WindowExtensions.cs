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

using System.Linq;
using GroomWise.Views;

namespace GroomWise.Extensions;

public static class WindowExtensions
{
    public static void CloseAllAndFocusMainView()
    {
        var parent = System.Windows.Application.Current.Windows.OfType<MainView>().FirstOrDefault();
        parent!.Focus();
    }
}
