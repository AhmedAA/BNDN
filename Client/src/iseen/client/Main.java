package iseen.client;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import javafx.stage.StageStyle;

import java.io.IOException;

public class Main extends Application {

    private static Main _this;
    public Stage _primaryStage;

    public static void main(String[] args) {
        launch(args);
    }

    public static Main This() {
        return _this;
    }

    public void start(Stage primaryStage) throws Exception {
        Parent root = FXMLLoader.load(getClass().getResource("Views/AppStartView.fxml"));
        primaryStage.setTitle("iSeen");
        Scene login = new Scene(root, 750, 500);
        primaryStage.setScene(login);
        primaryStage.initStyle(StageStyle.UNIFIED);
        _primaryStage = primaryStage;

        _this = this;

        this.setUserAgentStylesheet(STYLESHEET_MODENA);
        primaryStage.resizableProperty().setValue(false);
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

    public void GoToPersonalPage() throws IOException {
        GoToPage("Views/PersonalPageView.fxml");
    }

    public void GoToCreateUserForm() throws IOException {
        GoToPage("Views/CreateUserForm.fxml");
    }

    public void GoToEditUser() throws IOException {
        GoToPage("Views/EditUserForm.fxml");
    }

    public void GoToSearchFieldForm() throws IOException {
        GoToPage("Views/SearchFieldForm.fxml");
    }

    public void GoToMediaView(int id) throws IOException {
        GoToPage("Views/MediaView.fxml");
    }

    public void GoToAddNewMediaForm() throws IOException {
        GoToPage("Views/AddNewMediaForm.fxml");
    }

    public void GoToRentals() throws IOException {
        GoToPage("Views/Rentals.fxml");
    }

    private void GoToPage(String url) throws IOException {
        Parent root = FXMLLoader.load(getClass().getResource(url));
        _primaryStage.setScene(new Scene(root, 750, 500));
    }

}
