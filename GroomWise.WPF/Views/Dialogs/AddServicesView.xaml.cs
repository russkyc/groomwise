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

using System;
using System.ComponentModel;
using GroomWise.Extensions;

namespace GroomWise.Views.Dialogs;

public partial class AddServicesView
{
    public TimeSpan[] Durations =>
        new[]
        {
            TimeSpan.FromMinutes(15),
            TimeSpan.FromMinutes(20),
            TimeSpan.FromMinutes(25),
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(35),
            TimeSpan.FromMinutes(40),
            TimeSpan.FromMinutes(45),
            TimeSpan.FromMinutes(50),
            TimeSpan.FromMinutes(55),
            TimeSpan.FromMinutes(60),
            TimeSpan.FromMinutes(65),
            TimeSpan.FromMinutes(70),
            TimeSpan.FromMinutes(75),
            TimeSpan.FromMinutes(80),
            TimeSpan.FromMinutes(85),
            TimeSpan.FromMinutes(90),
            TimeSpan.FromMinutes(95),
            TimeSpan.FromMinutes(100),
            TimeSpan.FromMinutes(105),
            TimeSpan.FromMinutes(110),
            TimeSpan.FromMinutes(115),
            TimeSpan.FromMinutes(120),
            TimeSpan.FromMinutes(125),
            TimeSpan.FromMinutes(130),
            TimeSpan.FromMinutes(135),
            TimeSpan.FromMinutes(140),
            TimeSpan.FromMinutes(145),
            TimeSpan.FromMinutes(150),
            TimeSpan.FromMinutes(155),
            TimeSpan.FromMinutes(160),
            TimeSpan.FromMinutes(165),
            TimeSpan.FromMinutes(170),
            TimeSpan.FromMinutes(175),
            TimeSpan.FromMinutes(180),
            TimeSpan.FromMinutes(185),
            TimeSpan.FromMinutes(190),
            TimeSpan.FromMinutes(195),
            TimeSpan.FromMinutes(200),
            TimeSpan.FromMinutes(205),
            TimeSpan.FromMinutes(210),
            TimeSpan.FromMinutes(215),
            TimeSpan.FromMinutes(220),
            TimeSpan.FromMinutes(225),
            TimeSpan.FromMinutes(230),
            TimeSpan.FromMinutes(235),
            TimeSpan.FromMinutes(240),
            TimeSpan.FromMinutes(245),
            TimeSpan.FromMinutes(250),
            TimeSpan.FromMinutes(255),
            TimeSpan.FromMinutes(260),
            TimeSpan.FromMinutes(265),
            TimeSpan.FromMinutes(270),
            TimeSpan.FromMinutes(275),
            TimeSpan.FromMinutes(280),
            TimeSpan.FromMinutes(285),
            TimeSpan.FromMinutes(290),
            TimeSpan.FromMinutes(295),
            TimeSpan.FromMinutes(300),
            TimeSpan.FromMinutes(305),
            TimeSpan.FromMinutes(310),
            TimeSpan.FromMinutes(315),
            TimeSpan.FromMinutes(320),
            TimeSpan.FromMinutes(325),
            TimeSpan.FromMinutes(330),
            TimeSpan.FromMinutes(335),
            TimeSpan.FromMinutes(340),
            TimeSpan.FromMinutes(345),
            TimeSpan.FromMinutes(350),
            TimeSpan.FromMinutes(355),
            TimeSpan.FromMinutes(360)
        };

    public AddServicesView(object vm)
    {
        DataContext = vm;
        InitializeComponent();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        WindowExtensions.CloseAllAndFocusMainView();
        base.OnClosing(e);
    }
}
