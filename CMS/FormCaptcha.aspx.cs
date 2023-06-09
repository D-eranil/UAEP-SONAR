using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;

public partial class Captcha_FormCaptcha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < 5; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));

            builder.Append(ch);
        }

        this.Session["FormCaptcha"] = builder.ToString();


        // Create a CAPTCHA image using the text stored in the Session object.
        CaptchaImage ci = new CaptchaImage(builder.ToString(), 80, 30, "sans-serif", "/assets/images/captcha.jpg");

        // Change the response headers to output a JPEG image.
        this.Response.Clear();
        this.Response.ContentType = "image/jpeg";

        // Write the image to the response stream in JPEG format.
        ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

        // Dispose of the CAPTCHA image object.
        ci.Dispose();
    }
}
public class CaptchaImage
{
    // Public properties (all read-only).
    public string Text
    {
        get { return this.text; }
    }
    public Bitmap Image
    {
        get { return this.image; }
    }
    public int Width
    {
        get { return this.width; }
    }
    public int Height
    {
        get { return this.height; }
    }

    // Internal properties.
    private string text;
    private string backImage;
    private int width;
    private int height;
    private string familyName;
    private Bitmap image;

    // For generating random numbers.
    private Random random = new Random();

    // ====================================================================
    // Initializes a new instance of the CaptchaImage class using the
    // specified text, width and height.
    // ====================================================================
    public CaptchaImage(string s, int width, int height)
    {
        this.text = s;
        this.SetDimensions(width, height);
        this.GenerateImage();
    }

    // ====================================================================
    // Initializes a new instance of the CaptchaImage class using the
    // specified text, width, height and font family.
    // ====================================================================
    public CaptchaImage(string s, int width, int height, string familyName, string backgroundImagePath)
    {
        this.backImage = backgroundImagePath;
        this.text = s;
        this.SetDimensions(width, height);
        this.SetFamilyName(familyName);
        this.GenerateImage();

    }

    // ====================================================================
    // This member overrides Object.Finalize.
    // ====================================================================
    ~CaptchaImage()
    {
        Dispose(false);
    }

    // ====================================================================
    // Releases all resources used by this object.
    // ====================================================================
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Dispose(true);
    }

    // ====================================================================
    // Custom Dispose method to clean up unmanaged resources.
    // ====================================================================
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            // Dispose of the bitmap.
            this.image.Dispose();
    }

    // ====================================================================
    // Sets the image width and height.
    // ====================================================================
    private void SetDimensions(int width, int height)
    {
        // Check the width and height.
        if (width <= 0)
            throw new ArgumentOutOfRangeException("width", width, "Argument out of range, must be greater than zero.");
        if (height <= 0)
            throw new ArgumentOutOfRangeException("height", height, "Argument out of range, must be greater than zero.");
        this.width = width;
        this.height = height;
    }

    // ====================================================================
    // Sets the font used for the image text.
    // ====================================================================
    private void SetFamilyName(string familyName)
    {
        // If the named font is not installed, default to a system font.
        try
        {
            Font font = new Font(this.familyName, 12F);
            this.familyName = familyName;
            font.Dispose();
        }
        catch (Exception ex)
        {
            this.familyName = System.Drawing.FontFamily.GenericSerif.Name;
        }
    }

    public static Color FromHtml(string colorHTML)
    {
        Color result = Color.Empty;
        if (!string.IsNullOrEmpty(colorHTML))
        {
            result = ColorTranslator.FromHtml(colorHTML);
        }
        return result;
    }


    // ====================================================================
    // Creates the bitmap image.
    // ====================================================================
    private void GenerateImage()
    {
        try
        {
            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);

            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.width + 1, this.height);
            System.Drawing.Image pic = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(backImage)); //get circle image
            SolidBrush brush = new SolidBrush(Color.Gray);
            Size newSize = new Size(this.width, this.height);
            g.FillRectangle(brush, rect);
            g.DrawImage(pic, rect);

            // Set up the text font.
            SizeF size;
            float fontSize = 25;
            Font font;
            // Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                font = new Font(this.familyName, fontSize, FontStyle.Bold);
                size = g.MeasureString(this.text, font);
            } while (size.Width > rect.Width);

            // Set up the text format.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            path.AddString(this.text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float v = 4F;
            PointF[] points =
            {
                new PointF(this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v, this.random.Next(rect.Height) / v),
                new PointF(this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v),
                new PointF(rect.Width - this.random.Next(rect.Width) / v, rect.Height - this.random.Next(rect.Height) / v)
            };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);

            g.FillPath(brush, path);


            // Clean up.
            font.Dispose();
            brush.Dispose();
            g.Dispose();

            // Set the image.
            this.image = bitmap;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}

