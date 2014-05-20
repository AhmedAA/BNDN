package iseen.client.Controllers;

import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Main;
import iseen.client.Model.MediaTools;
import iseen.client.Storage.Memory;
import iseen.client.ViewEntities.MediaViewEnt;
import javafx.fxml.Initializable;
import javafx.geometry.Orientation;
import javafx.scene.control.Label;
import javafx.scene.control.Separator;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.VBox;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.ResourceBundle;

/**
 * Created by SebastianDybdal on 5/20/2014.
 */
public class RentalsController implements Initializable {
    public VBox SearchResults;

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        List<Media> rentals = null;
        try {
            rentals = MediaTools.GetAllRents();

        for (int i = 0; i< rentals.size(); i++) {
            MediaViewEnt mediaViewEnt = new MediaViewEnt(rentals.get(i));
            SearchResults.getChildren().add(mediaViewEnt);
            SearchResults.getChildren().add(new Separator(Orientation.HORIZONTAL));
        }

        } catch (Exception e) {
            SearchResults.getChildren().add(new Label(e.getMessage()));
        }
    }

    public void BreadCrumbHome(MouseEvent actionEvent) {
        try {
            Main.This().GoToPersonalPage();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
