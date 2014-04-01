import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Entities.MediaFormats.Movie;
import iseen.client.Entities.MediaFormats.Music;
import iseen.client.Entities.MediaFormats.Picture;
import iseen.client.Entities.User;
import iseen.client.Model.MediaTools;
import iseen.client.Model.UserTools;
import iseen.client.Storage.Memory;

import java.util.Date;
import java.util.List;

/**
 * Created by Rasmus on 01-04-2014.
 */
public class Main {

   // Method for creating an array of Users
   public static User[] createUsers() {

       User[] users = new User[11];

       for (int i=0; i<10; i++) {
           User user = new User();
           user.Bio = "UserBio " + i;
           user.City = "UserCity " + i;
           user.Country = "UserCountry " + i;
           user.Email = "UserEmail " + i;
           user.Gender = "Male";
           user.Id = i;
           user.IsAdmin = false;
           user.Name = "UserName " + i;
           user.Password = "UserPass " + i;

           users[i] = user;
       };

       return users;
   }

   // Method for creating an array of Movies
   public static Media[] createMovies() {

       Media[] movies = new Movie[11];

       for (int i=0; i<10; i++) {
           Movie movie = new Movie();
           movie.Description = "MovieDesc" + i;
           movie.Id = i;
           movie.ReleaseDate = new Date();
           movie.Title = "MovieTitle" + i;
           movie.Type = 1;
           movie.Director = "Steven Spillerberg!";
           movie.Image = "http://ia.media-imdb.com/images/M/MV5BMTk2NTI1MTU4N15BMl5BanBnXkFtZTcwODg0OTY0Nw@@._V1_SX640_SY720_.jpg";

           movies[i] = movie;
       }

       return movies;
   }

    // Method for creating an array of Pictures
    public static Media[] createPictures() {

        Media[] pictures = new Picture[11];

        for (int i=0; i<10; i++) {
            Picture picture = new Picture();
            picture.Description = "PicDesc" + i;
            picture.Id = i;
            picture.ReleaseDate = new Date();
            picture.Title = "PicTitle" + i;
            picture.Type = 3;
            picture.Image = "http://ia.media-imdb.com/images/M/MV5BMTk2NTI1MTU4N15BMl5BanBnXkFtZTcwODg0OTY0Nw@@._V1_SX640_SY720_.jpg";
            picture.Painter = "IMDB";

            pictures[i] = picture;
        }

        return pictures;
    }

    // Method for creating an array of Musics
    public static Media[] createMusics() {

        Media[] musics = new Music[11];

        for (int i=0; i<10; i++) {
            Music music = new Music();
            music.Description = "MusDesc" + i;
            music.Id = i;
            music.ReleaseDate = new Date();
            music.Title = "PMusTitle" + i;
            music.Type = 2;
            music.Artist = "50 Cents";
            music.Image = "http://ia.media-imdb.com/images/M/MV5BMTk2NTI1MTU4N15BMl5BanBnXkFtZTcwODg0OTY0Nw@@._V1_SX640_SY720_.jpg";

            musics[i] = music;
        }

        return musics;
    }

   public static User createAdmin() {

       User user = new User();
       user.Name = "Kongen af Lyngby";
       user.Password = "Admin password";
       user.IsAdmin = true;
       user.Id = 1337;
       user.Bio = "Vildeste user EU";
       user.City = "Lyngby";
       user.Country = "Denmark";
       user.Email = "Admin email";
       user.Gender = "Male";

       return user;
   }

   public static void main(String[] args) throws Exception {

       System.setProperty("http.proxyHost", "localhost");
       System.setProperty("http.proxyPort", "8888");

       System.out.println("-----! Program RUNNING !-----");

       // Opretter Admin user & modtager kartoffel
       User admin = createAdmin();
       // Memory.CurrentPotato = UserTools.CreateNewAccount(admin);
       Memory.CurrentPotato = UserTools.LoginAccount(admin.Email, admin.Password);
       Memory.CurrentUser = UserTools.GetAccount();

       // Opretter Stuff i DB
//       Media[] movies = createMovies();
//       Media[] pictures = createPictures();
//       Media[] musics = createMusics();
         User[] users = createUsers();
//
//       byte[] bytes = new byte[10];
//
//       for (int i=0; i<11; i++) {
//           MediaTools.CreateNewMedia(movies[i], bytes);
//           MediaTools.CreateNewMedia(pictures[i], bytes);
//           MediaTools.CreateNewMedia(musics[i], bytes);
//       }

       // Tjekker hvorvidt Medias rent faktisk er blevet oprettet!?
       List<Media> medias = MediaTools.GetAllMedia();

       for (int i=0; i<medias.size(); i++) {
           System.out.println("Media title: " + medias.get(i).Title);
       }

       for (int i=0; i<10; i++) {
           Memory.CurrentPotato = UserTools.CreateNewAccount(users[i]);
           System.out.println("Brugernavn: " + UserTools.GetAccount().Name);
       }
   }
}
