package iseen.client.Controllers;

import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;

import java.net.URL;
import java.util.ResourceBundle;

/**
 * Created by seb_000 on 20-03-2014.
 */
public class PersonalPageController implements Initializable {
    public Label TitlePersonName;

    public void FindNewMediaAction(ActionEvent actionEvent) {
    }

    public void AddNewMediaAction(ActionEvent actionEvent) {

    }

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        TitlePersonName.setText(Memory.CurrentUser.Name);
    }
}
