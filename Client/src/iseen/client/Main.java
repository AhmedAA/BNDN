package iseen.client;

import iseen.client.Entities.Media;
import iseen.client.Entities.Report;
import iseen.client.Exceptions.GeneralError;
import iseen.client.Helpers.HttpCommunication;
import java.io.IOException;
import java.text.ParseException;
import java.util.ArrayList;
import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.layout.StackPane;
import javafx.stage.Stage;
import static javafx.application.Application.launch;

public class Main extends Application {

        public static void main(String[] args) {
            launch(args);
        }

        public void start(Stage primaryStage) {
            primaryStage.setTitle("ISeeN Client");
            Button btn = new Button();
            btn.setText("Get Media");
            btn.setOnAction(new EventHandler<ActionEvent>() {

                public void handle(ActionEvent event) {

                    //TODO: Actual GUI.
                    try {
                        ArrayList<Media> medias = HttpCommunication.GetMedia();
                        System.out.println("===========================================");
                        System.out.println("Medias received");
                        System.out.println("===========================================");
                        for (int i = 0; i<medias.size(); i++) {
                            System.out.println("Title: " + medias.get(i).Title);
                            System.out.println("Id: " + medias.get(i).Id);
                            System.out.println("Type: " + medias.get(i).Type);
                            System.out.println("Release Date: " + medias.get(i).ReleaseDate.toString());
                            System.out.println("Description: " + medias.get(i).Description);
                            System.out.println("======================");
                        }
                    } catch (IOException e) {
                        e.printStackTrace();
                    } catch (ParseException e) {
                        e.printStackTrace();
                    } catch (GeneralError generalError) {
                        generalError.printStackTrace();
                    }

                }
            });

            StackPane root = new StackPane();
            root.getChildren().add(btn);
            primaryStage.setScene(new Scene(root, 300, 250));
            primaryStage.show();
        }
    }
