package iseen.client.Entities.MediaFormats;

import com.google.gson.Gson;
import com.google.gson.JsonObject;

/**
 * Created by SebastianDybdal on 21-03-2014.
 */
public class Movie extends Media {
    public String Director = "";

    public static Movie FromJson(JsonObject Json, Gson gson) {
        return gson.fromJson(Json, Movie.class);
    }

    public String ToJson() {
        return null;
    }
}
