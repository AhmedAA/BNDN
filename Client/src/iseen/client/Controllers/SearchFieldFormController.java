package iseen.client.Controllers;

import iseen.client.Main;
import javafx.event.ActionEvent;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;

import java.io.IOException;

/**
 * Created by SebastianDybdal on 20-03-2014.
 */
public class SearchFieldFormController {
    public TextField SearchField;
    public ComboBox MediaType;

    public void SearchAction(ActionEvent actionEvent) {

    }

    public void BreadCrumbHome(MouseEvent actionEvent) {
        try {
            Main.This().GoToPersonalPage();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
