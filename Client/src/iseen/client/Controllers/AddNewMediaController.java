package iseen.client.Controllers;

import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Entities.MediaFormats.MediaTypes;
import iseen.client.Entities.MediaFormats.Movie;
import iseen.client.Entities.MediaFormats.Music;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TextField;
import javafx.scene.layout.VBox;

import java.net.URL;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.ResourceBundle;

import static iseen.client.Entities.MediaFormats.MediaTypes.Types.MOVIE;
import static iseen.client.Entities.MediaFormats.MediaTypes.Types.MUSIC;

/**
 * Created by Ahmed on 02/04/2014.
 */
public class AddNewMediaController implements Initializable {
    Media currentMedia = Memory.CurrentMedia;

    public TextField Title;
    public ComboBox Type;
    public static MediaTypes Types;
    public TextField ReleaseDate;
    public TextField ImageURL;
    public VBox MediaSpecifics;
    public TextField Description;
    public Button Submit;
    public TextField[] Specifics;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        Title.setText(currentMedia.Title);
        MediaTypes.Types[] mediaTypes = MediaTypes.Types.values();
        Type.getItems().addAll(mediaTypes);
        Type.setValue(String.valueOf(currentMedia.Type));
        ReleaseDate.setText(String.valueOf(currentMedia.ReleaseDate));
        ImageURL.setText(currentMedia.Image);
        Description.setText(currentMedia.Description);

        if (MediaTypes.Types.MapEnum(currentMedia.Type) == MediaTypes.Types.MapEnum(MediaTypes.Types.MOVIE)){
            Specifics = new TextField[1];
            Specifics[1] = new TextField("Director");
            Movie movie = (Movie)currentMedia;
            Specifics[1].setText(movie.Director);
            MediaSpecifics.getChildren().addAll(Specifics[1]);
        }

        if (MediaTypes.Types.MapEnum(currentMedia.Type) == MediaTypes.Types.MapEnum(MediaTypes.Types.MUSIC)){
            Specifics = new TextField[1];
            Specifics[1] = new TextField("Artist");
            Music music = (Music)currentMedia;
            Specifics[1].setText(music.Artist);
            MediaSpecifics.getChildren().addAll(Specifics[1]);
        }
    }

    public void SubmitAction(ActionEvent actionEvent) {
        currentMedia.Title = Title.getText();
        currentMedia.Type = (MediaTypes.Types)Type.getSelectionModel().getSelectedItem();
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
        try {
            Date releaseDate = formatter.parse(ReleaseDate.getText());
            currentMedia.ReleaseDate = releaseDate;
        } catch (ParseException e) {
            e.printStackTrace();
        }

        currentMedia.Description = Description.getText();
        currentMedia.Image = ImageURL.getText();

        if (MediaTypes.Types.MapEnum(currentMedia.Type) == MediaTypes.Types.MapEnum(MOVIE)){
            Specifics = new TextField[1];
            Specifics[1] = new TextField("Director");
            Movie movie = (Movie)currentMedia;
            movie.Director = Specifics[1].getText();
        }

        if (MediaTypes.Types.MapEnum(currentMedia.Type) == MediaTypes.Types.MapEnum(MUSIC)){
            Specifics = new TextField[1];
            Specifics[1] = new TextField("Artist");
            Music music = (Music)currentMedia;
            music.Artist = Specifics[1].getText();
        }
    }
}
