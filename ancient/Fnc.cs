using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ancient
{
    class Fnc
    {

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }

        public static bool IsInteger(object x)
        {
            int value;
            if (int.TryParse(x.ToString(), out value))
                return true;
            else
                return false;
        }

        public static void OpenDialog(Form form)
        {
            var x = 0;
            for (var i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Name != form.Name) continue;
                x++;
                Application.OpenForms[i].Focus();
                Application.OpenForms[i].BringToFront();
            }
            if (x > 0) return;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        public static void OpenForm(Type formType, Form ribbonForm)
        {
            var x = 0;
            for (var i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].GetType() != formType) continue;
                x++;
                Application.OpenForms[i].Focus();
                Application.OpenForms[i].BringToFront();
            }
            if (x > 0) return;
            var form = (Form)Activator.CreateInstance(formType);
            form.MdiParent = ribbonForm;
            form.Show();
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static object S2N(string str)
        {
            if (str != null && str.Trim().Length > 0)
                return str;
            else
                return DBNull.Value;
        }

        public static void SendMail(String receiver, String subject, String content, int port = 587, bool ishtml = true)
        {
            try
            {
                var mail = new MailMessage { IsBodyHtml = ishtml };
                using (var smtpServer = new SmtpClient("yourmailserver.com"))
                {
                    mail.From = new MailAddress("yourmailaddress.sample.com");
                    mail.To.Add(receiver);
                    mail.Subject = subject;
                    mail.Body = content;
                    smtpServer.Port = port;
                    smtpServer.Credentials = new System.Net.NetworkCredential("yourmailusername@sample.com", "yourmailpassword");
                    smtpServer.Send(mail);
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        public static byte[] StringToByteArray(String hex)
        {
            var NumberChars = hex.Length;
            var bytes = new byte[NumberChars / 2];
            for (var i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public List<object> ObjToList(object input)
        {
            if (input is IEnumerable)
                return ((IEnumerable)input).Cast<Object>().ToList();
            return new List<Object>() { input };
        }

        public DataTable ToDataTable<T>(IList<T> data)
        {
            System.ComponentModel.PropertyDescriptorCollection props =
                System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
            System.Data.DataTable table = new System.Data.DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                System.ComponentModel.PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public string ToMd5(string val)
        {
            StringBuilder sb = new StringBuilder();
            MD5CryptoServiceProvider pd = new MD5CryptoServiceProvider();
            byte[] bytes = pd.ComputeHash(new UTF8Encoding().GetBytes(val));
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
