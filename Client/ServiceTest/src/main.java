import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Entities.Potato;
import iseen.client.Entities.User;
import iseen.client.Model.MediaTools;
import iseen.client.Model.UserTools;
import iseen.client.Storage.Memory;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Created by Rasmus on 20-05-2014.
 */
public class main {

    // main
    public static void main(String[] args) {

        runTest();

    }

    // run the tests
    private static void runTest() {
        List<String> strings = new ArrayList<String>();
        strings.add(testInvalidLogin());
        strings.add(testValidLogin());
        strings.add(testEditAccount());

        System.out.println("\n\n\n\n"); // clear console hack
        for ( int i = 0; i < strings.size(); i++) {
            System.out.println(strings.get(i));
        }
    }

    // testing invalid login
    private static String testInvalidLogin() {

        try {
            Potato invalidLogin = UserTools.LoginAccount("notExistingEmail@void.com", "notSoSecretPassword");
            Memory.CurrentPotato = invalidLogin;
            UserTools.GetAccount();
            UserTools.DeleteAccount();
            return ("Failed - testInvalidLogin()");

        } catch (Exception e) {
            return ("Passed - testInvalidLogin()");
        }

    }

    // testing valid login
    private static String testValidLogin() {

        // Bob - our valid test user.
        User bob = new User();
        bob.Name = "Bob";
        bob.Gender = "M";
        bob.Email = "exMail@rentit.dk";
        bob.Country = "Denmark";
        bob.Bio = "Legal login dude";
        bob.City = "Legal login town";
        bob.Id = 1;
        bob.IsAdmin = false;
        bob.Password = "secretPass";

        try {

            // Bob gets created and logs in.
            Potato bobLoggedIn = UserTools.CreateNewAccount(bob);
            Memory.CurrentPotato = bobLoggedIn;

            User bobs = UserTools.GetAccount();
            UserTools.DeleteAccount();

            if (bobs.Name.equals(bob.Name))
                return "Passed - testValidLogin()";
            else
                return "Failed Potato - testValidLogin()";

        } catch (Exception e) {
            System.out.println(e.getMessage());
            e.printStackTrace();
            return "Failed - testValidLogin()";
        }
    }

    private static String testEditAccount() {

        // Bob - our test user.
        User bob = new User();
        bob.Name = "Bob";
        bob.Gender = "M";
        bob.Email = "testEditAccount@rentit.dk";
        bob.Country = "Denmark";
        bob.Bio = "Legal login dude";
        bob.City = "Legal login town";
        bob.Id = 1;
        bob.IsAdmin = false;
        bob.Password = "secretPass";

        try {

            // saving potato
            Potato bobLoggedIn = UserTools.CreateNewAccount(bob);
            // set memory.currentPotato
            Memory.CurrentPotato = bobLoggedIn;

            User bobs = UserTools.GetAccount();

            // John - the new bob.
            User john = new User();
            john.Name = "john";
            john.Gender = "M";
            john.Email = bobs.Email;
            john.Country = "Denmark";
            john.Bio = "Legal login dude";
            john.City = "Legal login town";
            john.Id = bobs.Id;
            john.IsAdmin = false;
            john.Password = "secretPass";

            User johnEdited = UserTools.EditAccount(john);

            UserTools.DeleteAccount();

            if (john.Name.equals(johnEdited.Name))
                return "Passed - testEditAccount()";
            else
                return "Failed Potato - testEditAccount()";

        } catch (Exception e) {
            return "Failed - testEditAccount()";
        }
    }

}
