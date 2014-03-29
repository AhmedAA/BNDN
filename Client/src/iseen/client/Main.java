package iseen.client;

import iseen.client.Entities.Potato;
import iseen.client.Entities.User;
import iseen.client.Model.GeneralTools;
import iseen.client.Model.HttpCommunication;
import iseen.client.Model.MediaTools;
import iseen.client.Model.UserTools;
import iseen.client.Storage.Memory;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import javafx.stage.StageStyle;

import java.io.IOException;

public class Main extends Application {

    private Stage _primaryStage;
    private static Main _this;

    public void start(Stage primaryStage) throws Exception{
        Parent root = FXMLLoader.load(getClass().getResource("Views/AppStartView.fxml"));
        primaryStage.setTitle("Test");
        Scene login = new Scene(root, 750, 500);
        primaryStage.setScene(login);
        primaryStage.initStyle(StageStyle.UNIFIED);
        _primaryStage = primaryStage;

        _this = this;

        this.setUserAgentStylesheet(STYLESHEET_MODENA);

        primaryStage.show();


        /*/
        Fiddler setup
         */
        System.setProperty("http.proxyHost", "localhost");
        System.setProperty("http.proxyPort", "8888");

        /*//test related
        User user = new User();
        user.Name = "Sebastian";
        user.Bio = "This is bio";
        user.City = "Compton";
        user.Country = "lolpop";
        user.Email = "test@test.com";
        user.Gender = "M";
        user.Id = 1;
        user.IsAdmin = true;
        user.Password = "testtest";
        UserTools.CreateNewAccount(user);*/


    }

    public static void main(String[] args) {
        launch(args);
    }

    public static Main This () {
        return _this;
    }

    public void GoToPersonalPage () throws IOException {
        GoToPage("Views/PersonalPageView.fxml");
    }

    public void GoToCreateUserForm () throws IOException {
        GoToPage("Views/CreateUserForm.fxml");
    }

    public void GoToSearchFieldForm() throws IOException {
        GoToPage("Views/SearchFieldForm.fxml");
    }

    public void GoToMediaView(int id) throws IOException {
        GoToPage("Views/MediaView.fxml");
    }

    private void GoToPage(String url) throws IOException {
        Parent root = FXMLLoader.load(getClass().getResource(url));
        _primaryStage.setScene(new Scene(root, 750, 500));
    }

}
