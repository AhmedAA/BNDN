package iseen.client.Controllers;

import iseen.client.Entities.User;
import iseen.client.Main;
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
            //TODO: GET THIS FROM SERVER
            Memory.CurrentUser = new User();
            Memory.CurrentUser.Name = "Sebastian Dybdal Ehlers";
            Memory.CurrentUser.Gender = "M";
            Memory.CurrentUser.IsAdmin = true;
            Memory.CurrentUser.Email = "sdeh@itu.dk";
            Memory.CurrentUser.Country = "Denmark";
            Memory.CurrentUser.City = "Copenhagen";
            Memory.CurrentUser.Bio = "I am an awesome guy :-) \n Lol \n \n Sups?";
            Main.This().GoToPersonalPage();
        } catch (IOException e) {
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
