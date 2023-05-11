// Copyright (C) 2023 Russell Camo (Russkyc).- All Rights Reserved
//
// Unauthorized copying or redistribution of all files, in source and binary forms via any medium
// without written, signed consent from the author is strictly prohibited.

namespace GroomWise.Services.Data.Migrations;

public class AppointmentsMigration : IDatabaseMigration
{
    public void Migrate()
    {
        BuilderServices
            .Resolve<IDatabaseServiceAsync>()
            .AddMultiple(
                new[]
                {
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Grooming Appointment",
                            "Full grooming session for dog",
                            DateTime.Parse("2023-06-01 10:00:00"),
                            1,
                            1,
                            2,
                            1,
                            0
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Vaccination Appointment",
                            "Yearly vaccination for cat",
                            DateTime.Parse("2023-06-05 14:00:00"),
                            2,
                            2,
                            3,
                            2,
                            0
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Surgery Appointment",
                            "Spay surgery for dog",
                            DateTime.Parse("2023-06-10 09:00:00"),
                            3,
                            3,
                            1,
                            3,
                            1
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Training Appointment",
                            "Basic obedience training for puppy",
                            DateTime.Parse("2023-06-12 11:00:00"),
                            4,
                            4,
                            2,
                            4,
                            0
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Checkup Appointment",
                            "Routine checkup for cat",
                            DateTime.Parse("2023-06-15 15:00:00"),
                            5,
                            5,
                            3,
                            2,
                            0
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Grooming Appointment",
                            "Bath and trim for cat",
                            DateTime.Parse("2023-06-20 10:30:00"),
                            2,
                            6,
                            1,
                            1,
                            0
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Training Appointment",
                            "Advanced obedience training for dog",
                            DateTime.Parse("2023-06-22 13:00:00"),
                            1,
                            4,
                            2,
                            5,
                            0
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Surgery Appointment",
                            "Neuter surgery for cat",
                            DateTime.Parse("2023-06-25 08:30:00"),
                            6,
                            7,
                            1,
                            3,
                            1
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Checkup Appointment",
                            "Routine checkup for dog",
                            DateTime.Parse("2023-06-28 16:00:00"),
                            7,
                            8,
                            3,
                            2,
                            0
                        ),
                    BuilderServices
                        .Resolve<IAppointmentFactory>()
                        .Create(
                            "Grooming Appointment",
                            "Full grooming session for dog",
                            DateTime.Parse("2023-06-30 11:00:00"),
                            1,
                            9,
                            2,
                            1,
                            0
                        )
                }
            );
    }
}
