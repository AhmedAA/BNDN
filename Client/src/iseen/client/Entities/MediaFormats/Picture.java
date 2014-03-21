package iseen.client.Entities.MediaFormats;

import com.google.gson.Gson;
import com.google.gson.JsonObject;

/**
 * Created by SebastianDybdal on 21-03-2014.
 */
public class Picture extends Media {
    public String Author;

    public static Picture FromJson (JsonObject Json, Gson gson) {
        return gson.fromJson(Json,Picture.class);
    }

    public String ToJason () {
        return null;
    }
}
