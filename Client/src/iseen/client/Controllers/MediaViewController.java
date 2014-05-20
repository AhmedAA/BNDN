package iseen.client.Controllers;

import iseen.client.Entities.MediaFormats.MediaTypes;
import iseen.client.Entities.MediaFormats.Movie;
import iseen.client.Entities.MediaFormats.Music;
import iseen.client.Entities.MediaFormats.Picture;
import iseen.client.Main;
import iseen.client.Model.MediaTools;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.VBox;

import javax.swing.*;
import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

/**
 * Created by seb_000 on 29-03-2014.
 */
public class MediaViewController implements Initializable {
    public Label BigTitle;
    public ImageView Image;
    public VBox Properties;
    public Button InvokeButton;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        BigTitle.setText(Memory.CurrentMedia.Title);
        BigTitle.setWrapText(true);
        Image.setImage(new javafx.scene.image.Image(Memory.CurrentMedia.Image));

        if (Memory.CurrentMedia.Type == MediaTypes.Types.MapEnum(MediaTypes.Types.MOVIE)) {
            Properties.getChildren().add(new Label("Director: " + ((Movie) Memory.CurrentMedia).Director));
        }

        if (Memory.CurrentMedia.Type == MediaTypes.Types.MapEnum(MediaTypes.Types.MUSIC)) {
            Properties.getChildren().add(new Label("Artist: " + ((Music) Memory.CurrentMedia).Artist));
        }

        if (Memory.CurrentMedia.Type == MediaTypes.Types.MapEnum(MediaTypes.Types.PICTURE)) {
            Properties.getChildren().add(new Label("Painter: " + ((Picture) Memory.CurrentMedia).Painter));
        }

        Properties.getChildren().add(new Label("Date Released: " + Memory.CurrentMedia.ReleaseDate.toString()));

        Label decription = new Label("Description: " + Memory.CurrentMedia.Description);
        decription.setWrapText(true);
        decription.maxWidth(500);

        Properties.getChildren().add(decription);

        try {
            if (MediaTools.CheckRented())
                InvokeButton.setText("Click to enjoy this media");
            else
                InvokeButton.setText("Click here to rent this media");
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

    public void BreadCrumbSearch(MouseEvent actionEvent) {
        try {
            Main.This().GoToSearchFieldForm();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void InvokeInvoked(ActionEvent actionEvent) throws Exception {
        if (MediaTools.CheckRented())
            MediaTools.UseRent();
        else {
            MediaTools.RentMedia(Memory.CurrentMedia.Id);
            MediaTools.UseRent();
            InvokeButton.setText("Click to enjoy this media");
        }
    }

    public void GetStats(ActionEvent actionEvent) {
        JOptionPane.showMessageDialog(null, MediaTools.StatsMedia() + " rents are for " + BigTitle.getText());
    }
}
