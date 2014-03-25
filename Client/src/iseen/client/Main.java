package iseen.client;

import iseen.client.Entities.Potato;
import iseen.client.Model.GeneralTools;
import iseen.client.Model.HttpCommunication;
import iseen.client.Model.MediaTools;
import iseen.client.Storage.Memory;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.io.IOException;

public class Main extends Application {

    private Stage _primaryStage;
    private static Main _this;

    public void start(Stage primaryStage) throws Exception{
        Parent root = FXMLLoader.load(getClass().getResource("Views/AppStartView.fxml"));
        primaryStage.setTitle("Test");
        primaryStage.setScene(new Scene(root, 750, 500));

        _primaryStage = primaryStage;

        _this = this;

        primaryStage.show();


        //test related
        Memory.CurrentPotato = new Potato();
        Memory.CurrentPotato.EncPassword = "lolPopnese";
        Memory.CurrentPotato.Id = 2;


        MediaTools.GetMediaById(10);
        MediaTools.GetAllMedia();
        MediaTools.DeleteMedia(7);
        MediaTools.CreateNewMedia(MediaTools.GetMediaById(10),new byte[]{2,6});

        System.out.println(GeneralTools.File_To_Json(new byte[]{25,112,122,24,35}));
        System.out.println(GeneralTools.File_To_Json(new byte[]{25,112}));
        System.out.println(GeneralTools.File_To_Json(new byte[]{112}));
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

    private void GoToPage(String url) throws IOException {
        Parent root = FXMLLoader.load(getClass().getResource(url));
        _primaryStage.setScene(new Scene(root, 750, 500));
    }
}
