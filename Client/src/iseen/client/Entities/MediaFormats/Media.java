package iseen.client.Entities.MediaFormats;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import iseen.client.Exceptions.MediaTypeNotMatchedException;

import java.util.Date;

/**
 * Created by SebastianDybdal on 18-03-2014.
 */
public class Media {
    public int Id = 0;
    public String Title = "";
    public int Type = 0;
    public Date ReleaseDate = new Date(0L);
    public String Description = "";

    public static Media FromJson (JsonObject Json, Gson gson) throws MediaTypeNotMatchedException {
        int type = Integer.parseInt(Json.get("Type").toString());

        if (type == MediaTypes.Movie)
            return Movie.FromJson(Json,gson);
        if (type == MediaTypes.Music)
            return Music.FromJson(Json,gson);
        if (type == MediaTypes.Picture)
            return Picture.FromJson(Json,gson);
        if (type == MediaTypes.Media)
            return Media._fromJson(Json,gson);

        throw new MediaTypeNotMatchedException("Couldn't find match in MediaTypes");
    }

    private static Media _fromJson (JsonObject Json,Gson gson) {
        return gson.fromJson(Json,Media.class);
    }

    public String ToJason () {
        return null;
    }
}
