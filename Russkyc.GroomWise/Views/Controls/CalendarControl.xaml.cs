// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

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

    private readonly SynchronizedObservableCollection<CalendarDate> _dates;
    public SynchronizedObservableCollection<CalendarDate> Dates => _dates;

    public CalendarControl()
    {
        InitializeComponent();
        CurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        _dates = new SynchronizedObservableCollection<CalendarDate>();
        IsEditable = false;
        DisplayDates();
    }

    private async void DisplayDates()
    {
        Dates.Clear();
        var daysInMonth = DateTime.DaysInMonth(CurrentMonth.Year, CurrentMonth.Month);
        var firstDayOfMonth = new DateTime(CurrentMonth.Year, CurrentMonth.Month, 1);
        var firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek + 1;
        var tasks = new List<Task<CalendarDate>>();
        var dayIndex = 1;
        var dateIndex = 1;
        while (dateIndex <= daysInMonth)
        {
            if (dayIndex >= firstDayOfWeek)
            {
                var date = new DateTime(CurrentMonth.Year, CurrentMonth.Month, dateIndex);
                var isCurrentDate =
                    date.Month == DateTime.Today.Month
                    && date.Year == DateTime.Today.Year
                    && date.Day == DateTime.Today.Day;
                var calendarDate = new CalendarDate()
                    .SetDate(dateIndex)
                    .SetDateInfo(date)
                    .SetSelected(isCurrentDate)
                    .SetCurrentDate(isCurrentDate);
                tasks.Add(Task.FromResult(calendarDate));
                dateIndex++;
            }
            else
            {
                tasks.Add(Task.FromResult<CalendarDate>(null!));
            }
            dayIndex++;
        }

        var tempDates = await Task.WhenAll(tasks);
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
