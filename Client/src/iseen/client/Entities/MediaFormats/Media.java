package iseen.client.Entities.MediaFormats;

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

    public static Media FromJson (String Json) throws MediaTypeNotMatchedException {
        //TODO:
        //int type = TYPE OF MEDIA FROM Json
        int type=Integer.parseInt(Json);
        //int type = TYPE OF MEDIA FROM Json

        if (type == MediaTypes.Movie)
            return Movie.FromJson(Json);
        if (type == MediaTypes.Music)
            return Music.FromJson(Json);
        if (type == MediaTypes.Picture)
            return Picture.FromJson(Json);
        if (type == MediaTypes.Media)
            return Media._fromJson(Json);

        throw new MediaTypeNotMatchedException("Couldn't find match in MediaTypes");
    }

    private static Media _fromJson (String Json) {
        return null;
    }

    public String ToJason () {
        return null;
    }
}
