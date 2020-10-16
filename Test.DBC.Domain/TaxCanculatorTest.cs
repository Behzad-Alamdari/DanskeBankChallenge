using AutoMapper.Configuration.Conventions;
using DBC.Domain.Utilities;
using DBC.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Test.DBC.Domain
{
    public class TaxCanculatorTest
    {
        Municipality _municipality;
        public TaxCanculatorTest()
        {
            _municipality = new Municipality
            {
                Name = "Copenhagen",
                TaxRules = new List<TaxRule> {
                    new TaxRule
                    {
                        Name = "Yearly",
                        Priority = 1,
                        Percentage = 0.2F,
                        Periods = new List<Period>
                        {
                            new Period
                            {
                                From = new DateTime(2020,1,1),
                                To = new DateTime (2020, 12, 31)
                            }
                        }
                    },
                    new TaxRule
                    {
                        Name = "Monthly",
                        Priority = 2,
                        Percentage = 0.4F,
                        Periods = new List<Period>
                        {
                            new Period
                            {
                                From = new DateTime(2020,5,1),
                                To = new DateTime (2020, 5, 31)
                            }
                        }
                    },
                    new TaxRule
                    {
                        Name = "Daily",
                        Priority = 3,
                        Percentage = 0.1F,
                        Periods = new List<Period>
                        {
                            new Period
                            {
                                From = new DateTime(2020,1,1),
                                To = new DateTime (2020, 1,1)
                            },
                            new Period
                            {
                                From = new DateTime(2020,12,25),
                                To = new DateTime (2020, 12,25)
                            }
                        }
                    }
                }
            };
        }

        [Theory]
        [InlineData(2020, 1, 1)]
        [InlineData(2020, 12, 25)]
        public void CalculateTaxFor_ByGivingDateWithDailyTax_RightPercengateWillBeSelected(int year, int month, int day)
        {
            // Arrange

            var taxCalculator = new TaxCanculator();
            var date = new DateTime(year, month, day);

            // Act
            var percentage = taxCalculator.CalculateTaxFor(_municipality, date);

            //Assert
            percentage.Should().Be(0.1F);
        }

        [Theory]
        [InlineData(2020, 5, 1)]
        [InlineData(2020, 5, 15)]
        [InlineData(2020, 5, 31)]
        public void CalculateTaxFor_ByGivingDateWithMonthlyTax_RightPercengateWillBeSelected(int year, int month, int day)
        {
            // Arrange

            var taxCalculator = new TaxCanculator();
            var date = new DateTime(year, month, day);

            // Act
            var percentage = taxCalculator.CalculateTaxFor(_municipality, date);

            //Assert
            percentage.Should().Be(0.4F);
        }

        [Theory]
        [InlineData(2020, 1, 2)]
        [InlineData(2020, 4, 30)]
        [InlineData(2020, 6, 1)]
        [InlineData(2020, 7, 1)]
        [InlineData(2020, 12, 24)]
        [InlineData(2020, 12, 26)]
        public void CalculateTaxFor_ByGivingDateWithYearlyTax_RightPercengateWillBeSelected(int year, int month, int day)
        {
            // Arrange

            var taxCalculator = new TaxCanculator();
            var date = new DateTime(year, month, day);

            // Act
            var percentage = taxCalculator.CalculateTaxFor(_municipality, date);

            //Assert
            percentage.Should().Be(0.2F);
        }
    }
}
