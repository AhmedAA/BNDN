package iseen.client.Controllers;

import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Entities.MediaFormats.MediaTypes;
import iseen.client.Entities.MediaFormats.Movie;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TextField;
import javafx.scene.layout.VBox;

import java.net.URL;
import java.util.ResourceBundle;

/**
 * Created by Ahmed on 02/04/2014.
 */
public class AddNewMediaController implements Initializable {
    Media currentMedia = Memory.CurrentMedia;

    public TextField Title;
    public ComboBox Type;
    public TextField ReleaseDate;
    public TextField ImageURL;
    public VBox MediaSpecifics;
    public TextField Description;
    public Button Submit;
    public TextField[] Specifics;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        Title.setText(currentMedia.Title);
        Type.setValue(String.valueOf(currentMedia.Type));
        ReleaseDate.setText(String.valueOf(currentMedia.ReleaseDate));
        ImageURL.setText(currentMedia.Image);
        Description.setText(currentMedia.Description);

        if (currentMedia.Type == MediaTypes.Movie){
            Specifics = new TextField[1];
            Specifics[1] = new TextField("Director");
            Movie movie = (Movie)currentMedia;
            Specifics[1].setText(movie.Director);
            MediaSpecifics.getChildren().addAll(Specifics[1]);
        }
    }

    public void SubmitAction(ActionEvent actionEvent) {

    }
}