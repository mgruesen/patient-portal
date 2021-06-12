using PatientPortal.Api.Services;
using Xunit;
using PatientPortal.Api.Mappers;
using System;
using PatientPortal.Api.Domain;
using System.Linq;
using System.Collections.Generic;
using PatientPortal.Api.Models;
using PatientPortal.Api.Extensions;
using Microsoft.Extensions.Options;

namespace PatientPortal.Api.Tests.UnitTests.Services
{
    public class UserServiceTests : TestDatabaseFixture
    {
        private readonly IUserMapper _userMapper;
        private readonly IPasswordHash _passwordHash;
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Username = "u1",
                // p1
                Password = "10000.AvW0ueYFZtDuGXAg5pLdug==.VtKGReqlPeeNlDi761uf0KhXl+3PuLMTWm4PATTq1q4="
            },
            new User
            {
                Username = "u2",
                // p2
                Password = "10000.OcQPhmCID5g27crEOhaXdQ==.9bDeXDtFM5VqPxR4iHaHtfNr8leb5PaurWbnXECZveI="
            },new User
            {
                Username = "u3",
                // p3
                Password = "10000.9HjfgoXZQb4NUTdHv01dfg==.j21EXq22BNtnHBMRgwRJQn+9UZRps8dY+nZGNNXeI/c=",
                IsDeleted = true
            },
        };

        public UserServiceTests(TestDatabaseContext dbContext)
            : base(dbContext)
        {
            _userMapper = new UserMapper();
            _passwordHash = new PasswordHash(Options.Create(new HashOptions()));
        }

        [Fact]
        public void GetAllByIds_WithNoIdList_ShouldNotBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            dbContext.Users.AddRange(_users);
            dbContext.SaveChanges();

            var users = sut.GetByIds();
            Assert.NotEmpty(users);
        }

        [Fact]
        public void GetAllByIds_WithDeletedId_ShouldBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            dbContext.Users.AddRange(_users);
            dbContext.SaveChanges();

            var deleted = dbContext.Users.First(p => p.IsDeleted);
            var users = sut.GetByIds(deleted.Id);
            Assert.Empty(users);
        }

        [Fact]
        public void GetAllByIds_WithId_ShouldReturnUser()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            dbContext.Users.AddRange(_users);
            dbContext.SaveChanges();

            var user = dbContext.Users.First(p => !p.IsDeleted);
            var users = sut.GetByIds(user.Id);
            Assert.NotEmpty(users);
            Assert.Single(users);
            Assert.Equal(user.Id, users.Single().Id);
        }


        [Fact]
        public async void Update_ShouldUpdatePatient()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            var existingUser = new User
            {
                Username = "u",
                Password = "p"
            };
            dbContext.Users.Add(existingUser);

            var patient = new Patient();
            dbContext.Patients.Add(patient);

            dbContext.SaveChanges();

            var existingUserModel = _userMapper.MapUserModel(existingUser);
            existingUserModel.PatientId = patient.Id;

            var updatedUserModel = await sut.Update(existingUserModel);

            Assert.NotNull(updatedUserModel);
            Assert.Equal(existingUserModel.Id, updatedUserModel.Id);
            Assert.Equal(existingUserModel.Username, updatedUserModel.Username);
            Assert.Equal(existingUserModel.PatientId, updatedUserModel.PatientId);
        }

        [Fact]
        public async void DeleteByIds_WithNoIdList_ShouldBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            var user = new User
            {
                Username = "u",
                Password = "p"
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var deletedIds = await sut.DeleteByIds();

            Assert.Empty(deletedIds);

            var userModels = sut.GetByIds(user.Id);

            Assert.NotEmpty(userModels);
            Assert.Equal(user.Id, userModels.First().Id);
        }

        [Fact]
        public async void DeleteByIds_WithIdList_ShouldNotBeEmpty()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            var user1 = new User
            {
                Username = "u1",
                Password = "p1"
            };
            var user2 = new User
            {
                Username = "u2",
                Password = "p2"
            };
            dbContext.Users.AddRange(user1, user2);
            dbContext.SaveChanges();

            var deletedIds = await sut.DeleteByIds(user1.Id);

            Assert.NotEmpty(deletedIds);
            Assert.Contains(user1.Id, deletedIds);
            Assert.DoesNotContain(user2.Id, deletedIds);

            var userModels = sut.GetByIds(user1.Id, user2.Id);

            Assert.NotEmpty(userModels);
            Assert.Contains(user2.Id, userModels.Select(c => c.Id));
            Assert.DoesNotContain(user1.Id, userModels.Select(c => c.Id));
        }

        [Fact]
        public void Authenticate_WithWrongPassword_ShouldReturnNull()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            dbContext.Users.AddRange(_users);
            dbContext.SaveChanges();

            var user = sut.Authenticate(_users[0].Username, "wrongpassword");

            Assert.Null(user);
        }

        [Fact]
        public void Authenticate_WithUnknownUser_ShouldReturnNull()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            dbContext.Users.AddRange(_users);
            dbContext.SaveChanges();

            var user = sut.Authenticate("userdoesntexist", "doesntmatter");

            Assert.Null(user);
        }

        [Fact]
        public void Authenticate_WithValidCredentials_ShouldReturnUser()
        {
            using var transaction = Fixture.Connection.BeginTransaction();
            using var dbContext = Fixture.CreateDbContext(transaction);
            var sut = new UserService(_passwordHash, dbContext, _userMapper);

            dbContext.Users.AddRange(_users);
            dbContext.SaveChanges();

            var userName = "u1";
            var password = "p1";

            var user = sut.Authenticate(userName, password);

            Assert.NotNull(user);
            Assert.Equal(userName, user.Username);
        }
    }
}
