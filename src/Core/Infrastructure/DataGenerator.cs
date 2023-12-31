﻿using Core.Services;
using Core.Services.Shared;
using System;
using System.Linq;

namespace Core.Infrastructure
{
    public class DataGenerator
    {
        public static void InitializeUsers(VisitrackDbContext context)
        {
            if (context.Users.Any())
            {
                return;   // Data was already seeded
            }

            context.Users.AddRange(
                new User
                {
                    Id = Guid.Parse("3de6883f-9a0b-4667-aa53-0fbc52c4d300"), // Forced to specific Guid for tests
                    Email = "email1@test.it",
                    Password = "M0Cuk9OsrcS/rTLGf5SY6DUPqU2rGc1wwV2IL88GVGo=", // SHA-256 of text "Prova"
                    FirstName = "Nome1",
                    LastName = "Cognome1",
                    NickName = "Nickname1"
                },
                new User
                {
                    Id = Guid.Parse("a030ee81-31c7-47d0-9309-408cb5ac0ac7"), // Forced to specific Guid for tests
                    Email = "email2@test.it",
                    Password = "Uy6qvZV0iA2/drm4zACDLCCm7BE9aCKZVQ16bg80XiU=", // SHA-256 of text "Test"
                    FirstName = "Nome2",
                    LastName = "Cognome2",
                    NickName = "Nickname2"
                },
                new User
                {
                    Id = Guid.Parse("bfdef48b-c7ea-4227-8333-c635af267354"), // Forced to specific Guid for tests
                    Email = "pippo@mail.com",
                    Password = "B1B3773A05C0ED0176787A4F1574FF0075F7521E", // SHA-256 of text "qwerty"
                    FirstName = "Nome3",
                    LastName = "Cognome3",
                    NickName = "Nickname3"
                });

            context.SaveChanges();
        }
        public static void InitializeVisitors(VisitrackDbContext context)
        {
            if (context.Visitors.Any())
            {
                return;   // Data was already seeded
            }

            context.Visitors.AddRange(
                new Visitor
                {
                    Id = Guid.NewGuid(),
                    Nome = "Nome1",
                    Cognome = "Cognome1",
                    Email = "email1@test.it",
                    Azienda = "Azienda1",
                    Referente = "Referente1",
                    TimestampEntrata = new DateTime(2023, 11, 11, 9, 0, 0),
                    TimestampUscita = new DateTime(2023, 11, 11, 18, 0, 0)
                },
                new Visitor
                {
                    Id = Guid.NewGuid(),
                    Nome = "Nome5",
                    Cognome = "Cognome5",
                    Email = "email5@test.it",
                    Azienda = "Azienda5",
                    Referente = "Referente5",
                    TimestampEntrata = new DateTime(2023, 12, 4, 10, 0, 0),
                    TimestampUscita = new DateTime(2023, 12, 4, 17, 0, 0)
                },
                new Visitor
                {
                    Id = Guid.NewGuid(),
                    Nome = "Nome6",
                    Cognome = "Cognome6",
                    Email = "email6@test.it",
                    Azienda = "Azienda6",
                    Referente = "Referente6",
                    TimestampEntrata = new DateTime(2023, 12, 5, 9, 30, 0),
                    TimestampUscita = new DateTime(2023, 12, 5, 17, 30, 0)
                },
                    new Visitor
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Nome5",
                        Cognome = "Cognome5",
                        Email = "email5@test.it",
                        Azienda = "Azienda5",
                        Referente = "Referente5",
                        TimestampEntrata = new DateTime(2023, 12, 6, 10, 0, 0),
                        TimestampUscita = new DateTime(2023, 12, 4, 17, 0, 0)
                    },
                    new Visitor
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Nome6",
                        Cognome = "Cognome6",
                        Email = "email6@test.it",
                        Azienda = "Azienda6",
                        Referente = "Referente6",
                        TimestampEntrata = new DateTime(2023, 12, 7, 9, 30, 0),
                        TimestampUscita = new DateTime(2023, 12, 5, 17, 30, 0)
                    },
                new Visitor
                {
                    Id = Guid.NewGuid(),
                    Nome = "Pippo",
                    Cognome = "Pluto",
                    Email = "pippo.pluto@paperino.it",
                    Azienda = "Disney",
                    Referente = "Gianluca Verdi",
                    TimestampEntrata = DateTime.Now,

                },
                new Visitor
                {
                    Id = Guid.NewGuid(),
                    Nome = "Gino",
                    Cognome = "Pino",
                    Email = "gino.pino@test.it",
                    Azienda = "UniBo",
                    Referente = "Marco Rossi",
                    TimestampEntrata = DateTime.Now
                });

            context.SaveChanges();
        }
    }
}
