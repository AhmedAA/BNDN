package iseen.client.ViewEntities;

import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Main;
import iseen.client.Storage.Memory;
import javafx.scene.control.Label;
import javafx.scene.image.ImageView;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.scene.text.Font;

import java.io.IOException;

/**
 * Created by seb_000 on 29-03-2014.
 */
public class MediaViewEnt extends HBox {
    public MediaViewEnt(Media media) {
        setSpacing(10.0);
        setOnMouseClicked(event -> {
            try {
                Memory.CurrentMedia = media;
                Main.This().GoToMediaView(media.Id);
            } catch (IOException e) {
                //TODO: show box
                e.printStackTrace();
            }
        });

        try {
            ImageView image = new ImageView(media.Image);
            image.setFitHeight(200);
            image.setPreserveRatio(true);
            image.setSmooth(true);
            image.setFitWidth(200);
            this.getChildren().add(image);
        } catch (Exception e) {
            e.printStackTrace();
        }

        VBox vbox = new VBox();

        this.getChildren().add(vbox);

        Label title = new Label(media.Title);
        title.setFont(new Font("Arial", 18));
        vbox.getChildren().add(title);

        Label releasedate = new Label(media.ReleaseDate.toString());
        releasedate.setFont(new Font("Arial", 13));

        vbox.getChildren().add(releasedate);

        String text = media.Description;
        if (text != null && text.length() > 500)
            text = text.substring(0, 500);

        Label description = new Label(text + "...");
        description.setWrapText(true);
        description.setMaxWidth(500);

        vbox.getChildren().add(description);
    }
}
