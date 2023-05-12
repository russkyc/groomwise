// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Models.Entities;

public class CalendarDate
{
    public int Date { get; set; }
    public DateTime DateInfo { get; set; }
    public bool HasAppointment { get; set; }
    public bool Selected { get; set; }

    public CalendarDate SetDate(int date)
    {
        Date = date;
        return this;
    }

    public CalendarDate SetDateInfo(DateTime dateInfo)
    {
        DateInfo = dateInfo;
        return this;
    }

    public CalendarDate SetHasAppointment(bool hasAppointment)
    {
        HasAppointment = hasAppointment;
        return this;
    }

    public CalendarDate SetSelected(bool selected)
    {
        Selected = selected;
        return this;
    }
}
