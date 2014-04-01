package iseen.client.Entities.MediaFormats;

import com.google.gson.Gson;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import iseen.client.Exceptions.MediaTypeNotMatchedException;
import javafx.beans.property.SimpleStringProperty;

import java.util.Date;

/**
 * Created by SebastianDybdal on 18-03-2014.
 */
public class Media {
    public SimpleStringProperty test;
    public int Id = 0;
    public String Title = "";
    public int Type = 0;
    public Date ReleaseDate = new Date(0L);
    public String Description = "";
    public String Image = "";

    public static Media FromJson (JsonObject Json, Gson gson) throws MediaTypeNotMatchedException {
        int type = Json.get("Type").getAsInt();

        //Handle date to java parsable.
        String rel = Json.get("ReleaseDate").getAsString();
        rel = rel.replace('T',' ');
        Json.remove("ReleaseDate");
        Json.addProperty("ReleaseDate",rel);

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

    public String ToJson (Gson gson) {
        return gson.toJson(this);
    }

    private String _toJson (Gson gson) {
        return gson.toJson(this);
    }
}
