using DBC.DataAccess.EntityFramework;
using DBC.DataAccess.Repositories;
using DBC.Infrastructure.DataAccess;
using DBC.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Test.DBC.DataAccess
{
    public class MunicipalityRepositoryTest
    {
        #region Add
        [Fact]
        public void Add_Manucipality()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityName = "Copenhagen";
            var municipalityName2 = "Copenhagen2";
            var db = new DansBankDbContext(fackDatabaseBuilder);
            var municipalityRepository = new MunicipalityRepository(db);
            var mId = Guid.NewGuid();
            var municipality = new Municipality { Name = municipalityName };
            var municipality2 = new Municipality { Id = mId, Name = municipalityName2 };

            // Act
            var id = municipalityRepository.Add(municipality);
            var id2 = municipalityRepository.Add(municipality2);

            Municipality m = null;
            Municipality m2 = null;
            using (var db2 = new DansBankDbContext(fackDatabaseBuilder))
            {
                m = db2.Municipalities.Find(id);
                m2 = db2.Municipalities.Find(id2);
            }

            // Assert
            id.Should().NotBe(mId);
            id2.Should().Be(mId);
            db.Entry(municipality).State.Should().Be(EntityState.Added);
            db.Entry(municipality2).State.Should().Be(EntityState.Added);
            db.Municipalities.Local.Should().HaveCount(2);
            m.Should().Be(null);
            m2.Should().Be(null);
        }
        #endregion

        #region Exist
        [Fact]
        public async Task Exist_CanHandleDanishSpecialCharacters()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityName = "жеш";


            var id = Guid.NewGuid();
            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                db.Municipalities.Add(new Municipality { Id = id, Name = municipalityName });
                db.SaveChanges();
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var sameString = await municipalityRepository.Exist(municipalityName);
            var caseSensivity = await municipalityRepository.Exist(municipalityName.ToUpper());

            // Assert
            sameString.Should().BeTrue();
            caseSensivity.Should().BeTrue();
        }


        [Fact]
        public async Task Exist_ByGivingAnExistingMunicipalityName_ReturnTrueAsync()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityName = "Copenhagen";


            var id = Guid.NewGuid();
            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                db.Municipalities.Add(new Municipality { Id = id, Name = municipalityName });
                db.SaveChanges();
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var sameString = await municipalityRepository.Exist(municipalityName);
            var caseSensivity = await municipalityRepository.Exist(municipalityName.ToUpper());

            // Assert
            sameString.Should().BeTrue();
            caseSensivity.Should().BeTrue();
        }


        [Fact]
        public async Task Exist_ByGivingNoneExistingMunicipalityName_ReturnFalseAsync()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityName = "Copenhagen";


            var id = Guid.NewGuid();
            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                db.Municipalities.Add(new Municipality { Id = id, Name = municipalityName });
                db.SaveChanges();
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var sameString = await municipalityRepository.Exist("Aarhus");
            var caseSensivity = await municipalityRepository.Exist("Aarhus".ToUpper());

            // Assert
            sameString.Should().BeFalse();
            caseSensivity.Should().BeFalse();
        }

        #endregion

        #region GetAsync

        [Fact]
        public async Task GetAsync_SelectingById_ReturnTheItemAsync()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityName = "Copenhagen";


            var id = Guid.NewGuid();
            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                db.Municipalities.Add(new Municipality { Id = id, Name = municipalityName });
                db.SaveChanges();
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var municipality = await municipalityRepository.GetAsync(id);

            // Assert
            municipality.Should().NotBeNull();
            municipality.Name.Should().Be(municipalityName);
        }


        [Fact]
        public async Task GetAsync_SelectingByPredicate_ReturnTheItemAsync()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityName = "Copenhagen";


            var id = Guid.NewGuid();
            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                db.Municipalities.Add(new Municipality { Id = id, Name = municipalityName });
                db.SaveChanges();
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var municipality = await municipalityRepository.GetAsync(m => m.Name.ToLower() == municipalityName.ToLower());

            // Assert
            municipality.Should().NotBeNull();
            municipality.Name.Should().Be(municipalityName);
        }

        #endregion

        #region GetList

        [Fact]
        public async Task GetList_NotProvideingPaginationAndCondition_WouldReturnAllMunicipalitis()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityNames = new List<string>() { "Copenhagen", "Aarhus", "Aalborg", "Odense", "Esbjerg" };



            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                foreach (var municipalityName in municipalityNames)
                {
                    var id = Guid.NewGuid();
                    db.Municipalities.Add(new Municipality { Id = id, Name = municipalityName });
                    db.SaveChanges();
                }
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var municipalities = await municipalityRepository.GetListAsync();

            // Assert
            municipalities.Should().NotBeNull();
            municipalities.Should().NotBeEmpty();
            municipalities.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetList_ByProvideingCondition_FilterMunicipalitis()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityNames = new List<string>() { "Copenhagen", "Aarhus", "Aalborg", "Odense", "Esbjerg" };



            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                foreach (var municipalityName in municipalityNames)
                {
                    var id = Guid.NewGuid();
                    db.Municipalities.Add(new Municipality { Id = id, Name = municipalityName });
                    db.SaveChanges();
                }
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var municipalities = await municipalityRepository
                .GetListAsync(m => m.Name.ToLower().Contains("b"));

            // Assert
            municipalities.Should().NotBeNull();
            municipalities.Should().NotBeEmpty();
            municipalities.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetList_ByProvideingPagination_FilterMunicipalitis()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityNames = new List<string>() { "Copenhagen", "Aarhus", "Aalborg", "Odense", "Esbjerg" };



            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                foreach (var municipalityName in municipalityNames)
                {
                    var id = Guid.NewGuid();
                    db.Municipalities.Add(new Municipality { Id = id, Name = municipalityName });
                    db.SaveChanges();
                }
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var municipalities = await municipalityRepository
                .GetListAsync(new Pagination { PageNumber = 1, PageSize = 2 });

            // Assert
            municipalities.Should().NotBeNull();
            municipalities.Should().NotBeEmpty();
            municipalities.Should().HaveCount(2);
            municipalities.Select(m => m.Name).SequenceEqual(new List<string> { "Aalborg", "Aarhus" }).Should().BeTrue();
        }

        #endregion

        #region GetWithDetails

        [Fact]
        public async Task GetWithDetails_IncludeDetails()
        {
            // Arrange
            var fackDatabaseBuilder = new FackDataBaseBuildHelper();
            var context = new DansBankDbContext(fackDatabaseBuilder);
            context.Database.EnsureCreated();

            var municipalityName = "Copenhagen";



            using (var db = new DansBankDbContext(fackDatabaseBuilder))
            {
                var id = Guid.NewGuid();
                var m = new Municipality { Id = id, Name = municipalityName, TaxRules = new List<TaxRule>() };
                var tr = new TaxRule { Name = "Yearly", Percentage = 0.2F, Priority = 1 };
                var periods = new List<Period>
                {
                    new Period { From = DateTime.Today, To = DateTime.Today.AddDays(1) },
                    new Period { From = DateTime.Today.AddDays(2), To = DateTime.Today.AddDays(5) }
                };
                tr.Periods = periods;
                m.TaxRules.Add(tr);

                db.Municipalities.Add(m);

                db.SaveChanges();
            }

            var municipalityRepository = new MunicipalityRepository(new DansBankDbContext(fackDatabaseBuilder));

            // Act
            var municipality = await municipalityRepository.GetWithDetails("Copenhagen");

            // Assert
            municipality.Should().NotBeNull();
            municipality.TaxRules.Should().NotBeNullOrEmpty();
            municipality.TaxRules.Should().HaveCount(1);
            var taxRule = municipality.TaxRules.First();
            taxRule.Periods.Should().NotBeNullOrEmpty();
            taxRule.Periods.Should().HaveCount(2);
        }

        #endregion
    }
}
