package iseen.client.ViewEntities;

import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Main;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.scene.control.Label;
import javafx.scene.layout.VBox;

import java.io.IOException;

/**
 * Created by seb_000 on 29-03-2014.
 */
public class MediaViewEnt extends VBox {
    public MediaViewEnt(Media media) {
        Label title = new Label(media.Title);

        title.setOnMouseClicked(event -> {
            try {
                Memory.CurrentMedia = media;
                Main.This().GoToMediaView(media.Id);
            } catch (IOException e) {
                //TODO: show box
                e.printStackTrace();
            }
        });

        this.getChildren().add(title);
        this.getChildren().add(new Label(media.ReleaseDate.toString()));
        this.getChildren().add(new Label(media.Id +""));
    }
}
