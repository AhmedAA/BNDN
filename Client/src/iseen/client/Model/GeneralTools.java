package iseen.client.Model;

import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import iseen.client.Entities.Report;
import iseen.client.Exceptions.GeneralError;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

/**
 * Created by SebastianDybdal on 21-03-2014.
 */
public class GeneralTools {
    public static JsonArray JsonReport_To_DataJsonArray (String Json) throws GeneralError {
        //Unpack report into data and error JSON strings
        JsonObject JsonReport = JsonReport_To_JsonObject(Json);

        //Separated into Data and Error
        JsonArray data = JsonReport.getAsJsonArray("Data");
        HandleError(JsonReport.get("Error"));

        return data;
    }

    public static JsonObject JsonReport_To_DataJsonObject (String Json) throws GeneralError {
        //Unpack report into data and error JSON strings
        JsonObject JsonReport = JsonReport_To_JsonObject(Json);

        //Separated into Data and Error
        JsonObject data = JsonReport.getAsJsonObject("Data");
        HandleError(JsonReport.get("Error"));

        return data;
    }

    public static String File_To_Json(byte[] file) {
        if (file.length < 1)
            throw new IllegalArgumentException("Byte array was empty");

        StringBuilder sb = new StringBuilder();
        //TODO: 
        sb.append("\"Derp\"");

        return sb.toString();
    }

    private static JsonObject JsonReport_To_JsonObject (String Json) {
        //Unpack report into data and error JSON strings
        JsonElement top = new JsonParser().parse(Json);
        JsonObject JsonReport = top.getAsJsonObject();
        return JsonReport;
    }

    private static void HandleError (JsonElement error) throws GeneralError {
        if (Integer.parseInt(error.toString()) != 0)
            throw new GeneralError("Error received: " + error.toString());
    }
}
