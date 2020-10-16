﻿using DBC.DataAccess.EntityFramework;
using DBC.DataAccess.Repositories;
using DBC.Infrastructure.DataAccess;
using DBC.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Test.DBC.DataAccess
{
    public class TaxRuleRepositoryTest
    {
        [Fact]
        public async Task GetWithDetails_SelectingTasRule_WillIncludePeriosAsync()
        {
            // Arrange
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var connectionProvider = A.Fake<IConnectionStringProvider>();
            A.CallTo(() => connectionProvider.Connection()).Returns(connection);
            var context = new DansBankDbContext(connectionProvider);
            context.Database.EnsureCreated();

            var municipalityName = "Copenhagen";



            var id = Guid.NewGuid();
            var trId = Guid.NewGuid();
            using (var db = new DansBankDbContext(connectionProvider))
            {
                var m = new Municipality { Id = id, Name = municipalityName, TaxRules = new List<TaxRule>() };
                db.Municipalities.Add(m);
                db.SaveChanges();

                var tr = new TaxRule { Id = trId, Name = "Yearly", Percentage = 0.2F, Priority = 1, MunicipalityId = m.Id };
                var periods = new List<Period>
                {
                    new Period { From = DateTime.Today, To = DateTime.Today.AddDays(1) },
                    new Period { From = DateTime.Today.AddDays(2), To = DateTime.Today.AddDays(5) }
                };
                tr.Periods = periods;

                db.TaxRules.Add(tr);

                db.SaveChanges();
            }

            var taxRuleRepository = new TaxRuleRepository(new DansBankDbContext(connectionProvider));

            // Act
            var taxRule = await taxRuleRepository.GetWithDetails(trId);

            // Assert
            taxRule.Should().NotBeNull();
            taxRule.MunicipalityId.Should().Be(id);
            taxRule.Periods.Should().NotBeNullOrEmpty();
            taxRule.Periods.Should().HaveCount(2);
            taxRule.Periods.Select(p => p.TaxRuleId).Should().AllBeEquivalentTo(trId);
        }
    }
}
