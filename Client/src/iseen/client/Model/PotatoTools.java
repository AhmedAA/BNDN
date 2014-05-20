package iseen.client.Model;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import iseen.client.Entities.Potato;
import iseen.client.Exceptions.GeneralError;
import iseen.client.Storage.Memory;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

/**
 * Created by seb_000 on 20-03-2014.
 *
 * Used to handle a users token.
 *
 * Transforms an internal potato to a JSON potato, and vice versa.
 */
public class PotatoTools {

    private static Gson gson = new Gson();

    public static Potato ReportOfPotato_To_Potato (String Json) throws GeneralError {
        System.out.println(Json);
        //Get data from json report
        JsonObject data = GeneralTools.JsonReport_To_DataJsonObject(Json);

        Potato potato = gson.fromJson(data,Potato.class);
        System.out.println(potato.Id);
        System.out.println(potato.EncPassword);

        return potato;

    }

    public static String Potato_To_Json () {
        return gson.toJson(Memory.CurrentPotato);
    }
}
