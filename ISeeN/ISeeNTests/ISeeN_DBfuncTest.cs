using System;
using System.Security.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ISeeNEntityModel;
using ISeeNEntityModel.Funcs;
using System.Linq;
using System.Data;
using ISeeNEntityModel.POCO;

namespace ISeeNTests
{
    [TestClass]
    public class ISeeN_DBfuncTest
    {
        #region mediaDB Functions Tests
        [TestMethod]
        public void AddMediaTest()
        {
            // ----- setup -----

            // the media for adding
            var media = new Media()
            {
                Description = "AddMediaTest Desc",
                Id = 1337,
                Image = "",
                ReleaseDate = DateTime.Now,
                Title = "AddMediaTestTitle",
                Type = "3",
            };

            // potato
            var potato = new Potato() 
            {
                EncPassword = "SecretPass",
                Id = 1337
            };

            // a connection
            var conc = new RentIt02Entities();

            // ----- exec -----

            // adding media
            MediaDB.AddMedia(media);

            // extracting media to be checked
            var query = from m in conc.MediaSet
                        where m.Title.ToLower() == media.Title.ToLower()
                        select m;

            // ----- test -----
            Assert.AreEqual("AddMediaTestTitle", query.First().Title);
            
            // ----- tear down ----- 
            MediaDB.DeleteMedia(media.Id, potato);

        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException),
            "Identical media already found. Consider edit")]
        public void AddmediaAlreadyExistsTest()
        {
            // ----- setup -----

            // the media for adding
            var media = new Media()
            {
                Description = "AddMediaTest Desc",
                Id = 1337,
                Image = "",
                ReleaseDate = DateTime.Now,
                Title = "AddMediaTest Title",
                Type = "3",
            };

            // ----- exec -----

            // adding media
            MediaDB.AddMedia(media);
            // adding the same media again
            MediaDB.AddMedia(media);

        }

        [TestMethod]
        public void SearchTextTest()
        {
            // ---- setup ----
            // media
            var media = new Media()
            {
                Description = "SearchMedia",
                Id = 1337,
                Image = "",
                ReleaseDate = DateTime.Now,
                Title = "Searching for media I",
                Type = "1"
            };

            // potato
            var potato = new Potato() 
            {
                EncPassword = "SecretPass",
                Id = 1337
            };

            const string searchString = "Searching for media I";
            MediaDB.AddMedia(media);

            // ---- exec -----
            var mediaList = MediaDB.SearchText(searchString);

            foreach (Media m in mediaList)
            {
                Assert.IsTrue(m.Title.Contains("Searching for media I"));
            }

            // ----- teardown -----
            MediaDB.DeleteMedia(media.Id, potato);
        }

        [TestMethod]
        public void SearchTypeTest()
        {
            // ----- test -----    
            // searching for Movie Types
            var typeResults = MediaDB.SearchType(1);

            foreach (Media m in typeResults)
            {
                Assert.IsTrue(m.Type == "1");
            }
        }

        [TestMethod]
        public void SearchBothTest()
        {
            // ----- setup -----
            // media
            var media = new Media()
            {
                Description = "SearchBothMedia",
                Id = 1337,
                Image = "",
                ReleaseDate = DateTime.Now,
                Title = "Searching for both",
                Type = "2"
            };

            // potato
            var potato = new Potato()
            {
                EncPassword = "SecretPass",
                Id = 1337
            };

            // ----- exec -----
            MediaDB.AddMedia(media);
            var resultMedias = MediaDB.SearchBoth("Searching for both", 2);

            // ----- test -----
            foreach (var m in resultMedias)
            {
                Assert.IsTrue(m.Title.Contains("Searching for both") && m.Type == "2");
            }

            // ----- teardown -----
            MediaDB.DeleteMedia(media.Id, potato);

        }

        [TestMethod]
        public void GetAllTest()
        {
            // ----- setup -----
            var conc = new RentIt02Entities();

            var query = from m in conc.MediaSet
                 select m;

            var expectedMediaList = query.ToList();

            // ----- exec ------
            var actualMediaList = MediaDB.GetAll();

            // ----- test -----
            Assert.IsTrue(expectedMediaList.Count == actualMediaList.Count);

        }

        [TestMethod]
        public void GetMediaForIdTest()
        {
            // ----- setup -----
            var media = new Media()
            {
                Description = "GetMediaID",
                Id = 1337,
                Image = "",
                ReleaseDate = DateTime.Now,
                Title = "GetMediaID",
                Type = "1"
            };

            // potato
            var potato = new Potato()
            {
                EncPassword = "SecretPass",
                Id = 1337
            };

            // the media to be 'getted'
            var inputMedia = MediaDB.AddMedia(media);

            // ----- exec -----
            var outputMedia = MediaDB.GetMediaForId(inputMedia.Id);

            // ----- test -----
            Assert.IsTrue(inputMedia.Id == outputMedia.Id);

            // ----- teardown -----
            MediaDB.DeleteMedia(inputMedia.Id, potato);

        }

        [TestMethod]
        public void EditMediaTest()
        {
            // ----- setup -----
            var media = new Media()
            {
                Description = "Old_Media",
                Id = 1337,
                Image = "",
                ReleaseDate = DateTime.Now,
                Title = "Old_Media",
                Type = "1"
            };

            // potato
            var potato = new Potato()
            {
                EncPassword = "SecretPass",
                Id = 1337
            };

            // adding media to be edited
            var oldMedia = MediaDB.AddMedia(media);

            // the new media to overwrite
            var newMedia = new Media()
            {
                Description = "New_Media",
                Id = oldMedia.Id,
                Image = "",
                ReleaseDate = DateTime.Now,
                Title = "New_Media",
                Type = "1"
            };

            // ----- exec -----
            var updatedMedia = MediaDB.EditMedia(potato, newMedia);

            // ----- test -----
            Assert.IsTrue(newMedia.Title == updatedMedia.Title);

            // ----- teardown -----
            MediaDB.DeleteMedia(updatedMedia.Id, potato);

        }

        [TestMethod]
        public void RentMediaTest()
        {
            // ----- setup -----
            var media = new Media()
            {
                Description = "Media to be Rented!",
                Id = 1337,
                Image = "",
                ReleaseDate = DateTime.Now,
                Title = "Rent Media",
                Type = "1"
            };

            // new user
            var user = new User()
            {
                Id = 1,
                Bio = "RentStuff",
                City = "RentedCity",
                Country = "RentedCountry",
                Email = "RentUserMail@rentit.dk",
                Gender = "M",
                IsAdmin = false,
                Name = "RentGuy",
                Password = "SecretRent"
            };

            var addedPotato = UserDB.AddUser(user);

            var mediaToRent = MediaDB.AddMedia(media);

            // ----- exec -----
            // rent the media
            var rentedMedia = MediaDB.RentMedia(mediaToRent.Id, addedPotato);

            // check if media rented
            var rentedList = RentalDB.CheckUserRented(mediaToRent.Id, addedPotato);

            // ----- test -----
            Assert.IsTrue(rentedList);

            // ----- teardown -----
            MediaDB.DeleteMedia(mediaToRent.Id, addedPotato);
            UserDB.DeleteUser(addedPotato);

        }

        #endregion

        #region UserDB Functions Tests

        [TestMethod]
        public void AddUserTest()
        {
            // ----- setup -----
            // new user
            var user = new User()
            {
                Id = 1,
                Bio = "AddedUser",
                City = "AddCity",
                Country = "AddCountry",
                Email = "addUserMail@rentit.dk",
                Gender = "M",
                IsAdmin = false,
                Name = "AddGuy", // derp
                Password = "SecretAdd"
            };

            // ----- exec -----
            var addedPotato = UserDB.AddUser(user);

            var getUser = UserDB.GetUserByPotato(addedPotato);

            // ----- test -----
            Assert.IsTrue(getUser.Name == "AddGuy");

            // ----- teardown -----
            UserDB.DeleteUser(addedPotato);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCredentialException),
            "User name was not found!")]
        public void LoginUserTest()
        {
            // The call to 'LoginUser' is already beeing tested in the above call to 'AddUser'
            // Therefore we do a 'negative' test using a user/pass set which do not exist in the DB.

            // ----- test -----
            UserDB.LoginUser("Email@DoNotExists.dk", "password");

        }

        [TestMethod]
        public void EditAccountTest()
        {
            // ----- setup -----
            // 'old' user
            var user = new User()
            {
                Id = 1,
                Bio = "oldUser",
                City = "OldCity",
                Country = "OldCountry",
                Email = "OldUser_email@rentit.dk",
                Gender = "M",
                IsAdmin = false,
                Name = "OldGuy", // derp
                Password = "SecretOld"
            };

            // adding user
            var userOld = UserDB.AddUser(user);

            // 'new user'
            var userNew = new User()
            {
                Id = userOld.Id,
                Bio = "newUser",
                City = "newCity",
                Country = "newCountry",
                Email = "newUser_email@rentit.dk",
                Gender = "M",
                IsAdmin = false,
                Name = "NewGuy", // derp
                Password = "SecretNew"
            };

            var updatedUser = UserDB.EditAccount(userOld, userNew);
            var updatedPotato = UserDB.PotatoHandle(userNew.Id);

            // ----- test -----
            Assert.IsTrue(updatedUser.Name == userNew.Name);

            // ----- teardown -----
            UserDB.DeleteUser(updatedPotato);

        }

        [TestMethod]
        public void GetUserByPotatoTest()
        {
            // This test will check two functions:
            // 'GetUserByPotato()' and 'PotatoHandle()'.
            // We add a new User. Retrieves the potato via 'Potatohandle()',
            // and then 'GetUserByPotato()' via this potato.

            // ----- setup -----
            var user = new User()
            {
                Id = 1,
                Bio = "PotatoUser",
                City = "PotatoCity",
                Country = "PotatoCountry",
                Email = "Potato_email@rentit.dk",
                Gender = "M",
                IsAdmin = false,
                Name = "PotatoGuy", // derp
                Password = "SecretPotato"
            };

            // ----- exec -----
            var addedUser = UserDB.AddUser(user);
            var addedUserPotato = UserDB.PotatoHandle(addedUser.Id);
            var getUser = UserDB.GetUserByPotato(addedUserPotato);

            // ----- test -----
            Assert.IsTrue(getUser.Name == user.Name);

            // ----- teardown -----
            UserDB.DeleteUser(addedUserPotato);

        }
        #endregion
    }
}
