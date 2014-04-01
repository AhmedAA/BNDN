package iseen.client.Controllers;

import iseen.client.Entities.User;
import iseen.client.Main;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

/**
 * Created by seb_000 on 20-03-2014.
 */
public class PersonalPageController implements Initializable {
    public Label TitlePersonName;
    public Label InfoField;

    public void FindNewMediaAction(ActionEvent actionEvent) {
        try {
            Main.This().GoToSearchFieldForm();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void AddNewMediaAction(ActionEvent actionEvent) {

    }

    public void EditAccountInfo(ActionEvent actionEvent) {
        try {
            Main.This().GoToEditUser();
        } catch (IOException e) {
            e.printStackTrace();
        }

    }
    
    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        User currentUser = Memory.CurrentUser;
        InfoField.setText(currentUser.Email + " - " + currentUser.City + ", " + currentUser.Country + " - " + currentUser.Gender + " - " + Boolean.toString(currentUser.IsAdmin) );
        TitlePersonName.setText(currentUser.Name);
    }
}
