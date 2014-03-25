package iseen.client.Controllers;

import iseen.client.Entities.User;
import iseen.client.Main;
import iseen.client.Model.PotatoTools;
import iseen.client.Model.UserTools;
import iseen.client.Storage.Memory;
import javafx.event.ActionEvent;
import javafx.scene.control.TextField;
import javafx.scene.input.MouseEvent;

import java.io.IOException;

/**
 * Created by seb_000 on 20-03-2014.
 */
public class AppStartController {
    public TextField Email;
    public TextField Password;

    public void LoginAction(ActionEvent actionEvent) {
        try {
            Memory.CurrentPotato = UserTools.LoginAccount(Email.getText(),Password.getText());
            Memory.CurrentUser = UserTools.GetAccount();

            Main.This().GoToPersonalPage();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    public void CreateNewUser(MouseEvent actionEvent) {
        try {
            Main.This().GoToCreateUserForm();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
