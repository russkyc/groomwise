﻿// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Lombok.NET;
using org.russkyc.moderncontrols;
using Swordfish.NET.Collections.Auxiliary;

namespace GroomWise.Views.Controls;

public partial class CalendarControl
{
    public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register(
        nameof(SelectedDate),
        typeof(DateTime),
        typeof(CalendarControl),
        new FrameworkPropertyMetadata(
            default(DateTime),
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
        )
    );

    public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(
        nameof(IsEditable),
        typeof(bool),
        typeof(CalendarControl),
        new FrameworkPropertyMetadata(false)
    );

    public DateTime SelectedDate
    {
        get => (DateTime)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    public bool IsEditable
    {
        get => (bool)GetValue(IsEditableProperty);
        set => SetValue(IsEditableProperty, value);
    }
    public static readonly DependencyProperty CurrentMonthProperty = DependencyProperty.Register(
        nameof(CurrentMonth),
        typeof(DateTime),
        typeof(CalendarControl),
        new FrameworkPropertyMetadata(
            default(DateTime),
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
        )
    );
    public DateTime CurrentMonth
    {
        get => (DateTime)GetValue(CurrentMonthProperty);
        set => SetValue(CurrentMonthProperty, value);
    }

    private readonly ObservableCollection<CalendarDate> _dates;
    public ObservableCollection<CalendarDate> Dates => _dates;

    public CalendarControl()
    {
        InitializeComponent();
        CurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        _dates = new ObservableCollection<CalendarDate>();
        IsEditable = false;
        DisplayDates();
    }

    private async void DisplayDates()
    {
        var currentMonth = CurrentMonth;
        var daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
        var firstDayOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
        var firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek + 1;
        var tasks = new List<Task<CalendarDate>>();
        var dateIndex = 1;
        await Task.Run(() =>
        {
            for (int dayIndex = 1; dateIndex <= daysInMonth; dayIndex++)
            {
                if (dayIndex >= firstDayOfWeek)
                {
                    var date = new DateTime(currentMonth.Year, currentMonth.Month, dateIndex);
                    var isCurrentDate =
                        date.Month == DateTime.Today.Month
                        && date.Year == DateTime.Today.Year
                        && date.Day == DateTime.Today.Day;
                    var calendarDate = new CalendarDate()
                        .WithDate(dateIndex)
                        .WithDateInfo(date)
                        .WithSelected(isCurrentDate)
                        .WithCurrentDate(isCurrentDate);
                    tasks.Add(Task.FromResult(calendarDate));
                    dateIndex++;
                }
                else
                {
                    tasks.Add(Task.FromResult<CalendarDate>(null!));
                }
            }
        });

        var tempDates = await Task.WhenAll(tasks);

        Dates.Clear();
        Dates.AddRange(tempDates);
    }

    private void ToggleButton_OnChecked(object? sender, RoutedEventArgs e)
    {
        if (!(sender is ModernRadioButton control))
            return;
        if (!(control.DataContext is CalendarDate date))
            return;
        SelectedDate = date.DateInfo;
    }

    private void GoPrevious(object sender, RoutedEventArgs e)
    {
        CurrentMonth = CurrentMonth.AddMonths(-1);
        DisplayDates();
    }

    private void GoNext(object sender, RoutedEventArgs e)
    {
        CurrentMonth = CurrentMonth.AddMonths(1);
        DisplayDates();
    }
}

[With]
public partial class CalendarDate
{
    [Property]
    private int _date;

    [Property]
    private DateTime _dateInfo;

    [Property]
    private bool _hasAppointment;

    [Property]
    private bool _selected;

    [Property]
    private bool _currentDate;
}