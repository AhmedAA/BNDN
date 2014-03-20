package iseen.client;

import iseen.client.Exceptions.GeneralError;
import iseen.client.Model.Media;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;

import java.io.IOException;

import java.text.ParseException;

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
    }

    public static void main(String[] args) {
        launch(args);
    }

    public static Main This () {
        return _this;
    }

    public void GoToPersonalPage () throws IOException {
        Parent root = FXMLLoader.load(getClass().getResource("Views/PersonalPageView.fxml"));
        _primaryStage.setScene(new Scene(root, 750, 500));
    }

    public void GoToCreateUserForm () throws IOException {
        Parent root = FXMLLoader.load(getClass().getResource("Views/CreateUserForm.fxml"));
        _primaryStage.setScene(new Scene(root, 750, 500));
    }
}
