package iseen.client.Model;

import com.google.gson.Gson;
import iseen.client.Entities.Potato;
import iseen.client.Storage.Memory;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

/**
 * Created by seb_000 on 20-03-2014.
 */
public class PotatoTools {

    private static Gson gson = new Gson();

    public static Potato ReportOfPotato_To_Potato () {
        throw new NotImplementedException();
    }

    public static String Potato_To_Json () {
        return gson.toJson(Memory.CurrentPotato);
    }
}
