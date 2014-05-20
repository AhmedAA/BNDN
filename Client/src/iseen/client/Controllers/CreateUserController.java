package iseen.client.Controllers;

import iseen.client.Entities.User;
import iseen.client.Main;
import iseen.client.Model.UserTools;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TextField;

import java.net.URL;
import java.util.InputMismatchException;
import java.util.ResourceBundle;

/**
 * Created by seb_000 on 20-03-2014.
 */
public class CreateUserController implements Initializable {
    public TextField Name;
    public TextField Email;
    public TextField Password1;
    public TextField Password2;
    public TextField City;
    public TextField Country;
    public ComboBox Gender;
    public TextField Bio;

    public void SubmitAction(ActionEvent actionEvent) {
        User user = new User();
        user.Name = Name.getText();
        user.Email = Email.getText();

        if (!Password1.getText().equals(Password2.getText()))
            throw new InputMismatchException("Password 1 was not identical to password 2");

        user.Password = Password1.getText();
        user.City = City.getText();
        user.Country = Country.getText();
        user.Gender = (String) Gender.getValue();
        user.Bio = Bio.getText();
        user.Id = 1;

        try {
            Memory.CurrentPotato = UserTools.CreateNewAccount(user);
            Memory.CurrentUser = UserTools.GetAccount();
            Main.This().GoToPersonalPage();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        Gender.getItems().addAll(User.Genders());
        Gender.setValue(User.Genders()[0]);
    }
}
