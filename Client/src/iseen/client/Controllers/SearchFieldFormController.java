package iseen.client.Controllers;

import iseen.client.Main;
import iseen.client.Model.MediaTools;
import iseen.client.Storage.Memory;
import iseen.client.ViewEntities.MediaViewEnt;
import javafx.event.ActionEvent;
import javafx.geometry.Orientation;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Separator;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.VBox;

import java.io.IOException;

/**
 * Created by SebastianDybdal on 20-03-2014.
 */
public class SearchFieldFormController {
    public TextField SearchField;
    public ComboBox MediaType;
    public VBox SearchResults;

    public void SearchAction(ActionEvent actionEvent) {
        try {
            Memory.SearchResult = MediaTools.SearchMedia(SearchField.getText());

            for (int i = 0; i < Memory.SearchResult.size(); i++) {
                MediaViewEnt mediaViewEnt = new MediaViewEnt(Memory.SearchResult.get(i));
                SearchResults.getChildren().add(mediaViewEnt);
                SearchResults.getChildren().add(new Separator(Orientation.HORIZONTAL));
            }

        } catch (Exception e) {
            e.printStackTrace();
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
