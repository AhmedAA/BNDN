package iseen.client.Storage;

import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Entities.Potato;
import iseen.client.Entities.User;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by seb_000 on 20-03-2014.
 *
 * Used to handle recently used media, instead of looking them up every time.
 */
public class Memory {
    public static User CurrentUser;
    public static Potato CurrentPotato;
    public static List<Media> SearchResult;
    public static Media CurrentMedia;
}
