package iseen.client.Controllers;

import iseen.client.Entities.Media;
import iseen.client.Exceptions.GeneralError;
import javafx.event.ActionEvent;
import javafx.scene.control.DatePicker;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;

import java.io.IOException;
import java.text.ParseException;
import java.util.ArrayList;

/**
 * Created by SebastianDybdal on 19-03-2014.
 */
public class TestController {
    public DatePicker ReleaseDate;
    public TextField Id;
    public TextField Type;
    public TextField Title;
    public TextArea Description;

    //Method is triggered in TestView by the button
    public void GetAllMedia(ActionEvent actionEvent) {
        try {
            //First we print all medias to the console
            ArrayList<Media> medias = iseen.client.Model.Media.GetMedia();
            System.out.println("===========================================");
            System.out.println("Medias received");
            System.out.println("===========================================");
            for (int i = 0; i<medias.size(); i++) {
                System.out.println("Title: " + medias.get(i).Title);
                System.out.println("Id: " + medias.get(i).Id);
                System.out.println("Type: " + medias.get(i).Type);
                System.out.println("Release Date: " + medias.get(i).ReleaseDate.toString());
                System.out.println("Description: " + medias.get(i).Description);
                System.out.println("======================");
            }

            //Now lets show the first movie.
            ReleaseDate.getEditor().setText(medias.get(0).ReleaseDate.toString()); //BUGGY AS FUCK, BUT FAST FOR TEMP ;-)
            Id.setText(medias.get(0).Id + ""); //little ugly but fast for temp test
            Type.setText(medias.get(0).Type + "");
            Title.setText(medias.get(0).Title);
            Description.setText(medias.get(0).Description);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (ParseException e) {
            e.printStackTrace();
        } catch (GeneralError generalError) {
            generalError.printStackTrace();
        }
    }
}
