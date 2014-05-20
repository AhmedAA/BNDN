package iseen.client.Controllers;

import iseen.client.Entities.MediaFormats.*;
import iseen.client.Main;
import iseen.client.Model.MediaTools;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.VBox;

import java.io.IOException;
import java.net.URL;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ResourceBundle;

import static iseen.client.Entities.MediaFormats.MediaTypes.Types.*;

/**
 * Created by Ahmed on 02/04/2014.
 */
public class AddNewMediaController implements Initializable {
    //Media currentMedia = Memory.CurrentMedia;

    public static MediaTypes TypesOfMedia;
    public TextField Title;
    public ComboBox Type;
    public TextField ReleaseDate;
    public TextField ImageURL;
    public VBox MediaSpecifics;
    public TextField Description;
    public Button Submit;
    public TextField[] Specifics;

    public void SelectedType() {
        MediaSpecifics.getChildren().clear();

        String selectedType = String.valueOf(Type.getSelectionModel().getSelectedItem());

        if (selectedType.equals("MOVIE")) {
            Memory.CurrentMedia = new Movie();
            Specifics = new TextField[1];
            Specifics[0] = new TextField("Director");
            //Movie movie = new Movie();
            Specifics[0].setText("Director");
            MediaSpecifics.getChildren().addAll(Specifics[0]);
        }

        if (selectedType.equals("MUSIC")) {
            Memory.CurrentMedia = new Music();
            Specifics = new TextField[1];
            Specifics[0] = new TextField("Artist");
            //Music music = (Music)currentMedia;
            Specifics[0].setText("Artist");
            MediaSpecifics.getChildren().addAll(Specifics[0]);
        }

        if (selectedType.equals("PICTURE")) {
            Memory.CurrentMedia = new Picture();
            Specifics = new TextField[1];
            Specifics[0] = new TextField("Painter");
            Specifics[0].setText("Painter");
            MediaSpecifics.getChildren().addAll(Specifics[0]);
        }

        if (selectedType.equals("MEDIA")) {
            Memory.CurrentMedia = new Media();
        }
    }

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        Type.getSelectionModel().clearSelection();
        Type.getItems().clear();
        MediaTypes.Types[] mediaTypes = MediaTypes.Types.values();
        Type.getItems().addAll(mediaTypes);

        Title.setText("Title");
        Type.setValue(String.valueOf("Type"));
        ReleaseDate.setText(String.valueOf("Release date"));
        ImageURL.setText("Image URL");
        Description.setText("Description");
    }

    public void SubmitAction(ActionEvent actionEvent) {
        if (Type.getValue() == MediaTypes.Types.MOVIE) {
            Memory.CurrentMedia = new Movie();
            Memory.CurrentMedia.Type = MediaTypes.Types.MapEnum(MOVIE);
            System.out.println(Memory.CurrentMedia.Type + "------");
            ((Movie) Memory.CurrentMedia).Director = Specifics[0].getText();
        }

        if (Type.getValue() == MediaTypes.Types.MUSIC) {
            Memory.CurrentMedia = new Music();
            Memory.CurrentMedia.Type = MediaTypes.Types.MapEnum(MUSIC);
            ((Music) Memory.CurrentMedia).Artist = Specifics[0].getText();
        }

        if (Type.getValue() == MediaTypes.Types.PICTURE) {
            Memory.CurrentMedia = new Picture();
            Memory.CurrentMedia.Type = MediaTypes.Types.MapEnum(PICTURE);
            ((Picture) Memory.CurrentMedia).Painter = Specifics[0].getText();
        }

        Memory.CurrentMedia.Title = Title.getText();
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
        try {
            Memory.CurrentMedia.ReleaseDate = formatter.parse(ReleaseDate.getText());
        } catch (ParseException e) {
            e.printStackTrace();
        }

        Memory.CurrentMedia.Description = Description.getText();
        Memory.CurrentMedia.Image = ImageURL.getText();

        try {
            MediaTools.CreateNewMedia(Memory.CurrentMedia);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            Main.This().GoToPersonalPage();
        } catch (IOException e) {
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
