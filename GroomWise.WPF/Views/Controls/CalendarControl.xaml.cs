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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using org.russkyc.moderncontrols;
using Swordfish.NET.Collections.Auxiliary;

namespace GroomWise.Views.Controls;

public partial class CalendarControl
{
    // Define the DateChangedEvent
    public static readonly RoutedEvent DateChangedEvent = EventManager.RegisterRoutedEvent(
        "DateChanged",
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(CalendarControl)
    );

    // .NET event wrapper for the DateChangedEvent
    public event RoutedEventHandler DateChanged
    {
        add { AddHandler(DateChangedEvent, value); }
        remove { RemoveHandler(DateChangedEvent, value); }
    }

    public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register(
        nameof(SelectedDate),
        typeof(DateTime),
        typeof(CalendarControl),
        new FrameworkPropertyMetadata(
            default(DateTime),
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnSelectedDateChanged // Added property changed callback
        )
    );

    public DateTime SelectedDate
    {
        get => (DateTime)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    private static void OnSelectedDateChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e
    )
    {
        var control = d as CalendarControl;
        if (control != null)
        {
            // Raise the DateChanged event
            var args = new RoutedEventArgs(DateChangedEvent);
            control.RaiseEvent(args);
        }
    }

    public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register(
        nameof(IsEditable),
        typeof(bool),
        typeof(CalendarControl),
        new FrameworkPropertyMetadata(false)
    );

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
                    var calendarDate = new CalendarDate
                    {
                        Date = dateIndex,
                        DateInfo = date,
                        Selected = isCurrentDate,
                        CurrentDate = isCurrentDate
                    };
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

public class CalendarDate
{
    public int Date { get; init; }
    public DateTime DateInfo { get; init; }
    public bool HasAppointment { get; set; }
    public bool Selected { get; init; }
    public bool CurrentDate { get; init; }
}
