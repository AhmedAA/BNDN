package iseen.client.Model;

import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.sun.org.apache.xerces.internal.impl.dv.util.Base64;
import iseen.client.Entities.Report;
import iseen.client.Exceptions.GeneralError;
import iseen.client.Main;
import javafx.stage.FileChooser;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

import java.awt.*;
import java.io.*;

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

    public static String File_To_Base64() {
        try {
        FileChooser fileChooser = new FileChooser();
        fileChooser.setTitle("Open Resource File");
        File file = fileChooser.showOpenDialog(Main.This()._primaryStage);

        StringBuilder sb = new StringBuilder();
        sb.append('\"');

        sb.append(Base64FromFile(file));

        sb.append('\"');
        return sb.toString();

        } catch (IOException e) {
            return File_To_Base64();
        }
    }

    private static String Base64FromFile(File file) throws IOException {
        InputStream is = new FileInputStream(file);
        long length = file.length();

        if (length > Integer.MAX_VALUE) {
            throw new IOException("File was too big");
        }

        byte[] bytes = new byte[(int)length];

        int offset = 0;
        int numRead = 0;
        while (offset < bytes.length
                && (numRead=is.read(bytes, offset, bytes.length-offset)) >= 0) {
            offset += numRead;
        }

        if (offset < bytes.length) {
            throw new IOException("Could not completely read file "+file.getName());
        }

        is.close();

        return Base64.encode(bytes);
    }

    public static void SaveFile(String base64string, String filename) throws IOException {
        byte[] restored = Base64.decode(base64string);
        File newfile = new File(filename);
        FileOutputStream output = new FileOutputStream(newfile);
        output.write(restored);
        Desktop.getDesktop().open(newfile);
    }

    public static JsonObject JsonReport_To_JsonObject (String Json) {
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
