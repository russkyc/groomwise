// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Views.Controls;

public partial class CalendarControl
{
    public CalendarControl()
    {
        InitializeComponent();
        DataContext = new CalendarViewModel();
    }

    internal partial class CalendarViewModel : ViewModelBase
    {
        [ObservableProperty]
        private DateTime _currentMonth;

        [ObservableProperty]
        private CalendarDatesCollection _dates;

        public CalendarViewModel()
        {
            CurrentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            Dates = new CalendarDatesCollection();
            DisplayDates();
        }

        [RelayCommand]
        private void GoPreviousMonth()
        {
            CurrentMonth = CurrentMonth.AddMonths(-1);
            DisplayDates();
        }

        [RelayCommand]
        private void GoNextMonth()
        {
            CurrentMonth = CurrentMonth.AddMonths(1);
            DisplayDates();
        }

        private void DisplayDates()
        {
            // Clear Dates
            Dates.Clear();

            // Get the number of days in the current month
            var daysInMonth = DateTime.DaysInMonth(CurrentMonth.Year, CurrentMonth.Month);

            // Get the first day of the current month
            var firstDayOfMonth = new DateTime(CurrentMonth.Year, CurrentMonth.Month, 1);

            // Get the day of the week for the first day of the current month
            var firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek + 1;

            Task.Run(async () =>
                {
                    var dayIndex = 1;
                    var dateIndex = 1;

                    while (dateIndex <= daysInMonth)
                    {
                        if (dayIndex >= firstDayOfWeek)
                        {
                            lock (_dates.Lock)
                            {
                                _dates.Add(
                                    new CalendarDate()
                                        .SetDate(dateIndex)
                                        .SetDateInfo(CurrentMonth)
                                        .SetSelected(
                                            CurrentMonth.Year == DateTime.Today.Year
                                                && CurrentMonth.Month == DateTime.Today.Month
                                                && dateIndex == DateTime.Now.Day
                                        )
                                );
                            }
                            dateIndex++;
                        }
                        else
                        {
                            lock (_dates.Lock)
                            {
                                _dates.Add(null);
                            }
                        }

                        dayIndex++;
                    }
                })
                .ContinueWith(_ => OnPropertyChanged(nameof(Dates)));
        }
    }
}
