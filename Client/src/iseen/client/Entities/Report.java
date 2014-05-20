package iseen.client.Entities;

/**
 * Created by SebastianDybdal on 18-03-2014.
 *
 * General internal format of data.
 * Used both in client and server. This is then sent as JSON objects on the wire, and then handled at the server/client.
 */
public class Report<T> {

    public T Data;
    public int Error;
}
