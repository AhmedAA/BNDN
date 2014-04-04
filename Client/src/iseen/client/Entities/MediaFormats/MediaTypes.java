package iseen.client.Entities.MediaFormats;

/**
 * Created by SebastianDybdal on 21-03-2014.
 */
public class MediaTypes {
    public enum Types {
        MEDIA(0), MOVIE(1), MUSIC(2), PICTURE(3);

        private int type;

        Types(int t) {
            type = t;
        }

        public static int MapEnum(Types type) {
            if (type == MOVIE)
                return 1;
            if (type == MUSIC)
                return 2;
            if (type == PICTURE)
                return 3;
            return 0;
        }

    }
//    public static final int Media = 0;
//    public static final int Movie = 1;
//    public static final int Music = 2;
//    public static final int Picture = 3;
}
