package iseen.client.Model;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import iseen.client.Entities.Potato;
import iseen.client.Entities.User;
import iseen.client.Exceptions.GeneralError;
import iseen.client.Storage.Memory;

/**
 * Created by seb_000 on 25-03-2014.
 */
public class UserTools {
    private static String PATH_NEW_ACCOUNT = "account/new";
    private static String PATH_EDIT_ACCOUNT = "account/edit";
    private static String PATH_GET_ACCOUNT = "account";
    private static String PATH_LOGIN = "account/login";
    private static String PATH_DELETE_ACCOUNT = "account/delete";
    private static Gson gson = new Gson();

    public static Potato CreateNewAccount(User user) throws Exception {
        String response = HttpCommunication.sendPostPut(PATH_NEW_ACCOUNT, gson.toJson(user), true);

        return PotatoTools.ReportOfPotato_To_Potato(response);
    }

    public static Potato LoginAccount(String email, String password) throws Exception {
        String response = HttpCommunication.sendPostPut(PATH_LOGIN, "[\"" + email + "\",\"" + password + "\"]", true);

        return PotatoTools.ReportOfPotato_To_Potato(response);
    }

    public static User GetAccount() throws Exception {
        String response = HttpCommunication.sendPostPut(PATH_GET_ACCOUNT, PotatoTools.Potato_To_Json(), true);

        return JsonReportOfUser_To_User(response);
    }

    public static User EditAccount(User user) throws Exception {
        StringBuilder sb = new StringBuilder();
        sb.append('[');
        sb.append(gson.toJson(Memory.CurrentPotato));
        sb.append(',');
        sb.append(gson.toJson(user));
        sb.append(']');

        String response = HttpCommunication.sendPostPut(PATH_EDIT_ACCOUNT, sb.toString(), false);

        return JsonReportOfUser_To_User(response);
    }

    public static String DeleteAccount() throws Exception {
        String response = HttpCommunication.sendPostPut(PATH_DELETE_ACCOUNT, gson.toJson(Memory.CurrentPotato), true);

        //int code = GeneralTools.JsonReport_To_DataJsonObject(response).getAsInt();
        int code = GeneralTools.JsonReport_To_JsonObject(response).get("Data").getAsInt();

        if (code == 1)
            return "Success";
        else
            return "Failure";
    }

    private static User JsonReportOfUser_To_User(String Json) throws GeneralError {
        System.out.println(Json);
        //Get data from json report
        JsonObject data = GeneralTools.JsonReport_To_DataJsonObject(Json);

        User user = gson.fromJson(data, User.class);

        return user;

    }
}
