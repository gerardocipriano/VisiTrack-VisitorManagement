using Core.Services.Shared;
using System;
using System.Linq;
using Core.Services;

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
                    Password = "Uy6qvZV0iA2/drm4zACDLCCm7BE9aCKZVQ16bg80XiU=", // SHA-256 of text "Test"
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
                    Nome = "Nome2",
                    Cognome = "Cognome2",
                    Email = "email2@test.it",
                    Azienda = "Azienda2",
                    Referente = "Referente2",
                    TimestampEntrata = new DateTime(2023, 11, 11, 9, 0, 0),
                    TimestampUscita = new DateTime(2023, 11, 11, 18, 0, 0)
                },
                new Visitor
                {
                    Id = Guid.NewGuid(),
                    Nome = "Nome3",
                    Cognome = "Cognome3",
                    Email = "email3@test.it",
                    Azienda = "Azienda3",
                    Referente = "Referente3",
                    TimestampEntrata = DateTime.Now
                },
                new Visitor
                {
                    Id = Guid.NewGuid(),
                    Nome = "Nome4",
                    Cognome = "Cognome4",
                    Email = "email4@test.it",
                    Azienda = "Azienda4",
                    Referente = "Referente4",
                    TimestampEntrata = DateTime.Now
                });

            context.SaveChanges();
        }
    }
}
