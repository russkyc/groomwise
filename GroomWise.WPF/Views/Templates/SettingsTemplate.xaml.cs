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

namespace GroomWise.Views.Templates;

public partial class SettingsTemplate
{
    public double[] CooldownOptions => new double[] { 3.0, 5.0, 10.0 };

    public SettingsTemplate()
    {
        InitializeComponent();
    }
}
