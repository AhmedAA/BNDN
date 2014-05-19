package iseen.client.Controllers;

import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Entities.MediaFormats.MediaTypes;
import iseen.client.Entities.MediaFormats.*;
import iseen.client.Main;
import iseen.client.Storage.Memory;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.VBox;

import java.io.IOException;
import java.lang.reflect.Field;
import java.net.URL;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ResourceBundle;

/**
 * Created by seb_000 on 29-03-2014.
 */
public class MediaViewController implements Initializable {
    public Label BigTitle;
    public ImageView Image;
    public VBox Properties;
    public TextArea MediaInfo;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        BigTitle.setText(Memory.CurrentMedia.Title);
        Image.setImage(new javafx.scene.image.Image(Memory.CurrentMedia.Image));
        MediaInfo.setWrapText(true);

        if (Memory.CurrentMedia.Type == MediaTypes.Types.MapEnum(MediaTypes.Types.MOVIE)) {
            MediaInfo.appendText("Director: " + ((Movie) Memory.CurrentMedia).Director + "\n\n");
        }

        if (Memory.CurrentMedia.Type == MediaTypes.Types.MapEnum(MediaTypes.Types.MUSIC)) {
            MediaInfo.appendText(String.format("%s\n\n", "Artist: " + ((Music) Memory.CurrentMedia).Artist));
        }

        if (Memory.CurrentMedia.Type == MediaTypes.Types.MapEnum(MediaTypes.Types.PICTURE)) {
            MediaInfo.appendText("Painter: " + ((Picture) Memory.CurrentMedia).Painter + "\n\n");
        }

        MediaInfo.appendText("Date Released: " + Memory.CurrentMedia.ReleaseDate.toString() + "\n\n");
        MediaInfo.appendText("Description: " + Memory.CurrentMedia.Description + "\n");
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
}
