package iseen.client.Controllers;

import iseen.client.Entities.Potato;
import iseen.client.Entities.User;
import iseen.client.Main;
import iseen.client.Model.PotatoTools;
import iseen.client.Model.UserTools;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.fxml.Initializable;
import javafx.scene.control.*;
import javafx.scene.control.TextField;

import java.awt.*;
import java.net.URL;
import java.util.InputMismatchException;
import java.util.ResourceBundle;

/**
 * Created by Ahmed on 01/04/2014.
 */
public class EditUserController implements Initializable {

    public TextField Name;
    public TextField Email;
    public TextField Password1;
    public TextField Password2;
    public TextField City;
    public TextField Country;
    public ComboBox Gender;
    public TextField Bio;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        User currentUser = Memory.CurrentUser;

        Name.setText(currentUser.Name);
        Email.setText(currentUser.Email);
        Password1.setText(currentUser.Password);
        Password2.setText(currentUser.Password);
        City.setText(currentUser.City);
        Country.setText(currentUser.Country);
        Gender.setValue(currentUser.Gender);
        Bio.setText(currentUser.Bio);
    }

    public void SubmitAction(ActionEvent actionEvent) {
        User newUserInfo = Memory.CurrentUser;
        newUserInfo.Name = Name.getText();
        newUserInfo.Email = Email.getText();
        newUserInfo.City = City.getText();
        newUserInfo.Country = Country.getText();
        newUserInfo.Gender = Gender.getValue().toString();
        newUserInfo.Bio = Bio.getText();

        if (!Password1.getText().equals(Password2.getText()))
            throw new InputMismatchException("Password 1 was not identical to password 2");

        newUserInfo.Password = Password1.getText();

        try {
            Memory.CurrentUser = UserTools.EditAccount(newUserInfo);
            Memory.CurrentPotato = UserTools.LoginAccount(Memory.CurrentUser.Email, Memory.CurrentUser.Password);
            Main.This().GoToPersonalPage();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
