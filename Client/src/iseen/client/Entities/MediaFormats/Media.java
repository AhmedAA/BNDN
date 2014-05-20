package iseen.client.Entities.MediaFormats;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonObject;
import iseen.client.Exceptions.MediaTypeNotMatchedException;
import javafx.beans.property.SimpleStringProperty;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * Created by SebastianDybdal on 18-03-2014.
 */
public class Media {
    public SimpleStringProperty test;
    public int Id = 0;
    public String Title = "";
    //public int Type = 0;
    public int Type;
    public Date ReleaseDate = new Date();
    public String Description = "";
    public String Image = "";

    public static Media FromJson(JsonObject Json, Gson gson) throws MediaTypeNotMatchedException {
        gson = new GsonBuilder().setDateFormat("yyyy-MM-dd").create();
        int type = Json.get("Type").getAsInt();

        //Handle date to java parsable.
        String rel = Json.get("ReleaseDate").getAsString();
        String newRel = rel.replace('T', ' ');
        SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd");
        try {
            Date date = formatter.parse(newRel);
            String formattedDate = formatter.format(date);
            Json.remove("ReleaseDate");
            Json.addProperty("ReleaseDate", formattedDate);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        if (type == MediaTypes.Types.MapEnum(MediaTypes.Types.MOVIE))
            return Movie.FromJson(Json, gson);
        if (type == MediaTypes.Types.MapEnum(MediaTypes.Types.MUSIC))
            return Music.FromJson(Json, gson);
        if (type == MediaTypes.Types.MapEnum(MediaTypes.Types.PICTURE))
            return Picture.FromJson(Json, gson);
        if (type == MediaTypes.Types.MapEnum(MediaTypes.Types.MEDIA))
            return Media._fromJson(Json, gson);

//        if (type == MediaTypes.Movie)
//            return Movie.FromJson(Json,gson);
//        if (type == MediaTypes.Music)
//            return Music.FromJson(Json,gson);
//        if (type == MediaTypes.Picture)
//            return Picture.FromJson(Json,gson);
//        if (type == MediaTypes.Media)
//            return Media._fromJson(Json,gson);

        throw new MediaTypeNotMatchedException("Couldn't find match in MediaTypes");
    }

    private static Media _fromJson(JsonObject Json, Gson gson) {
        return gson.fromJson(Json, Media.class);
    }

    public String ToJson(Gson gson) {
        System.out.println(Type);
        return gson.toJson(this);
    }

    private String _toJson(Gson gson) {
        return gson.toJson(this);
    }
}
