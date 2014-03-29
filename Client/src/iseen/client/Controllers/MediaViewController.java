package iseen.client.Controllers;

import iseen.client.Main;
import iseen.client.Storage.Memory;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.VBox;

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

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        BigTitle.setText(Memory.CurrentMedia.Title);
        Image.setImage(new javafx.scene.image.Image("http://www.google.dk/images/google_favicon_128.png"));
    }

    public void BreadCrumbHome(MouseEvent actionEvent) {
        try {
            Main.This().GoToPersonalPage();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
