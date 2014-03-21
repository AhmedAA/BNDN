package iseen.client.Entities.MediaFormats;

import com.google.gson.Gson;
import com.google.gson.JsonObject;

/**
 * Created by SebastianDybdal on 21-03-2014.
 */
public class Music extends Media {
    public String Artist;

    public static Music FromJson (JsonObject Json, Gson gson) {
        return gson.fromJson(Json,Music.class);
    }

    public String ToJason () {
        return null;
    }
}
