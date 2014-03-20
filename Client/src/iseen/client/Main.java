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

    public void start(Stage primaryStage) throws Exception{
        Parent root = FXMLLoader.load(getClass().getResource("Views/TestView.fxml"));
        primaryStage.setTitle("Test");
        primaryStage.setScene(new Scene(root, 750, 500));
        primaryStage.show();
    }


    public static void main(String[] args) {
        launch(args);
    }
}
